#include "Client.h"

typedef struct{
    DWORD clientId;
}ClientResponseArgs;

#ifdef UNICODE

void ServerBusiness(HANDLE read, HANDLE send){
	int min = -1, max = -1, message = -1;
	DWORD messages = 0, bytes;
	do{
		while (GetMailslotInfo(read, NULL, NULL, &messages, NULL) == TRUE && messages == 0)
			Sleep(1000);
		for (DWORD i = 0; i < messages; i++){
			if (ReadFile(read, &message, sizeof(int), &bytes, NULL) == FALSE)
				printf("Error while reading from mailslot\r\n");
			else{
				if (min == -1 || message < min && message != -1)
					min = message;
				if (max == -1 || message > max)
					max = message;
			}
		}
	}while (message >= 0);
	printf("Writing result to client\r\n");
	if (WriteFile(send, &min, sizeof(int), &bytes, NULL) == FALSE)
		printf("Error sending minimum value\r\n");
	if (WriteFile(send, &max, sizeof(int), &bytes, NULL) == FALSE)
		printf("Error sending maximum value\r\n");
	printf("Results sent\r\n");
}

DWORD ClientResponse(LPVOID args){
    ClientResponseArgs* clientResponseArgs = (ClientResponseArgs*)args;
	DWORD bytes;
	wchar_t *sendPath = (wchar_t*)calloc(sizeof(wchar_t), 100),
			*readPath = (wchar_t*)calloc(sizeof(wchar_t), 100);
	HANDLE read, send;
	SetPaths(sendPath, readPath, clientResponseArgs->clientId);
	read = CreateMailslot(readPath, sizeof(int), MAILSLOT_WAIT_FOREVER, NULL);
	send = CreateFile(sendPath, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    printf("Created thread for %d\r\n", clientResponseArgs->clientId);
	if (send == INVALID_HANDLE_VALUE)
		printf("Error connecting to client mailslot\r\n");
	else
		if (WriteFile(send, &clientResponseArgs->clientId, sizeof(DWORD), &bytes, NULL) == FALSE)
			printf("Error writing to client\r\n");
		else
			ServerBusiness(read, send);

    free(args);
	free(readPath);
	free(sendPath);
	CloseHandle(read);
	CloseHandle(send);
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
    HANDLE connectMailslot = CreateMailslot(L"\\\\.\\mailslot\\fair1064server", sizeof(DWORD), MAILSLOT_WAIT_FOREVER, NULL);
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

#else
#endif /* UNICODE */
