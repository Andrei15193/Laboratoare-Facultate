#include <stdlib.h>
#include <string.h>
#include "Depozit.h"

struct Depozit* dCreaza(const struct Banca* banca, const struct Persoana* persoana, double suma, Bool capitalizare){
    struct Depozit* depozit = (struct Depozit*)malloc(sizeof(struct Depozit));
    time_t currentTime = time(NULL);
    struct tm* timeConverted = gmtime(&currentTime);

    strncpy(depozit->numeBanca, banca->nume, 30);
    depozit->numeBanca[30] = 0;
    depozit->dobanda = banca->dobandaCurenta;
    depozit->dobandaFerma = banca->dobandaFerma;
    depozit->cnp = persoana->cnp;
    depozit->suma = suma;
    depozit->capitalizare = capitalizare;
    depozit->dataCrearii = *timeConverted;

    return depozit;
}

void dDistruge(struct Depozit* depozit){
    free(depozit);
}
