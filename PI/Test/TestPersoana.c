#include <assert.h>
#include <string.h>
#include "TestPersoana.h"
#include "../Persoana.h"

void testPersoana(){
    struct Persoana* p = pCreaza("Andrei Fangli", 1234567890123);
    assert(p->cnp == 1234567890123);
    assert(strcmp(p->nume, "Andrei Fangli") == 0);
    pDistruge(p);
}
