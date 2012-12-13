#ifndef UI_H
#define UI_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "Comun.h"
#include "Depozit.h"
#include "OperatiiDeConsistenta.h"

// Afiseaza, pentru fiecare depozit pe care persoana o detine, dobanzile aferente si
// totala.
void afiseazaDobanzi(struct Persoana* persoana, FILE* out, const char numeFisierDepozite[], const char numeFisierBanci[]);

// Afiseaza intr-un fisier situatia generala a tuturor depozitelor pe care o persoana
// le detine (Numele banci unde se afla depozitul, data in care urmeaza sa se incaseze
// suma si suma de incasat).
void afiseazaSituatieGenerala(const char numeFisierDepozite[], const char numeFisierBanci[], struct Persoana* persoana, const char numeFisierIesire[]);

// Afiseaza intr-un fisier situatia la sfarsitul lunii a tuturor depozitelor unei
// persoane. Capitalezeaza depozitele atunci cand e cazul.
void afiseazaSituatieLaSfarsitulLunii(const char numeFisierDepozite[], const char numeFisierBanci[], struct Persoana* persoana, const char numeFisierIesire[]);

// Ruleazaa interfata cu utilizatorul, implicit si programul.
void ruleaza();

#endif // UI_H
