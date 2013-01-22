#include "Client.h"
#include "Server.h"

int Server(){
    if (RunServer() == TRUE)
        return EXIT_SUCCESS;
    else
        return EXIT_FAILURE;
}

int Client(){
    if (RunClient() == TRUE)
        return EXIT_SUCCESS;
    else
        return EXIT_FAILURE;
}

int CloseServer(){
    if (SendCloseRequest() == TRUE)
        return EXIT_SUCCESS;
    else
        return EXIT_FAILURE;
}

int main(int argc, char* args[]){
    int returnCode = EXIT_FAILURE;

    if (argc > 1)
        if (strcmp(args[1], "server") == 0)
            Server();
        else if (strcmp(args[1], "client") == 0)
            Client();
        else if (strcmp(args[1], "close") == 0)
            CloseServer();
        else
            printf("Invalid argument\r\n");
    else
        printf("Use with arguments\r\n - server\r\n - client\r\n");
    return returnCode;
}
