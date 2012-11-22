#include "Common.h"

enum RunState doWorkerLogic(struct WorkerParameters* params){
    int oldValue, signalCount = 0, i;
    struct Elem* current;
    enum RunState runState = Done;
    WaitForSingleObject(params->common->listMutex, INFINITE);
    printf("Worker %d has locked list mutex\r\n", params->worker);
    for (current = params->common->list->first; current != NULL; current = current->next)
        if (current->value >= 2 && current->worker == params->worker){
            runState = Running;
            oldValue = current->value;
            current->value = (int)sqrt((double)current->value);
            printf("Old value is %d, new value is %d\r\n", oldValue, current->value);
            if (current->value < 2){
                params->common->count++;
                if (params->common->count % 5 == 0 || params->common->count == params->common->initialElementCount)
                    signalCount++;
            }
        }
    printf("Worker %d unlocked list mutex\r\n\r\n", params->worker);
    ReleaseMutex(params->common->listMutex);

    for (i = 0; i < signalCount; i++){
        while (ReleaseSemaphore(params->common->signalSemaphore, 1L, NULL) == FALSE)
            Sleep(100);
        printf("Worker %d has signaled main thread\r\n", params->worker);
    }
    return runState;
}

DWORD WorkerThread(LPVOID parameters){
    enum RunState runState;
    struct WorkerParameters* params = (struct WorkerParameters*)parameters;
    
    if (WaitForSingleObject(params->common->accessSempahore, INFINITE) == WAIT_OBJECT_0){
        printf("\r\nWorker %d starts working\r\n", params->worker);
        do
            runState = doWorkerLogic(params);
        while (runState == Running);
        printf("Worker %d has finished working\r\n", params->worker);
        ReleaseSemaphore(params->common->accessSempahore, 1L, NULL);
    }
    else
        runState = Error;

    if (runState == Done)
        return EXIT_SUCCESS;
    else
        return EXIT_FAILURE;
}
