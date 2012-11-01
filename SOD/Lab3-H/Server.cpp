#include "Server.h"

void RunServer(){
    HANDLE cfmHandle;
    struct {HANDLE read, write;}sem;
    struct Message* memory;
    sem.read  = CreateSemaphore(NULL, 0, 1, SEMAPHORE_READ);
    sem.write = CreateSemaphore(NULL, 1, 1, SEMAPHORE_WRITE);
    if (GetLastError() == ERROR_ALREADY_EXISTS || sem.read == NULL || sem.write == NULL)
        printf("Error whire starting serve\r\n");
    else{
        cfmHandle = CreateFileMapping((HANDLE)0xFFFFFFFF, NULL, PAGE_READWRITE, 0, sizeof(struct Message), SHM_NAME);
        if (cfmHandle == NULL)
            printf("Error creating file mapping\r\n");
        else{
            memory = (struct Message*)MapViewOfFile(cfmHandle, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, sizeof(struct Message));
            if (memory == NULL)
                printf("Error ataching to file map (shm)\r\n");
            else{
                memory->minimum = -1;
                memory->maximum = -1;
                printf("Server started\r\n");
                do{
                    while (WaitForSingleObject(sem.read, 1000) != WAIT_OBJECT_0);
                    if (memory->number < memory->minimum || memory->minimum == -1)
                        memory->minimum = memory->number;
                    if (memory->maximum < memory->number)
                        memory->maximum = memory->number;
                    ReleaseSemaphore(sem.write, 1L, NULL);
                }while (memory->number >= 0);
                UnmapViewOfFile((LPVOID)memory);
            }
        }
    }

    CloseHandle(sem.read);
    CloseHandle(sem.write);
}
