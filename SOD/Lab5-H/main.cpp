#include "Common.h"

void PrintList(struct List* list){
    struct Elem* current = list->first;
    printf("Printing list\r\n");
    while (current != NULL){
        printf("Worker: %d, Value: %d\r\n", current->worker, current->value);
        current = current->next;
    }
    printf("-----\r\n");
}

void ReadElementsAndWorkers(int* elements, int* workers){
    printf("Enter the nubmer of elements (strict positive integer 0)\r\n");
    *elements = ReadMyNumber(1);
    printf("Enter the nubmer of workers (strict positive integer 0)\r\n");
    *workers = ReadMyNumber(1);
}

int main(int argc, char* args[]){
    int elements, workers, i;
    struct MainThreadArgs mainThreadArgs;
    struct WorkerThreadArgs* workerThreadArgs;
    HANDLE sem = CreateSemaphore(NULL, 3, 3, NULL);
    HANDLE* threads;
    
    ReadElementsAndWorkers(&elements, &workers);
    
    workerThreadArgs = (struct WorkerThreadArgs*)calloc(workers, sizeof(struct WorkerThreadArgs));
    threads = (HANDLE*)calloc(workers + 1, sizeof(HANDLE));

    mainThreadArgs.list = CreateList(elements, workers);
    mainThreadArgs.listMutex = CreateMutex(NULL, FALSE, NULL);
    mainThreadArgs.valueEvent = CreateEvent(NULL, FALSE, FALSE, NULL);

    PrintList(mainThreadArgs.list);

    threads[workers] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)MainThread, (LPVOID)&mainThreadArgs, 0, 0);
    for (i = 0; i < workers; i++){
        workerThreadArgs[i].list = mainThreadArgs.list;
        workerThreadArgs[i].listAccessSemaphore = sem;
        workerThreadArgs[i].listMutex = mainThreadArgs.listMutex;
        workerThreadArgs[i].valueEvent = mainThreadArgs.valueEvent;
        workerThreadArgs[i].worker = i;
        threads[i] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WorkerThread, (LPVOID)(&workerThreadArgs[i]), 0, 0);
    }
    for (i = 0; i <= workers; i++){
        WaitForSingleObject(threads[i], INFINITE);
        CloseHandle(threads[i]);
    }
    DestroyList(mainThreadArgs.list);
    return 0;
}
