#ifndef CONSISTENCYOPERATIONS_H
#define CONSISTENCYOPERATIONS_H

#include "Persoana.h"
#include "Banca.h"
#include "Depozit.h"

// Adauga o persoana in fisierul specificat.
// Pre:  persoana sa fie returnat de pCreaza(),
//       numeFisier sa nu fie NULL sau sirul vid.
// Post: persoana specificata este adaugata in fisierul cu numele specificat.
void pAdauga(const struct Persoana* persoana, const char numeFisier[]);

// Adauga o banca in fisierul specificat.
// Pre:  banca sa fie returnat de bCreaza(),
//       numeFisier sa nu fie NULL sau sirul vid.
// Post: banca specificata este adaugata in fisierul cu numele specificat.
void bAdauga(const struct Banca* banca, const char numeFisier[]);

// Adauga un depozit in fisierul specificat.
// Pre:  depozitul sa fie returnat de dCreaza(),
//       numeFisier sa nu fie NULL sau sirul vid.
// Post: depozitul specificata este adaugata in fisierul cu numele specificat.
void dAdauga(const struct Depozit* depozit, const char numeFisier[]);

// Cauta si returneaza persoana din fisierul specificat care are cnpul dat.
// Persoana returnata trebuie distrusa cu pDistruge().
// In cazul in care nu se returneaza NULL valoarea returnata este echivalenta in
// validitate cu una returnata de pCreaza().
// Pre:  numeFisier sa nu fie NULL sau sirul vid,
//       cnp sa fie din 13 cifre.
// Post: Persoana returnata are acelasi CNP cu cel specificat,
//       in cazul in care fisierul nu contine o astfel de persoana se returneazaa NULL.
struct Persoana* pCiteste(const char numeFisier[], unsigned long long cnp);

// Cauta si returneaza banca din fisierul specificat care are numele dat.
// Banca returnata trebuie distrusa cu bDistruge().
// In cazul in care nu se returneaza NULL valoarea returnata este echivalenta in
// validitate cu una returnata de bCreaza().
// Pre:  numeFisier sa nu fie NULL sau sirul vid,
//       nume sa nu fie NULL sau sirul vid.
// Post: Banca returnata are acelasi nume cu cel specificat,
//       in cazul in care fisierul nu contine o astfel de banca se returneazaa NULL.
struct Banca* bCiteste(const char numeFisier[], const char numeBanca[]);

// Cauta si returneaza depozitul din fisierul specificat care este asociat persoanei
// date prin cnp si bancii date prin numeBanca.
// Depozitul returnat trebuie distrusa cu dDistruge().
// In cazul in care nu se returneaza NULL valoarea returnata este echivalenta in
// validitate cu una returnata de dCreaza().
// Pre:  numeFisier sa nu fie NULL sau sirul vid,
//       cnp sa fie format din 13 cifre,
//       numeBanca sa nu fie NULL sau sirul vid.
// Post: depozitul returnat este asociat Bancii date prin numeBanca si persoanei date
//       prin cnp. In cazul in care fisierul nu contine o astfel de banca se
//       returneazaa NULL.
struct Depozit* dCiteste(const char numeFisier[], unsigned long long cnp, const char numeBanca[]);

// Modifica in fisierul specificat prin nume depozitul dat.
// Cautarea se face dupa cnp si numeBanca stocate in depozitul dat, daca nu se
// gaseste o potrivire atunci fisierul ramane neschimbat.
// Pre:  d trebuie sa fie returnat de dCreaza(),
//       numeFisier sa nu fie NULl sau sirul vid.
// Post: fisierul specificat prin numeFisier contine o versiune actualizata a depoziului.
void dModifica(const struct Depozit* d, const char numeFisier[]);

#endif // CONSISTENCYOPERATIONS_H
