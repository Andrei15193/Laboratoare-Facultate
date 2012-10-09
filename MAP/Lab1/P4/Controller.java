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

    // Gets the age of a person in days.
    // Preconditions:
    //      birthDate: a Date that represents the birth dare of the person.
    // Postconditions:
    //      Returns an integer representing the number of days if the given
    //      birth date predates the current date, -1 otherwise.
    public int getAgeInDays(java.util.Date birthDate){
        java.util.Date today = new java.util.Date();
        if (today.after(birthDate))
            return (int)((today.getTime() - birthDate.getTime()) / (1000 * 3600 * 24));
        else
            return -1;
    }

    @Test
    public void test_getAgeInDays(){
        try{
            assertEquals(-1, getAgeInDays((new java.text.SimpleDateFormat("dd/MM/yyyy")).parse("1/1/9999")));
            assertEquals(true, -1 != getAgeInDays((new java.text.SimpleDateFormat("dd/MM/yyyy")).parse("1/1/2000")));
        }
        catch (java.text.ParseException exception){
            assertEquals(true, false);
        }
    }

    // Gets the first two twin prime numbers that are greater than a give number.
    // For two prime numbers to be twin their absolute difference must be 2.
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns a vector of two integers that are twin primes.
    public int[] getFirstTwinPrimes(int n){
        boolean notFound = true;
        int firstPrime = 0;
        int[] result = {3, 5};
        if (n >= 3){
            if (n % 2 == 0)
                n--;
            do{
                n += 2;
                if (Util.isPrime(n))
                    if (firstPrime != 0 && n - firstPrime == 2)
                        notFound = false;
                    else
                        firstPrime = n;
            }while (notFound);
            result[0] = firstPrime;
            result[1] = n;
        }
        return result;
    }
    
    @Test
    public void test_getFirstTwinPrimes(){
        int[] result = getFirstTwinPrimes(2);
        assertEquals(3, result[0]);
        assertEquals(5, result[1]);
        
        result = getFirstTwinPrimes(3);
        assertEquals(5, result[0]);
        assertEquals(7, result[1]);
        
        result = getFirstTwinPrimes(4);
        assertEquals(5, result[0]);
        assertEquals(7, result[1]);
    }
}
