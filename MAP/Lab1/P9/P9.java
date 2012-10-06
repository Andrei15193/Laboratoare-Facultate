// Genereaza cel mai mare numar perfect mai mic decat un numar n dat. In cazul in 
// care nu exista, se afiseaza mesaj. Un numar este perfect daca este egal cu suma 
// divizorilor sai, exeptandu-l pe el insusi. (6=1+2+3).

public class P9 {
    public static java.util.AbstractList<Integer> getDivisors(int n){
        java.util.AbstractList<Integer> divisors = new java.util.LinkedList<Integer>();
        for (int i = 1, end = n/2; i <= end; i++)
            if (n%i == 0)
                divisors.add(i);
        divisors.add(n);
        return divisors;
    }
    
    public static boolean isPerfect(int n, java.util.AbstractList<Integer> divisors){
        int s = 0;
        java.util.Iterator<Integer> it = divisors.iterator();
        while(it.hasNext())
            s += it.next();
        return 2 * n == s;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]);
            String result;
            java.util.AbstractList<Integer> divisors;
            if (n < 7)
                System.out.println("Nu exista un numar perfect mai mic decat 6");
            else{
                while (!P9.isPerfect(n, (divisors = P9.getDivisors(n))))
                    n--;
                result = n + "=";
                java.util.Iterator<Integer> it = divisors.iterator();
                n = it.next();
                while (it.hasNext()){
                    result += n + "+";
                    n = it.next();
                }
                System.out.println(result.substring(0, result.length() - 1));
            }
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P9 <N>");
        }
    }
}
