import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the sum of all prime numbers found in the given array.
    // Preconditions:
    //      numbers: an array of integers
    // Postconditions:
    //      Returns an integer that is equal to the sum of all prime numbers found.
    //      If there are no prime numbers or the array is of length 0, 0 is returned.
    public int getPrimesSum(int[] numbers){
        int s = 0;
        for (int i = 0; i < numbers.length; i++)
            if (Util.isPrime(numbers[i]))
                s += numbers[i];
        return s;
    }
    
    @Test
    public void test_getPrimesSum(){
        int[] numbers1 = {1, 2, 3, 4, 5};
        assertEquals(10, getPrimesSum(numbers1));
        
        int[] numbers2 = {};
        assertEquals(0, getPrimesSum(numbers2));
        
        int[] numbers3 = {1, 4, 6, 8, 10};
        assertEquals(0, getPrimesSum(numbers3));
    }
}
