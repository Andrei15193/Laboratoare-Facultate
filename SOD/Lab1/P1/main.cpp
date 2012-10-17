// Sa se scrie un server si un client unde clientul citeste un numar natural nenul mai mic decat 100 care il trimite serverului,
// serverul il dubleaza dupa care il trimite clientului. Clientul la randul lui il dubleaza si il trimite serverului. Acest
// ciclu se repeta pana cand numarul ajunge mai mare decat 100.

#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>

int server(){
    int number, returnCode = EXIT_SUCCESS;
    BOOL runServer = TRUE;
    DWORD bytes;
    HANDLE outNamedPipe = CreateNamedPipe("\\\\.\\pipe\\serverToClient", PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE | PIPE_WAIT, 10, sizeof(int), sizeof(int), 1000, NULL),
            inNamedPipe = CreateNamedPipe("\\\\.\\pipe\\clientToServer", PIPE_ACCESS_INBOUND, PIPE_READMODE_BYTE | PIPE_WAIT, 10, sizeof(int), sizeof(int), 1000, NULL);

    if (inNamedPipe == INVALID_HANDLE_VALUE || outNamedPipe == INVALID_HANDLE_VALUE){
        printf("Error while creating named pipes\r\n => Server closes\r\n");
        returnCode = EXIT_FAILURE;
    }
    else
        do{
            printf("Awaiting connection\r\n");
            if ((ConnectNamedPipe(inNamedPipe, NULL) == TRUE || GetLastError() == ERROR_PIPE_CONNECTED) &&
                (ConnectNamedPipe(outNamedPipe, NULL) == TRUE || GetLastError() == ERROR_PIPE_CONNECTED))
                while (ReadFile(inNamedPipe, &number, sizeof(int), &bytes, NULL) == TRUE && number <= 100){
                    printf("Number received is %d\r\n", number);
                    number *= 2;
                    WriteFile(outNamedPipe, &number, sizeof(int), &bytes, NULL);
                    FlushFileBuffers(outNamedPipe);
                }
            else
                printf("Error while connecting to client\r\n");
            DisconnectNamedPipe(inNamedPipe);
            DisconnectNamedPipe(outNamedPipe);
            printf("Client disconnected\r\n\r\n");
        }while (runServer == TRUE);

    CloseHandle(inNamedPipe);
    CloseHandle(outNamedPipe);
    return returnCode;
}

int readMyNumber(){
    int number;
    char buffer[6];
    do{
        printf("Number: ");
        fgets(buffer, 6, stdin);
        number = atoi(buffer);
    }while (number <= 0 || 100 <= number);
    return number;
}

int client(){
    int number = readMyNumber(), returnCode = EXIT_SUCCESS;
    DWORD bytes;
    HANDLE outNamedPipe = CreateFile("\\\\.\\pipe\\clientToServer", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL),
            inNamedPipe = CreateFile("\\\\.\\pipe\\serverToClient", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_ATTRIBUTE_READONLY, NULL);

    if (inNamedPipe == INVALID_HANDLE_VALUE || outNamedPipe == INVALID_HANDLE_VALUE){
        printf("Failed to connect to server\r\n");
        returnCode = EXIT_FAILURE;
    }
    else{
        do{
            WriteFile(outNamedPipe, &number, sizeof(int), &bytes, NULL);
            if (ReadFile(inNamedPipe, &number, sizeof(int), &bytes, NULL) == FALSE){
                printf("Connection interrupted\r\nClient closing\r\n");
                returnCode = EXIT_FAILURE;
            }
            else{
                printf("Number is %d\r\n", number);
                number *= 2;
            }
        }while (number <= 100 && returnCode == EXIT_SUCCESS);
        if (100 < number / 2)
            number /= 2;
        printf("Final number is %d\r\n", number);
    }

    CloseHandle(inNamedPipe);
    CloseHandle(outNamedPipe);
    return returnCode;
}

