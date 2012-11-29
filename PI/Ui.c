#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "Ui.h"
#include "Comun.h"
#include "Depozit.h"
#include "OperatiiDeConsistenta.h"

enum Optiune{
    AdaugaPersoana = 1,
    AdaugaBanca,
    AdaugaDepozit,
    AfiseazaDobanzi,
    AfiseazaSituatieGenerala,
    AfiseazaSituatieLaSfarsitulLunii,
    Iesire
};

// Afiseaza mesajul concatenat cu (d/n). In cazul in care s-a introdus de la
// intrarea standard caracterul 'd' functia returneaza True, False altfel.
Bool citesteDaNu(const char mesaj[]){
    char buff[3];
    printf("%s (d/n)", mesaj);
    fgets(buff, 3, stdin);
    if (buff[0] == 'd')
        return True;
    else
        return False;
}

// Afiseaza meniul la iesirea standard.
void afiseazaMeniu(){
    printf("1. Adauga o persoana\n2. Adauga o banca\n3. Creaza un depozit\n4. Afiseaza toate dobanzile unei persoane\n5. Afiseaza situatia generala a depozitelor unei persoane\n6. Afiseaza situatia de la sfarsitul lunii ale depozitelor unei persoane\n7. Iesire\n");
}

// Citeste o optiune (numar) de la intrarea standard. Optiunea poate sa fie invalida.
enum Optiune citesteOptiune(){
    char buff[5];
    printf("Optiunea dumneavoastra este: ");
    fgets(buff, sizeof(buff), stdin);
    return atoi(buff);
}

// Afiseaza meniul la iesirea standard si citeste o optiune (numar) de la intrarea
// standard. Optiunea poate sa fie invalida.
enum Optiune obtineOptiune(){
    afiseazaMeniu();
    return citesteOptiune();
}

// Citeste datele unei persoane de la intrarea standard si se incearca crearea unei
// persoane. Daca datele sunt valide se returneaza o referinta catre o variabila
// de tip Persoana, altfel se returneaza NULL.
// In cazul in care se creaza persoana aceasta trebuie distrusa folosid pCreaza().
struct Persoana* citestePersoana(){
    char nume[31], cnp[15];
    printf("Nume persoana (max 30 caractere): ");
    fgets(nume, 31, stdin);
    nume[strlen(nume) - 1] = 0;
    printf("CNP persoana (exact 13 cifre):    ");
    fgets(cnp, 15, stdin);
    cnp[strlen(cnp) - 1] = 0;
    return pCreaza(nume, cnp);
}

// Incearca sa adauge in fisierul de persoane persoana specificata. In caz de reusita
// se returneaza True, False altfel.
Bool adaugaPersoana(struct Persoana* persoana){
    struct Persoana* citit = pCiteste(FISIER_PERSOANE, persoana->cnp);
    if (citit == NULL){
        pAdauga(persoana, FISIER_PERSOANE);
        return True;
    }
    else{
        pDistruge(citit);
        return False;
    }
}

// Citeste datele unei persoane de la intrarea standard si se incearca crearea unei
// persoane. Daca datele sunt valide se returneaza o referinta catre o variabila
// de tip Persoana, altfel se returneaza NULL.
// In cazul in care se creaza persoana aceasta trebuie distrusa folosid pCreaza().
struct Banca* citesteBanca(){
    char nume[31], dobanda[15];
    printf("Nume banca (max 30 caractere):   ");
    fgets(nume, 31, stdin);
    nume[strlen(nume) - 1] = 0;
    printf("Dobanda (din intervalul (0, 1)): ");
    fgets(dobanda, 15, stdin);
    dobanda[strlen(dobanda) - 1] = 0;
    return bCreaza(nume, atof(dobanda), citesteDaNu("Este dobanda ferma?"));
}

// Incearca sa adauge in fisierul de persoane persoana specificata. In caz de reusita
// se returneaza True, False altfel.
Bool adaugaBanca(struct Banca* banca){
    struct Banca* citit = bCiteste(FISIER_BANCI, banca->nume);
    if (citit == NULL){
        bAdauga(banca, FISIER_BANCI);
        return True;
    }
    else{
        bDistruge(citit);
        return False;
    }
}

