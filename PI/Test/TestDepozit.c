#include <assert.h>
#include <string.h>
#include "TestDepozit.h"
#include "../Depozit.h"

void testDepozit(){
    struct Persoana* p = pCreaza("Andrei Fangli", 1234567890123);

    struct Banca* b = bCreaza("ING", 0.3, True);

    struct Depozit* d = dCreaza(b, p, 1000, True);
    assert(strcmp(d->numeBanca, "ING") == 0);
    assert(d->numeBanca != b->nume);
    assert(d->cnp == p->cnp);
    assert(d->capitalizare == True);
    assert(d->dobandaFerma == b->dobandaFerma);
    assert(d->suma == 1000);
    assert(d->dobanda == b->dobandaCurenta);
    dDistruge(d);
    bDistruge(b);
    pDistruge(p);
}
