#ifndef UTILS_H
#define UTILS_H

typedef enum Bool{
    False, True
}Bool;

// Returneaza numarul de cifre al lui n
unsigned int numarDeCifre(unsigned long long n);

// Verifica daca sirul dat contien doar litere, spatii sau cratime.
// Pre:  sir sa nu fie NULL.
Bool contineDoarLitereCratimeSpatii(const char sir[]);

#endif // UTILS_H
