#ifndef COMUN_H
#define COMUN_H

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <Windows.h>

#include "Utile.h"

#ifdef UNICODE
void SetPaths(wchar_t* s2c, wchar_t* c2s, DWORD clientId);

#else
void addLast(char* str, DWORD nr);
#endif /* UNICODE */

#endif /* COMUN_H */
