#include "common.h"

void* workerThread(void* workerParams){
    int jobDone, oldValue, canContinue;
    struct WorkerParams* params = (struct WorkerParams*)workerParams;
    struct Elem* it = params->common->list->first;
    sem_wait(&params->accessSem);
    printf("Worker %d starts working\n", params->worker);
    do{
        pthread_mutex_lock(&params->common->listMutex);
        printf("Worker %d locked mutex\n", params->worker);
        it = params->common->list->first;
        jobDone = 1;
        canContinue = 1;
        while (it != NULL){
            if (it->value >= 2 && it->worker == params->worker){
                jobDone = 0;
                oldValue = it->value;
                it->value = (int)sqrt((double)(it->value));
                printf("Old value is %d, new value is %d\n", oldValue, it->value);
                if (it->value < 2){
                    params->common->count++;
                    if (params->common->count == 5 || params->common->count == params->common->list->dim){
                        printf("\nWorker %d sent signal to main thread\n\n", params->worker);
                        sem_post(&params->common->signalSem);
                        canContinue = 0;
                    }
                }
            }
            if (canContinue == 1)
                it = it->next;
            else
                it = NULL;
        }
        if (canContinue == 1){
            printf("Worker %d unlocked mutex\n", params->worker);
            pthread_mutex_unlock(&params->common->listMutex);
        }
    }while (jobDone == 0);
    printf("Worker %d finished working\n", params->worker);
    sem_post(&params->accessSem);
    return NULL;
}
