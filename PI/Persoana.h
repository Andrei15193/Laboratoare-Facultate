#ifndef PERSOANA_H
#define PERSOANA_H

// Reprezinta o persoana.
struct Persoana{
    char nume[31];
    char cnp[14];
};

// Creaza o persoana cu numele si cnpul specificat.
// Pre:  nume sa contina doar spatii, cratime sau litere,
//       cnp sa fie format din 13 cifre.
// Post: Valoarea returnata refera o persoana valida.
struct Persoana* pCreaza(const char nume[], const char cnp[]);

// Distruge o persoana.
// Pre:  persoana sa fie returnat de pCreaza().
// Post: Dupa apel, persoana referita nu mai exista.
void pDistruge(struct Persoana* persoana);

#endif // PERSOANA_H
