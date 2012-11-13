#include "Common.h"

int ReadMyNumber(int n){
    int number;
    char buffer[10];
    do{
        printf("Number: \r\n");
        fgets(buffer, 10, stdin);
        number = atoi(buffer);
    }while (number <= n);
    return number;
}
