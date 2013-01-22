#ifndef COMMON_H
#define COMMON_H

#ifdef UNICODE

#define CONNECT_NAMEDPIPE L"\\\\.\\pipe\\fair1064NamedPipe"
#else

#define CONNECT_NAMEDPIPE "\\\\.\\pipe\\fair1064NamedPipe"
#endif /* UNICODE */

#include <stdio.h>
#include <string.h>
#include <Windows.h>

struct IntPair{
    int first;
    int second;
};

#endif /* COMMON_H */
