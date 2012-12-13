#include <assert.h>
#include "../Depozit.h"
#include "../Ui.h"
#include "TestFunctionalitati.h"

// Creaza fisiere cu multe inregistrari.
void creazaContext(const char fisierPersoane[], const char fisierBanci[], const char fisierDepozite[]);

// Sterge, fizic, fisierele.
void distrugeContext(const char fisierPersoane[], const char fisierBanci[], const char fisierDepozite[]);

// Testeaza afisarea dobanzlor pentru persoana specificata. Depozitele si bancile se afla
// in fisierele specificate prin nume.
void testAfiseazaDobanzi(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]);

// Testeaza afisarea situatiei generale a depozitelor.
void testAfiseazaSituatieGenerala(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]);

// Testeaza afisarea situatie depozitelor la sfarsitul lunii a depozitelor.
void testAfiseazaSituatieLaSfarsitulLunii(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]);

void testFunctionalitati(){
    struct Persoana* p = pCreaza("Andrei", "1000000099999");

    creazaContext("multePersoane.test", "multeBanci.test", "multeDepozite.test");
    testAfiseazaDobanzi(p, "multeDepozite.test", "multeBanci.test");
    testAfiseazaSituatieGenerala(p, "multeDepozite.test", "multeBanci.test");
    testAfiseazaSituatieLaSfarsitulLunii(p, "multeDepozite.test", "multeBanci.test");
    distrugeContext("multePersoane.test", "multeBanci.test", "multeDepozite.test");

    pDistruge(p);
}

// Populeaza cu nrPersoane Persoane fisierul specificat. Toate persoanele au numele Andrei
// iar cnpurile sunt consecutive si incept de la 1 000 000 000 000
void populeazaCuPersoane(long nrPersoane, const char numeFisier[]){
    long i;
    char cnp[14];
    struct Persoana* p;
    cnp[14] = 0;
    for (i = 0; i < nrPersoane; i++){
        sprintf(cnp, "1%012ld", i);
        assert((p = pCreaza("Andrei", cnp)) != NULL);
        pAdauga(p, numeFisier);
        pDistruge(p);
    }
}

// Creaza nrBanci in fisierul specificat. Numele bancilor este de forma aaaaaa, aaaaab,
// aaaaac...
void populeazaCuBanci(long nrBanci, const char numeFisier[]){
    int j;
    long i;
    double p;
    char numeBanca[7];
    struct Banca* b;
    numeBanca[6] = 0;
    for (i = 0; i < nrBanci; i++){
        p = 1;
        for (j = sizeof(numeBanca) - 2; j >= 0; j--){
            numeBanca[j] = (int)(i / p) % 26 + 'a';
            p *= 26;
        }
        assert((b = bCreaza(numeBanca, ((i % 19 + 1) / 100.0), True)) != NULL);
        bAdauga(b, numeFisier);
        bDistruge(b);
    }
}

// Pentru persoana specificata se creaza cate un depozit de banca cu suma 2000 si
// capitalizare automata.
void populeazaCuDepozite(struct Persoana* p, const char fisierBanci[], const char numeFisier[]){
    struct Banca b;
    struct Depozit* d;
    FILE* file = fopen(fisierBanci, "r");
    assert(file != NULL);
    while (fread((void*)&b, sizeof(struct Banca), 1, file) == 1){
        assert((d = dCreaza(&b, p, 2000, True)) != NULL);
        dAdauga(d, numeFisier);
        dDistruge(d);
    }
    fclose(file);
}

void creazaContext(const char fisierPersoane[], const char fisierBanci[], const char fisierDepozite[]){
    struct Persoana* p = pCreaza("Andrei", "1000000099999");
    assert(p != NULL);
    printf("Se populeaza fisierul cu persoane\n");
    populeazaCuPersoane(100000, fisierPersoane);
    assert(pCiteste(fisierPersoane, p->cnp) != NULL);

    printf("S-a populat fisierul cu persoane\nSe populeaza fisierul cu banci\n");
    populeazaCuBanci(10000, fisierBanci);

    printf("S-a populat fisierul cu banci\nSe populeaza fisierul cu depozite\n");
    populeazaCuDepozite(p, fisierBanci, fisierDepozite);
    printf("S-a populat fisierul cu depozite\n");

    pDistruge(p);
}

void distrugeContext(const char fisierPersoane[], const char fisierBanci[], const char fisierDepozite[]){
    remove(fisierPersoane);
    remove(fisierBanci);
    remove(fisierDepozite);
}

void testAfiseazaDobanzi(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]){
    FILE* f = fopen("dobanzi.test", "w");
    assert(f != NULL);
    afiseazaDobanzi(p, f, numeFisierDepozite, numeFisierBanci);
    fclose(f);
    assert(system("./dobanzi.sh dobanzi.test 10000") != -1);
    remove("dobanzi.test");
}

void testAfiseazaSituatieGenerala(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]){
    afiseazaSituatieGenerala(numeFisierDepozite, numeFisierBanci, p, "situatieGenerala.test");
    assert(system("./situatieGenerala.sh situatieGenerala.test 10000") != -1);
    remove("situatieGenerala.test");
}

void testAfiseazaSituatieLaSfarsitulLunii(struct Persoana* p, const char numeFisierDepozite[], const char numeFisierBanci[]){
    afiseazaSituatieLaSfarsitulLunii(numeFisierDepozite, numeFisierBanci, p, "situatieLaSfarsitulLunii.test");
    assert(system("./situatieLaSfarsitulLunii.sh situatieLaSfarsitulLunii.test 10000") != -1);
    remove("situatieLaSfarsitulLunii.test");
}
