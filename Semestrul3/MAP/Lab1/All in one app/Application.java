// Genereaza primul numar prim mai mare decat un numarul natural n dat.

// Dandu-se numarul natural n, determina numerele prime p1 si p2 astfel ca
// n  = p1 + p2 (verificarea ipotezei lui Goldbach).

// Determina varsta (in numar de zile) pentru o persoana.

// Determina numerele prime p1 si p2 gemene imediat superioare numarului natural 
// nenul n dat. Doua numere prime p si q sunt gemene daca q-p = 2.

// Fie n un numar natural dat. Calculati produsul p al tuturor factorilor  proprii 
// ai lui n.

// Palindromul unui numar este numarul obtinut prin scrierea cifrelor in ordine  
// inversa (Ex. palindrom(237) = 732). Pentru un n dat calculati palindromul sau.

// Genereaza cel mai mic numar perfect mai mare decat un numar n dat. In cazul in 
// care nu exista, se afiseaza mesaj. Un numar este perfect daca este egal cu suma 
// divizorilor sai, exeptandu-l pe el insusi. (6=1+2+3).

// Genereaza cel mai mare numar prim mai mic decat un numar n dat. In cazul in care 
// nu exista, se afiseaza mesaj.

// Genereaza cel mai mare numar perfect mai mic decat un numar n dat. In cazul in 
// care nu exista, se afiseaza mesaj. Un numar este perfect daca este egal cu suma 
// divizorilor sai, exeptandu-l pe el insusi. (6=1+2+3).

// Gaseste cel mai mic numar m din sirul lui Fibonacci definit de
// f[0]=f[1]=1, f[n]=f[n-1]+f[n-2], pentru n>2, 
// mai mare decat numarul natural n dat, deci exista k astfel ca f[k]=m si m>n.

// Se da un vector X cu n componente numare naturale. Sa se determine produsul 
// numerelor prime din sir.

// Se da un vector X cu n componente numere naturale. Sa se determine suma 
// numerelor prime din sir.

// Se da un vector X cu n componente numare naturale. Sa se cel mai mare numar prim 
// din sir. In cazul in care nu exista, se afiseaza mesaj.

// Se da un vector X cu n componente numare naturale. Sa se cel mai mic numar prim 
// din sir. In cazul in care nu exista, se afiseaza mesaj.

// Se da un vector X cu n componente numare naturale. Sa se determine suma numerelor
// compuse (neprime) din sir.

// Represents the application
public class Application {
    // Point of entry. The program starts here.
    // Preconditions:
    //      args: refers an array of Strings that represent the command line
    //            arguments.
    // Postconditions:
    //      Solves the given problem by requesting data (the n number) and prints
    //      the solution to the standard output.
    public static void main(String[] args){
        UserInterface ui = new UserInterface(new Controller());
        ui.run();
    }
}