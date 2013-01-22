#include <stdlib.h>
#include <string.h>
#include "Depozit.h"

struct Depozit* dCreaza(const struct Banca* banca, const struct Persoana* persoana, double suma, Bool capitalizare){
    struct Depozit* depozit = NULL;
    time_t currentTime;
    struct tm* timeConverted;

    if (suma > 0){
        currentTime = time(NULL);
        timeConverted = gmtime(&currentTime);
        depozit = (struct Depozit*)malloc(sizeof(struct Depozit));
        strncpy(depozit->numeBanca, banca->nume, 30);
        depozit->numeBanca[30] = 0;
        depozit->dobanda = banca->dobandaCurenta;
        depozit->dobandaFerma = banca->dobandaFerma;
        strcpy(depozit->cnp, persoana->cnp);
        depozit->suma = suma;
        depozit->capitalizare = capitalizare;
        depozit->dataCapitalizarii = *timeConverted;
    }

    return depozit;
}

void dDistruge(struct Depozit* depozit){
    free(depozit);
}
