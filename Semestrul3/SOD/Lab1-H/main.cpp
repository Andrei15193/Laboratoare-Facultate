// Se cere un server si unul sau mai multi clienti.
// Clientii ii transmit serverului numere intregi si pozitive.
// Trimiterea unui numar negativ inseamna incheierea trimiterii
// de numere de catre clientul respectiv. Serverul determina,
// pentru fiecare client in parte, cel mai mare si cel mai mic
// numar primit si trimite aceste rezultate clientului.

#include "Server.h"
#include "Client.h"

int main(int argc, char* args[]){
    if (argc > 1)
        if (strcmp(args[1], "client") == 0)
            RunClient();
        else if (strcmp(args[1], "server") == 0)
            RunServer();
        else if (strcmp(args[1], "close") == 0)
            CloseServer();
        else
            printf("Invalid arguments\r\n");
    else
        printf("Error! use with arguments client, server or close\r\n");
    return 0;
}
