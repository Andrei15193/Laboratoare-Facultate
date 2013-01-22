// Sa se scrie un server si un client unde clientul citeste un numar natural nenul mai mic decat 100 care il trimite serverului,
// serverul il dubleaza dupa care il trimite clientului. Clientul la randul lui il dubleaza si il trimite serverului. Acest
// ciclu se repeta pana cand numarul ajunge mai mare decat 100.

#include <stdio.h>
#include <string.h>
#include <Windows.h>

int readMyNumber(){
    int number;
    char buffer[6];
    do{
        printf("Number: ");
        fgets(buffer, 6, stdin);
        number = atoi(buffer);
    }while (number <= 0 || 100 <= number);
    return number;
}

void client(){
    int number = readMyNumber();
    BOOL ok = TRUE;
    DWORD bytes;
    HANDLE namedPipe = CreateFile("\\\\.\\pipe\\fair1064sNamedPipe", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (namedPipe != INVALID_HANDLE_VALUE || namedPipe == INVALID_HANDLE_VALUE && GetLastError() == ERROR_PIPE_CONNECTED){
        do{
            WriteFile(namedPipe, &number, sizeof(int), &bytes, NULL);
            FlushFileBuffers(namedPipe);
            if (ReadFile(namedPipe, &number, sizeof(int), &bytes, NULL) == FALSE){
                printf("Error on reading message\r\nClient closing\r\n");
                ok = FALSE;
            }
            printf("Number is: %d\r\n", number);
        }while (number <= 100 && ok == TRUE);
    }
    else
        printf("Error connecting to server\r\nClient closing");
    CloseHandle(namedPipe);
}

void server(){
    int number;
    BOOL runServer = TRUE;
    DWORD bytes;
    HANDLE namedPipe = CreateNamedPipe("\\\\.\\pipe\\fair1064sNamedPipe", PIPE_ACCESS_INBOUND | PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE | PIPE_READMODE_BYTE, 10, sizeof(int), sizeof(int), 1000, NULL);
    if (namedPipe == INVALID_HANDLE_VALUE)
        printf("Unable to create named pipe\r\nServer closing\r\n");
    else{
        printf("Server created\r\n");
        do{
            printf("Awaiting connection\r\n");
            ConnectNamedPipe(namedPipe, NULL);
            printf("Connection established\r\n");
            while (ReadFile(namedPipe, &number, sizeof(int), &bytes, NULL) == TRUE && number <= 100)
                if (number == -1){
                    printf("Received close request\r\n");
                    runServer = FALSE;
                }
                else{
                    printf("Number is: %d\r\n", number);
                    number *= 2;
                    WriteFile(namedPipe, &number, sizeof(int), &bytes, NULL);
                    FlushFileBuffers(namedPipe);
                }
                printf("Disconnecting client\r\n\r\n");
                DisconnectNamedPipe(namedPipe);
        }while (runServer == TRUE);
    }
    CloseHandle(namedPipe);
}

void closeServer(){
    int number = -1;
    DWORD bytes;
    HANDLE namedPipe = CreateFile("\\\\.\\pipe\\fair1064sNamedPipe", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (namedPipe != INVALID_HANDLE_VALUE || namedPipe == INVALID_HANDLE_VALUE && GetLastError() == ERROR_PIPE_CONNECTED){
        WriteFile(namedPipe, &number, sizeof(int), &bytes, NULL);
        FlushFileBuffers(namedPipe);
    }
    else
        printf("Error connecting to server\r\nClient closing");
    CloseHandle(namedPipe);
}

int main(int argc, char* args[]){
    if (argc > 1)
        if (strcmp(args[1], "client") == 0)
            client();
        else if (strcmp(args[1], "server") == 0)
            server();
        else if (strcmp(args[1], "close") == 0)
            closeServer();
        else
            printf("Invalid arguments\r\n");
    else
        printf("Error! use with arguments client, server or close\r\n");
    return 0;
}
