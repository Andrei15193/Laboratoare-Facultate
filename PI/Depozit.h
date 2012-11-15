#ifndef DEPOZIT_H
#define DEPOZIT_H

#include <time.h>
#include "Utils.h"
#include "Banca.h"
#include "Persoana.h"

struct Depozit{
    char numeBanca[31];
    unsigned long long cnp;
    double suma;
    double dobanda;
    Bool capitalizare;
    Bool dobandaFerma;
    struct tm dataCrearii;
};

// Creaza un depozit pentru o persoana la o banca.
// Pre:  banca sa fie returnat de bCreaza(),
//       persoana sa fie returnat de pCreaza(),
//       suma sa fie pozitiva.
// Post: valoarea returnata este o referinta catre un depozit valid.
struct Depozit* dCreaza(const struct Banca* banca, const struct Persoana* persoana, double suma, Bool capitalizare);

// Distruge un depozit.
// Pre:  depozit sa fie returnat de dCreaza().
// Post: depozitul nu mai este valid.
void dDistruge(struct Depozit* depozit);

#endif // DEPOZIT_H
