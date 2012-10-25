#include "Client.h"

#ifdef UNICODE

int ReadMyNumber(){
	char buffer[10];
	printf("Number: ");
	fgets(buffer, 10, stdin);
	return atoi(buffer);
}

void ClientBusiness(HANDLE read, HANDLE send){
	int number, min, max;
	DWORD messages = 0, bytes;
	do{
		number = ReadMyNumber();
		if (WriteFile(send, &number, sizeof(int), &bytes, NULL) == FALSE)
			printf("Error while sending message to server\r\n");
	}while (number >= 0);
	while (GetMailslotInfo(read, NULL, NULL, &messages, NULL) == TRUE && messages != 2)
		Sleep(1000);
	ReadFile(read, &min, sizeof(int), &bytes, NULL);
	ReadFile(read, &max, sizeof(int), &bytes, NULL);
	printf("Min value is: %d\r\nMax value is: %d\r\n", min, max);
}

void Client(){
	wchar_t *readPath = (wchar_t*)calloc(sizeof(wchar_t), 100),
			*sendPath = (wchar_t*)calloc(sizeof(wchar_t), 100);
    DWORD message = GetCurrentProcessId(), messages, bytes;
	HANDLE read, send = INVALID_HANDLE_VALUE;

	SetPaths(readPath, sendPath, message);
	read = CreateMailslot(readPath, sizeof(int), MAILSLOT_WAIT_FOREVER, NULL);
	if (read == INVALID_HANDLE_VALUE)
		printf("Error creating mailslot\r\nClosing\r\n");
	else{
		send = CreateFile(L"\\\\.\\mailslot\\fair1064server", GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
		if (send == INVALID_HANDLE_VALUE)
			printf("Could not connect to server\r\nClosing\r\n");
		else
			if (WriteFile(send, &message, sizeof(DWORD), &bytes, NULL) == FALSE)
				printf("Error writing message\r\n");
			else{
				printf("Message sent\r\n");
				
				messages = 0;
				while (GetMailslotInfo(read, NULL, NULL, &messages, NULL) == TRUE && messages == 0)
					Sleep(1000);
				CloseHandle(send);
				send = CreateFile(sendPath, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
				if (send == INVALID_HANDLE_VALUE)
					Sleep(1000);
				printf("Connected to server thread\r\n");
				ReadFile(read, &message, sizeof(int), &bytes, NULL);
				if (message == GetCurrentProcessId())
					ClientBusiness(read, send);
				else
					printf("Server denied access\r\n");
			}
	}
	CloseHandle(read);
	CloseHandle(send);
	free(readPath);
	free(sendPath);
}
#else
#endif

//int cif(DWORD number){
//    int s = 0;
//    do s++; while (number /= 10);
//    return s;
//}
//
//void DwordToCharArray(DWORD number, char* result){
//    int n = cif(number);
//    result[n--] = '\0';
//    do{
//        result[n--] = number % 10 + '0';
//        number /= 10;
//    }while (number > 0);
//}
//
//void MakeClientMailSlotPaths(char* c2s, char* s2c, DWORD clientId){
//    strcpy(c2s, "\\\\,\\mailslot\\fair1064\\");
//    DwordToCharArray(clientId, c2s + strlen(c2s));
//    strcpy(s2c, c2s);
//    strcat(c2s, "send");
//    strcat(s2c, "read");
//}
//
//void ClientBusiness(DWORD clientId){
//    char *clientToServerMailSlotPath = (char*)calloc(sizeof(char), 100),
//         *serverToClientMailSlotPath = (char*)calloc(sizeof(char), 100);
//    MakeClientMailSlotPaths(clientToServerMailSlotPath, serverToClientMailSlotPath, clientId);
//    free(clientToServerMailSlotPath);
//    free(serverToClientMailSlotPath);
//}
//
//void Client(){
//    DWORD message = GetCurrentProcessId(), bytes;
//
//    HANDLE sendProcessId = CreateFile(L"\\\\.\\mailslot\\fair1064\\server", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
//    HANDLE sendMailSlot = CreateMailslot((LPCTSTR)clientToServerMailSlotPath, sizeof(DWORD), MAILSLOT_WAIT_FOREVER, NULL);
//    if (sendProcessId == INVALID_HANDLE_VALUE)
//        printf("Could not connect to server\r\nClosing\r\n");
//    else
//        if (WriteFile(sendProcessId, &message, sizeof(DWORD), &bytes, NULL) == FALSE)
//            printf("Error writing message\r\n");
//        else
//            printf("Message sent\r\n");
//    CloseHandle(sendProcessId);
//}
