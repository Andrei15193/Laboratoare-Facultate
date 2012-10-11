import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the first number greater than n in the Fibonacci array.
    // NOTE! The Fibonacci array, for this method, starts with 1!
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns the smallest number in the Fibonacci array that is grater than n.
    public int getGreaterFibonacci(int n){
        int a = 1, b = 1, c;
        if (n > 0)
            while (b <= n){
                c = a + b;
                a = b;
                b = c;
            }
        return b;
    }

    @Test
    public void test_getGreaterFibonacci(){
        assertEquals(1, getGreaterFibonacci(-1));
        assertEquals(1, getGreaterFibonacci(0));
        assertEquals(2, getGreaterFibonacci(1));
        assertEquals(5, getGreaterFibonacci(4));
    }
}
