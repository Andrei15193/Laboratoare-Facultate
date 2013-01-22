#include "Client.h"

int ReadMyNumber(){
    char buffer[10];
    printf("Number: ");
    fgets(buffer, 10, stdin);
    return atoi(buffer);
}

void RunClient(){
    int number;
    HANDLE cfmHandle;
    struct {HANDLE read, write;}sem;
    struct Message* memory;
    sem.read  = CreateSemaphore(NULL, 0, 1, SEMAPHORE_READ);
    sem.write = CreateSemaphore(NULL, 1, 1, SEMAPHORE_WRITE);
    if (GetLastError() != ERROR_ALREADY_EXISTS)
        printf("Error whire starting client\r\n");
    else{
        cfmHandle = CreateFileMapping((HANDLE)0xFFFFFFFF, NULL, PAGE_READWRITE, 0, sizeof(struct Message), SHM_NAME);
        if (cfmHandle == NULL)
            printf("Error creating file mapping\r\n");
        else{
            memory = (struct Message*)MapViewOfFile(cfmHandle, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, sizeof(struct Message));
            if (memory == NULL)
                printf("Error ataching to file map (shm)\r\n");
            else{
                printf("Client started\r\n");
                do number = ReadMyNumber(); while (number < 0);
                do{
                    while (WaitForSingleObject(sem.write, 1000) != WAIT_OBJECT_0);
                    memory->number = number;
                    ReleaseSemaphore(sem.read, 1L, NULL);
                    number = ReadMyNumber();
                }while (number >= 0);
                printf("Minimum: %d\r\nMaximum: %d\r\n", memory->minimum, memory->maximum);
                memory->minimum = -1;
                memory->maximum = -1;
                UnmapViewOfFile((LPVOID)memory);
            }
        }
    }

    CloseHandle(sem.read);
    CloseHandle(sem.write);
}
