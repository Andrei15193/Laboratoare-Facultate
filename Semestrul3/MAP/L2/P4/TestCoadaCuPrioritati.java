import org.junit.Test;
import static org.junit.Assert.*;

public class TestCoadaCuPrioritati{
    @Test
    public void test_insereaza(){
        CoadaCuPrioritati cp = new CoadaCuPrioritati(new ComparatieInt());
        cp.insereaza(2);
        assertEquals(2, cp.element());
        assertEquals(1, cp.dimensiune());
        cp.insereaza(1);
        assertEquals(1, cp.element());
        assertEquals(2, cp.dimensiune());
        cp.insereaza(3);
        assertEquals(1, cp.element());
        assertEquals(3, cp.dimensiune());
    }

    @Test
    public void test_elimina(){
        CoadaCuPrioritati cp = new CoadaCuPrioritati(new ComparatieInt());
        assertEquals(null, cp.elimina());
        cp.insereaza(10);
        assertTrue(((Integer)cp.elimina()).compareTo(10) == 0);
        assertEquals(0, cp.dimensiune());

        cp.insereaza(10);
        cp.insereaza(12);
        cp.insereaza(8);
        assertTrue(((Integer)cp.elimina()).compareTo(8) == 0);
    }
    
    @Test
    public void test_element(){
        CoadaCuPrioritati cp = new CoadaCuPrioritati(new ComparatieInt());
        assertNull(cp.element());
        cp.insereaza(10);
	    assertTrue(((Integer)cp.element()).compareTo(10) == 0);
	    assertEquals(1, cp.dimensiune());

	    cp.insereaza(10);
	    cp.insereaza(12);
	    cp.insereaza(8);
	    assertTrue(((Integer)cp.element()).compareTo(8) == 0);
    }

    @Test
    public void test_dimensiune(){
        CoadaCuPrioritati cp = new CoadaCuPrioritati(new ComparatieInt());
        assertEquals(0, cp.dimensiune());
        cp.elimina();
        assertEquals(0, cp.dimensiune());
        cp.insereaza(10);
        assertEquals(1, cp.dimensiune());
        cp.insereaza(12);
        assertEquals(2, cp.dimensiune());
        cp.elimina();
        assertEquals(1, cp.dimensiune());
    }

    @Test
    public void test_eVida(){
        CoadaCuPrioritati cp = new CoadaCuPrioritati(new ComparatieInt());
        assertEquals(true, cp.eVida());
        cp.insereaza(10);
        assertEquals(false, cp.eVida());
        cp.elimina();
        assertEquals(true, cp.eVida());
    }
}