// Citeste datele necesare pentru crearea unui depozit si (in cazul in care este posibil)
// se creaza un nou depozit persoanei date ca parametru.
// In cazul in care se creaza un depozit se returneaza o referinta catre o variabila
// de tip struct Depozit (se foloseste dDistruge() cand nu mai este nevoie de depozit).
// In cazul in care nu se poate crea un depozit se returneaza NULL.
struct Depozit* citesteDepozit(struct Persoana* persoana){
    char numeBanca[31], suma[31];
    struct Banca* banca;
    struct Depozit* depozit = NULL;
    printf("Nume banca (max 30 caractere): ");
    fgets(numeBanca, 31, stdin);
    numeBanca[strlen(numeBanca) - 1] = 0;
    printf("Suma (numar strict pozitiv):   ");
    fgets(suma, 31, stdin);
    suma[strlen(suma) - 1] = 0;
    banca = bCiteste(FISIER_BANCI, numeBanca);
    if (banca != NULL){
        depozit = dCreaza(banca, persoana, atof(suma), citesteDaNu("Capitalizare automata a depozitului?"));
        bDistruge(banca);
    }
    return depozit;
}

// Incearca sa adauge in fisierul de depozite depozitul specificat. In caz de reusita
// se returneaza True, False altfel.
Bool adaugaDepozit(struct Depozit* depozit){
    struct Depozit* citit = dCiteste(FISIER_DEPOZITE, depozit->cnp, depozit->numeBanca);
    if (citit == NULL){
        dAdauga(depozit, FISIER_DEPOZITE);
        return True;
    }
    else{
        dDistruge(citit);
        return False;
    }
}

// Afiseaza toate bancile existente pe o linie (doar numele lor).
// Returneaza True daca exista banci memorate, False altfel.
Bool afiseazaBancile(){
    struct Banca banca, pre;
    FILE* file = fopen(FISIER_BANCI, "r");
    if (file != NULL && fread((void*)&pre, sizeof(struct Banca), 1, file) == 1){
        printf("Bancile memorate sunt: ");
        while (fread((void*)&banca, sizeof(struct Banca), 1, file) == 1){
            printf("%s, ", pre.nume);
            pre = banca;
        }
        printf("%s\n", pre.nume);
        fclose(file);
        return True;
    }
    else{
        printf("Nu exista banci!\n");
        return False;
    }
}

// Afiseaza, pentru fiecare depozit pe care persoana o detine, dobanzile aferente si
// totala.
void afiseazaDobanzi(struct Persoana* persoana){
    char linie[41];
    FILE* file = fopen(FISIER_DEPOZITE, "r");
    double dobandaTotala = 0;
    struct Banca* banca;
    struct Depozit depozit;
    enum Bool citit = False;

    memset(linie, '-', 40);
    linie[41] = 0;
    if (file != NULL){
        printf("%s\n|%30s|%7s|\n%s\n", linie, "Banca", "Dobanda", linie);
        while (fread((void*)&depozit, sizeof(struct Depozit), 1, file) == 1)
            if (strcmp(persoana->cnp, depozit.cnp) == 0){
                if (depozit.dobandaFerma || depozit.dobanda == 0){
                    printf("|%30.30s|%7.2f|\n", depozit.numeBanca, depozit.dobanda);
                    dobandaTotala += depozit.dobanda;
                }
                else{
                    banca = bCiteste(FISIER_BANCI, depozit.numeBanca);
                    printf("|%30.30s|%7.2f|\n", depozit.numeBanca, banca->dobandaCurenta);
                    dobandaTotala += banca->dobandaCurenta;
                    bDistruge(banca);
                }
                citit = True;
            }
        fclose(file);
    }
    if (!citit)
        printf("Nu exista depozite inregistrate!\n\n");
    else
        printf("%s\n Dobanda totala: %22.2f\n\n", linie, dobandaTotala);
}

