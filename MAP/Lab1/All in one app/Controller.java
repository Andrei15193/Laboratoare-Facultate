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

    // Gets the product of n's own factors.
    // Preconditions:
    //      n: Integer
    // Postconditions:
    //      Returns an integer that is equal with the product of n's own factors.
    //      If n has no own factors, 0 is returned.
    public int getProduct(int n){
        int p = 1;
        for (int i = 2, end = java.lang.Math.abs(n) / 2; i <= end; i++)
            if (n % i == 0)
                p *= i;
        if (p == 1)
            return 0;
        else
            return p;
    }

    @Test
    public void test_getProduct(){
        assertEquals(2, getProduct(-4));
        assertEquals(0, getProduct(-3));
        assertEquals(0, getProduct(3));
        assertEquals(2, getProduct(4));
        assertEquals(400, getProduct(20));
    }

    // Gets the mirrored number of n.
    // Preconditions:
    //      n: an Integer
    // Postconditions:
    //      Returns the mirrored number of n with the same sign as n.
    public int getMirrored(int n){
        int mirrored = 0;
        boolean negative = n < 0;
        n = java.lang.Math.abs(n);
        while (n != 0){
            mirrored = mirrored * 10 + n % 10;
            n /= 10;
        }
        if (negative)
            mirrored *= -1;
        return mirrored;
    }

    @Test
    public void test_getMirrored(){
        assertEquals(-1, getMirrored(-1));
        assertEquals(-321, getMirrored(-123));
        assertEquals(141, getMirrored(141));
        assertEquals(0, getMirrored(0));
    }

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
    
    // Gets the sum of all non prime numbers found in the given array.
    // Preconditions:
    //      numbers: an array of integers
    // Postconditions:
    //      Returns an integer that is equal to the sum of all non prime numbers found.
    //      If there are only prime numbers or the array is of length 0, 0 is returned.
    public int getNonPrimesSum(int[] numbers){
        int s = 0;
        for (int i = 0; i < numbers.length; i++)
            if (!Util.isPrime(numbers[i]))
                s += numbers[i];
        return s;
    }
    
    @Test
    public void test_getNonPrimesSum(){
        int[] numbers1 = {1, 2, 3, 4, 5};
        assertEquals(5, getNonPrimesSum(numbers1));
        
        int[] numbers2 = {};
        assertEquals(0, getNonPrimesSum(numbers2));
        
        int[] numbers3 = {2, 3, 5, 7, 11};
        assertEquals(0, getNonPrimesSum(numbers3));
    }
}
