// Genereaza cel mai mare numar prim mai mic decat un numar n dat. In cazul in care 
// nu exista, se afiseaza mesaj.

public class P8 {
    public static boolean isPrime(int n){
        boolean p = n > 1;
        int i = 3;
        double squareRootOfN = java.lang.Math.sqrt(n); 
        if (n != 2 && n % 2 == 0)
            p = false;
        else
            while (p && i <= squareRootOfN)
                if (n % i == 0)
                    p = false;
                else
                    i += 2;
        return p;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            if (n < 3)
                System.out.println("Nu exista un numar prim mai mic decat " + args[0]);
            else{
                while (!P8.isPrime(--n));
                System.out.println("Cel mai mare numar prim mai mic decat " + args[0] + " este " + n);
            }
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P8 <N>");
        }
    }
}
