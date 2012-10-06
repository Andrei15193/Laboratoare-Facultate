// Se da un vector X cu n componente numare naturale. Sa se cel mai mic numar prim 
// din sir. In cazul in care nu exista, se afiseaza mesaj.

public class P14 {
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
        int minimulPrim = 1, n;
        for (int i = 0; i < args.length; i++){
            n = Integer.parseInt(args[i]);
            if (P14.isPrime(n) && (minimulPrim == 1 || n < minimulPrim))
                minimulPrim = n;
        }
        if (minimulPrim == 1)
            System.out.println("Nu exista numere prime in vectorul dat");
        else
            System.out.println("Cel mai mic numar prim din vectorul dat este " + minimulPrim);
    }
}
