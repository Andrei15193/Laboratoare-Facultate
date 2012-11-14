#include "Common.h"

void PrintList(struct List* list){
    struct Elem* current = list->first;
    printf("Printing list\n");
    while (current != NULL){
        printf("Worker: %d, Value: %d\n", current->worker, current->value);
        current = current->next;
    }
    printf("-----\n");
}

void ReadElementsAndWorkers(int* elements, int* workers){
    printf("Enter the nubmer of elements (strict positive integer 0)\n");
    *elements = ReadMyNumber(1);
    printf("Enter the nubmer of workers (strict positive integer 0)\n");
    *workers = ReadMyNumber(1);
}

int main(int argc, char* args[]){
    int elements, workers, i;
    struct MainThreadArgs mainThreadArgs;
    struct WorkerThreadArgs* workerThreadArgs;
    sem_t sem;
    pthread_t* threads;
    pthread_mutex_t listMutex, signalMutex, signalBackMutex;
    pthread_cond_t valueEvent;
    
    ReadElementsAndWorkers(&elements, &workers);
    
    workerThreadArgs = (struct WorkerThreadArgs*)calloc(workers, sizeof(struct WorkerThreadArgs));
    threads = (pthread_t*)calloc(workers + 1, sizeof(pthread_t));

    sem_init(&sem, 0, 3);
    pthread_mutex_init(&listMutex, NULL);
    pthread_mutex_init(&signalMutex, NULL);
    pthread_mutex_lock(&signalMutex);
    pthread_mutex_init(&signalBackMutex, NULL);
    pthread_cond_init(&valueEvent, NULL);
    mainThreadArgs.list = CreateList(elements, workers);

    mainThreadArgs.listMutex = &listMutex;
    mainThreadArgs.signalMutex = &signalMutex;
    mainThreadArgs.signalBackMutex = &signalBackMutex;
    mainThreadArgs.valueEvent = &valueEvent;

    PrintList(mainThreadArgs.list);

    pthread_create(threads + workers, NULL, MainThread, &mainThreadArgs);
    for (i = 0; i < workers; i++){
        workerThreadArgs[i].list = mainThreadArgs.list;
        workerThreadArgs[i].listAccessSemaphore = &sem;
        workerThreadArgs[i].listMutex = mainThreadArgs.listMutex;
        workerThreadArgs[i].signalMutex = mainThreadArgs.signalMutex;
        workerThreadArgs[i].signalBackMutex = &signalBackMutex;
        workerThreadArgs[i].valueEvent = mainThreadArgs.valueEvent;
        workerThreadArgs[i].worker = i;
        pthread_create(threads + i, NULL, WorkerThread, workerThreadArgs + i);
    }
    for (i = 0; i <= workers; i++)
        pthread_join(threads[i], NULL);
    DestroyList(mainThreadArgs.list);
    sem_destroy(&sem);
    pthread_mutex_destroy(&listMutex);
    pthread_mutex_destroy(&signalMutex);
    pthread_mutex_destroy(&signalBackMutex);
    pthread_cond_destroy(&valueEvent);
    free(threads);
    free(workerThreadArgs);
    return 0;
}
