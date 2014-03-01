#ifndef COMUN_H
#define COMUN_H

#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <semaphore.h>
#include <pthread.h>

struct CommonParams{
    int count;
    sem_t signalSem;
    pthread_mutex_t listMutex;
    struct List* list;
};

struct WorkerParams{
    int worker;
    sem_t accessSem;
    struct CommonParams* common;
};

struct Elem{
    int worker;
    int value;
    struct Elem* next;
};

struct List{
    int dim;
    struct Elem* first;
};

struct List* createList(int elems, int workers);

void destroyList(struct List* list);

void printList(FILE* out, struct List* list);

void* workerThread(void*);

void* mainThread(void*);

#endif // COMUN_H
