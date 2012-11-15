#include <assert.h>
#include <string.h>
#include "TestBanca.h"
#include "../Banca.h"

void testBanca(){
    struct Banca* b = bCreaza("ING", 0.3, True);
    assert(strcmp(b->nume, "ING") == 0);
    assert(b->dobandaCurenta == 0.3);
    assert(b->dobandaViitoare.valoare == 0);
    assert(b->dobandaFerma == True);
    bDistruge(b);
}
