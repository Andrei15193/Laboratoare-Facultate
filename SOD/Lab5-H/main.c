#include "Common.h"

void readNumbers(int* elements, int* workers){
    printf("Enter the number of elements\r\n");
    *elements = ReadMyNumber(0);
    printf("Enter the number of workers\r\n");
    *workers = ReadMyNumber(0);
}

HANDLE* createThreads(struct WorkerParameters* workerParameters, struct MainThreadParameters* mainThreadParameters, int workerCount){
    int i;
    HANDLE* threads = (HANDLE*)calloc(workerCount + 1, sizeof(HANDLE));
    threads[workerCount] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)MainThread, (LPVOID)mainThreadParameters, 0, 0);
    for (i = 0; i < workerCount; i++)
        threads[i] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)WorkerThread, (LPVOID)(workerParameters + i), 0, 0);
    return threads;
}

int setParameters(struct WorkerParameters** workerParameters, struct MainThreadParameters** mainThreadParameters){
    int i, elementCount, workerCount;
    struct CommonWorkerParameters* commonWorkerParameters;
    readNumbers(&elementCount, &workerCount);
    srand((unsigned int)GetCurrentProcessId());

    commonWorkerParameters = (struct CommonWorkerParameters*)malloc(sizeof(struct CommonWorkerParameters));
    *mainThreadParameters = (struct MainThreadParameters*)malloc(sizeof(struct MainThreadParameters));
    *workerParameters = (struct WorkerParameters*)calloc(workerCount, sizeof(struct WorkerParameters));

    commonWorkerParameters->count = 0;
    commonWorkerParameters->initialElementCount = elementCount;
    commonWorkerParameters->list = CreateList(elementCount, workerCount);
    commonWorkerParameters->listMutex = CreateMutex(NULL, FALSE, NULL);
    commonWorkerParameters->signalSemaphore = CreateSemaphore(NULL, 0, 10, NULL);
    commonWorkerParameters->accessSempahore = CreateSemaphore(NULL, 3, 3, NULL);
    for (i = 0; i < workerCount; i++){
        (*workerParameters)[i].common = commonWorkerParameters;
        (*workerParameters)[i].worker = i;
    }

    mainThreadParameters[0]->list = commonWorkerParameters->list;
    mainThreadParameters[0]->listMutex = commonWorkerParameters->listMutex;
    mainThreadParameters[0]->signalSemaphore = commonWorkerParameters->signalSemaphore;

    return workerCount;
}

void unsetParameters(struct WorkerParameters* workerParameters, struct MainThreadParameters* mainThreadParameters){
    DestroyList(workerParameters->common->list);
    CloseHandle(workerParameters->common->listMutex);
    CloseHandle(workerParameters->common->signalSemaphore);
    CloseHandle(workerParameters->common->accessSempahore);
    free(workerParameters->common);
    free(workerParameters);
    free(mainThreadParameters);
}

void waitForThreads(HANDLE* threads, int count){
    int i;
    WaitForMultipleObjects(count, threads, TRUE, INFINITE);
    for (i = 0; i < count; i++)
        CloseHandle(threads[i]);
    free(threads);
}

int main(){
    int workers;
    struct WorkerParameters* workerParameters = NULL;
    struct MainThreadParameters* mainThreadParameters = NULL;
    
    workers = setParameters(&workerParameters, &mainThreadParameters);
    PrintList(stdout, workerParameters->common->list);
    waitForThreads(createThreads(workerParameters, mainThreadParameters, workers), workers + 1);
    unsetParameters(workerParameters, mainThreadParameters);
    return 0;
}
