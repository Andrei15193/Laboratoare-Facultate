#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "OperatiiDeConsistenta.h"

void pAdauga(const struct Persoana* persoana, const char numeFisier[]){
    FILE* file = fopen(numeFisier, "a");
    if (file != NULL){
        fseek(file, 0L, SEEK_END);
        fwrite((const void*)persoana, sizeof(char), sizeof(struct Persoana), file);
        fclose(file);
    }
}

void bAdauga(const struct Banca* banca, const char numeFisier[]){
    FILE* file = fopen(numeFisier, "a");
    if (file != NULL){
        fseek(file, 0L, SEEK_END);
        fwrite((const void*)banca, sizeof(char), sizeof(struct Banca), file);
        fclose(file);
    }
}

void dAdauga(const struct Depozit* depozit, const char numeFisier[]){
    FILE* file = fopen(numeFisier, "a");
    if (file != NULL){
        fseek(file, 0L, SEEK_END);
        fwrite((const void*)depozit, sizeof(char), sizeof(struct Depozit), file);
        fclose(file);
    }
}

struct Persoana* pCiteste(const char numeFisier[], const char cnp[]){
    size_t citit = 0;
    struct Persoana* p = (struct Persoana*)malloc(sizeof(struct Persoana));
    FILE* file = fopen(numeFisier, "r");
    if (file != NULL){
        citit = fread((void*)p, sizeof(char), sizeof(struct Persoana), file);
        while (citit != 0 && strcmp(p->cnp, cnp) != 0)
            citit = fread((void*)p, sizeof(char), sizeof(struct Persoana), file);
        fclose(file);
    }
    if (citit == 0){
        free(p);
        p = NULL;
    }
    return p;
}

struct Banca* bCiteste(const char numeFisier[], const char numeBanca[]){
    size_t citit = 0;
    struct Banca* b = (struct Banca*)malloc(sizeof(struct Banca));
    FILE* file = fopen(numeFisier, "r");
    if (file != NULL){
        citit = fread((void*)b, sizeof(char), sizeof(struct Banca), file);
        while (citit != 0 && strcmp(b->nume, numeBanca) != 0)
            citit = fread((void*)b, sizeof(char), sizeof(struct Banca), file);
        fclose(file);
    }
    if (citit == 0){
        free(b);
        b = NULL;
    }
    return b;
}

struct Depozit* dCiteste(const char numeFisier[], const char cnp[], const char numeBanca[]){
    int citit = 0;
    struct Depozit* depozit = (struct Depozit*)malloc(sizeof(struct Depozit));
    FILE* file = fopen(numeFisier, "r");
    if (file != NULL){
        citit = fread((void*)depozit, sizeof(struct Depozit), 1, file);
        while (citit == 1 && (strcmp(depozit->cnp, cnp) != 0 || strcmp(depozit->numeBanca, numeBanca) != 0))
            citit = fread((void*)depozit, sizeof(struct Depozit), 1, file);
        fclose(file);
    }
    if (citit == 0){
        free(depozit);
        depozit = NULL;
    }
    return depozit;
}

void dModifica(const struct Depozit* d, const char numeFisier[]){
    size_t citit = 0;
    struct Depozit* buffer = (struct Depozit*)malloc(sizeof(struct Depozit));
    FILE* file = fopen(numeFisier, "r+");
    if (file != NULL){
        citit = fread((void*)buffer, sizeof(char), sizeof(struct Depozit), file);
        while (citit != 0 && buffer->cnp != d->cnp && strcmp(buffer->numeBanca, d->numeBanca) != 0)
            citit = fread((void*)buffer, sizeof(char), sizeof(struct Depozit), file);
        if (citit != 0){
            fseek(file, - sizeof(struct Depozit), SEEK_CUR);
            fwrite((void*)d, sizeof(char), sizeof(struct Depozit), file);
        }
        fclose(file);
    }
    free(buffer);
}
