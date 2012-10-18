// Se cere un server si unul sau mai multi clienti.
// Clientii ii transmit serverului numere intregi si pozitive.
// Trimiterea unui numar negativ inseamna incheierea trimiterii
// de numere de catre clientul respectiv. Serverul determina,
// pentru fiecare client in parte, cel mai mare si cel mai mic
// numar primit si trimite aceste rezultate clientului.


#include <stdio.h>
#include <string.h>
#include <Windows.h>

typedef struct{
    int first;
    int second;
}IntPair;

int readMyNumber(){
    char buffer[6];
    printf("Number: ");
    fgets(buffer, 6, stdin);
    return atoi(buffer);
}

void client(){
    int number;
    IntPair minMax;
    BOOL ok = TRUE;
    DWORD bytes;
    HANDLE namedPipe = CreateFile("\\\\.\\pipe\\fair1064sNamedPipe", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (namedPipe != INVALID_HANDLE_VALUE || namedPipe == INVALID_HANDLE_VALUE && GetLastError() == ERROR_PIPE_CONNECTED){
        do{
            number = readMyNumber();
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

void server(){
    int number;
    IntPair minMax;
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
            minMax.first = minMax.second = -1;
            while (ReadFile(namedPipe, &number, sizeof(int), &bytes, NULL) == TRUE && 0 <= number){
                if (number < minMax.first || minMax.first == -1)
                    minMax.first = number;
                if (minMax.second < number || minMax.second == -1)
                    minMax.second = number;
            }
            if (minMax.first == -1)
                runServer = FALSE;
            else{
                WriteFile(namedPipe, &minMax, sizeof(IntPair), &bytes, NULL);
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
        printf("Error connecting to server\r\nClient closing\r\n");
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
