#ifndef COMMON_H
#define COMMON_H

#include <stdio.h>
#include <stdlib.h>
#include <Windows.h>

#ifdef UNICODE

#define SEMAPHORE_READ  L"Local\\SemaphoreRead"
#define SEMAPHORE_WRITE L"Local\\SempahoreWrite"
#define SHM_NAME        L"Local\\fair1064SHM"
#else

#define SEMAPHORE_READ  "Local\\SemaphoreRead"
#define SEMAPHORE_WRITE "Local\\SempahoreWrite"
#define SHM_NAME        "Local\\fair1064SHM"

#endif /* UNICODE */

struct Message{
    int number;
    int maximum;
    int minimum;
};

#endif /* COMMON_H */
