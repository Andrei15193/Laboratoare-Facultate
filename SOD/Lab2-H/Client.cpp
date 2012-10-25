#include "Client.h"

//typedef struct{
//    wchar_t* clientToServer;
//    wchar_t* serverToClient;
//    HANDLE sendToMailslot;
//    HANDLE readFromMailslot;
//}ClientData;

// Make itoa (wide and narrow)
void Client(){
    DWORD message = GetCurrentProcessId(), bytes;

    HANDLE sendProcessId = CreateFile(L"\\\\.\\mailslot\\fair1064\\server", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    //HANDLE sendMailSlot = CreateMailslot((LPCTSTR)clientToServerMailSlotPath, sizeof(DWORD), MAILSLOT_WAIT_FOREVER, NULL);
    if (sendProcessId == INVALID_HANDLE_VALUE)
        printf("Could not connect to server\r\nClosing\r\n");
    else
        if (WriteFile(sendProcessId, &message, sizeof(DWORD), &bytes, NULL) == FALSE)
            printf("Error writing message\r\n");
        else
            printf("Message sent\r\n");
    CloseHandle(sendProcessId);
}


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