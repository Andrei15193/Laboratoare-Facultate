import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets an array of all divisors of the immediate superior perfect number to n.
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns an array of all divisors of the perfect number immediately
    //      superior to n, this means that the perfect number is located on
    //      the last index of the array.
    public java.util.AbstractList<Integer> getPerfectNumberDivisors(int n){
        java.util.ArrayList<Integer> divisors = new java.util.ArrayList<Integer>();
        int p = 2, neededToBePrime, perfectNumber;
        do{
            neededToBePrime = p * 2 - 1;
            perfectNumber = p * (neededToBePrime);
            p *= 2;
        }while (!Util.isPrime(neededToBePrime) || perfectNumber <= n);
        divisors.add(1);
        for (int i = 2, end = perfectNumber / 2; i <= end; i++)
            if (perfectNumber % i == 0)
                divisors.add(i);
        divisors.add(perfectNumber);
        return divisors;
    }
    
    @Test
    public void test_getPerfectNumberDivisors(){
        java.util.AbstractList<Integer> divisors = getPerfectNumberDivisors(1);
        assertEquals(new Integer(6), divisors.get(divisors.size() - 1));
        
        divisors = getPerfectNumberDivisors(6);
        assertEquals(new Integer(28), divisors.get(divisors.size() - 1));
    }
}
