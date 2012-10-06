// Genereaza cel mai mic numar perfect mai mare decat un numar n dat. In cazul in 
// care nu exista, se afiseaza mesaj. Un numar este perfect daca este egal cu suma 
// divizorilor sai, exeptandu-l pe el insusi. (6=1+2+3). 

public class P7 {
    public static java.util.AbstractList<Integer> getDivisors(int n){
        java.util.AbstractList<Integer> divisors = new java.util.LinkedList<Integer>();
        for (int i = 1, end = n / 2; i <= end; i++)
            if (n % i == 0)
                divisors.add(i);
        divisors.add(n);
        return divisors;
    }
    
    public static boolean isPerfect(int n, java.util.AbstractList<Integer> divisors){
        int s = 1;
        java.util.Iterator<Integer> it = divisors.iterator();
        it.next();
        while (it.hasNext())
            s += it.next();
        return s == 2 * n;
    }
    
    public static void main(String[] args){
        try{
            int n = Integer.parseInt(args[0]) + 1;
            String result;
            java.util.AbstractList<Integer> divisors;
            while (!isPerfect(n, (divisors = getDivisors(n))))
                n++;
            result = n + "=";
            java.util.Iterator<Integer> it = divisors.iterator();
            n = it.next();
            while (it.hasNext()){
                result += n + "+";
                n = it.next();
            }
            System.out.println(result.substring(0, result.length() - 1));
        }
        catch (java.lang.IndexOutOfBoundsException exception){
            System.out.println("Utilizare: P7 <N>");
        }
    }
}
