// Determina numerele prime p1 si p2 gemene imediat superioare numarului natural 
// nenul n dat. Doua numere prime p si q sunt gemene daca q-p = 2.

public class P4 {
    public static boolean isPrime(int n){
        boolean p = n > 1;
        int i = 3;
        double squareRootOfN = java.lang.Math.sqrt(n);
        if (n != 2 && n % 2 == 0)
            p = false;
        else
            while (p && i <= squareRootOfN)
                if (n % i != 0)
                    i += 2;
                else
                    p = false;
        return p;
    }
    
    public static int[] getFirstTwinPrimes(int n){
        boolean notFound = true;
        int firstPrime = 0;
        int[] result = new int[2];
        if (n % 2 == 0)
            n--;
        while (notFound){
            n += 2;
            if (P4.isPrime(n))
                if (firstPrime != 0 && n - firstPrime == 2)
                    notFound = false;
                else
                    firstPrime = n;
        }
        result[0] = firstPrime;
        result[1] = n;
        return result;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            int[] twins = {3, 5};
            if (n >= 3)
                twins = P4.getFirstTwinPrimes(n);
            System.out.println("Numerele prime gemene imedeat superioare lui " + n + " sunt " + twins[0] + " si " + twins[1]);
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P4 <N>");
        }
    }
}
