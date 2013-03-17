#include "Common.h"

enum ReadStatus{
    NotDone,
    Done,
    TimedOut,
    ClientClosedConnection
};

int16_t ReadInt(const char *message){
    int16_t number;
    printf("%s", message);
    while (scanf("%hi", &number) != 1){
        printf("%s", message);
        ClearStdIn();
    }
    return number;
}

uint16_t ReadUInt16(const char* message){
    uint16_t number;
    printf("%s", message);
    while (scanf("%hu", &number) != 1){
        ClearStdIn();
        printf("%s", message);
    }
    return number;
}

struct Array ReadArray(){
    uint16_t i;
    char message[10];
    struct Array array = ARRAY_INIT;
    array.Length = ReadUInt16("Dimensiune sir: ");
    if (array.Length > 0){
        array.Items = (int16_t*)calloc(array.Length, sizeof(int16_t));
        for (i = 0; i < array.Length; i++){
            sprintf(message, "sir[%hu]=", i);
            array.Items[i] = ReadInt(message);
        }
    }
    return array;
}

void PrintArray(struct Array* array){
    uint16_t i;
    printf("Dimensiune sir: %u\n", array->Length);
    if (array->Length > 0){
        for (i = 0; i < array->Length - 1; i++)
            printf("%hi, ", array->Items[i]);
        printf("%hi\n", array->Items[array->Length - 1]);
    }
}

void ClearArray(struct Array* array){
    if (array->Length > 0){
        free(array->Items);
        array->Items = NULL;
        array->Length = 0;
    }
}

struct Array CastArray(struct Array* array, uint16_t (*cast)(uint16_t)){
    uint16_t i;
    struct Array result;
    result.Length = cast(array->Length);
    if (array->Length > 0){
        result.Items = (int16_t*)calloc(result.Length, sizeof(int16_t));
        for (i = 0; i < result.Length; i++)
            result.Items[i] = cast(array->Items[i]);
    }
    else
        result.Items = NULL;
    return result;
}

int SendArray(int sock, struct Array* array){
    struct Array converted = CastArray(array, htons);
    if (send(sock, (void*) &converted.Length, sizeof(converted.Length), 0) != -1 &&
            array->Length > 0 &&
            send(sock, (void*) converted.Items, array->Length * sizeof(array->Items[0]), 0) != -1)
        return 0;
    else
        return -1;
}

int RecvUInt16(int sock, int timeOut_ms, uint16_t* value){
    struct pollfd pollFd = {sock, POLLIN ,0};
    if (poll(&pollFd, 1, timeOut_ms) == 1){
        recv(sock, (void*) value, sizeof(*value), 0);
        *value = ntohs(*value);
        return 0;
    }
    else
        return -1;
}

int RecvArrayAux(int sock, time_t timeOut_ms, struct Array* array){
    ssize_t recvResult;
    uint16_t hasBeenRead = 0, i, lengthInBytes = sizeof(int16_t) * array->Length;
    enum ReadStatus readStatus = NotDone;
    struct pollfd pollFd = {sock, POLLIN, 0};
    do{
        if (poll(&pollFd, 1, timeOut_ms) == 1){
            recvResult = recv(sock, ((char*)array->Items) + hasBeenRead, sizeof(int16_t), 0);
            if (recvResult == -1)
                readStatus = ClientClosedConnection;
            else{
                hasBeenRead += recvResult;
                if (hasBeenRead == lengthInBytes)
                    readStatus = Done;
            }
        }
        else
            readStatus = TimedOut;
    }while (readStatus == NotDone);
    switch (readStatus){
    case Done:
        for (i = 0; i < array->Length; i++)
            array->Items[i] = ntohs(array->Items[i]);
        return 0;
    case TimedOut:
        return -1;
    case ClientClosedConnection:
        return -2;
    }
}

int RecvArray(int sock, time_t timeOut, struct Array* array){
    struct Array result = ARRAY_INIT;
    if (RecvUInt16(sock, timeOut, &result.Length) == -1)
        return -1;
    else
        if (result.Length > 0){
            result.Items = (int16_t*)calloc(result.Length, sizeof(int16_t));
            switch (RecvArrayAux(sock, timeOut, &result)){
            case 0:
                *array = result;
                return 0;
            case -1:
                return -2;
            case -2:
                return -3;
            }
        }
        else
            return 0;
}
