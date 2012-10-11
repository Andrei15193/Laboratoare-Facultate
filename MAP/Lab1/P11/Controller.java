import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the product of all prime numbers found in the given array.
    // Preconditions:
    //      numbers: an array of integers
    // Postconditions:
    //      Returns an integer that is equal to the product of all prime numbers found.
    //      If there are no prime numbers or the array is of length 0, 0 is returned.
    public int getPrimesProduct(int[] numbers){
        int p = 1;
        for (int i = 0; i < numbers.length; i++)
            if (Util.isPrime(numbers[i]))
                p *= numbers[i];
        if (p == 1)
            p--;
        return p;
    }
    
    @Test
    public void test_getPrimesProduct(){
        int[] numbers1 = {1, 2, 3, 4, 5};
        assertEquals(30, getPrimesProduct(numbers1));
        
        int[] numbers2 = {};
        assertEquals(0, getPrimesProduct(numbers2));
        
        int[] numbers3 = {1, 4, 6, 8, 10};
        assertEquals(0, getPrimesProduct(numbers3));
    }
}
