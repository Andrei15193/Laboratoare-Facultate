#include "common.h"

void setCommonParams(struct CommonParams* common, int elems, int workers){
    common->count = 0;
    common->list = createList(elems, workers);
    mutex_init(&common->listMutex, 0, NULL);
    mutex_lock(&common->listMutex);
    sem_init(&common->signalSem, 0, 0);
}

void setWorkerParams(struct WorkerParams* workerParams, struct CommonParams* common, int workers, sem_t accessSem){
    int i;
    for (i = 0; i < workers; i++){
        workerParams[i].accessSem = accessSem;
        workerParams[i].common = common;
        workerParams[i].worker = i;
    }
}

int readMyNumber(const char message[]){
    int number;
    char buff[6];
    do{
        printf("%s", message);
        fgets(buff, 6, stdin);
        number = atoi(buff);
    }while (number < 1);
    return number;
}

int main(void)
{
    int elems, workers, i;
    sem_t accessSem;
    thread_t* threads;
    struct CommonParams mainParams;
    struct WorkerParams* workerParams;
    elems = readMyNumber("Number of elements (greater than 0): ");
    workers = readMyNumber("Number of workers (greater than 0): ");

    sem_init(&accessSem, 0, 3);
    setCommonParams(&mainParams, elems, workers);
    workerParams = (struct WorkerParams*)calloc(workers, sizeof(struct WorkerParams));
    threads = (thread_t*)calloc(workers + 1, sizeof(thread_t));
    setWorkerParams(workerParams, &mainParams, workers, accessSem);

    thr_create(NULL, 0, &mainThread, &mainParams, 0, threads + workers);
    for (i = 0; i < workers; i++)
        thr_create(NULL, 0, &workerThread, workerParams + i, 0, threads + i);
    for (i = 0; i <= workers; i++)
        thr_join(threads[i], NULL, NULL);
    sem_destroy(&accessSem);
    sem_destroy(&mainParams.signalSem);
    mutex_destroy(&mainParams.listMutex);
    free(workerParams);
    free(threads);

    return 0;
}
