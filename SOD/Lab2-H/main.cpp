#include "Client.h"

typedef struct{
    DWORD clientId;
}ClientResponseArgs;

DWORD ClientResponse(LPVOID args){
    ClientResponseArgs* clientResponseArgs = (ClientResponseArgs*)args;
    printf("Thread response to %d\r\n", clientResponseArgs->clientId);
    free(args);
    return 0;
}

int CreateResponseThread(DWORD clientId){
    int result = EXIT_SUCCESS;
    ClientResponseArgs* args = (ClientResponseArgs*)malloc(sizeof(ClientResponseArgs));
    args->clientId = clientId;
    if (CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&ClientResponse, (LPVOID)args, 0, NULL) == NULL)
        result = EXIT_FAILURE;
    return result;
}

void Server(){
    DWORD bytes, clientId, messages;
    HANDLE connectMailslot = CreateMailslot(L"\\\\.\\mailslot\\fair1064\\server", sizeof(DWORD), MAILSLOT_WAIT_FOREVER, NULL);
    if (connectMailslot == INVALID_HANDLE_VALUE)
        printf("Failed to create server\r\nClosing\r\n");
    else
        do{
            messages = 0;
            while (GetMailslotInfo(connectMailslot, NULL, NULL, &messages, NULL) == TRUE && messages == 0)
                Sleep(1000);
            for (DWORD i = 0; i < messages; i++)
                if (ReadFile(connectMailslot, &clientId, sizeof(DWORD), &bytes, NULL) == FALSE)
                    printf("Error while getting client ID\r\n");
                else
                    CreateResponseThread(clientId);
        }while (messages != 0);
    printf("Server closing\r\n");
    CloseHandle(connectMailslot);
}

int main(int argc, char* args[]){
	if (argc > 1)
        if (strcmp(args[1], "server") == 0)
            Server();
        else if (strcmp(args[1], "client") == 0)
            Client();
        else
            printf("Invalid argument\r\n");
    else
        printf("Use with arguments\r\n - server\r\n - client\r\n");
	return 0;
}