#include "Common.h"

void* MainThread(void* mainThreadArgs){
    int count = 0;
    enum JobStatus jobStatus = NotDone;
    struct MainThreadArgs* args = (struct MainThreadArgs*)mainThreadArgs;
    struct Elem* current;
    struct StackElement* top = NULL;
    
    printf("Main thread: list size is %d\n", args->list->dim);
    do{
        if (pthread_cond_wait(args->valueEvent, args->signalMutex) != -1){
            count++;
            printf("Count is %d\n", count);
            if (count == 5 || count == args->list->dim){
                MoveNodes(args->list, &top, count);
                if (args->list->dim == 0)
                    jobStatus = Done;
                count = 0;
                printf("Main thread: list size is %d\n", args->list->dim);
            }
            pthread_mutex_unlock(args->signalBackMutex);
        }
        else
            jobStatus = Error;
    }while (jobStatus != Done);
    pthread_mutex_unlock(args->signalMutex);
    ClearNodes(&top, args->list);

    if (jobStatus == Done)
        printf("Main thread has finished job!\n");
    else
        printf("Error from main thread! Job interrupted!\n");
    pthread_exit(NULL);
}

