import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the minimum prime number from the given array.
    // Preconditions:
    //      numbers: an array of integers
    // Postconditions:
    //      Returns the lowest prime number from the given array, if there is no prime number -1 is returned.
    //      If there are no prime numbers or the array is of length 0, -1 is returned.
    public int getLowestPrime(int[] numbers){
        int min = -1;
        for (int i = 0; i < numbers.length; i++)
            if (Util.isPrime(numbers[i]) && (numbers[i] < min || min == -1))
                 min = numbers[i];
        return min;
    }
    
    @Test
    public void test_getLowestPrime(){
        int[] numbers1 = {1, 2, 3, 4, 5};
        assertEquals(2, getLowestPrime(numbers1));
        
        int[] numbers2 = {};
        assertEquals(-1, getLowestPrime(numbers2));
        
        int[] numbers3 = {1, 4, 6, 8, 10};
        assertEquals(-1, getLowestPrime(numbers3));
    }
}
