// Genereaza primul numar prim mai mare decat un numarul natural n dat.

public class P1 {
    public static boolean isPrime(int n) {
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

    public static int getFirstPrime(int n) {
        while (!P1.isPrime(n))
            n++;
        return n;
    }

    public static void main(String[] args) {
        try {
            int n = Integer.parseInt(args[0]);
            if (n < 2)
                System.out.println("2");
            else
                System.out.println(P1.getFirstPrime(n));
        }
        catch (java.lang.IndexOutOfBoundsException exception) {
            System.out.println("Utilizare: P1 <N>");
        }
    }
}
