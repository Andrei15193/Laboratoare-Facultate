#include "Client.h"

int SetServerStruct(struct sockaddr_in* server, char* address, uint16_t port){
    struct hostent *host = gethostbyname(address);
    if (host == NULL)
        return -1;
    else{
        memset((void*) server, 0, sizeof(*server));
        server->sin_family = AF_INET;
        server->sin_port = htons(port);
        memcpy((void*) &server->sin_addr.s_addr, (void*)host->h_addr, host->h_length);
        return 0;
    }
}

int ConnectToServer(char* address, uint16_t port){
    int sock;
    struct sockaddr_in server;
    if (SetServerStruct(&server, address, port) == 0){
        sock = socket(AF_INET, SOCK_STREAM, 0);
        if (sock >= 0)
            if (connect(sock, (struct sockaddr*) &server, sizeof(server)) == 0)
                return sock;
            else
                return -3;
        else
            return -2;
    }
    else
        return -1;
}

int GetServerResponse(int sock, struct Array* array){
    unsigned char responseCode = 1;
    if (recv(sock, (void*) &responseCode, sizeof(responseCode), 0) != -1)
        switch (responseCode){
        case 0:
            if (RecvArray(sock, 10000, array) == 0)
                return 0;
            else
                return -4;
        case 1:
            return -1;
        case 2:
            return -2;
        case 3:
            return -3;
        }
    else
        return -5;
}

int RunClient(char* address, uint16_t port){
    struct Array array[3] = {ARRAY_INIT, ARRAY_INIT, ARRAY_INIT};
    array[0] = ReadArray();
    array[1] = ReadArray();
    int result = EXIT_FAILURE, sock = ConnectToServer(address, port);
    unsigned char i = 0;
    switch (sock){
    case -1:
        printf("Error @ gethostbyname()\n");
        break;
    case -2:
        printf("Error @ socket()\n");
        break;
    case -3:
        printf("Error @ connect()\n");
        break;
    default:
        printf("Connected to %s:%u\n", address, port);
        do
            SendArray(sock, &array[i]);
        while (++i < 2);
        switch (GetServerResponse(sock, array + 2)){
        case 0:
            PrintArray(array + 2);
            break;
        default:
            printf("An error occurred while sending or receiving data\n");
            break;
        }

        close(sock);
        printf("Disconnected from %s:%u\n", address, port);
        break;
    }
    return result;
}

int main(int argc, char* args[]){
    if (argc > 2)
        return RunClient(args[1], atoi(args[2]));
    else{
        printf("Usage: ./Client <SERVER_IP> <SERVER_PORT>\n");
        return EXIT_FAILURE;
    }
}
