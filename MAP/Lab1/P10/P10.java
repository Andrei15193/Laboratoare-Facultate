// Gaseste cel mai mic numar m din sirul lui Fibonacci definit de
//           f[0]=f[1]=1, f[n]=f[n-1]+f[n-2], pentru n>2, 
// mai mare decat numarul natural n dat, deci exista k astfel ca f[k]=m si m>n.

public class P10 {
    public static int getGreaterFibonacci(int n){
        int a = 1, b = 1, c;
        while (b <= n){
            c = a + b;
            a = b;
            b = c;
        }
        return b;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            if (n < 3)
                System.out.println("N trebuie sa fie cel putin 3");
            else
                System.out.println("Cel mai mic numar din sirul lui Fibonacci si mai mare decat " + args[0] + " este " + P10.getGreaterFibonacci(n));
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P10 <N>");
        }
    }
}
