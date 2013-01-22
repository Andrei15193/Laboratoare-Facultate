#include <stdlib.h>
#include <string.h>
#include "Utils.h"
#include "Persoana.h"

enum Bool pValideaza(const char nume[], const char cnp[]){
    size_t i = 0;
    if (strlen(cnp) == 13 && contineDoarLitereCratimeSpatii(nume)){
        while (i < 13 && '0' <= cnp[i] && cnp[i] <= '9')
            i++;
        if (i == 13)
            return True;
        else
            return False;
    }
    else
        return False;
}

struct Persoana* pCreaza(const char nume[], const char cnp[]){
    struct Persoana* persoana = NULL;
    if (pValideaza(nume, cnp)){
        persoana = (struct Persoana*)malloc(sizeof(struct Persoana));
        strncpy(persoana->nume, nume, 30);
        persoana->nume[30] = '\0';
        strcpy(persoana->cnp, cnp);
    }
    return persoana;
}

void pDistruge(struct Persoana* persoana){
    free(persoana);
}
