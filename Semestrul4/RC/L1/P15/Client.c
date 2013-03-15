#include "Client.h"

int ConnectToServer(const char* address, uint16_t port){
    int sock = socket(AF_INET, SOCK_STREAM, 0);
    struct sockaddr_in server;
    struct hostent *host;
    if (sock < 0)
        return -1;
    else{
        host = gethostbyname(address);
        memset((char*) &server, 0, sizeof(server));
        server.sin_family = AF_INET;
        server.sin_port = htons(port);
        memcpy((char*) &server.sin_addr.s_addr, host->h_addr, host->h_length);
        if (connect(sock, (struct sockaddr*) &server, sizeof(struct sockaddr_in)) == 0)
            return sock;
        else{
            close(sock);
            return -2;
        }
    }
}

int ReadInt(const char* message){
    char buff[10];
    printf("%s", message);
    fgets(buff, sizeof(buff), stdin);
    return atoi(buff);
}

int ReadNatural(const char* message){
    int number;
    do
        number = ReadInt(message);
    while (number < 0);
    return number;
}

void ReadArray(struct Array* out){
    uint16_t i;
    char message[10];
    out->Length = ReadNatural("Dimensiunea sirului: ");
    out->Items = (int16_t*)calloc(out->Length, sizeof(int16_t));
    for (i = 0; i < out->Length; i++){
        sprintf(message, "sir[%u]=", i);
        out->Items[i] = ReadInt(message);
    }
}

int RunClient(const char* address, uint16_t port){
    int sock, result;
    struct Array array1, array2, array3;
    ReadArray(&array1);
    ReadArray(&array2);
    sock = ConnectToServer(address, port);
    if (sock < 0){
        printf("Failed to connect to server\n");
        return EXIT_FAILURE;
    }
    else{
        printf("Connected to %s:%d\n", address, port);
        WriteMessage(sock, &array1);
        WriteMessage(sock, &array2);
        result = ReadMessage(sock, 30, &array3);
        close(sock);
        if (result != 0)
            printf("Server timed out (30 seconds).\n");
        else{
            PrintArray(&array3);
            ClearArray(&array3);
        }
        ClearArray(&array1);
        ClearArray(&array2);
        printf("Disconnected from %s:%d\n", address, port);
        return EXIT_SUCCESS;
    }
}

int main(int argc, char* args[])
{
    if (argc > 2)
        return RunClient(args[1], atoi(args[2]));
    else{
        printf("Usage: ./Client <SERVER_IP> <PORT>\n");
        return EXIT_FAILURE;
    }
}

