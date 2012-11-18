import static org.junit.Assert.*;

import org.junit.Test;

public class TestCoadaCuPrioritati {
    @Test
    public void test_insereaza_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        try{
            assertEquals(3, (int)cp.element());
        }
        catch(TADException ex){
            fail("Cel mai prioritar element este 3!");
        }
        
        cp.insereaza(1);
        assertEquals(2, cp.dimensiune());
        try{
            assertEquals(1, (int)cp.element());
        }
        catch (TADException ex){
            fail("Cel mai prioritar element este 1!");
        }
        
        cp.insereaza(5);
        assertEquals(3, cp.dimensiune());
        try{
            assertEquals(1, (int)cp.element());
        }
        catch (TADException ex){
            fail("Cel mai prioritar element este 1!");
        }
        
        cp.insereaza(0);
        assertEquals(4, cp.dimensiune());
        try{
            assertEquals(0, (int)cp.element());
        }
        catch (TADException ex){
            fail("Cel mai prioritar element este 0!");
        }
        
        cp.insereaza(0);
        assertEquals(5, cp.dimensiune());
        try{
            assertEquals(0, (int)cp.element());
        }
        catch (TADException ex){
            fail("Cel mai prioritar element este 0!");
        }
    }

    @Test
    public void test_elimina_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        try{
            assertEquals(null, cp.elimina());
            fail("Coada cu prioritati este vida!");
        }
        catch (TADException ex){
        }
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        try{
            assertEquals(3, (int)cp.elimina());
        }
        catch (TADException ex){
            fail("Elementul eliminat este 3!");
        }
        assertEquals(0, cp.dimensiune());
        
        cp.insereaza(3);
        assertEquals(1, cp.dimensiune());
        try{
            assertEquals(3, (int)cp.elimina());
        }
        catch (TADException ex){
            fail("Elementul eliminat este 3!");
        }
        assertEquals(0, cp.dimensiune());

        cp.insereaza(3);
        cp.insereaza(1);
        cp.insereaza(5);
        assertEquals(3, cp.dimensiune());
        try{
            assertEquals(1, (int)cp.elimina());
        }
        catch (TADException ex){
            fail("Elementul eliminat este 1!");
        }
        assertEquals(2, cp.dimensiune());
        try{
            assertEquals(3, (int)cp.elimina());
        }
        catch (TADException ex){
            fail("Elementul eliminat este 3!");
        }
        assertEquals(1, cp.dimensiune());
        try{
            assertEquals(5, (int)cp.elimina());
        }
        catch (TADException ex){
            fail("Elementul eliminat este 5!");
        }
        assertEquals(0, cp.dimensiune());
        try{
            assertEquals(null, cp.elimina());
            fail("Coada cu prioritati este vida! Nu se pot elimina elemente!");
        }
        catch (TADException ex){
        }
        assertEquals(0, cp.dimensiune());
    }
    
    @Test
    public void test_element_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        try{
            assertEquals(null, cp.element());
            fail("Coada cu prioritati este vida! Nu exista un cel mai prioritar element!");
        }
        catch (TADException ex){
        }
        
        cp.insereaza(1);
        assertEquals(1, cp.dimensiune());
        
        try{
            assertEquals(1, (int)cp.element());
        }
        catch (TADException ex){
            fail("Cel mai prioritar element este 1!");
        }
        
        try{
            cp.elimina();
        }
        catch (TADException ex){
            fail("Coada cu prioritati nu este vida!");
        }
        assertEquals(0, cp.dimensiune());
        try{
            assertEquals(null, cp.element());
            fail("Coada cu prioritati este vida! Nu exista un cel mai prioritar element!");
        }
        catch (TADException ex){
        }
        
        cp.insereaza(3);
        cp.insereaza(1);
        cp.insereaza(2);
        assertEquals(3, cp.dimensiune());
        try{
            assertEquals(1, (int)cp.element());
        }
        catch (TADException ex){
            fail("Coada cu prioritati nu este vida! Cel mai prioritar element este 1!");
        }

    }
    
    @Test
    public void test_eVida_integer(){
        CoadaCuPrioritati<Integer> cp = new CoadaCuPrioritati<Integer>();
        assertEquals(0, cp.dimensiune());
        assertTrue(cp.eVida());
        
        cp.insereaza(1);
        assertEquals(1, cp.dimensiune());
        assertFalse(cp.eVida());

        try{
            cp.elimina();
        }
        catch (TADException ex){
            fail("Coada cu prioritati nu este vida!");
        }
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
        try{
            assertNull(it.element());
            fail("Coada cu prioritati este vida! Nu paote fi iterata!");
        }
        catch (IteratorException ex){
        }
        assertFalse(it.eValid());
        
        cp.insereaza(1);
        it = cp.iterator();
        assertNotNull(it);
        assertTrue(it.eValid());
        try{
            assertEquals(1, (int)it.element());
            it.urmator();
        }
        catch (IteratorException ex){
            fail("Iteratorul este valid!");
        }
        try{
            assertNull(it.element());
            fail("Iteratorul este invalid!");
        }
        catch (IteratorException ex){
        }
        assertFalse(it.eValid());
        
        cp.insereaza(5);
        cp.insereaza(0);
        it = cp.iterator();
        int[] date = {0, 1, 5};
        try{
        for (int i = 0; i < date.length; i++, it.urmator())
            assertEquals(date[i], (int)it.element());
        }
        catch (IteratorException ex){
            fail("Iteratorul este valid pe durata iterarii!");
        }
        try{
            assertNull(it.element());
            fail("Iteratorul este invalid!");
        }
        catch (IteratorException ex){
        }
        assertFalse(it.eValid());
    }
}
