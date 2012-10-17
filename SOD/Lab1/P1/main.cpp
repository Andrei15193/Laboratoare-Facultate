// Sa se scrie un server si un client unde clientul citeste un numar natural nenul mai mic decat 100 care il trimite serverului,
// serverul il dubleaza dupa care il trimite clientului. Clientul la randul lui il dubleaza si il trimite serverului. Acest
// ciclu se repeta pana cand numarul ajunge mai mare decat 100.

#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>

int server(){
    int number, returnCode = EXIT_SUCCESS;
    BOOL runServer = TRUE;
    DWORD bytes;
    HANDLE outNamedPipe = CreateNamedPipe("\\\\.\\pipe\\serverToClient", PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE | PIPE_WAIT, 10, sizeof(int), sizeof(int), 1000, NULL),
            inNamedPipe = CreateNamedPipe("\\\\.\\pipe\\clientToServer", PIPE_ACCESS_INBOUND, PIPE_READMODE_BYTE | PIPE_WAIT, 10, sizeof(int), sizeof(int), 1000, NULL);

    if (inNamedPipe == INVALID_HANDLE_VALUE || outNamedPipe == INVALID_HANDLE_VALUE){
        printf("Error while creating named pipes\r\n => Server closes\r\n");
        returnCode = EXIT_FAILURE;
    }
    else
        do{
            printf("Awaiting connection\r\n");
            if ((ConnectNamedPipe(inNamedPipe, NULL) == TRUE || GetLastError() == ERROR_PIPE_CONNECTED) &&
                (ConnectNamedPipe(outNamedPipe, NULL) == TRUE || GetLastError() == ERROR_PIPE_CONNECTED))
                while (ReadFile(inNamedPipe, &number, sizeof(int), &bytes, NULL) == TRUE && number <= 100){
                    printf("Number received is %d\r\n", number);
                    number *= 2;
                    WriteFile(outNamedPipe, &number, sizeof(int), &bytes, NULL);
                    FlushFileBuffers(outNamedPipe);
                }
            else
                printf("Error while connecting to client\r\n");
            DisconnectNamedPipe(inNamedPipe);
            DisconnectNamedPipe(outNamedPipe);
            printf("Client disconnected\r\n\r\n");
        }while (runServer == TRUE);

    CloseHandle(inNamedPipe);
    CloseHandle(outNamedPipe);
    return returnCode;
}

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

int client(){
    int number = readMyNumber(), returnCode = EXIT_SUCCESS;
    DWORD bytes;
    HANDLE outNamedPipe = CreateFile("\\\\.\\pipe\\clientToServer", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL),
            inNamedPipe = CreateFile("\\\\.\\pipe\\serverToClient", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_ATTRIBUTE_READONLY, NULL);

    if (inNamedPipe == INVALID_HANDLE_VALUE || outNamedPipe == INVALID_HANDLE_VALUE){
        printf("Failed to connect to server\r\n");
        returnCode = EXIT_FAILURE;
    }
    else{
        do{
            WriteFile(outNamedPipe, &number, sizeof(int), &bytes, NULL);
            if (ReadFile(inNamedPipe, &number, sizeof(int), &bytes, NULL) == FALSE){
                printf("Connection interrupted\r\nClient closing\r\n");
                returnCode = EXIT_FAILURE;
            }
            else{
                printf("Number is %d\r\n", number);
                number *= 2;
            }
        }while (number <= 100 && returnCode == EXIT_SUCCESS);
        if (100 < number / 2)
            number /= 2;
        printf("Final number is %d\r\n", number);
    }

    CloseHandle(inNamedPipe);
    CloseHandle(outNamedPipe);
    return returnCode;
}

int main(int argc, char* args[]){
    if (argc > 1 && strcmp(args[1], "server") == 0)
        server();
    else
        client();
}