import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents a collection of useful methods
public class Util {
    // Checks if the given number is prime
    public static boolean isPrime(int n){
        boolean p = n > 1;
        int i = 3;
        double squareRootOfN = java.lang.Math.sqrt(n);
        if (n != 2 && n%2== 0)
            p = false;
        else
            while (p && i <= squareRootOfN)
                if (n%i == 0)
                    p = false;
                else
                    i += 2;
        return p;
    }
    
    @Test
    public void test_isPrime(){
        assertEquals(false, isPrime(1));
        assertEquals(true, isPrime(2));
        assertEquals(true, isPrime(3));
        assertEquals(false, isPrime(4));
        assertEquals(true, isPrime(5));
        assertEquals(false, isPrime(6));
        assertEquals(true, isPrime(7));
        assertEquals(false, isPrime(8));
        assertEquals(false, isPrime(9));
        assertEquals(false, isPrime(10));
    }
}
