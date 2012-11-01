#include "Client.h"

int ReadMyNumber(){
    char buffer[6];
    printf("Number: ");
    fgets(buffer, 6, stdin);
    return atoi(buffer);
}

void RunClient(){
    int number;
    struct IntPair minMax;
    BOOL ok = TRUE;
    DWORD bytes;
    HANDLE namedPipe = CreateFile(CONNECT_NAMEDPIPE, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (namedPipe != INVALID_HANDLE_VALUE || namedPipe == INVALID_HANDLE_VALUE && GetLastError() == ERROR_PIPE_CONNECTED){
        do{
            number = ReadMyNumber();
            WriteFile(namedPipe, &number, sizeof(int), &bytes, NULL);
            FlushFileBuffers(namedPipe);
        }while (0 <= number);
        if (ReadFile(namedPipe, &minMax, sizeof(IntPair), &bytes, NULL) == FALSE || bytes != sizeof(IntPair))
            printf("Error while reading result\r\nClient closing");
        else
            printf("Min value is: %d\r\nMax value is: %d\r\n", minMax.first, minMax.second);
    }
    else
        printf("Error connecting to server\r\nClient closing");
    CloseHandle(namedPipe);
}

void CloseServer(){
    int number = -1;
    DWORD bytes;
    HANDLE namedPipe = CreateFile(CONNECT_NAMEDPIPE, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (namedPipe != INVALID_HANDLE_VALUE || namedPipe == INVALID_HANDLE_VALUE && GetLastError() == ERROR_PIPE_CONNECTED){
        WriteFile(namedPipe, &number, sizeof(int), &bytes, NULL);
        FlushFileBuffers(namedPipe);
    }
    else
        printf("Error connecting to server\r\nClient closing\r\n");
    CloseHandle(namedPipe);
}
