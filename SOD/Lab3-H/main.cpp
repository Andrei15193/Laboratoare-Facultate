#include <string.h>
#include "Server.h"
#include "Client.h"

int main(int argc, char* args[]){
    if (argc > 1)
        if (strcmp(args[1], "server") == 0)
            RunServer();
        else if (strcmp(args[1], "client") == 0)
            RunClient();
        else
            printf("Invalid argument\r\n");
    else
        printf("Invalid argument\r\n - server\r\n - client\r\n");
    return 0;
}
