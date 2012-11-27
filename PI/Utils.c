#include <string.h>
#include "Utils.h"

#define conditie(sir, i) (('a' <= sir[i] && sir[i] <= 'z') || ('A' <= sir[i] && sir[i] <= 'Z') || sir[i] == ' ' || sir[i] == '-')

unsigned int numarDeCifre(unsigned long long n){
    unsigned int s = 0;
    do s++; while ((n /= 10) != 0);
    return s;
}

Bool contineDoarLitereCratimeSpatii(const char sir[]){
    size_t i = 0, size = strlen(sir);
    while (i < size && conditie(sir, i))
        i++;
    if (i == size)
        return True;
    else
        return False;
}
