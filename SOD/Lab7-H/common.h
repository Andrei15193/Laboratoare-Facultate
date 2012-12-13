#ifndef COMMON_H
#define COMMON_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <netinet/in.h>
#include <semaphore.h>

#define PORT 11120

struct ClientResponseParams{
    int socket;
    int* count;
    int* runServer;
    FILE* file;
    sem_t* fileSem;
    sem_t* runSem;
    sem_t* readySem;
    struct sockaddr_in clientAddr;
};

#endif /* COMMON_H */
