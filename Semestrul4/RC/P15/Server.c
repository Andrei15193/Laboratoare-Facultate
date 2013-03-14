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

int ClientResponse(int sock){
    printf("I am in the client response process!");
    return EXIT_SUCCESS;
}

void AcceptClient(struct ServerState *serverState, long sec, long microsec){
    int clientSock, size = sizeof(struct sockaddr_in);
    fd_set fdSet;
    struct timeval timeout = {sec, microsec};
    struct sockaddr_in client;
    memset((char*) &client, 0, sizeof(struct sockaddr_in));
    
    FD_ZERO(&fdSet);
    FD_SET(serverState->private.socket, &fdSet);
    
    if (select(1, &fdSet, NULL, NULL, &timeout) == 1){
        clientSock = accept(serverState->private.socket, (struct sockaddr*) &client, &size);
        if (fork() == 0){
            int result;
            close(serverState->private.socket);
            pthread_rwlock_destroy(&serverState->public.runStateRW);
            result = ClientResponse(clientSock);
            close(clientSock);
            exit(result);
        }
        else{
            // Log IP into a text file.
        }
    }
}

enum StartServerResult StartServer(uint16_t port){
    int ok = 1;
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
                    ok = 0;
                pthread_rwlock_unlock(&serverState.public.runStateRW);
            }while(ok != 0);
            close(serverState.private.socket);
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

