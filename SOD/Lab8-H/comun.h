#ifndef COMUN_H
#define COMUN_H

#include <fcntl.h>
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <sys/socket.h>

/* mtype = 1 */
struct msgreg_t
{
    char mtype;
    short rvport;
};

/* mtype = 2 */
struct msggetnod_t
{
    char mtype;
};

/* mtype = 3 */
struct msgnodlst_t
{
    char mtype;
    struct sockaddr_in addr[10];
};

/* mtype = 4 */
struct msggetfile_t
{
    char mtype;
    char filename[100];
    long from;
};

/* mtype = 5 */
struct msgsendfile_t
{
    char mtype;
    char data[100];
    int length;
};

struct node
{
    struct sockaddr_in nodeInfo;
    struct node* next;
};

void readMsg(int sock, char* where)
{
    int len;
    read(sock, (char*)&len, sizeof(int));
    read(sock, where, ntohs(len));
}

void writeMsg(int sock, char* what, int size)
{
    int len = htons(size);
    write(sock, (char*)&len, sizeof(int));
    write(sock, what, size);
}

#endif

