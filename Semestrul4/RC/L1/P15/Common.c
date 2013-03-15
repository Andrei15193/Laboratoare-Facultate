#define SET_TIMEOUT(var, sec) var.tv_sec = sec; var.tv_usec = 0

#include "Common.h"

unsigned char ReadMessage(int fileDescriptor, unsigned int timeOut, struct Array* array){
    unsigned char retur = 0;
    int i;
    uint16_t length = 0, citit = 0, j;
    int16_t* items;
    fd_set fdSet;
    struct timeval timeout = {timeOut, 0};
    
    FD_ZERO(&fdSet);
    FD_SET(fileDescriptor, &fdSet);
    
    if (select(fileDescriptor + 1, &fdSet, NULL, NULL, &timeout) > 0){
        SET_TIMEOUT(timeout, timeOut);
        recv(fileDescriptor, (void*) &length, sizeof(uint16_t), 0);
        array->Length = ntohs(length);
        items = (int16_t*)calloc(array->Length, sizeof(int16_t));
        do{
            SET_TIMEOUT(timeout, timeOut);
            if (select(fileDescriptor + 1, &fdSet, NULL, NULL, &timeout) > 0){
                citit = recv(fileDescriptor, ((void*)items) + citit, array->Length - citit, 0);
                if (citit < 0)
                    citit = array->Length = -1;
            }
            else
                citit = array->Length = -1;
        }while (citit < array->Length);
        if (array->Length == -1){
            free(items);
            retur = 2;
        }
        else{
            for (j = 0; j < array->Length; j++)
                items[j] = ntohs(items[j]);
            array->Items = items;
        }
    }
    else
        retur = 1;
    for (i = 0; i < fileDescriptor + 1; i++)
        if (FD_ISSET(i, &fdSet))
            close(i);
    return retur;
}

unsigned char WriteMessage(int fileDescriptor, struct Array* array){
    uint16_t length, i;
    int16_t* items;
    length = htons(array->Length);
    items = (int16_t*)calloc(array->Length, sizeof(int16_t));
    for (i = 0; i < array->Length; i++)
        items[i] = htons(array->Items[i]);
    send(fileDescriptor, (void*) &length, sizeof(uint16_t), 0);
    send(fileDescriptor, (void*) items, array->Length * sizeof(int16_t), 0);
    free(items);
    return 0;
}

void PrintArray(struct Array* array){
    uint16_t i;
    for (i = 0; i < array->Length - 1; i++)
        printf("%i, ", array->Items[i]);
    printf("%d\n", array->Items[array->Length]);
}

void ClearArray(struct Array* array){
    array->Length = 0;
    free(array->Items);
}

