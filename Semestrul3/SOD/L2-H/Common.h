#ifndef COMMON_H
#define COMMON_H

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <Windows.h>

struct Message{
    DWORD clientId;
    int number;
};

struct Response{
    int minimum;
    int maximum;
};

#ifdef UNICODE
#define SERVER_MAILSLOT L"\\\\.\\mailslot\\fair1064Server"
#define CLIENT_PREFIX L"\\\\.\\mailslot\\fair1064C"

void DwordToArray(wchar_t*, DWORD);

#else
#define SERVER_MAILSLOT "\\\\.\\mailslot\\fair1064Server"
#define CLIENT_PREFIX "\\\\.\\mailslot\\fair1064C"

void DwordToArray(char*, DWORD);
#endif /* UNICODE */

#endif /* COMMON_H */
