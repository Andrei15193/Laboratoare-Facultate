import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
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
}
