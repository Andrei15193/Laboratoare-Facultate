// Se da un vector X cu n componente numere naturale. Sa se determine suma 
// numerelor prime din sir.

public class P12 {
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
            if (P12.isPrime(n))
                s += n;
        }
        if (s == 0)
            System.out.println("Nu exista numere prime in vectorul dat, suma este 0");
        else
            System.out.println("Suma numerelor prime din vectorul dat este " + s);
    }
}
