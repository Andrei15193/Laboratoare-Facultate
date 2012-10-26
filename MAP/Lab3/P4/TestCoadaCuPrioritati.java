import static org.junit.Assert.*;

import org.junit.Test;

public class TestCoadaCuPrioritati {
    @Test
    public void test_insereaza_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        assertEquals(3, (int)cp.element());
        
        cp.insereaza(1);
        assertEquals(2, cp.dimensiune());
        assertEquals(1, (int)cp.element());
        
        cp.insereaza(5);
        assertEquals(3, cp.dimensiune());
        assertEquals(1, (int)cp.element());
        
        cp.insereaza(0);
        assertEquals(4, cp.dimensiune());
        assertEquals(0, (int)cp.element());
        
        cp.insereaza(0);
        assertEquals(5, cp.dimensiune());
        assertEquals(0, (int)cp.element());
    }

    @Test
    public void test_elimina_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        assertEquals(null, cp.elimina());
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        assertEquals(3, (int)cp.elimina());
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        assertEquals(3, (int)cp.elimina());
        assertEquals(0, cp.dimensiune());

        cp.insereaza(3);
        cp.insereaza(1);
        cp.insereaza(5);
        assertEquals(3, cp.dimensiune());
        assertEquals(1, (int)cp.elimina());
        assertEquals(2, cp.dimensiune());
        assertEquals(3, (int)cp.elimina());
        assertEquals(1, cp.dimensiune());
        assertEquals(5, (int)cp.elimina());
        assertEquals(0, cp.dimensiune());
        assertEquals(null, cp.elimina());
        assertEquals(0, cp.dimensiune());
    }
    
    @Test
    public void test_element_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        assertEquals(null, cp.element());
        
        cp.insereaza(1);
        assertEquals(1, cp.dimensiune());
        assertEquals(1, (int)cp.element());
        
        cp.elimina();
        assertEquals(0, cp.dimensiune());
        assertEquals(null, cp.element());
        
        cp.insereaza(3);
        cp.insereaza(1);
        cp.insereaza(2);
        assertEquals(3, cp.dimensiune());
        assertEquals(1, (int)cp.element());
    }
    
    @Test
    public void test_eVida_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        assertTrue(cp.eVida());
        
        cp.insereaza(1);
        assertEquals(1, cp.dimensiune());
        assertFalse(cp.eVida());
        
        cp.elimina();
        assertEquals(0, cp.dimensiune());
        assertTrue(cp.eVida());
        
        cp.insereaza(3);
        cp.insereaza(1);
        cp.insereaza(2);
        assertEquals(3, cp.dimensiune());
        assertFalse(cp.eVida());
    }
    
    @Test
    public void test_iterator_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        Iterator<Integer> it = cp.iterator();
        assertNotNull(it);
        assertNull(it.element());
        assertFalse(it.eValid());
        
        cp.insereaza(1);
        it = cp.iterator();
        assertNotNull(it);
        assertTrue(it.eValid());
        assertEquals(1, (int)it.element());
        it.urmator();
        assertNull(it.element());
        assertFalse(it.eValid());
        
        cp.insereaza(5);
        cp.insereaza(0);
        it = cp.iterator();
        int[] date = {0, 1, 5};
        for (int i = 0; i < date.length; i++, it.urmator())
            assertEquals(date[i], (int)it.element());
        assertNull(it.element());
        assertFalse(it.eValid());
    }
}
