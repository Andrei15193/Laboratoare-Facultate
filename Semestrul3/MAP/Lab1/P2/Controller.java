import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets a vector of at most 2 elements representing two prime numbers that
    // summed equal the given number (Goldbach's conjecture).
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns a vector made out of two elements if there are two prime
    //      numbers that summed are equal to n. If there are no such numbers
    //      the length of the vector is 0.
    public int[] getGoldbachsNumbers(int n){
        int i = 2;
        int[] result;
        if (n < 4)
            result = new int[0];
        else{
            while (i <= n/2 && (!Util.isPrime(i) || !Util.isPrime(n - i)))
                i++;
            if (i > n/2)
                result = new int[0];
            else{
                result = new int[2];
                result[0] = i;
                result[1] = n - i;
            }
        }
        return result;
    }

    @Test
    public void test_getGoldbachsNumbers(){
        int [] result = getGoldbachsNumbers(1);
        assertEquals(0, result.length);

        result = getGoldbachsNumbers(2);
        assertEquals(0, result.length);

        result = getGoldbachsNumbers(3);
        assertEquals(0, result.length);

        result = getGoldbachsNumbers(4);
        assertEquals(2, result.length);
        assertEquals(2, result[0]);
        assertEquals(2, result[1]);

        result = getGoldbachsNumbers(5);
        assertEquals(2, result.length);
        assertEquals(2, result[0]);
        assertEquals(3, result[1]);

        result = getGoldbachsNumbers(14);
        assertEquals(2, result.length);
        assertEquals(3, result[0]);
        assertEquals(11, result[1]);

        result = getGoldbachsNumbers(200);
        assertEquals(2, result.length);
        assertEquals(3, result[0]);
        assertEquals(197, result[1]);
    }
}