int main(int argc, char* args[]){
    if (argc > 1 && strcmp(args[1], "server") == 0)
        server();
    else
        client();
}
//
//
////#include <stdlib.h>
////#include <stdio.h>
////#include <Windows.h>
////
////// Creates a client.
////// Returns:
//////      EXIT_SUCCESS upon succesful creation of client (connected to named pipe and managed to communicate without problems).
//////      EXIT_FAILUER otherwise.
////int client(){
////    int number = 10;
////    DWORD bytesWrote, bytesRed;
////    HANDLE outNamedPipe = CreateFile("\\\\.\\pipe\\clientToServerNamedPipe", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL),
////            inNamedPipe = CreateFile("\\\\.\\pipe\\serverToClientNamedPipe", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);
////    printf("Client\r\n--------------------\r\n");
////    if (outNamedPipe == INVALID_HANDLE_VALUE){
////        printf("Failed to connect to named pipe: clientToServerNamedPipe\r\n");
////        return EXIT_FAILURE;
////    }
////    if (inNamedPipe == INVALID_HANDLE_VALUE){
////        printf("Failed to connect to named pipe: serverToClientNamedPipe\r\n");
////        return EXIT_FAILURE;
////    }
////    else{
////        if (WriteFile(outNamedPipe, &number, sizeof(int), &bytesWrote, NULL) == FALSE)
////            printf("Failed to write to named pipe: clientToServerNamedPipe\r\n");
////        else{
////            printf("Sent message to named pipe: clientToServerNamedPipe\r\n");
////            if (ReadFile(inNamedPipe, &number, sizeof(int), &bytesRed, NULL) == TRUE && bytesRed == sizeof(int))
////                printf("%d\r\n", number);
////            else
////                printf("Could not read message\r\n");
////        }
////        printf("Client closing\r\n--------------------");
////        /*DisconnectNamedPipe(inNamedPipe);
////        DisconnectNamedPipe(outNamedPipe);*/
////        CloseHandle(inNamedPipe);
////        CloseHandle(outNamedPipe);
////        return EXIT_SUCCESS;
////    }
////}
////
////// Remarks:
//////      Usage: createServerResources(&varName); or createServerResources(pointerToDynamicVarName);
////// Returns:
//////      FALSE if any of the server resources could not be created.
//////      TRUE if all server resources have been succesfully created.
//////
//////      inNamedPipe and outNamedPipe will hold data about two valid handles that can be used to read and write
//////      information from clients.
////BOOL createServerResources(HANDLE* inNamedPipe, HANDLE* outNamedPipe){
////    *inNamedPipe = CreateNamedPipe("\\\\.\\PIPE\\clientToServerNamedPipe", PIPE_ACCESS_INBOUND, PIPE_READMODE_BYTE | PIPE_WAIT, PIPE_UNLIMITED_INSTANCES, sizeof(int), sizeof(int), 10000, NULL);
////    *outNamedPipe = CreateNamedPipe("\\\\.\\PIPE\\serverToClientNamedPipe", PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE | PIPE_WAIT, PIPE_UNLIMITED_INSTANCES, sizeof(int), sizeof(int), 20000, NULL);
////    if (*inNamedPipe == INVALID_HANDLE_VALUE || *outNamedPipe == INVALID_HANDLE_VALUE)
////        return FALSE;
////    else
////        return TRUE;
////}
////
////// Creates a server.
////// Returns:
//////      EXIT_FAILUER if anything goes bad with the server (e.g.: could not create resources).
//////      EXIT_SUCCESS if all goes well and the server is closed by special request (run the exe file with no parameters).
////int server(){
////    int number;
////    BOOL runServer = TRUE;
////    DWORD bytesRed, bytesWrote;
////    HANDLE inNamedPipe, outNamedPipe;
////    printf("Server\r\n--------------------\r\n");
////    if (createServerResources(&inNamedPipe, &outNamedPipe) == FALSE){
////        printf("Failed to create server, closing\r\n--------------------\r\n");
////        return EXIT_FAILURE;
////    }
////    else{
////        do{
////            printf("Waiting for client to connect\r\n");
////            ConnectNamedPipe(inNamedPipe, NULL);
////            ConnectNamedPipe(outNamedPipe, NULL);
////            if (ReadFile(inNamedPipe, &number, sizeof(int), &bytesRed, NULL) == TRUE && bytesRed == sizeof(int))
////                if (number <= 0)
////                    runServer = FALSE;
////                else{
////                    printf("%d\r\n", number);
////                    number *= 2;
////                    if (WriteFile(outNamedPipe, &number, sizeof(int), &bytesWrote, NULL) == FALSE && bytesWrote != sizeof(int)){
////                        printf("Error while writing message\r\nServer closing\r\n");
////                        runServer = FALSE;
////                        DisconnectNamedPipe(inNamedPipe);
////                        DisconnectNamedPipe(outNamedPipe);
////                    }
////                    else{
////                        DisconnectNamedPipe(inNamedPipe);
////                        DisconnectNamedPipe(outNamedPipe);
////                        printf("Wrote message\r\n");
////                    }
////                }
////        }while (runServer == TRUE);
////        CloseHandle(inNamedPipe);
////        CloseHandle(outNamedPipe);
////        printf("Server is closing\r\n--------------------");
////        return EXIT_SUCCESS;
////    }
////}
////
////int closeServer(){
////    printf("Close server\r\n");
////    return EXIT_SUCCESS;
////}
////
////int main(int c, char* args[]){
////    int option;
////    int (*options[])(void) = {client, server, closeServer};
////    if (c > 1 && 0 <= (option = atoi(args[1]) - 1) && option < sizeof(options) / sizeof(int(*)(void)))
////        return options[option]();
////    else{
////        printf("! INVALID ARGUMENTS !\r\nUsage: executableName N\r\n where N is\r\n  1 - to open client\r\n  2 - to open server\r\n  3 - to close server\r\n");
////        return EXIT_FAILURE;
////    }
////}
////
////
////
/////* CLIENT
////#include <stdio.h>
////#include <windows.h>
////
////#define MAXS 10000
////#define MAXL 1000
////#define PLUS (sizeof(int) * 3)
////
////typedef struct{
////    int lung;
////    int i;
////    int max;
////    int min;
////    int s[MAXS];
////}Mesaj;
////
////void Read(HANDLE f, char *t, int n){
////    char *p;
////    int c;
////    DWORD i;
////    for (p=t, c=n; ; ){
////        ReadFile(f, p, c, &i, NULL);
////        if (i == c) return;
////        c -= i;
////        p += i;
////    }
////}
////
////void Write(HANDLE f, char *t, int n){
////    char *p;
////    int c;
////    DWORD i;
////    for (p=t, c=n; c; ){
////        WriteFile(f, p, c, &i, NULL);
////        if (i == c)
////            return;
////        c -= i;
////        p += i;
////    }
////}
////
////Mesaj *ReadMes(HANDLE canal) {
////    static Mesaj mesaj;
////    DWORD no;
////    ReadFile(canal, (char*)&mesaj.lung, sizeof(int), &no, NULL);
////    Read(canal, (char*)&mesaj+sizeof(int), mesaj.lung);
////    return &mesaj;
////}
////
////void WriteMes(HANDLE canal, Mesaj *pm) {
////    DWORD no;
////    WriteFile(canal, (char*)pm, sizeof(int), &no, NULL);
////    Write(canal, (char*)pm+sizeof(int), pm->lung);
////}
////
////void fiu(HANDLE in, HANDLE out){
////    Mesaj *pm, mesaj;
////    int x[100], j=0, m=0, l=0, i;
////    char *pc, linie[100], ok=1, val=0;
////    for ( ; ; j = 0, l = 0){
////        if (ok == 0)
////            break;
////        else
////            while(val==0){
////                printf("Dati un numar: ");
////                pc=(char*)fgets(linie,100,stdin);
////                m=atoi(linie);
////                if(m<0)
////                    val = val + 1;
////                if(pc==NULL)
////                    ok=0;
////                else{
////                    l=l+1;
////                    mesaj.s[j]=m;
////                    j=j+1;
////                }
////            }
////        val=0;
////        mesaj.i = l;
////        mesaj.lung = PLUS + l * sizeof(int);
////        WriteMes(out, &mesaj);
////        pm=ReadMes(in);
////        printf("Valoarea minima este :%d\n", pm->min);
////        printf("Valoarea maxima este :%d\n", pm->max);
////    }
////}
////
////int main() {
////    HANDLE f1, f2;
////
////    f1=CreateFile("\\\\.\\PIPE\\fifo1", GENERIC_WRITE, FILE_SHARE_WRITE, NULL, OPEN_EXISTING, 0, NULL);
////    f2=CreateFile("\\\\.\\PIPE\\fifo2", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);
////
////    fiu(f2, f1);
////
////    CloseHandle(f1);
////    CloseHandle(f2);
////}
//////*/
////
////
/////* SERVER
////#include <stdio.h>
////#include <windows.h>
////
////#define MAXS 10000
////#define MAXL 1000
////#define PLUS (sizeof(int) * 3)
////
////typedef struct{
////    int lung;
////    int i;
////    int max;
////    int min;
////    int s[MAXS];
////}Mesaj;
////
////void Read(HANDLE f, char *t, int n){
////    char *p;
////    int c;
////    DWORD i;
////    for (p=t, c=n; ; ){
////        ReadFile(f, p, c, &i, NULL);
////        if (i == c) return;
////        c -= i;
////        p += i;
////    }
////}
////
////void Write(HANDLE f, char *t, int n){
////    char *p;
////    int c;
////    DWORD i;
////    for (p=t, c=n; c; ){
////        WriteFile(f, p, c, &i, NULL);
////        if (i == c)
////            return;
////        c -= i;
////        p += i;
////    }
////}
////
////Mesaj *ReadMes(HANDLE canal){
////    static Mesaj mesaj;
////    DWORD no;
////    ReadFile(canal, (char*)&mesaj.lung, sizeof(int), &no, NULL);
////    Read(canal, (char*)&mesaj+sizeof(int), mesaj.lung);
////    return &mesaj;
////}
////
////void WriteMes(HANDLE canal, Mesaj *pm) {
////    DWORD no;
////    WriteFile(canal, (char*)pm, sizeof(int), &no, NULL);
////    Write(canal, (char*)pm+sizeof(int), pm->lung);   
////}
////
////void parinte(HANDLE in, HANDLE out) {
////    Mesaj *pm;
////    for ( ; ; ){
////        pm=ReadMes(in);
////        printf("%d", pm->s[0]);
////        
////        //for(int h=1;h<pm->i;h++){
////        //    if(pm->s[h]>pm->max){(pm->max)=(pm->s[h]);
////        //}
////        //if(pm->s[h]<pm->min)pm->min=pm->s[h];}
////        
////        WriteMes(out, pm);
////    }
////}
////
////int main() {
////    HANDLE f1, f2;
////  
////   // fclose(stdin);
////   // fclose(stdout);
////  
////    f1=CreateNamedPipe("\\\\.\\PIPE\\fifo1", PIPE_ACCESS_INBOUND, PIPE_TYPE_BYTE|PIPE_WAIT, 3, 512, 512, 1000, NULL);//creaza pipe
////    f2=CreateNamedPipe("\\\\.\\PIPE\\fifo2", PIPE_ACCESS_OUTBOUND, PIPE_TYPE_BYTE|PIPE_WAIT, 3, 512, 512, 1000, NULL);
//// 
////    ConnectNamedPipe(f1, NULL);//conecteaza
////    ConnectNamedPipe(f2, NULL);
//// 
////    parinte(f1, f2);
////  
////    DisconnectNamedPipe(f1);
////    DisconnectNamedPipe(f2);
////    CloseHandle(f1);
////    CloseHandle(f2);
////}
//////*/
