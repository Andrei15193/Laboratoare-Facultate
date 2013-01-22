#include "common.h"

void removeElems(struct List* list){
    struct Elem *pre, *it = list->first;
    while (it != NULL){
        if (it->value < 2){
            list->dim--;
            if (it == list->first){
                list->first = list->first->next;
                free(it);
                it = list->first;
            }
            else{
                pre->next = it->next;
                free(it);
                it = pre->next;
            }
        }
        else{
            pre = it;
            it = it->next;
        }
    }
}

void* mainThread(void* mainParams){
    struct CommonParams* params = (struct CommonParams*)mainParams;
    do{
        printf("Main thread has unlocked list mutex\n\n");
        pthread_mutex_unlock(&params->listMutex);
        sem_wait(&params->signalSem);
        printf("Main thread received a signal\n");
        removeElems(params->list);
        printf("List size is %d\n", params->list->dim);
        params->count = 0;
    }while (params->list->dim > 0);
    printf("Main thread has unlocked list mutex\n\n");
    pthread_mutex_unlock(&params->listMutex);
    return NULL;
}