// Afiseaza intr-un fisier situatia generala a tuturor depozitelor pe care o persoana
// le detine (Numele banci unde se afla depozitul, data in care urmeaza sa se incaseze
// suma si suma de incasat).
void afiseazaSituatieGenerala(struct Persoana* persoana){
    char linie[57];
    FILE* file = fopen(FISIER_DEPOZITE, "r");
    FILE* out = fopen("situatieGenerala.txt", "w");
    double dobanda = 0;
    double sumaTotala = 0, sumaCurenta;
    struct Banca* banca;
    struct Depozit depozit;
    enum Bool citit = False;

    if (out != NULL){
        memset(linie, '-', 56);
        linie[57] = 0;
        if (file != NULL){
            fprintf(out, "Afiseaza situatia generala depozit:\n%s\n|%30s|%7s|%15s|\n%s\n", linie, "Banca", "Zi cap.", "Suma de incasat", linie);
            while (fread((void*)&depozit, sizeof(struct Depozit), 1, file) == 1)
                if (strcmp(persoana->cnp, depozit.cnp) == 0){
                    if (depozit.dobandaFerma || depozit.dobanda == 0)
                        dobanda = depozit.dobanda;
                    else{
                        banca = bCiteste(FISIER_BANCI, depozit.numeBanca);
                        dobanda = banca->dobandaCurenta;
                        bDistruge(banca);
                    }
                    sumaCurenta = depozit.suma + dobanda * depozit.suma;
                    sumaTotala += sumaCurenta;
                    fprintf(out, "|%30s|%7d|%15.2f|\n", depozit.numeBanca, depozit.dataCapitalizarii.tm_mday, sumaCurenta);
                    citit = True;
                }
            fclose(file);
        }
        if (!citit)
            printf("Nu exista depozite inregistrate!\n\n");
        else{
            fprintf(out, "%s\n Suma totala: %41.2f\n", linie, sumaTotala);
            printf("S-a generat fisierul situatieGenerala.txt\n\n");
        }
        fclose(out);
    }
    else
        printf("Nu s-a putut crea fisierul situatieGenerala.txt\n");
}

// Afiseaza intr-un fisier situatia la sfarsitul lunii a tuturor depozitelor unei
// persoane. Capitalezeaza depozitele atunci cand e cazul.
void afiseazaSituatieLaSfarsitulLunii(struct Persoana* persoana){
    char linie[57];
    FILE* file = fopen(FISIER_DEPOZITE, "r+");
    FILE* out = fopen("situatieLaSfarsitulLunii.txt", "w");
    double dobanda = 0;
    double sumaCurenta;
    struct Banca* banca;
    struct Depozit depozit;
    enum Bool citit = False;
    time_t timp = time(NULL);
    struct tm* tmTimp = gmtime(&timp);

    if (out != NULL){
        memset(linie, '-', 56);
        linie[57] = 0;
        if (file != NULL){
            fprintf(out, "Afiseaza situatia la sfarsitul lunii:\n%s\n|%30s|%7s|%15s|\n%s\n", linie, "Banca", "Dobanda", "Suma de incasat", linie);
            while (fread((void*)&depozit, sizeof(struct Depozit), 1, file) == 1)
                if (strcmp(persoana->cnp, depozit.cnp) == 0){
                    if (depozit.dobandaFerma || depozit.dobanda == 0)
                        dobanda = depozit.dobanda;
                    else{
                        banca = bCiteste(FISIER_BANCI, depozit.numeBanca);
                        dobanda = banca->dobandaCurenta;
                        bDistruge(banca);
                    }
                    if (depozit.dataCapitalizarii.tm_mon < tmTimp->tm_mon &&
                            depozit.dataCapitalizarii.tm_mday == tmTimp->tm_mday &&
                            depozit.dobanda != 0){
                        sumaCurenta = depozit.suma + dobanda * depozit.suma;
                        depozit.suma = sumaCurenta;
                        depozit.dataCapitalizarii.tm_mon = tmTimp->tm_mon;
                        fseek(file, -(sizeof(struct Depozit)), SEEK_CUR);
                        fwrite((void*)&depozit, sizeof(struct Depozit), 1, file);
                    }
                    else
                        sumaCurenta = depozit.suma;
                    fprintf(out, "|%30s|%7.2f|%15.2f|\n", depozit.numeBanca, dobanda, sumaCurenta);
                    citit = True;
                }
            fclose(file);
        }
        if (!citit)
            printf("Nu exista depozite inregistrate!\n\n");
        else{
            fprintf(out, "%s\n\n", linie);
            printf("S-a generat fisierul situatieLaSfarsitulLunii.txt cu informatiile cerute\n");
        }
        fclose(out);
    }
    else
        printf("Nu s-a putut crea fisierul situatieLaSfarsitulLunii.txt");
}

