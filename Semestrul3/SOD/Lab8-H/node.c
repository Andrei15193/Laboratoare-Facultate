#include "comun.h"

int _ok;
int _socket;
int _port;

int tellServer(char* domain, int port)
{
    int rez = 0;
    int sock = socket(AF_INET, SOCK_STREAM, 0);
    struct hostent* host = gethostbyname(domain);
    struct sockaddr_in hub;
    struct msgreg_t mesaj = {1, _port};
    memset((char*) &hub, 0, sizeof(struct sockaddr_in));
    hub.sin_family = AF_INET;
    memcpy(&hub.sin_addr.s_addr, host->h_addr, host->h_length);
    hub.sin_port = htons(port);
    if (connect(sock, (struct sockaddr*) &hub, (socklen_t) sizeof(struct sockaddr_in)) == -1)
        rez = -1;
    else
    {
        writeMsg(sock, (char*) &mesaj, sizeof(struct msgreg_t));
        close(sock);
    }
    return rez;
}

void initNodeServer()
{
    struct sockaddr_in server;
    _socket = socket(AF_INET, SOCK_STREAM, 0);
    memset((char*) &server, 0, sizeof(struct sockaddr_in));
    server.sin_family = AF_INET;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_port = _port;
    bind(_socket, (struct sockaddr*) &server, sizeof(struct sockaddr_in));
    listen(_socket, 5);
}

void init(char* domain, int serverPort, int nodePort)
{
    _ok = 1;
    _socket = -1;
    _port = htons(nodePort);
    if (tellServer(domain, serverPort) == -1)
        printf("Nu s-a putut contacta hubul\n");
    else
        initNodeServer();
}

void destroy()
{
    _ok = 0;
    close(_socket);
}

void trimiteDinFisier(int sock, struct msggetfile_t* mesaj)
{
    int len;
    int file = open(mesaj->filename, O_RDONLY);
    long offset = ntohl(mesaj->from);
    struct msgsendfile_t raspuns;
    raspuns.mtype = 5;
    if (file == -1)
    {
        raspuns.length = 0;
        printf("Fisierul cerut nu exista\n");
    }
    else
    {
        lseek(file, offset, SEEK_SET);
        len = read(file, raspuns.data, 100);
        raspuns.length = htons(len);
        close(file);
    }
    writeMsg(sock, (char*) &raspuns, sizeof(struct msgsendfile_t));
    printf("Cerere: %s\nDeplasament: %ld\nTrimis: %d\n", mesaj->filename, offset, len);
}

void doLogic()
{
    int clientSock, len = 0;
    struct sockaddr_in client;
    struct msggetfile_t mesaj;
    printf("Se asteapta clienti...\n");
    clientSock = accept(_socket, (struct sockaddr*) &client, (socklen_t*) &len);
    printf("S-a conectat un client!\nIP: %s\n", inet_ntoa(client.sin_addr));
    readMsg(clientSock, (char*) &mesaj);
    trimiteDinFisier(clientSock, &mesaj);
    close(clientSock);
}

void start()
{
    if (_socket != -1)
    {
        printf("Nod pornit\n----------\nSe asteapta la portul %d\n", _port);
        while (_ok == 1)
            doLogic();
    }
    else
        printf("Eroare la initializarea nodului\n");
}

void endSignal(int sig)
{
    destroy();
    printf("----------\nNod oprit\n");
    exit(0);
}

int main(int argc, char* args[])
{
    if (signal(SIGINT, endSignal) != SIG_ERR)
        if (argc > 4)
        {
            chdir(args[3]);
            init(args[1], atoi(args[2]), atoi(args[4]));
            start();
        }
        else
            printf("Argumente insuficiente!\nApelati astfel: ./node <hub-ip> <hub-port> <data-dir> <node-port>\n");
    else
        printf("Eroare: SIGINT init\n");
    return 0;
}

