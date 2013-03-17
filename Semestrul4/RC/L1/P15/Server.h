#ifndef SERVER_H
#define SERVER_H

#define TIMEOUT_MS 5000
#define SERVERSTATE_INIT {Closed, PTHREAD_RWLOCK_INITIALIZER}
#define LOGFILE "serverLogs.txt"

#include "Common.h"

enum ServerRunState{
    Open,
    Closing,
    Closed
};

enum ServerConsoleCommand{
    InvalidCommand,
    Help,
    Close
};

struct ServerState{
    enum ServerRunState runState;
    pthread_rwlock_t rwStateVariable;
};

#endif // SERVER_H
