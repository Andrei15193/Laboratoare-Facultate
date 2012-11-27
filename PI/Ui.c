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
    if (pCiteste(FISIER_PERSOANE, persoana->cnp) == NULL){
        pAdauga(persoana, FISIER_PERSOANE);
        return True;
    }
    else
        return False;
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
    if (bCiteste(FISIER_BANCI, banca->nume) == NULL){
        bAdauga(banca, FISIER_BANCI);
        return True;
    }
    else
        return False;
}

// Autentifica utilizatorul ca fiind o persoana (dupa nume si CNP). In cazul in care
// autentificarea reuseste se ruleaza optiunea specificata. Altfel se incheie rularea
// functiei.
void autentificare(enum Optiune optiune){
    struct Persoana* persoana;
    printf("Autentificare:\n");
    persoana = citestePersoana();
    if (persoana != NULL && pCiteste(FISIER_PERSOANE, persoana->cnp)){
        printf("Autentificarea a avut succes!\n");
        switch (optiune){
        case AdaugaDepozit:
            printf("Adaugarea unui depozit:\n");
            break;
        case AfiseazaDobanzi:
            printf("Afiseaza toate dobanzile depozit:\n");
            break;
        case AfiseazaSituatieGenerala:
            printf("Afiseaza situatia generala depozit:\n");
            break;
        case AfiseazaSituatieLaSfarsitulLunii:
            printf("Afiseaza situatia la sfarsitul lunii:\n");
        default: { }
        }
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

// Citeste un depozit de la intrarea standard si il adauga in fisierul care retine
// banci daca acesta nu exista.
void adaugaDepozit(struct Depozit* depozit);

// Afiseaza dobanzile unei persoane.
void afiseazaDobanzi(struct Persoana*);

// Afiseaza situatia generala a unei persoane.
void afiseazaSituatiaGenerala(struct Persoana*);

// Afiseaza situatia de la sfarsitul lunii pentru o persoana.
void afiseazaSituatiaLaSfarsitulLunii(struct Persoana*);

