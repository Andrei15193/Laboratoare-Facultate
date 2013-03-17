#ifndef COMMON_H
#define COMMON_H

#define ARRAY_INIT {0, NULL}

#define ClearStdIn() while (getchar() != '\n')

#include <signal.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include <arpa/inet.h>
#include <fcntl.h>
#include <netdb.h>
#include <netinet/in.h>
#include <poll.h>
#include <pthread.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <unistd.h>

struct Array{
    uint16_t Length;
    int16_t* Items;
};

int16_t ReadInt16(const char* message);

uint16_t ReadUInt16(const char* message);

struct Array ReadArray();

void PrintArray(struct Array* array);

void ClearArray(struct Array* array);

struct Array CastArray(struct Array* array, uint16_t (*ntohs_htons)(uint16_t));

int SendArray(int sock, struct Array* array);

int RecvArray(int sock, time_t timeOut, struct Array* array);

#endif // COMMON_H
