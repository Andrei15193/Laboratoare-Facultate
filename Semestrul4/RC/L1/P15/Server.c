#define SERVER_INITIAL_STATE { {-1, port, 0}, {PTHREAD_RWLOCK_INITIALIZER, Closed} }

#include "Server.h"

enum ServerRunState{
    Open,
    SignaledToClose,
    Closed
};

struct PublicServerState{
    pthread_rwlock_t runStateRW;
    enum ServerRunState runState;
};

struct ServerState{
    struct{
        int socket;
        uint16_t port;
        pthread_t consoleThread;
        fd_set fdSet;
    }private;
    struct PublicServerState public;
};


void PrintServerState(enum ServerRunState serverRunState){
    char runState[3][8] = {"Open", "Closing", "Closed"};
    printf("Server run state: %s\n", runState[serverRunState]);
}

enum StartServerResult InitializeServer(struct ServerState *serverState, uint16_t port){
    struct sockaddr_in server;
    serverState->private.socket = socket(AF_INET, SOCK_STREAM, 0);
    
    FD_ZERO(&serverState->private.fdSet);
    FD_SET(serverState->private.socket, &serverState->private.fdSet);
    
    if (serverState->private.socket < 0)
        return FailedToCreateSocket;
    else{
        memset((char*) &server, 0, sizeof(struct sockaddr_in));
        server.sin_family = AF_INET;
        server.sin_port = htons(port);
        server.sin_addr.s_addr = INADDR_ANY;
        if (bind(serverState->private.socket, (const struct sockaddr*) &server, sizeof(struct sockaddr_in)) < 0)
            return FailedToBindPort;
        else
            if (listen(serverState->private.socket, 5) < 0)
                return FailedToListen;
            else{
                serverState->public.runState = Open;
                return Success;
            }
    }
}

void* ServerConsole(void* param){
    char buffer[101];
    struct PublicServerState* state = (struct PublicServerState*)param;
    printf("Server console:\n");
    do{
        printf("> ");
        fgets(buffer, sizeof(buffer), stdin);
    }while (strcmp(buffer, "exit\n") != 0);
    pthread_rwlock_wrlock(&state->runStateRW);
    state->runState = SignaledToClose;
    PrintServerState(state->runState);
    pthread_rwlock_unlock(&state->runStateRW);
    return NULL;
}

unsigned char ReadArrays(int sock, struct Array *array1, struct Array *array2){
    unsigned char response;
    response = ReadMessage(sock, 5, array1);
    if (response == 0)
        response = ReadMessage(sock, 5, array2);
    return response;
}

void ExtractArray(struct Array *array1, struct Array *array2, struct Array *array3){
}

int ClientResponse(int sock){
    int retur = EXIT_SUCCESS;
    unsigned char response;
    struct Array array1 = ARRAY_INIT, array2 = ARRAY_INIT, array3 = ARRAY_INIT;
    response = ReadArrays(sock, &array1, &array2);
    send(sock, &response, sizeof(unsigned char), 0);
    if (response == 0){
        ExtractArray(&array1, &array2, &array3);
        WriteMessage(sock, &array3);
    }
    else
        retur = EXIT_FAILURE;
    ClearArray(&array1);
    ClearArray(&array2);
    ClearArray(&array3);
    return retur;
}

void AcceptClient(struct ServerState *serverState, long sec, long microsec){
    char* message;
    int clientSock, size = sizeof(struct sockaddr_in), result;
    FILE* logFile;
    struct timeval timeout = {sec, microsec};
    struct sockaddr_in client;
    memset((char*) &client, 0, sizeof(struct sockaddr_in));
    
    if (select(serverState->private.socket + 1, &serverState->private.fdSet, NULL, NULL, &timeout) > 0){
        clientSock = accept(serverState->private.socket, (struct sockaddr*) &client, &size);
        if (fork() == 0){
            close(serverState->private.socket);
            pthread_rwlock_destroy(&serverState->public.runStateRW);
            result = ClientResponse(clientSock);
            close(clientSock);
            exit(result);
        }
        else{
            close(clientSock);
            logFile = fopen("logFile.txt", "a");
            if (logFile != NULL){
                fprintf(logFile, "%s:%d\r\n", inet_ntoa(client.sin_addr), ntohs(client.sin_port));
                fclose(logFile);
            }
        }
    }
}

enum StartServerResult StartServer(uint16_t port){
    int canRun = 1, i;
    struct ServerState serverState = SERVER_INITIAL_STATE;
    enum StartServerResult startResult;
    signal(SIGCHLD, SIG_IGN);
    PrintServerState(serverState.public.runState);
    printf("Initializing server\n");
    startResult = InitializeServer(&serverState, port);
    PrintServerState(serverState.public.runState);
    if (startResult == Success)
        if (pthread_create(&serverState.private.consoleThread, NULL, ServerConsole, &serverState.public) == 0){
            do{
                AcceptClient(&serverState, 3, 0);
                pthread_rwlock_rdlock(&serverState.public.runStateRW);
                if (serverState.public.runState == SignaledToClose)
                    canRun = 0;
                pthread_rwlock_unlock(&serverState.public.runStateRW);
            }while (canRun == 1);
            for (i = 0; i < serverState.private.socket + 1; i++)
                if (FD_ISSET(i, &serverState.private.fdSet))
                    close(i);
            pthread_join(serverState.private.consoleThread, NULL);
            pthread_rwlock_destroy(&serverState.public.runStateRW);
            serverState.public.runState = Closed;
            PrintServerState(serverState.public.runState);
            return Success;
        }
        else
            return FailedToStartConsoleThread;
    else
        return startResult;
}

void PrintStartServerResult(enum StartServerResult result){
    char initializeResult[5][31] = { "successful", "failed to create socket", "failed to bind port", "failed to listen", "failed to start console thread" };
    printf("Server initialize result: %s\n", initializeResult[result]);
}

int main(int argc, char* args[]){
    enum StartServerResult result;
    if (argc > 1){
        result = StartServer(atoi(args[1]));
        if (result != Success){
            PrintStartServerResult(result);
            return EXIT_FAILURE;
        }
        else
            return EXIT_SUCCESS;
    }
    else{
        printf("Usage: ./Server <PORT>\n");
        return EXIT_FAILURE;
    }
}

