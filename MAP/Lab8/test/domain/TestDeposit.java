package test.domain;

import java.util.Calendar;
import java.util.Date;

import junit.framework.Assert;

import org.junit.Test;

import domain.Deposit;

public class TestDeposit
{
    @Test
    public void testGetters()
    {
        final Deposit depo = new domain.Deposit("ING", "123", 1000, true);
        Assert.assertEquals("ING", depo.getBankName());
        Assert.assertEquals("123", depo.getPersonId());
        Assert.assertEquals(1000.0, depo.getSum());
        Assert.assertTrue(depo.isAutomaticCapitalisation());
        Assert.assertEquals(depo.getCreationDate(), depo.getLastUpdate());
    }

    @Test
    public void testAddInterest()
    {
        final Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        cal.add(Calendar.MONTH, 1);
        Date date = cal.getTime();
        final Deposit depo = new domain.Deposit("ING", "123", 1000, true);
        Assert.assertTrue(depo.addInterest(0.1, date));
        Assert.assertEquals(1100.0, depo.getSum());
        Assert.assertFalse(depo.addInterest(0.1, date));
        Assert.assertEquals(1100.0, depo.getSum());
    }

    @Test
    public void testClone()
    {
        final Deposit depo = new domain.Deposit("ING", "123", 1000, true);
        Deposit clone = domain.Deposit.cloneDeposit(depo);
        Assert.assertEquals(depo.getBankName(), clone.getBankName());
        Assert.assertEquals(depo.getPersonId(), clone.getPersonId());
        Assert.assertEquals(depo.getSum(), clone.getSum());
        Assert.assertEquals(depo.getLastUpdate(), clone.getLastUpdate());
        Assert.assertEquals(depo.getCreationDate(), clone.getCreationDate());
        Assert.assertEquals(depo.isAutomaticCapitalisation(),
                        clone.isAutomaticCapitalisation());
        clone = domain.Deposit.cloneDeposit(depo, false);
        Assert.assertEquals(depo.getBankName(), clone.getBankName());
        Assert.assertEquals(depo.getPersonId(), clone.getPersonId());
        Assert.assertEquals(depo.getSum(), clone.getSum());
        Assert.assertEquals(depo.getLastUpdate(), clone.getLastUpdate());
        Assert.assertEquals(depo.getCreationDate(), clone.getCreationDate());
        Assert.assertFalse(clone.isAutomaticCapitalisation());
        clone = domain.Deposit.cloneDeposit(depo, true);
        Assert.assertEquals(depo.getBankName(), clone.getBankName());
        Assert.assertEquals(depo.getPersonId(), clone.getPersonId());
        Assert.assertEquals(depo.getSum(), clone.getSum());
        Assert.assertEquals(depo.getLastUpdate(), clone.getLastUpdate());
        Assert.assertEquals(depo.getCreationDate(), clone.getCreationDate());
        Assert.assertTrue(clone.isAutomaticCapitalisation());
    }
}
