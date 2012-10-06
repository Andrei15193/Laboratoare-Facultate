// Se da un vector X cu n componente numare naturale. Sa se determine produsul 
// numerelor prime din sir.

public class P11 {
    public static boolean isPrime(int n){
        boolean p = n > 1;
        int i = 3;
        double squareRootOfN = java.lang.Math.sqrt(n);
        if (n != 2 && n % 2 == 0)
            p = false;
        else
            while (p && i <= squareRootOfN)
                if (n%i == 0)
                    p = false;
                else
                    i += 2;
        return p;
    }
    
    public static void main(String[] args){
        int p = 1, n;
        for (int i = 0; i < args.length; i++){
            n = Integer.parseInt(args[i]);
            if (P11.isPrime(n))
                p *= n;
        }
        if (p == 1)
            System.out.println("Nu exista numere prime in vectorul dat");
        else
            System.out.println("Produsul numerelor prime din vectorul dat este " + p);
    }
}
