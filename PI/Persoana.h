#ifndef PERSOANA_H
#define PERSOANA_H

// Reprezinta o persoana.
struct Persoana{
    char nume[31];
    unsigned long long cnp;
};

// Creaza o persoana cu numele si cnpul specificat.
// Pre:  nume sa contina doar spatii, cratime sau litere,
//       cnp sa fie format din 13 cifre.
// Post: Valoarea returnata refera o persoana valida.
struct Persoana* pCreaza(const char nume[], unsigned long long cnp);

// Distruge o persoana.
// Pre:  persoana sa fie returnat de pCreaza().
// Post: Dupa apel, persoana referita nu mai exista.
void pDistruge(struct Persoana* persoana);

// Seteaza numele persoanei date.
// Pre:  persoana sa fie returnat de pCreaza,
//       nume sa contina doar cratime, spatii sau litere.
// Post: numele vechi al persoanei se pierde si este retinut doar numele nou.
void pSeteazaNume(struct Persoana* persoana, const char nume[]);

#endif // PERSOANA_H
