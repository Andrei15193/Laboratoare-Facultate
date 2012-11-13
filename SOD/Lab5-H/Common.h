#ifndef COMMON_H
#define COMMON_H

#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>

// Utility enumeration for Threads to have more readable code.
enum JobStatus{ NotDone, Done, Error };

// Represents the worker thread function arguments.
struct WorkerThreadArgs{
    int worker;
    struct List* list;
    HANDLE listMutex;
    HANDLE listAccessSemaphore;
    HANDLE valueEvent;
};

// Represents the main thread function arguments.
struct MainThreadArgs{
    struct List* list;
    HANDLE listMutex;
    HANDLE valueEvent;
};

// Represents the list.
struct List{
    int dim;
    struct Elem* first;
};

// Represents an element in the linked list.
struct Elem{
    int value;
    int worker;
    struct Elem* next;
};

// Utility stack, used to temporarely store the list of Elements.
struct StackElement{
    struct Elem* value;
    struct StackElement* next;
};

// Reads a number greater than n.
int ReadMyNumber(int n);

// Adds a node on top of the stack.
void AddNode(struct StackElement** top, struct Elem* value);

// Clears the nodes from the specified stack into the specified list.
// Pre: The list must be returned by CreateList().
void ClearNodes(struct StackElement** top, struct List* list);

// Moves count elements that have the value lower than 2 on top of the stack.
// Pre: the list must be returned by CreateList()
void MoveNodes(struct List* list, struct StackElement** top, int count);

// Pre: elements > 0 and workers > 0.
// Post: a pointer to a single linked list.
struct List* CreateList(int elements, int workers);

// Pre: list is a list created with CreateList.
// Post: the list is no longer valid (using the pointer after this call can result to undefined behaviour!).
void DestroyList(struct List* list);

// Worker function, call with pointer to valid WorkerThreadArgs variable.
// Post: Returns EXIT_SUCCESS on job succesful job completion, EXIT_FAILURE otherwise
DWORD WorkerThread(LPVOID workerThreadArgs);

// Main function, call with pointer to valid MainThreadArgs variable.
// Post: Returns EXIT_SUCCESS on job succesful job completion, EXIT_FAILURE otherwise
DWORD MainThread(LPVOID mainThreadArgs);

#endif /* COMMON_H */
