#include "Common.h"

struct List* CreateList(int elementCount, int workerCount){
    int i = 1;
    struct List* newList = (struct List*)malloc(sizeof(struct List));
    struct Elem* current;
    if (elementCount > 0){
        current = newList->first = (struct Elem*)malloc(sizeof(struct Elem));
        newList->n = elementCount;
        current->value = rand() % 200 + 10;
        current->worker = 0;
        do{
            current->next = (struct Elem*)malloc(sizeof(struct Elem));
            current = current->next;
            current->value = rand() % 200 + 10;
            current->worker = i % workerCount;
            i++;
        }while (i < elementCount);
        current->next = NULL;
    }
    else{
        newList->first = NULL;
        newList->n = 0;
    }
    return newList;
}

void DestroyList(struct List* list){
    struct Elem* deleted;
    while (list->first != NULL){
        deleted = list->first;
        list->first = list->first->next;
        free(deleted);
    }
    free(list);
}

void PrintList(FILE* output, struct List* list){
    struct Elem* current;
    for (current = list->first; current != NULL; current = current->next)
        fprintf(output, "Value: %d, Worker %d\r\n", current->value, current->worker);
}
