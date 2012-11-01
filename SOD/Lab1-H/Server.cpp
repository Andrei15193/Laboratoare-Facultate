#include "Server.h"

void RunServer(){
    int number;
    struct IntPair minMax;
    BOOL runServer = TRUE;
    DWORD bytes;
    HANDLE namedPipe = CreateNamedPipe(CONNECT_NAMEDPIPE, PIPE_ACCESS_INBOUND | PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE | PIPE_READMODE_BYTE, 2, sizeof(struct IntPair), sizeof(struct IntPair), 1000, NULL);
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
