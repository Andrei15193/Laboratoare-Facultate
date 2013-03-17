#include "Server.h"

void PrintServerState(struct ServerState* serverState){
    pthread_rwlock_rdlock(&serverState->rwStateVariable);
    printf("Server state:\n");
    switch(serverState->runState){
    case Open:
        printf("Run state: Open\n");
        break;
    case Closing:
        printf("Run state: Closing\n");
        break;
    case Closed:
        printf("Run state: Closed\n");
        break;
    }
    pthread_rwlock_unlock(&serverState->rwStateVariable);
}

void SetServerRunState(struct ServerState* serverState, enum ServerRunState newRunState){
    pthread_rwlock_wrlock(&serverState->rwStateVariable);
    serverState->runState = newRunState;
    pthread_rwlock_unlock(&serverState->rwStateVariable);
    PrintServerState(serverState);
}

enum ServerConsoleCommand ReadCommand(const char* message){
    char buff[128];
    printf("%s", message);
    fgets(buff, sizeof(buff), stdin);
    if (strcasecmp(buff, "close\n") == 0)
        return Close;
    else if (strcasecmp(buff, "help\n") == 0 || strcasecmp(buff, "?\n") == 0)
        return Help;
    else
        return InvalidCommand;
}

void* ServerConsoleThread(void* param){
    struct ServerState* serverState = (struct ServerState*)param;
    enum ServerConsoleCommand command;
    printf("Server console:\n");
    do{
        command = ReadCommand("> ");
        switch(command){
        case Close:
            SetServerRunState(serverState, Closing);
            break;
        case Help:
            printf("Availalble commands\n  > close\n    Closes the server.\n  > help\n    Shows this help page (equivalent to ?).\n");
            break;
        }
    }while(command != Close);
    return NULL;
}

void SetServerStruct(struct sockaddr_in* server, uint16_t port){
    memset((void*)server, 0, sizeof(*server));
    server->sin_family = AF_INET;
    server->sin_addr.s_addr = INADDR_ANY;
    server->sin_port = htons(port);
}

int SetUpServer(struct sockaddr_in* server){
    int sock = socket(AF_INET, SOCK_STREAM, 0);
    if (sock >= 0){
        if (bind(sock, (struct sockaddr*)server, sizeof(*server)) == 0)
            if (listen(sock, 5) == 0)
                return sock;
            else
                return -3;
        else
            return -2;
    }
    else
        return -1;
}

int ReadArrays(int clientSock, struct Array* array1, struct Array* array2){
    struct Array* array[2] = {array1, array2};
    unsigned char responseCode = 0, i = 0;
    do
        switch (RecvArray(clientSock, TIMEOUT_MS, array[i])){
        case 0:
            break;
        case -1:
            responseCode = 1;
            break;
        case -2:
            responseCode = 2;
            break;
        }
    while (++i < 2 && responseCode == 0);
    return responseCode;
}

struct Array ExtractArray(struct Array* array1, struct Array* array2){
    uint16_t i, j;
    struct Array result = ARRAY_INIT;
    result.Items = (int16_t*)calloc(array1->Length, sizeof(int16_t));
    for (i = 0; i < array1->Length; i++){
        j = 0;
        while (j < array2->Length && array1->Items[i] != array2->Items[j])
            j++;
        if (j == array2->Length)
            result.Items[result.Length++] = array1->Items[i];
    }
    return result;
}

int ReplyToClient(int clientSock, unsigned char responseCode, struct Array* array1, struct Array* array2){
    struct Array array3 = ARRAY_INIT;
    if (send(clientSock, (void*) &responseCode, sizeof(responseCode), 0) != -1 && responseCode == 0){
        array3 = ExtractArray(array1, array2);
        if (SendArray(clientSock, &array3) == 0)
            return 0;
        else
            return -2;
    }
    else
        return -1;
}

int AcceptClient(int listenSock, int* clientSock, struct sockaddr* clientInfo, int* clientInfoLength, int timeout_ms){
    struct pollfd pollFd = {listenSock, POLLIN, 0};
    if (poll(&pollFd, 1, timeout_ms) == 1){
        *clientSock = accept(listenSock, clientInfo, clientInfoLength);
        return 0;
    }
    else
        return -1;
}

void LogClient(const char* logFileName, struct sockaddr_in* clientInfo){
    FILE* logFile = fopen(logFileName, "a");
    if (logFile != NULL){
        fprintf(logFile, "%s:%d\r\n", inet_ntoa(clientInfo->sin_addr), ntohs(clientInfo->sin_port));
        fclose(logFile);
    }
}

void RunServerLoop(int serverSock, struct ServerState* serverState){
    unsigned char responseCode;
    int clientSock, clientInfoLength, replyResult;
    struct sockaddr_in clientInfo;
    struct Array array[3] = {ARRAY_INIT, ARRAY_INIT, ARRAY_INIT};
    pthread_rwlock_rdlock(&serverState->rwStateVariable);
    while (serverState->runState != Closing){
        pthread_rwlock_unlock(&serverState->rwStateVariable);
        clientInfoLength = sizeof(clientInfo);
        if (AcceptClient(serverSock, &clientSock, (struct sockaddr*) &clientInfo, &clientInfoLength, 1000) == 0)
            switch (fork()){
            case 0:
                close(serverSock);
                pthread_rwlock_destroy(&serverState->rwStateVariable);
                replyResult = ReplyToClient(clientSock, ReadArrays(clientSock, array, array + 1), array, array + 1);
                close(clientSock);
                exit(replyResult == 0 ? EXIT_SUCCESS : EXIT_FAILURE);
                break;
            case -1:
                responseCode = 3;
                send(clientSock, (void*) &responseCode, sizeof(responseCode), 0);
                close(clientSock);
                break;
            default:
                close(clientSock);
                LogClient(LOGFILE, &clientInfo);
                break;
            }
        pthread_rwlock_rdlock(&serverState->rwStateVariable);
    }
    pthread_rwlock_unlock(&serverState->rwStateVariable);
}

int RunServerLogic(int sock){
    pthread_t consoleThread;
    struct ServerState serverState = SERVERSTATE_INIT;
    SetServerRunState(&serverState, Open);
    if (pthread_create(&consoleThread, NULL, ServerConsoleThread, (void*) &serverState) == 0)
    {
        RunServerLoop(sock, &serverState);
        pthread_join(consoleThread, NULL);
        SetServerRunState(&serverState, Closed);
        pthread_rwlock_destroy(&serverState.rwStateVariable);
        return EXIT_SUCCESS;
    }
    else
        return EXIT_FAILURE;
}

int StartServer(uint16_t port){
    int sock, result = EXIT_FAILURE;
    struct sockaddr_in server;
    signal(SIGCHLD, SIG_IGN);
    SetServerStruct(&server, port);
    sock = SetUpServer(&server);
    switch (sock){
    case -1:
        printf("Error @ socket()\n");
        break;
    case -2:
        printf("Error @ bind()\n");
        break;
    case -3:
        printf("Error @ listen()\n");
        break;
    default:
        result = RunServerLogic(sock);
        close(sock);
        break;
    }
    return result;
}

int main(int argc, char* args[]){
    if (argc > 1)
        return StartServer(atoi(args[1]));
    else{
        printf("Usage: ./Server <SERVER_PORT>\n");
        return EXIT_SUCCESS;
    }
}
