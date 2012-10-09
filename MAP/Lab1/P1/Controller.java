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
}
