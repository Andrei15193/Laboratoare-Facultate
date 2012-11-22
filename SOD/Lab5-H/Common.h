#ifndef COMMON_H
#define COMMON_H

#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>

struct Elem{
    int worker;
    int value;
    struct Elem* next;
};

struct List{
    struct Elem* first;
    int n;
};

struct CommonWorkerParameters{
    struct List* list;
    int initialElementCount;
    int count;
    HANDLE listMutex;
    HANDLE signalSemaphore;
    HANDLE accessSempahore;
};

struct WorkerParameters{
    int worker;
    struct CommonWorkerParameters* common;
};

struct MainThreadParameters{
    struct List* list;
    HANDLE listMutex;
    HANDLE signalSemaphore;
};

enum RunState{
    Running,
    Done,
    Error
};

enum Bool{
    False,
    True
};

struct List* CreateList(int elementCount, int workerCount);

void DestroyList(struct List* list);

void PrintList(FILE* output, struct List* list);

DWORD WorkerThread(LPVOID parameters);

DWORD MainThread(LPVOID parameters);

int ReadMyNumber(int lowerBound);

#endif /* COMMON_H */