// Autentifica utilizatorul ca fiind o persoana (dupa nume si CNP). In cazul in care
// autentificarea reuseste se ruleaza optiunea specificata. Altfel se incheie rularea
// functiei.
void autentificare(enum Optiune optiune){
    struct Persoana* persoana;
    struct Depozit* depozit;
    printf("Autentificare:\n");
    persoana = citestePersoana();
    if (persoana != NULL && pCiteste(FISIER_PERSOANE, persoana->cnp)){
        printf("Autentificarea a avut succes!\n");
        switch (optiune){
        case AdaugaDepozit:
            printf("Adaugarea unui depozit:\n");
            if (afiseazaBancile())
            {
                depozit = citesteDepozit(persoana);
                if (depozit != NULL)
                    if (adaugaDepozit(depozit))
                        printf("Depozitul a fost creat cu succes!\n\n");
                    else
                        printf("Persoana cu numele %s detine deja un depozit la banca %s!\n\n", persoana->nume, depozit->numeBanca);
                else
                    printf("S-au introdus date invalide! Suma nu poate fi negativa!\n\n");
            }
            else
                printf("Nu se pot crea depozite!\n\n");
            break;
        case AfiseazaDobanzi:
            printf("Afiseaza toate dobanzile depozit:\n");
            afiseazaDobanzi(persoana);
            break;
        case AfiseazaSituatieGenerala:
            afiseazaSituatieGenerala(persoana);
            break;
        case AfiseazaSituatieLaSfarsitulLunii:
            afiseazaSituatieLaSfarsitulLunii(persoana);
        default: { }
        }
        pDistruge(persoana);
    }
    else
        printf("Autentificarea a esuat!\n\n");
}

void ruleaza(){
    struct Persoana* persoana;
    struct Banca* banca;
    enum Optiune optiune;
    do{
        optiune = obtineOptiune();
        switch (optiune){
        case AdaugaPersoana:
            printf("Adaugarea unei persoane\n");
            persoana = citestePersoana();
            if (persoana == NULL)
                printf("S-au introdus date invalide!\nNumele persoanei poate contine doar litere, spatii sau cratime!\n\n");
            else
                if (adaugaPersoana(persoana) == True)
                    printf("Persoana a fost adaugata cu succes!\n\n");
                else
                    printf("Eroare! Persoana este deja memorata!\n\n");
            break;
        case AdaugaBanca:
            printf("Adaugarea unei banci:\n");
            banca = citesteBanca();
            if (banca == NULL)
                printf("S-au introdus date invalide!\nNumele bancii poate contine doar litere, spatii sau cratime!\n\n");
            else
                if (adaugaBanca(banca) == True)
                    printf("Banca a fost adaugata cu succes!\n\n");
                else
                    printf("Eroare! Banca este deja memorata!\n\n");
            break;
        case AdaugaDepozit:
        case AfiseazaDobanzi:
        case AfiseazaSituatieGenerala:
        case AfiseazaSituatieLaSfarsitulLunii:
            autentificare(optiune);
            break;
        case Iesire:
            printf("Programul si-a terminat executia\n\n");
            break;
        default:
            printf("Optiune invalida\n\n");
        }
    }while (optiune != Iesire);
}
