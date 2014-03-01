#include "comun.h"

int _transferat = 1;
int _numarNoduri;
char filename[100];
pthread_t threads[10];
struct msgnodlst_t _noduri;
/*{
    char mtype;
    struct sockaddr_in addr[10];
};

/* mtype = 4
struct msggetfile_t
{
    char mtype;
    char filename[100];
    long from;
};

/* mtype = 5 
struct msgsendfile_t
{
    char mtype;
    char data[100];
    int length;
};*/

void citesteLista(int sock)
{
    int len;
    read(sock, (char*) &len, sizeof(int));
    len = ntohs(len);
    _numarNoduri = (len - sizeof(char))/sizeof(struct sockaddr_in);
    read(sock, (char*) &_noduri, len);
}

int obtineLista(char* domain, int port)
{
    int rez = 0;
    int sock = socket(AF_INET, SOCK_STREAM, 0);
    struct hostent* host = gethostbyname(domain);
    struct sockaddr_in hub;
    struct msggetnod_t mesaj = {2};
    memset((char*) &hub, 0, sizeof(struct sockaddr_in));
    hub.sin_family = AF_INET;
    memcpy(&hub.sin_addr.s_addr, host->h_addr, host->h_length);
    hub.sin_port = htons(port);
    if (connect(sock, (struct sockaddr*) &hub, (socklen_t) sizeof(struct sockaddr_in)) == -1)
        rez = -1;
    else
    {
        writeMsg(sock, (char*) &mesaj, sizeof(struct msggetnod_t));
        citesteLista(sock);
        close(sock);
    }
    return rez;
}

void writeToFile(struct msgsendfile_t* mesaj, long offset)
{
    int len = ntohs(mesaj->length);
    int file;
    if (len > 0)
    {
        file = open(filename, O_CREAT | O_WRONLY, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
        if (file == -1)
            printf("Eroare la deschiderea fisierului\n");
        else
        {
            lseek(file, offset, SEEK_SET);
            printf("S-au scris %d din %d octeti\n", write(file, mesaj->data, len), len);
            close(file);
        }
    }
}

void* copyFile(void* param)
{
    int i = 0;
    int sock;
    int index = *((int*)param);
    long offset;
    struct msggetfile_t cerere;
    struct msgsendfile_t raspuns;
    struct sockaddr_in nod;
    struct hostent* host = gethostbyname("127.0.0.1");
    cerere.mtype = 4;
    strcpy(cerere.filename, filename);
    do{
        sock = socket(AF_INET, SOCK_STREAM, 0);
        memset((char*) &nod, 0, sizeof(struct sockaddr_in));
        nod.sin_family = AF_INET;
//        memcpy((char*) &nod.sin_addr, (char*) &_noduri.addr[index].sin_addr, sizeof(_noduri.addr[index].sin_addr));
        memcpy((char*) &nod.sin_addr.s_addr, host->h_addr, host->h_length);
        nod.sin_port = _noduri.addr[index].sin_port;
        if (connect(sock, (struct sockaddr*) &nod, (socklen_t) sizeof(struct sockaddr_in)) == 0)
        {
            printf("Conexiune reusita la nod\nIP: %s:%d\n", inet_ntoa(nod.sin_addr), nod.sin_port);
            offset = (index + _numarNoduri * i++) * 100;
            cerere.from = htonl(offset);
            writeMsg(sock, (char*)&cerere, sizeof(struct msggetfile_t));
            readMsg(sock, (char*)&raspuns);
            close(sock);
            writeToFile(&raspuns, offset);
        }
        else
        {
            printf("Nu s-a putut contacta nodul\nIP: %s:%d\n", inet_ntoa(nod.sin_addr), nod.sin_port);
            _transferat = 0;
            raspuns.length = 0;
        }
    } while (raspuns.length != 0);
    return NULL;
}

int main(int argc, char* args[])
{
    int i;
    int index[10];
    if (argc > 3)
        if (obtineLista(args[1], atoi(args[2])) == 0)
        {
            strncpy(filename, args[3], 99);
            filename[99] = 0;
            printf("Se transfera fisierul %s\nNumar noduri: %d\n", filename, _numarNoduri);
            for (i = 0; i < _numarNoduri; i++)
            {
                index[i] = i;
                pthread_create(threads + i, NULL, &copyFile, index + i);
            }
            for (i = 0; i < _numarNoduri; i++)
                pthread_join(threads[i], NULL);
            if (_transferat == 1)
                printf("S-a transferat fisierul %s\n", filename);
        }
        else
            printf("Nu s-a putut contacta hubul\n");
    else
        printf("Argumente insuficiente\nApelati astfel: ./client <hub-ip> <hub-port> <file-name>\n");
}
