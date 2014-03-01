#include "server.h"

void* clientResponseThread(void* attr){
    int l;
    char msg[100];
    struct ClientResponseParams params = *((struct ClientResponseParams*)attr);
    sem_post(params.readySem);
    sem_wait(params.runSem);
    read(params.socket, (void*)&l, sizeof(int));
    read(params.socket, (void*)msg, ntohs(l));
    sem_wait(params.fileSem);
    fprintf(params.file, "%s%s %d\n", msg, inet_ntoa(params.clientAddr.sin_addr), ntohs(params.clientAddr.sin_port));
    sem_post(params.fileSem);
    fflush(params.file);
    sprintf(msg, "GATA %d", ++(*params.count));
    l = htons(strlen(msg));
    write(params.socket, (void*)&l, sizeof(int));
    write(params.socket, (void*)&msg, strlen(msg));
    close(params.socket);
    printf("S-a deconectat un client\n");
    sem_post(params.runSem);
}

void serverLogic(int* sd){
    pthread_t thr;
    int len, clientSocket, run = 1, count = 0;
    struct sockaddr_in client;
    struct ClientResponseParams params;
    sem_t readyS, runS, fileS;
    params.runServer = &run;
    params.count = &count;
    params.file = fopen("logs.txt", "w");
    sem_init(&runS, 0, 5);
    sem_init(&readyS, 0, 0);
    sem_init(&fileS, 0, 1);
    params.runSem = &runS;
    params.readySem = &readyS;
    params.fileSem = &fileS;
    do{
        len = sizeof(struct sockaddr_in);
        printf("Se asteapta conexiune\n");
        params.socket = accept(*sd, (struct sockaddr*)&params.clientAddr, &len);
        printf("S-a conectat un client\n");
        pthread_create(&thr, NULL, &clientResponseThread, &params);
        sem_wait(params.readySem);
    }while (run == 1);
    sem_destroy(&runS);
    fclose(params.file);
}

int makeServerSocket(int* sd, int port){
    struct sockaddr_in addr;
    *sd = socket(AF_INET, SOCK_STREAM, 0);
    if (*sd == -1)
        return 1;
    else{
        memset(&addr, sizeof(struct sockaddr_in), 0);
        addr.sin_family = AF_INET;
        addr.sin_addr.s_addr = INADDR_ANY;
        addr.sin_port = htons(port);
        if (bind(*sd, (struct sockaddr*)&addr, sizeof(struct sockaddr_in)) == -1)
            return 2;
        else
            if (listen(*sd, 5) == -1)
                return 3;
            else
                return 0;
    }
}

void runServer(){
    int sd;
    if (makeServerSocket(&sd, PORT) != 0)
        printf("Eroare la crearea serverului!\n");
    else{
		printf("server pornit\n");
        serverLogic(&sd);
        printf("server oprit\n");
    }
    close(sd);
}
