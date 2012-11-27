#include <assert.h>
#include <string.h>
#include "TestBanca.h"
#include "../Banca.h"

void bTestCreareCuSucces(){
    struct Banca* b = bCreaza("ING", 0.3, True);
    assert(strcmp(b->nume, "ING") == 0);
    assert(b->dobandaCurenta == 0.3);
    assert(b->dobandaViitoare.valoare == 0);
    assert(b->dobandaFerma == True);
    bDistruge(b);
    b = bCreaza("Banca Transilvania", 0.3, False);
    assert(strcmp(b->nume, "Banca Transilvania") == 0);
    bDistruge(b);
    b = bCreaza("Banca-Transilvania", 0.3, False);
    assert(strcmp(b->nume, "Banca-Transilvania") == 0);
    bDistruge(b);
}

void bTestCreareFaraSucces(){
    assert(bCreaza("1", 0.3, True) == NULL);
    assert(bCreaza("ING", 0, False) == NULL);
    assert(bCreaza("ING", 1, True) == NULL);
    assert(bCreaza("1", 1, False) == NULL);
    assert(bCreaza("Banca-Transilvania1", 0.3, True) == NULL);
}

void testBanca(){
    bTestCreareCuSucces();
    bTestCreareFaraSucces();
}
