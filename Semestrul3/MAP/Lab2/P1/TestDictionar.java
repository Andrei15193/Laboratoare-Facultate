import static org.junit.Assert.*;

import org.junit.Test;

public class TestDictionar {
    @Test
    public void test_adauga(){
        Dictionar d = new Dictionar();

        assertTrue(d.adauga("cheie", 1));
        assertEquals(1, (int)d.element("cheie"));
        assertFalse(d.adauga("cheie", 2));
        assertEquals(1, (int)d.element("cheie"));

        assertTrue(d.adauga("cheie2", 2));
        assertEquals(2, (int)d.element("cheie2"));
        assertFalse(d.adauga("cheie2", 2));
        assertEquals(2, (int)d.element("cheie2"));
    }
    
    @Test
    public void test_elimina(){
        Dictionar d = new Dictionar();
        
        assertEquals(null, d.elimina("cheie"));
        d.adauga("cheie1", 1);
        d.adauga("cheie2", 2);
        d.adauga("cheie3", 3);
        d.adauga("cheie4", 4);
        assertEquals(1, (int)d.elimina("cheie1"));
        assertEquals(null, d.elimina("cheie1"));
        assertEquals(2, (int)d.elimina("cheie2"));
        assertEquals(null, d.elimina("cheie2"));
        assertEquals(3, (int)d.elimina("cheie3"));
        assertEquals(null, d.elimina("cheie3"));
        assertEquals(4, (int)d.elimina("cheie4"));
        assertEquals(null, d.elimina("cheie4"));
        assertEquals(null, d.elimina("cheie"));
    }
    
    @Test
    public void test_element(){
        Dictionar d = new Dictionar();
        
        assertEquals(null, d.element("cheie"));
        d.adauga("cheie1", 1);
        d.adauga("cheie2", 2);
        d.adauga("cheie3", 3);
        d.adauga("cheie4", 4);
        
        assertEquals(1, (int)d.element("cheie1"));
        assertEquals(2, (int)d.element("cheie2"));
        assertEquals(3, (int)d.element("cheie3"));
        assertEquals(4, (int)d.element("cheie4"));
        
        assertEquals(1, (int)d.element("cheie1"));
        assertEquals(2, (int)d.element("cheie2"));
        assertEquals(3, (int)d.element("cheie3"));
        assertEquals(4, (int)d.element("cheie4"));
    }
    
    @Test
    public void test_contine(){
        Dictionar d = new Dictionar();
        
        assertFalse(d.contine("cheie", 1));
        d.adauga("cheie1", 1);
        d.adauga("cheie2", 2);
        d.adauga("cheie3", 3);
        d.adauga("cheie4", 4);
        
        assertTrue(d.contine("cheie1", 1));
        assertTrue(d.contine("cheie2", 2));
        assertTrue(d.contine("cheie3", 3));
        assertTrue(d.contine("cheie4", 4));
    }
    
    @Test
    public void test_dimensiune(){
        Dictionar d = new Dictionar();
        
        assertEquals(0, d.dimensiune());
        d.adauga("cheie1", 1);
        assertEquals(1, d.dimensiune());
        d.adauga("cheie2", 2);
        assertEquals(2, d.dimensiune());
        d.adauga("cheie3", 3);
        assertEquals(3, d.dimensiune());
        d.adauga("cheie4", 4);
        assertEquals(4, d.dimensiune());
        d.elimina("cheie4");
        assertEquals(3, d.dimensiune());
        d.elimina("cheie3");
        assertEquals(2, d.dimensiune());
        d.elimina("cheie2");
        assertEquals(1, d.dimensiune());
        d.elimina("cheie1");
        assertEquals(0, d.dimensiune());
    }
    
    @Test
    public void test_eVid(){
        Dictionar d = new Dictionar();
        
        assertTrue(d.eVid());
        d.adauga("cheie", 1);
        assertFalse(d.eVid());
        d.elimina("cheie");
        assertTrue(d.eVid());
    }
    
    @Test
    public void test_iterator(){
        int j;
        int[] date = {1, 2, 3, 4};
        Dictionar d = new Dictionar();
        Iterator it = d.iterator();
        
        assertFalse(it.eValid());
        
        d.adauga("cheie1", 1);
        d.adauga("cheie2", 2);
        d.adauga("cheie3", 3);
        d.adauga("cheie4", 4);
        
        for (it = d.iterator(); it.eValid(); it.urmator()){
            j = 0;
            while (j < date.length && date[j] != (int)it.element())
                j++;
            if (j == date.length)
                fail("Dictionarul contine o valoare necorespunzatoare: " + it.element());
        }
    }
}
