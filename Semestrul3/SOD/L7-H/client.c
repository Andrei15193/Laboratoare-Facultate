#include "client.h"

void readMessage(char* buff){
    printf("Introduceti un mesaj: ");
    fgets(buff, 100, stdin);
}

void runClient(){
    int sd, l;
    char buff[100];
    struct sockaddr_in server;
    struct hostent* hp;
    sd = socket(AF_INET, SOCK_STREAM, 0);
    hp = gethostbyname("localhost");
    memset(&server, 0, sizeof (server));
    server.sin_family = AF_INET;
    server.sin_port = htons(PORT);
    memcpy((void*)&server.sin_addr.s_addr, hp->h_addr, hp->h_length);

    readMessage(buff);
    if (connect(sd, (struct sockaddr *) &server, sizeof(server)) != -1){
        printf("Conenctat\nSe trimite mesajul");
        l = htons(strlen(buff));
        write(sd, &l, sizeof(int));
        write(sd, buff, strlen(buff));
        read(sd, &l, sizeof(int));
        read(sd, buff, ntohs(l));
        buff[ntohs(l)] = 0;
        printf("Mesaj primit:\n%s\n", buff);
    }
    else
        printf("Conexiune nereusita\n");
    close(sd);
}
