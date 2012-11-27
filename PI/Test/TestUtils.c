#include <assert.h>
#include "../Utils.h"

void testUtils(){
    char t1[] = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM- ";
    char t2[] = ".";
    char t3[] = "";

    assert(contineDoarLitereCratimeSpatii(t1) == True);
    assert(contineDoarLitereCratimeSpatii(t2) == False);
    assert(contineDoarLitereCratimeSpatii(t3) == True);
}
