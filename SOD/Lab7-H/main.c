#include "server.h"
#include "client.h"

int main(int argc, char* args[]){
    if (argc > 1)
        runClient();
    else
	    runServer();
	return 0;
}
