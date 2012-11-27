#include <assert.h>
#include <string.h>
#include "TestPersoana.h"
#include "../Persoana.h"

void pTestCreareCuSucces(){
    struct Persoana* p = pCreaza("Andrei Fangli", "1234567890123");
    assert(strcmp(p->cnp, "1234567890123") == 0);
    assert(strcmp(p->nume, "Andrei Fangli") == 0);
    pDistruge(p);
    p = pCreaza("Mihai-Marian Ionel", "1234567890123");
    assert(strcmp(p->nume, "Mihai-Marian Ionel") == 0);
    pDistruge(p);
}

void pTestCreareFaraSucces(){
    assert(pCreaza("Mihai-Marian Ionel1", "1234567890123") == NULL);
    assert(pCreaza("Mihai-Marian Ionel", "123456789012") == NULL);
}

void testPersoana(){
    pTestCreareCuSucces();
    pTestCreareFaraSucces();
}
