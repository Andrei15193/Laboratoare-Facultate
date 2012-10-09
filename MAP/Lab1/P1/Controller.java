import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Returns the first prime number after the given number.
    // Preconditions:
    //      n: an Integer
    // Postcondition:
    //      Returns the first prime number after n
    public int getFirstPrimeAfterN(int n){
        if (n < 2)
            return 2;
        else{
            do
                n++;
            while (!Util.isPrime(n));
            return n;
        }
    }
    
    @Test
    public void test_getFirstPrimeAfterN(){
        assertEquals(2, getFirstPrimeAfterN(-1));
        assertEquals(2, getFirstPrimeAfterN(0));
        assertEquals(2, getFirstPrimeAfterN(1));
        assertEquals(3, getFirstPrimeAfterN(2));
        assertEquals(5, getFirstPrimeAfterN(3));
        assertEquals(5, getFirstPrimeAfterN(4));
        assertEquals(7, getFirstPrimeAfterN(5));
    }
}
