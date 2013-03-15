#ifndef COMMON_H
#define COMMON_H

#define ARRAY_INIT {0, NULL}

#include <stdio.h>
#include <signal.h>
#include <stdlib.h>
#include <string.h>

#include <arpa/inet.h>
#include <netdb.h>
#include <netinet/in.h>
#include <pthread.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/time.h>
#include <unistd.h>

struct Array
{
    uint16_t Length;
    int16_t* Items;
};

unsigned char WriteMessage(int fileDescriptor, struct Array* array);

unsigned char ReadMessage(int fileDescriptor, unsigned int timeOut, struct Array* array);

void PrintArray(struct Array* array);

void ClearArray(struct Array* array);

#endif /* COMMON_H */

