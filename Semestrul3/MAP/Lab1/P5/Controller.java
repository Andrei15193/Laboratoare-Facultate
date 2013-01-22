import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
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
}
