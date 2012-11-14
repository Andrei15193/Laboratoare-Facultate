#include "Common.h"

DWORD MainThread(LPVOID mainThreadArgs){
    int count = 0;
    enum JobStatus jobStatus = NotDone;
    struct MainThreadArgs* args = (struct MainThreadArgs*)mainThreadArgs;
    struct Elem* current;
    struct StackElement* top = NULL;
    
    printf("Main thread: list size is %d\r\n", args->list->dim);
    do{
        if (WaitForSingleObject(args->valueEvent, INFINITE) == WAIT_OBJECT_0){
            count++;
            printf("Main thread: count is %d\r\n", count);
            if (count == 5 || count == args->list->dim){
                MoveNodes(args->list, &top, count);
                if (args->list->dim == 0)
                    jobStatus = Done;
                count = 0;
                printf("Main thread: list size is %d\r\n", args->list->dim);
            }
            ReleaseSemaphore(args->signalSem, 1L, NULL);
        }
        else
            jobStatus = Error;
    }while (jobStatus != Done);
    ClearNodes(&top, args->list);

    if (jobStatus == Done){
        printf("Main thread has finished job!\r\n");
        return EXIT_SUCCESS;
    }
    else{
        printf("Error from main thread! Job interrupted!\r\n");
        return EXIT_FAILURE;
    }
}
