#include <assert.h>
#include <stdio.h>
#include <string.h>
#include "../OperatiiDeConsistenta.h"
#include "TestOperatiiDeConsistenta.h"

void testAdaugaCitestePersoana();

void testAdaugaCitesteBanca();

void testAdaugaCitesteDepozit();

void testModificaDepozit();

void testOperatiiDeConsistenta(){
    testAdaugaCitestePersoana();
    testAdaugaCitesteBanca();
    testAdaugaCitesteDepozit();
//    testModificaDepozit();
    remove("pAdaugaTest.test");
    remove("bAdaugaTest.test");
    remove("dAdaugaTest.test");
}

void testAdaugaCitestePersoana(){
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    struct Persoana* citit;
    pAdauga(p, "pAdaugaTest.test");
    citit = pCiteste("pAdaugaTest.test", "1234567890123");
    assert(strcmp(p->cnp, citit->cnp) == 0);
    assert(strcmp(p->nume, citit->nume) == 0);
    pDistruge(p);
    pDistruge(citit);
}

void testAdaugaCitesteBanca(){
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Banca* citit;
    bAdauga(b, "bAdaugaTest.test");
    citit = bCiteste("bAdaugaTest.test", "ING");
    assert(strcmp(b->nume, citit->nume) == 0);
    assert(b->dobandaFerma == citit->dobandaFerma);
    assert(b->dobandaCurenta == citit->dobandaCurenta);
    assert(b->dobandaViitoare.valoare == citit->dobandaViitoare.valoare);
    bDistruge(b);
    bDistruge(citit);
}

void testAdaugaCitesteDepozit(){
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Depozit* d = dCreaza(b, p, 1000, True);
    struct Depozit* citit;
    dAdauga(d, "dAdaugaTest.test");
    citit = dCiteste("dAdaugaTest.test", p->cnp, b->nume);
    assert(strcmp(d->numeBanca, citit->numeBanca) == 0);
    assert(citit->numeBanca != d->numeBanca);
    assert(strcmp(citit->cnp, d->cnp) == 0);
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
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    struct Banca* b = bCreaza("ING", 0.3, True);
    struct Depozit* d = dCreaza(b, p, 1000, True);
    struct Depozit* citit;
    dAdauga(d, "dAdaugaTest.test");
    d->suma = d->suma * 2;
    dModifica(d, "dAdaugaTest.test");
    citit = dCiteste("dAdaugaTest.test", p->cnp, p->nume);
    assert(strcmp(d->numeBanca, citit->numeBanca) == 0);
    assert(citit->numeBanca != d->numeBanca);
    assert(strcmp(citit->cnp, d->cnp) == 0);
    assert(citit->capitalizare == True);
    assert(citit->dobandaFerma == d->dobandaFerma);
    assert(citit->suma == d->suma);
    assert(citit->dobanda == d->dobanda);
    dDistruge(d);
    dDistruge(citit);
    pDistruge(p);
    bDistruge(b);
}
