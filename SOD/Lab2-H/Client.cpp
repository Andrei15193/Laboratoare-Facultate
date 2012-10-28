#include "Client.h"

struct Client{
    HANDLE sendMailslot;
    HANDLE readMailslot;
    struct Message message;
    struct Response result;
};

enum ClientRunCode{
    NeedsToSendMessages,
    NeedsToReadResult,
    LostConnectionToServer,
};

#ifdef UNICODE
HANDLE CreateClientMailslot(DWORD clientId){
    wchar_t mailslotPath[40] = CLIENT_PREFIX;
    DwordToArray(mailslotPath + wcslen(mailslotPath), clientId);
    return CreateMailslot(mailslotPath, sizeof(struct Response), MAILSLOT_WAIT_FOREVER, NULL);
}
#else

HANDLE CreateClientMailslot(DWORD clientId){
    char mailslotPath[40] = CLIENT_PREFIX;
    DwordToArray(mailslotPath + strlen(mailslotPath), clientId);
    return CreateMailslot(mailslotPath, sizeof(struct Response), MAILSLOT_WAIT_FOREVER, NULL);
}
#endif /* UNICODE */

int ReadMyNumber(){
    char buffer[10];
    printf("Number: ");
    fgets(buffer, 10, stdin);
    return atoi(buffer);
}

struct Client* CreateClient(){
    DWORD clientId = GetCurrentProcessId();
    HANDLE serverMailslot = CreateFile(SERVER_MAILSLOT, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    HANDLE readMailslot = CreateClientMailslot(clientId);
    struct Client* client = NULL;
    if (readMailslot == INVALID_HANDLE_VALUE || serverMailslot == INVALID_HANDLE_VALUE)
        CloseHandle(readMailslot);
    else{
        client = (struct Client*)malloc(sizeof(struct Client));
        client->sendMailslot = serverMailslot;
        client->readMailslot = readMailslot;
        client->message.clientId = clientId;
    }
    return client;
}

void DestoryClient(struct Client *client){
    CloseHandle(client->readMailslot);
    CloseHandle(client->sendMailslot);
    free(client);
}

void GetResults(struct Client* client, DWORD interval){
    DWORD messages = 0, bytes;
    while (GetMailslotInfo(client->readMailslot, NULL, NULL, &messages, NULL) == TRUE && messages == 0)
        Sleep(interval);
    if (ReadFile(client->readMailslot, &client->result, sizeof(struct Response), &bytes, NULL) == TRUE)
        printf("Minimum is: %d\r\nMaximum is: %d\r\n", client->result.minimum, client->result.maximum);
    else
        printf("Error while getting results\r\n");
}

ClientRunCode SendMessages(struct Client* client){
    DWORD bytes;
    enum ClientRunCode returnValue = NeedsToSendMessages;
    do client->message.number = ReadMyNumber(); while (client->message.number < 0);
    do
        if (WriteFile(client->sendMailslot, &client->message, sizeof(struct Message), &bytes, NULL) == FALSE)
            returnValue = LostConnectionToServer;
        else
            if (client->message.number < 0)
                returnValue = NeedsToReadResult;
            else
                client->message.number = ReadMyNumber();
    while (returnValue == NeedsToSendMessages);
    return returnValue;
}

BOOL RunClient(){
    BOOL returnValue = TRUE;
    struct Client* client = CreateClient();
    if (client == NULL)
        return FALSE;
    else{
        printf("Client open\r\n");
        switch (SendMessages(client)){
        case LostConnectionToServer:
            printf("Error while communicating with server\r\n");
            returnValue = FALSE;
            break;
        default:
            GetResults(client, 1000);
            break;
        }
        printf("Client closing\r\n");
        DestoryClient(client);
        return returnValue;
    }
}

BOOL SendCloseRequest(){
    BOOL returnValue = TRUE;
    DWORD bytes;
    HANDLE serverMailslot = CreateFile(SERVER_MAILSLOT, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    struct Message message = {GetCurrentProcessId(), -1};
    printf("Client open\r\n");
    if (serverMailslot == INVALID_HANDLE_VALUE)
        returnValue = FALSE;
    else{
        WriteFile(serverMailslot, &message, sizeof(struct Message), &bytes, NULL);
        printf("Sent close request\r\n");
    }
    printf("Client closing\r\n");
    return returnValue;
}
