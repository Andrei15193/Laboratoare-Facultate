#ifndef BANCA_H
#define BANCA_H

#include <time.h>
#include "Utils.h"

// Reprezinta o banca.
struct Banca{
    struct _DobandaViitoare{
        double valoare;
        struct tm data;
    }dobandaViitoare;
    char nume[31];
    double dobandaCurenta;
    Bool dobandaFerma;
};

// Creaza o noua banca
// Pre:  nume sa contina doar litere, cratiem sau spatii,
//       dobanda sa fie cuprins in intervalul (0, 1).
// Post: valoarea returnata refera o banca valida.
struct Banca* bCreaza(const char nume[], double dobanda, Bool dobandaFerma);

// Distruge o banca.
// Pre:  banca trebuie sa fie retrunat de bCreaza().
// Post: dupa apel banca nu mai este valida.
void bDistruge(struct Banca* banca);

#endif // BANCA_H
