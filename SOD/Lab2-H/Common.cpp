#include "Common.h"

int cif(DWORD number){
    int s = 0;
    do s++; while(number /= 10);
    return s;
}

//#ifdef UNICODE
void DwordToArray(wchar_t* str, DWORD number){
    int n = cif(number);
    str[n] = str[0];
    do str[--n] = number % 10 + L'0'; while (number /= 10);
}
//#else
//
//void DwordToArray(char* str, DWORD number){
//    int n = cif(number);
//    str[n] = str[0];
//    do str[--n] = number % 10 + '0'; while (number /= 10);
//}
//#endif /* UNICODE */
