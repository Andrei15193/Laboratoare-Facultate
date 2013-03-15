#ifndef SERVER_H
#define SERVER_H

#include "Common.h"

enum StartServerResult{
    Success,
    FailedToCreateSocket,
    FailedToBindPort,
    FailedToListen,
    FailedToStartConsoleThread
};

enum StartServerResult StartServer(uint16_t port);

#endif /* SERVER_H */

