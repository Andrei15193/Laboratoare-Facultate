#include <assert.h>
#include <string.h>
#include "TestDepozit.h"
#include "../Depozit.h"

void dTestCreareCuSucces(){
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Depozit* d = dCreaza(b, p, 1000, True);
    assert(strcmp(d->numeBanca, "ING") == 0);
    assert(d->numeBanca != b->nume);
    assert(strcmp(d->cnp, p->cnp) == 0);
    assert(d->capitalizare == True);
    assert(d->dobandaFerma == b->dobandaFerma);
    assert(d->suma == 1000);
    assert(d->dobanda == b->dobandaCurenta);
    dDistruge(d);
    d = dCreaza(b, p, 1, True);
    assert(d->suma == 1);
    dDistruge(d);
    bDistruge(b);
    pDistruge(p);
}

void dTestCreareFaraSucces(){
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    struct Banca* b = bCreaza("ING", 0.3, True);
    assert(dCreaza(b, p, 0, True) == NULL);
    assert(dCreaza(b, p, -1, True) == NULL);
    bDistruge(b);
    pDistruge(p);
}

void testDepozit(){
    dTestCreareCuSucces();
    dTestCreareFaraSucces();
}
