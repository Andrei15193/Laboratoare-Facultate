#include "Client.h"

int main(int argc, char* args[])
{
    int sock;
    if (argc > 2){
//        sock = ConnectToServer(args[1], atoi(args[2]));
        if (sock < 0){
            printf("Failed to connect to server\n");
            return EXIT_FAILURE;
        }
        else
            return EXIT_SUCCESS;
    }
    else{
        printf("Usage: ./Client <SERVER_IP> <PORT>\n");
        return EXIT_FAILURE;
    }
}

