#include "Utile.h"

int numarDeCifre(DWORD numar){
	int s = 0;
	do s++; while ((numar /= 10) != 0);
	return s;
}
