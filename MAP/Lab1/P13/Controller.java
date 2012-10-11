import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the maximum prime number from the given array.
    // Preconditions:
    //      numbers: an array of integers
    // Postconditions:
    //      Returns the highest prime number from the given array, if there is no prime number -1 is returned.
    //      If there are no prime numbers or the array is of length 0, -1 is returned.
    public int getHighestPrime(int[] numbers){
        int max = -1;
        for (int i = 0; i < numbers.length; i++)
            if (Util.isPrime(numbers[i]) && max < numbers[i])
                 max = numbers[i];
        return max;
    }
    
    @Test
    public void test_getHighestPrime(){
        int[] numbers1 = {1, 2, 3, 4, 5};
        assertEquals(5, getHighestPrime(numbers1));
        
        int[] numbers2 = {};
        assertEquals(-1, getHighestPrime(numbers2));
        
        int[] numbers3 = {1, 4, 6, 8, 10};
        assertEquals(-1, getHighestPrime(numbers3));
    }
}
