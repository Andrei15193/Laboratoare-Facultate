#define TEST

#ifndef TEST
#include "Ui.h"

int main(void)
{
    ruleaza();
    return 0;
}

#else
#include <stdio.h>
#include "Test/TestPersoana.h"
#include "Test/TestBanca.h"
#include "Test/TestDepozit.h"
#include "Test/TestOperatiiDeConsistenta.h"
#include "Test/TestUtils.h"
#include "Test/TestFunctionalitati.h"

int main(){
    testPersoana();
    testBanca();
    testDepozit();
    testOperatiiDeConsistenta();
    testUtils();
    testFunctionalitati();
    printf("Testarea s-a finalizat fara erori!\r\n");
    return 0;
}

#endif // TEST
