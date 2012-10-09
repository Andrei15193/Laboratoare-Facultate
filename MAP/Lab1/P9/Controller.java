import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets an array of all divisors of the highest perfect number that is lower
    // than n.
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns an array of all divisors of the highest perfect number that is
    //      lower than n if it exists, otherwise an empty array.
    public java.util.AbstractList<Integer> getPreviousPerfectNumberDivisors(int n){
        java.util.ArrayList<Integer> divisors = new java.util.ArrayList<Integer>();
        int p = 2, neededToBePrime, perfectNumber, previousPerfectNumber = 6;
        boolean foundPerfectNumber = false;
        if (n > 6){
            do{
                neededToBePrime = p * 2 - 1;
                perfectNumber = p * (neededToBePrime);
                if (Util.isPrime(neededToBePrime)){
                    if (perfectNumber < n)
                        previousPerfectNumber = perfectNumber;
                    foundPerfectNumber = true;
                }
                else
                    foundPerfectNumber = false;
                p *= 2;
            }while (!foundPerfectNumber || perfectNumber <= n);
            divisors.add(1);
            for (int i = 2, end = previousPerfectNumber / 2; i <= end; i++)
                if (previousPerfectNumber % i == 0)
                    divisors.add(i);
            divisors.add(previousPerfectNumber);
        }
        return divisors;
    }

    @Test
    public void test_getPreviousPerfectNumber(){
        java.util.AbstractList<Integer> divisors = getPreviousPerfectNumberDivisors(6);
        assertEquals(0, divisors.size());

        divisors = getPreviousPerfectNumberDivisors(7);
        assertEquals(new Integer(6), divisors.get(divisors.size() - 1));

        divisors = getPreviousPerfectNumberDivisors(28);
        assertEquals(new Integer(6), divisors.get(divisors.size() - 1));
    }
}
