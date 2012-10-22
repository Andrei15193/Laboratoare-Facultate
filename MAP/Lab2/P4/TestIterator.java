import static org.junit.Assert.*;

import org.junit.Test;

public class TestIterator{
    @Test
    public void test_creaza(){
        ICoada cp = new CoadaCuPrioritati(new ComparatieInt());
        Iterator it = cp.iterator();
        assertFalse(it.eValid());
        cp.insereaza(1);
        it = cp.iterator();
        assertTrue(it.eValid());
    }

    @Test
    public void test_urmator(){
    	Integer unu = new Integer(1), doi = new Integer(2);
        ICoada cp = new CoadaCuPrioritati(new ComparatieInt());
        cp.insereaza(unu);
        cp.insereaza(doi);
        Iterator it = cp.iterator();
        assertTrue(it.eValid());
        assertTrue(((Integer)it.element()).compareTo(unu) == 0);
        it.urmator();
        assertTrue(((Integer)it.element()).compareTo(doi) == 0);
        it.urmator();
        assertNull(it.element());
    }
    
    @Test
    public void test_eValid(){
    	Integer unu = new Integer(1), doi = new Integer(2);
        ICoada cp = new CoadaCuPrioritati(new ComparatieInt());
        cp.insereaza(unu);
        cp.insereaza(doi);
        Iterator it = cp.iterator();
        assertTrue(it.eValid());
        it.urmator();
        assertTrue(it.eValid());
        it.urmator();
        assertFalse(it.eValid());
    }
}
