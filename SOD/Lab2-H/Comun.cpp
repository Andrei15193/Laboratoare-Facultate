#include "Comun.h"

#ifdef UNICODE
void addLast(wchar_t* str, DWORD nr){
	int n = numarDeCifre(nr);
	str[n] = str[0];
	do str[--n] = (wchar_t)(nr % 10); while (nr /= 10);
}

void SetPaths(wchar_t* s2c, wchar_t* c2s, DWORD clientId){
	wcscpy(s2c, L"\\\\.\\mailslot\\fair1064");
	//addLast(s2c + wcslen(s2c), clientId);
	wcscpy(c2s, s2c);
	wcscat(c2s, L"c2s");
	wcscat(s2c, L"s2c");
}

#else
void addLast(char* str, DWORD nr){
	int n = numarDeCifre(nr);
	str[n--] = str[0];
	do{
		str[n--] = nr % 10;
		nr /= 10;
	}while (n);
}
#endif
