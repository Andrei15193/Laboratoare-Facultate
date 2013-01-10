#include "comun.h"

int _run;
int _sock;
int _port;
int _lungime;
struct node* _noduri;

void init(int port)
{
    _lungime = 0;
    _run = 1;
    _port = port;
    _noduri = NULL;
    struct sockaddr_in server;
    memset((void*)&server, 0, sizeof(struct sockaddr_in));
    server.sin_family = AF_INET;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_port = htons(_port);
    _sock = socket(AF_INET, SOCK_STREAM, 0);
    bind(_sock, (struct sockaddr*)&server, sizeof(server));
    listen(_sock, 5);
}


void destroy()
{
    struct node* sters;
    while (_noduri != NULL)
    {
        sters = _noduri;
        _noduri = _noduri->next;
        free(sters);
    }
    _run = 0;
    close(_sock);
}

void inregistreazaNod(struct msgreg_t* mesaj, struct sockaddr_in* info)
{
    if (_lungime < 10)
    {
        struct node* nod = (struct node*)malloc(sizeof(struct node));
        printf("Nod\n");
        nod->nodeInfo = *info;
        nod->nodeInfo.sin_port = mesaj->rvport;
        if (_noduri == NULL)
            _noduri = nod;
        else
        {
            nod->next = _noduri;
            _noduri = nod;
        }
        _lungime++;
    }
}

void trimiteNoduri(int sock)
{
    int i = 0;
    struct node* it;
    struct msgnodlst_t raspuns;
    raspuns.mtype = 3;
    printf("Client\n");
    for (it = _noduri; it != NULL; it = it->next)
        raspuns.addr[i++] = it->nodeInfo;
    writeMsg(sock, (char*)&raspuns, sizeof(char) + _lungime * sizeof(struct sockaddr_in));
}

void citesteMesaj(int sock, struct sockaddr_in* info)
{
    char buff[100];
    readMsg(sock, buff);
    if (buff[0] == 1)
        inregistreazaNod((struct msgreg_t*)buff, info);
    else
        if (buff[0] == 2)
            trimiteNoduri(sock);
}

void doLogic()
{
    int clientSock, len = 0;
    struct sockaddr_in client;
    printf("Se asteapta clienti...\n");
    clientSock = accept(_sock, (struct sockaddr*) &client, (socklen_t*) &len);
    printf("S-a conectat un client!\nIP: %s:%d\nTip client: ", inet_ntoa(client.sin_addr), client.sin_port);
    citesteMesaj(clientSock, (struct sockaddr_in*) &client);
}

void start()
{
    if (_sock != -1)
    {
        printf("Server pornit\n----------\n");
        while (_run == 1)
            doLogic();
    }
    else
        printf("Eroare la initializarea serverului\n");
}

void endSignal(int sig)
{
    destroy();
    printf("----------\nServer oprit\n");
    exit(0);
}

int main(int argc, char* args[])
{
    if (signal(SIGINT, endSignal) != SIG_ERR)
        if (argc > 1)
        {
            init(atoi(args[1]));
            start();
        }
        else
            printf("Portul nu a fost specificat\n");
    else
        printf("Eroare: SIGINT init\n");
    return 0;
}

