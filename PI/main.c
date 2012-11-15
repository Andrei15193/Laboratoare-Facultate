#define TEST

#ifndef TEST
#include <stdio.h>
#include "Depozit.h"

int main(void)
{
    return 0;
}

#else
#include <stdio.h>
#include "Test/TestPersoana.h"
#include "Test/TestBanca.h"
#include "Test/TestDepozit.h"
#include "Test/TestOperatiiDeConsistenta.h"

int main(){
    testPersoana();
    testBanca();
    testDepozit();
    testOperatiiDeConsistenta();
    printf("Tests finished without any errors!\r\n");
    return 0;
}

#endif // TEST
