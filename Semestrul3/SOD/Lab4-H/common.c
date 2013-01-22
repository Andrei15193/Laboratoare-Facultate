#include "common.h"

struct List* createList(int elems, int workers){
    int i;
    struct Elem* newElem;
    struct List* list = (struct List*)malloc(sizeof(struct List));
    list->first = NULL;
    list->dim = elems;
    for (i = elems - 1; i >= 0; i--){
        newElem = (struct Elem*)malloc(sizeof(struct Elem));
        newElem->next = list->first;
        newElem->value = rand() % 200 + 10;
        newElem->worker = i % workers;
        list->first = newElem;
    }
    return list;
}

void destroyList(struct List* list){
    struct Elem* deleted;
    while (list->first != NULL){
        deleted = list->first;
        list->first = list->first->next;
        free(deleted);
    }
    free(list);
}

void printList(FILE* out, struct List* list){
    struct Elem* it;
    for (it = list->first; it != NULL; it = it->next)
        fprintf(out, "Value: %d, worker: %d\n", it->value, it->worker);
}
