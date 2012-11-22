#include "Common.h"

void removeFromList(struct List* list){
    int n = 0;
    struct Elem *current = list->first, *pre;
    while (current != NULL && n < 5)
        if (current->value < 2){
            if (current == list->first){
                pre = current;
                list->first = current = current->next;
                free(pre);
            }
            else{
                pre->next = current->next;
                free(current);
                current = pre->next;
            }
            list->n--;
            n++;
        }
        else{
            pre = current;
            current = current->next;
        }
}

DWORD MainThread(LPVOID parameters){
    enum RunState runState = Running;
    struct MainThreadParameters* params = (struct MainThreadParameters*)(parameters);
    do{
        printf("Main thread is waiting for signals\r\n");
        if (WaitForSingleObject(params->signalSemaphore, INFINITE) == WAIT_OBJECT_0){
            printf("Main thread has received a signal\r\n");
            if (WaitForSingleObject(params->listMutex, INFINITE) == WAIT_OBJECT_0){
                printf("Main thread locked list mutex\r\n");
                removeFromList(params->list);
                if (params->list->n == 0)
                    runState = Done;
                printf("List size is %d\r\nMain thread has unlocked list mutex\r\n", params->list->n);
                ReleaseMutex(params->listMutex);
            }
            else
                runState = Error;
        }
        else
            runState = Error;
    }while (runState == Running);
    printf("Main thread has finished job\r\n");
    if (runState == Done)
        return EXIT_SUCCESS;
    else
        return EXIT_FAILURE;
}
