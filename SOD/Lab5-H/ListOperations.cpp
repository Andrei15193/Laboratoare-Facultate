#include "Common.h"

struct List* CreateList(int elements, int workers){
    int worker = 1, i;
    struct List* list = (struct List*)malloc(sizeof(struct List));
    struct Elem* last = list->first = (struct Elem*)malloc(sizeof(struct Elem));

    list->dim = elements;
    last->value = rand() % 190 + 10;
    last->worker = 0;
    last->next = NULL;
    for (i = 1; i < elements; i++, worker++){
        if (worker >= workers)
            worker = 0;
        last->next = (struct Elem*)malloc(sizeof(struct Elem));
        last = last->next;
        last->value = rand() % 190 + 10;
        last->worker = worker;
        last->next = NULL;
    }
    return list;
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
