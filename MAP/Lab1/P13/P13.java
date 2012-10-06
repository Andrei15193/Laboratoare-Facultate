// Se da un vector X cu n componente numare naturale. Sa se cel mai mare numar prim 
// din sir. In cazul in care nu exista, se afiseaza mesaj.

public class P13 {
    public static boolean isPrime(int n){
        boolean p = n > 1;
        int i = 3;
        double squareRootOfN = java.lang.Math.sqrt(n);
        if (n != 2 && n % 2 == 0)
            p = false;
        while (p && i <= squareRootOfN)
            if (n % i == 0)
                p = false;
            else
                i += 2;
        return p;
    }
    
    public static void main(String[] args){
        int maximulPrim = 1, n;
        for (int i = 0; i < args.length; i++){
            n = Integer.parseInt(args[i]);
            if (maximulPrim < n && P13.isPrime(n))
                maximulPrim = n;
        }
        if (maximulPrim == 1)
            System.out.println("Nu exista numere prime in vectorul dat");
        else
            System.out.println("Cel mai mare numar prim din vectorul dat este " + maximulPrim);
    }
}
