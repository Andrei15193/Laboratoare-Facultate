import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the greatest and less than n prime number.
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns the highest prime number that is less than n if it exists. If
    //      no such prime number exists (n <= 2) -1 is returned.
    public int getPreviousPrime(int n){
        if (n <= 2)
            n = -1;
        else
            do
                n--;
            while (!Util.isPrime(n));
        return n;
    }
    
    @Test
    public void test_getPreviousPrime(){
        assertEquals(-1, getPreviousPrime(2));
        assertEquals(2, getPreviousPrime(3));
        assertEquals(3, getPreviousPrime(5));
        assertEquals(5, getPreviousPrime(6));
    }
}
