package proiect.test.domain;

import org.junit.Assert;
import org.junit.Test;

import proiect.domain.Firm;
import proiect.domain.FirmException;

public class TestFirm
{
    @Test
    public void testCreate()
    {
        try
        {
            Firm firm = new Firm("ING", 1);
            Assert.assertTrue(firm.equals(new Firm("ING", 1)));
            Assert.assertEquals(firm.toString(), "ING(1)");
        }
        catch (FirmException exception)
        {
            Assert.fail("ING 1 firm is valid!");
        }
        try
        {
            new Firm("Banca-Transilvania", 1);
        }
        catch (FirmException exception)
        {
            Assert.fail("Banca-Transilvania 1 firm is valid!");
        }
        try
        {
            new Firm("Banca Transilvania", 1);
        }
        catch (FirmException exception)
        {
            Assert.fail("Banca Transilvania 1 firm is valid!");
        }
        try
        {
            new Firm("ING", null);
            Assert.fail("Invalid turnover!");
        }
        catch (FirmException exception)
        {
        }
        try
        {
            new Firm("ING$", 1);
            Assert.fail("Invalid name!");
        }
        catch (FirmException exception)
        {
        }
        try
        {
            new Firm("", 1);
            Assert.fail("Invalid name!");
        }
        catch (FirmException exception)
        {
        }
        try
        {
            new Firm(null, 1);
            Assert.fail("Invalid name!");
        }
        catch (FirmException exception)
        {
        }
    }

    @Test
    public void testGetSetName()
    {
        Firm firm = null;
        try
        {
            firm = new Firm("ING", 1);
            Assert.assertEquals("ING", firm.getName());
            firm.setName("BT");
            Assert.assertEquals("BT", firm.getName());
            try
            {
                firm.setName("BT$");
                Assert.fail("BT$ name is invalid!");
            }
            catch (FirmException exception)
            {
            }
            try
            {
                firm.setName(null);
                Assert.fail("null name is invalid!");
            }
            catch (FirmException exception)
            {
            }
        }
        catch (FirmException exception)
        {
            Assert.fail("ING 1 firm is valid!");
        }
    }

    @Test
    public void testGetSetTurnover()
    {
        Firm firm = null;
        try
        {
            firm = new Firm("ING", 1);
            Assert.assertEquals(new Integer(1), firm.getTurnover());
            firm.setTurnover(2);
            Assert.assertEquals(new Integer(2), firm.getTurnover());
            try
            {
                firm.setTurnover(null);
                Assert.fail("null turnover is invalid!");
            }
            catch (FirmException exception)
            {
            }
        }
        catch (FirmException exception)
        {
            Assert.fail("ING 1 firm is valid!");
        }
    }
}
