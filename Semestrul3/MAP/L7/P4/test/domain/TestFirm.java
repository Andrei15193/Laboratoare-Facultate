package test.domain;

import static org.junit.Assert.*;
import org.junit.Test;

public class TestFirm{
    @Test
    public void testCreate(){
        try{
            domain.Firm firm = new domain.Firm("ING", 1);
            assertTrue(firm.equals(new domain.Firm("ING", 1)));
            assertEquals(firm.toString(), "ING(1)");
        }
        catch (domain.FirmException exception){
            fail("ING 1 firm is valid!");
        }

        try{
            new domain.Firm("Banca-Transilvania", 1);
        }
        catch (domain.FirmException exception){
            fail("Banca-Transilvania 1 firm is valid!");
        }

        try{
            new domain.Firm("Banca Transilvania", 1);
        }
        catch (domain.FirmException exception){
            fail("Banca Transilvania 1 firm is valid!");
        }
        
        try{
            new domain.Firm("ING", null);
            fail("Invalid turnover!");
        }
        catch (domain.FirmException exception){
        }
        
        try{
            new domain.Firm("ING$", 1);
            fail("Invalid name!");
        }
        catch (domain.FirmException exception){
        }
        
        try{
            new domain.Firm("", 1);
            fail("Invalid name!");
        }
        catch (domain.FirmException exception){
        }
        
        try{
            new domain.Firm(null, 1);
            fail("Invalid name!");
        }
        catch (domain.FirmException exception){
        }
    }
    
    @Test
    public void testGetSetName(){
        domain.Firm firm = null;
        try{
            firm = new domain.Firm("ING", 1);
            assertEquals("ING", firm.getName());
            firm.setName("BT");
            assertEquals("BT", firm.getName());
            try{
                firm.setName("BT$");
                fail("BT$ name is invalid!");
            }
            catch (domain.FirmException exception){
            }
            try{
                firm.setName(null);
                fail("null name is invalid!");
            }
            catch (domain.FirmException exception){
            }
        }
        catch (domain.FirmException exception){
            fail("ING 1 firm is valid!");
        }
    }
    
    @Test
    public void testGetSetTurnover(){
        domain.Firm firm = null;
        try{
            firm = new domain.Firm("ING", 1);
            assertEquals(new Integer(1), firm.getTurnover());
            firm.setTurnover(2);
            assertEquals(new Integer(2), firm.getTurnover());
            try{
                firm.setTurnover(null);
                fail("null turnover is invalid!");
            }
            catch (domain.FirmException exception){
            }
        }
        catch (domain.FirmException exception){
            fail("ING 1 firm is valid!");
        }
    }
}
