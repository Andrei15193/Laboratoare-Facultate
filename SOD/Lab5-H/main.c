#include "common.h"

void setCommonParams(struct CommonParams* common, int elems, int workers){
    common->count = 0;
    common->list = createList(elems, workers);
    common->listSem = CreateSemaphore(NULL, 0, 1, NULL);
    common->signalSem = CreateSemaphore(NULL, 0, 1, NULL);
}

void setWorkerParams(struct WorkerParams* workerParams, struct CommonParams* common, int workers, HANDLE accessSem){
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
    HANDLE accessSem;
    HANDLE* threads;
    struct CommonParams mainParams;
    struct WorkerParams* workerParams;
    elems = readMyNumber("Number of elements (greater than 0): ");
    workers = readMyNumber("Number of workers (greater than 0): ");

    accessSem = CreateSemaphore(NULL, 3, 3, NULL);
    setCommonParams(&mainParams, elems, workers);
    workerParams = (struct WorkerParams*)calloc(workers, sizeof(struct WorkerParams));
    threads = (HANDLE*)calloc(workers + 1, sizeof(HANDLE));
    setWorkerParams(workerParams, &mainParams, workers, accessSem);

    threads[workers] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&mainThread, &mainParams, 0, NULL);
    for (i = 0; i < workers; i++)
        threads[i] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&workerThread, workerParams + i, 0, NULL);
    WaitForMultipleObjects(workers + 1, threads, TRUE, INFINITE);
    CloseHandle(mainParams.listSem);
    CloseHandle(mainParams.signalSem);
    CloseHandle(accessSem);
    free(workerParams);
    free(threads);

    return 0;
}
