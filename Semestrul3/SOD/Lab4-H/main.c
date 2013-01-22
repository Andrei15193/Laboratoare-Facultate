#include "common.h"

void setCommonParams(struct CommonParams* common, int elems, int workers){
    common->count = 0;
    common->list = createList(elems, workers);
    pthread_mutex_init(&common->listMutex, NULL);
    pthread_mutex_lock(&common->listMutex);
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
    pthread_t* threads;
    struct CommonParams mainParams;
    struct WorkerParams* workerParams;
    elems = readMyNumber("Number of elements (greater than 0): ");
    workers = readMyNumber("Number of workers (greater than 0): ");

    sem_init(&accessSem, 0, 3);
    setCommonParams(&mainParams, elems, workers);
    workerParams = (struct WorkerParams*)calloc(workers, sizeof(struct WorkerParams));
    threads = (pthread_t*)calloc(workers + 1, sizeof(pthread_t));
    setWorkerParams(workerParams, &mainParams, workers, accessSem);

    pthread_create(threads + workers, NULL, &mainThread, &mainParams);
    for (i = 0; i < workers; i++)
        pthread_create(threads + i, NULL, &workerThread, workerParams + i);
    for (i = 0; i <= workers; i++)
        pthread_join(threads[i], NULL);
    pthread_mutex_destroy(&mainParams.listMutex);
    sem_destroy(&mainParams.signalSem);
    sem_destroy(&accessSem);
    free(workerParams);
    free(threads);

    return 0;
}
