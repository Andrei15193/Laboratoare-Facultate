#include <stdlib.h>
#include <string.h>
#include "Banca.h"

enum Bool bValideaza(const char nume[], double dobanda){
    if (0 < dobanda && dobanda < 1 && contineDoarLitereCratimeSpatii(nume))
        return True;
    else
        return False;
}

struct Banca* bCreaza(const char nume[], double dobanda, Bool dobandaFerma){
    struct Banca* banca = NULL;
    if (bValideaza(nume, dobanda)){
        banca = (struct Banca*)malloc(sizeof(struct Banca));
        strncpy(banca->nume, nume, 30);
        banca->nume[30] = 0;
        banca->dobandaCurenta = dobanda;
        banca->dobandaViitoare.valoare = 0;
        banca->dobandaFerma = dobandaFerma;
    }
    return banca;
}

void bDistruge(struct Banca* banca){
    free(banca);
}
