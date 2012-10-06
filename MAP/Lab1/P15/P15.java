// Se da un vector X cu n componente numare naturale. Sa se determine suma numerelor
// compuse (neprime) din sir.

public class P15 {
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
        int s = 0, n;
        for (int i = 0; i < args.length; i++){
            n = Integer.parseInt(args[i]);
            if (!P15.isPrime(n))
                s += n;
        }
        if (s == 0)
            System.out.println("Nu exista numere compuse (neprime) in vectorul dat, suma este 0");
        else
            System.out.println("Suma numerelor compuse (neprime) din vectorul dat este " + s);
    }
}
