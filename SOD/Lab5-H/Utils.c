#include "Common.h"

int ReadMyNumber(int lowerBound){
    int number;
    char buff[6];
    do{
        printf("Enter a number higher than %d: ", lowerBound);
        fgets(buff, 6, stdin);
        number = atoi(buff);
    }while (number <= lowerBound);
    return number;
}
