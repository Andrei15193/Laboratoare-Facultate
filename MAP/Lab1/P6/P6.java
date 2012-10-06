// Palindromul unui numar este numarul obtinut prin scrierea cifrelor in ordine  
// inversa (Ex. palindrom(237) = 732). Pentru un n dat calculati palindromul sau.

public class P6 {
    public static int reverse(int n){
        boolean negative = n < 0;
        int mirrored = 0;
        n = java.lang.Math.abs(n);
        while (n != 0){
            mirrored = mirrored * 10 + n % 10;
            n /= 10;
        }
        if (negative)
            mirrored *= -1;
        return mirrored;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            System.out.println("Oglinditul lui " + n + " este " + P6.reverse(n));
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P6 <N>");
        }
    }
}
