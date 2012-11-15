#include <stdlib.h>
#include <string.h>
#include "Persoana.h"

struct Persoana* pCreaza(const char nume[], unsigned long long cnp){
    struct Persoana* persoana = (struct Persoana*)malloc(sizeof(struct Persoana));
    pSeteazaNume(persoana, nume);
    persoana->cnp = cnp;
    return persoana;
}

void pDistruge(struct Persoana* persoana){
    free(persoana);
}

void pSeteazaNume(struct Persoana* persoana, const char nume[]){
    strncpy(persoana->nume, nume, 30);
    persoana->nume[30] = '\0';
}
