#include <assert.h>
#include <string.h>
#include "../OperatiiDeConsistenta.h"
#include "TestOperatiiDeConsistenta.h"

void testAdaugaPersoana();

void testAdaugaBanca();

void testAdaugaDepozit();

void testModificaDepozit();

void testOperatiiDeConsistenta(){
    testAdaugaPersoana();
    testAdaugaBanca();
    testAdaugaDepozit();
    testModificaDepozit();
}

void testAdaugaPersoana(){
    struct Persoana* p = pCreaza("Andrei Fangli", 1234567890123);
    struct Persoana* citit;
    pAdauga(p, "pAdaugaTest");
    citit = pCiteste("pAdaugaTest", 1234567890123);
    assert(p->cnp == citit->cnp);
    assert(strcmp(p->nume, citit->nume) == 0);
    pDistruge(p);
    pDistruge(citit);
}

void testAdaugaBanca(){
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Banca* citit;
    bAdauga(b, "bAdaugaTest");
    citit = bCiteste("bAdaugaTest", "ING");
    assert(strcmp(b->nume, citit->nume) == 0);
    assert(b->dobandaFerma == citit->dobandaFerma);
    assert(b->dobandaCurenta == citit->dobandaCurenta);
    assert(b->dobandaViitoare.valoare == citit->dobandaViitoare.valoare);
    bDistruge(b);
    bDistruge(citit);
}

void testAdaugaDepozit(){
    struct Persoana* p = pCreaza("Andrei Fangli", 1234567890123);
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Depozit* d = dCreaza(b, p, 1000, True);
    struct Depozit* citit;
    dAdauga(d, "dAdaugaTest");
    citit = dCiteste("dAdaugaTest", p->cnp, p->nume);
    assert(strcmp(d->numeBanca, citit->numeBanca) == 0);
    assert(citit->numeBanca != d->numeBanca);
    assert(citit->cnp == d->cnp);
    assert(citit->capitalizare == True);
    assert(citit->dobandaFerma == d->dobandaFerma);
    assert(citit->suma == 1000);
    assert(citit->dobanda == d->dobanda);
    dDistruge(d);
    dDistruge(citit);
    pDistruge(p);
    bDistruge(b);
}

void testModificaDepozit(){
    struct Persoana* p = pCreaza("Andrei Fangli", 1234567890123);
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Depozit* d = dCreaza(b, p, 1000, True);
    struct Depozit* citit;
    dAdauga(d, "dAdaugaTest");
    d->suma = d->suma * 2;
    dModifica(d, "dAdaugaTest");
    citit = dCiteste("dAdaugaTest", p->cnp, p->nume);
    assert(strcmp(d->numeBanca, citit->numeBanca) == 0);
    assert(citit->numeBanca != d->numeBanca);
    assert(citit->cnp == d->cnp);
    assert(citit->capitalizare == True);
    assert(citit->dobandaFerma == d->dobandaFerma);
    assert(citit->suma == d->suma);
    assert(citit->dobanda == d->dobanda);
    dDistruge(d);
    dDistruge(citit);
    pDistruge(p);
    bDistruge(b);
}
