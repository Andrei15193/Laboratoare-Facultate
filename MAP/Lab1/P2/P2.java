// Dandu-se numarul natural n, determina numerele prime p1 si p2 astfel ca
// n  = p1 + p2 (verificarea ipotezei lui Goldbach).

public class P2 {
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

    public static int[] getTwoPrimeTermsForN(int n){
        int i = n/2;
        int[] result;
        while (i <= n - 2 && !P1.isPrime(i) && !P1.isPrime(n - i))
            i++;
        if (i > n - 2)
            result = new int[0];
        else{
            result = new int[2];
            result[0] = i;
            result[1] = n - i;
        }
        return result;
    }

    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            int[] result;
            if (n >= 4){
                result = P2.getTwoPrimeTermsForN(n);
                if (result.length == 2)
                    System.out.println(Integer.toString(result[0]) + " + " +
                                       Integer.toString(result[1]) + " = " +
                                       args[0]);
                else
                    System.out.println("Nu s-au gasit doua numere prime a caror suma sa fie egala cu n.");
            }
            else
                System.out.println("Nu exista doua numere prime a caror suma sa fie egala cu n. Numarul trebuie sa fie minim 4.");
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P2 <N>");
        }
    }
}
