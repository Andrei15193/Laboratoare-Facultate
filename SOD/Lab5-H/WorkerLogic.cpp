#include "Common.h"

DWORD WorkerThread(LPVOID workerThreadArgs){
    int newValue;
    enum JobStatus jobStatus;
    struct WorkerThreadArgs* args = (struct WorkerThreadArgs*)workerThreadArgs;
    struct Elem* current;

    if (WaitForSingleObject(args->listAccessSemaphore, INFINITE) == WAIT_OBJECT_0){
        printf("Wroker %d starts working\r\n", args->worker);
        do{
            current = args->list->first;
            jobStatus = Done;
            while (current != NULL){
                if (current->worker == args->worker && current->value >= 2)
                    if (WaitForSingleObject(args->listMutex, INFINITE) == WAIT_OBJECT_0){
                        printf("\r\n--> Mutex locked by %d\r\n", args->worker);
                        jobStatus = NotDone;
                        newValue = (int)sqrt((double)current->value);
                        printf("    Old value: %d, new value %d\r\n", current->value, newValue);
                        current->value = newValue;
                        if (current->value < 2){
                            // Signal the main thread.
                            printf("Signaling the main thread\r\n");
                            WaitForSingleObject(args->signalSem, INFINITE);
                            SetEvent(args->valueEvent);
                            printf("Signaled the main thread\r\n");
                            // Wait for main thread to signal back
                            WaitForSingleObject(args->signalSem, INFINITE);
                            ReleaseSemaphore(args->signalSem, 1L, NULL);
                            // Go on with work
                            printf("Continuing with job\n");
                        }
                        printf("<-- Mutex unlocked by %d\r\n\r\n", args->worker);
                        ReleaseMutex(args->listMutex);
                    }
                    else
                        jobStatus = Error;
                current = current->next;
            }
        }while (jobStatus != Done);
        ReleaseSemaphore(args->listAccessSemaphore, 1L, NULL);
    }
    else
        jobStatus = Error;
    
    if (jobStatus == Done){
        printf("Job done for worker %d!\r\n", args->worker);
        return EXIT_SUCCESS;
    }
    else{
        printf("Error from worker: %d! Job interrupted!\r\n", args->worker);
        return EXIT_FAILURE;
    }
}
