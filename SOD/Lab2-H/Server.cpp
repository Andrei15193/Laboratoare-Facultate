#include "Server.h"

struct ClientNode{
    HANDLE sendMailslot;
    DWORD clientId;
    struct Response response;
    struct ClientNode* next;
};

struct Server{
    HANDLE listenMailslot;
    struct ClientNode *firstClient;
    unsigned int numberOfClients;
};

#ifdef UNICODE
HANDLE ConnectToClient(DWORD clientId){
    wchar_t mailslotPath[40] = CLIENT_PREFIX;
    DwordToArray(mailslotPath + wcslen(mailslotPath), clientId);
    return CreateFile(mailslotPath, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
}
#else

HANDLE ConnectToClient(DWORD clientId){
    char mailslotPath[40] = CLIENT_PREFIX;
    DwordToArray(mailslotPath + strlen(mailslotPath), clientId);
    return CreateFile(mailslotPath, GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
}
#endif /* UNICODE */

struct Server* CreateServer(){
    HANDLE mailslot = CreateMailslot(SERVER_MAILSLOT, sizeof(struct Message), MAILSLOT_WAIT_FOREVER, NULL);
    struct Server* server = NULL;
    if (mailslot != INVALID_HANDLE_VALUE){
        server = (struct Server*)malloc(sizeof(struct Server));
        server->listenMailslot = mailslot;
        server->firstClient = NULL;
        server->numberOfClients = 0;
    }
    return server;
}

void DestroyServer(struct Server* server){
    struct ClientNode *it = server->firstClient, *deleted;
    while (it != NULL){
        deleted = it;
        it = it->next;
        CloseHandle(deleted->sendMailslot);
        free(deleted);
    }
    free(server);
}

void RemoveClient(struct Server* server, struct ClientNode* client){
    struct ClientNode* it;
    if (server->firstClient == client)
        server->firstClient = client->next;
    else{
        it = server->firstClient;
        while (it->next != NULL && it->next != client)
            it = it->next;
        it->next = client->next;
    }
    CloseHandle(client->sendMailslot);
    free(client);
}

struct ClientNode* NewClient(HANDLE clientMailslot, struct Message* message, struct ClientNode* next){
    struct ClientNode* client = (struct ClientNode*)malloc(sizeof(struct ClientNode));
    client->clientId = message->clientId;
    client->sendMailslot = clientMailslot;
    client->response.minimum = client->response.maximum = message->number;
    client->next = next;
    return client;
}

BOOL AddClient(struct Server* server, struct Message* message){
    if (message->number == -1){
        CloseHandle(server->listenMailslot);
        printf("Cleint with id %d requested to close the server\r\n", message->clientId);
        return FALSE;
    }
    else{
        HANDLE clientMailslot = ConnectToClient(message->clientId);
        if (clientMailslot != INVALID_HANDLE_VALUE){
            server->firstClient = NewClient(clientMailslot, message, server->firstClient);
            printf("Client with id %d has connected\r\n", message->clientId);
        }
        else
            printf("Unable to connect to client\r\n");
        return TRUE;
    }
}

void InterpretMessage(struct Server* server, struct Message* message, struct ClientNode* client){
    DWORD bytes;
    if (message->number < 0){
        WriteFile(client->sendMailslot, &client->response, sizeof(struct Response), &bytes, NULL);
        printf("Client with id %d has disconnected\r\n", client->clientId);
        RemoveClient(server, client);
    }
    else
        if (client->response.minimum > message->number)
            client->response.minimum = message->number;
        else if (message->number > client->response.maximum)
            client->response.maximum = message->number;
}

BOOL ParseMessages(struct Server *server, DWORD messages){
    BOOL stillRuns = TRUE;
    DWORD bytes;
    struct ClientNode* it;
    struct Message message;
    for (DWORD i = 0; i < messages; i++){
        ReadFile(server->listenMailslot, &message, sizeof(struct Message), &bytes, NULL);
        it = server->firstClient;
        while (it != NULL && it->clientId != message.clientId)
            it = it->next;
        if (it == NULL){
            if (AddClient(server, &message) == FALSE)
                stillRuns = FALSE;
        }
        else
            InterpretMessage(server, &message, it);
    }
    return stillRuns;
}

BOOL WaitForMessages(struct Server* server, DWORD* messages, DWORD interval){
    *messages = 0;
    while (GetMailslotInfo(server->listenMailslot, NULL, NULL, messages, NULL) == TRUE && *messages == 0)
        Sleep(interval);
    if (*messages == 0)
        return FALSE;
    else
        return TRUE;
}

BOOL RunServer(){
    DWORD messages;
    struct Server* server = CreateServer();
    if (server == NULL)
        return FALSE;
    else{
        printf("Server open\r\n");
        do WaitForMessages(server, &messages, 1000); while (ParseMessages(server, messages) == TRUE);
        DestroyServer(server);
        printf("Server closing\r\n");
        return TRUE;
    }
}
