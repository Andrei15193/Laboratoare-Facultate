#include "Common.h"

void* WorkerThread(void* workerThreadArgs){
    int newValue;
    enum JobStatus jobStatus;
    struct WorkerThreadArgs* args = (struct WorkerThreadArgs*)workerThreadArgs;
    struct Elem* current;

    if (sem_wait(args->listAccessSemaphore) != -1){
        printf("Wroker %d starts working\n", args->worker);
        do{
            current = args->list->first;
            jobStatus = Done;
            while (current != NULL){
                if (current->worker == args->worker && current->value >= 2)
                    if (pthread_mutex_lock(args->listMutex) != -1){
                        printf("\n--> Mutex locked by %d\n", args->worker);
                        jobStatus = NotDone;
                        newValue = (int)sqrt((double)current->value);
                        printf("    Old value: %d, new value %d\n", current->value, newValue);
                        current->value = newValue;
                        printf("<-- Mutex unlocked by %d\n\n", args->worker);
                        if (current->value < 2){
                            // Wait for main thread to be ready to receive signals.
                            printf("Waiting for main thread to receive signals\n");
                            pthread_mutex_lock(args->signalMutex);
                            pthread_mutex_unlock(args->signalMutex);
                            // Signal the main thread.
                            printf("Signaled main thread\n");
                            pthread_cond_signal(args->valueEvent);
                            pthread_mutex_lock(args->signalBackMutex);
                            // Wait for main thread to signal back (through mutex).
                            printf("Waiting for main thread to get into listening state\n");
                            pthread_mutex_lock(args->signalBackMutex);
                            pthread_mutex_unlock(args->signalBackMutex);
                            // Wait for main thread to get into listening state.
                            pthread_mutex_lock(args->signalMutex);
                            pthread_mutex_unlock(args->signalMutex);
                            printf("Continuing with job\n");
                            // Go on with work
                        }
                        pthread_mutex_unlock(args->listMutex);
                    }
                    else
                        jobStatus = Error;
                current = current->next;
            }
        }while (jobStatus != Done);
        sem_post(args->listAccessSemaphore);
    }
    else
        jobStatus = Error;
    
    if (jobStatus == Done)
        printf("Job done for worker %d!\r\n", args->worker);
    else
        printf("Error from worker: %d! Job interrupted!\r\n", args->worker);
    pthread_exit(NULL);
}

