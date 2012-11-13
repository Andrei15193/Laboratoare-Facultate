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
            if (count == 5 || count == args->list->dim)
                if (WaitForSingleObject(args->listMutex, INFINITE) == WAIT_OBJECT_0){
                    printf("\r\n--> Mutex locked by main thread\r\n");
                    MoveNodes(args->list, &top, count);
                    if (args->list->dim == 0)
                        jobStatus = Done;
                    count = 0;
                    ReleaseMutex(args->listMutex);
                    printf("Main thread: list size is %d\r\n", args->list->dim);
                    printf("<-- Mutex unlocked by main thread\r\n\r\n");
                }
                else
                    jobStatus = Error;
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
