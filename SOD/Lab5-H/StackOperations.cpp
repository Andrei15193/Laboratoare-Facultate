#include "Common.h"

void AddNode(struct StackElement** top, struct Elem* value){
    struct StackElement* newTop = (struct StackElement*)malloc(sizeof(struct StackElement));
    newTop->value = value;
    newTop->next = *top;
    *top = newTop;
}

void ClearNodes(struct StackElement** top, struct List* list){
    struct StackElement* deleted;
    while (*top != NULL){
        deleted = *top;
        *top = (*top)->next;
        deleted->value->next = list->first;
        list->first = deleted->value;
        list->dim++;
        free(deleted);
    }
}

void MoveNodes(struct List* list, struct StackElement** top, int count){
    struct Elem* pre = NULL;
    struct Elem* current = list->first;
    while (current != NULL && count > 0){
        if (current->value < 2){
            if (current == list->first){
                AddNode(top, current);
                list->first = current->next;
            }
            else{
                AddNode(top, current);
                pre->next = current->next;
            }
            list->dim--;
            count--;
        }
        else
            pre = current;
        current = current->next;
    }
}
