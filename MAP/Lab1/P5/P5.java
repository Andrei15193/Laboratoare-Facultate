// Fie n un numar natural dat. Calculati produsul p al tuturor factorilor  proprii 
// ai lui n.

public class P5 {
    public static int getProduct(int n){
        int p = 1;
        n = java.lang.Math.abs(n);
        for (int i = 2, end = n / 2; i <= end; i++)
            if (n % i == 0)
                p *= i;
        return p;
    }
    
    public static void main(String[] args){
        try{
            System.out.println("Produsul tuturor factorilor proprii este " + P5.getProduct(Integer.parseInt(args[0])));
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P5 <N>");
        }
    }
}
