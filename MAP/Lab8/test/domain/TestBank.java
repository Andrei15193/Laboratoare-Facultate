package test.domain;

import java.util.Date;

import org.junit.Assert;
import org.junit.Test;

public class TestBank
{
    @Test
    public void testGetters()
    {
        TestBankSuper bank = new TestBankSuper(TestBank.bankNames[0], true);
        Assert.assertEquals(bank.getName(), TestBank.bankNames[0]);
        Assert.assertTrue(bank.isInterestStatic());
        Assert.assertNotNull(bank = new TestBankSuper(TestBank.bankNames[1],
                        false));
        Assert.assertEquals(bank.getName(), TestBank.bankNames[1]);
        Assert.assertFalse(bank.isInterestStatic());
    }

    @Test
    public void testEquals()
    {
        final TestBankSuper bank1 = new TestBankSuper(TestBank.bankNames[0],
                        true);
        final TestBankSuper bank2 = new TestBankSuper(TestBank.bankNames[0],
                        true);
        final TestBankSuper bank3 = new TestBankSuper(TestBank.bankNames[0],
                        false);
        final TestBankSuper bank4 = new TestBankSuper(TestBank.bankNames[1],
                        false);
        final TestBankSuper bank5 = new TestBankSuper(TestBank.bankNames[1],
                        true);
        Assert.assertTrue(bank1.equals(bank2));
        Assert.assertFalse(bank1.equals(bank3));
        Assert.assertFalse(bank1.equals(bank4));
        Assert.assertFalse(bank1.equals(bank5));
    }

    private class TestBankSuper extends domain.Bank
    {
        public TestBankSuper(String name, boolean isInterestStatic)
        {
            super(name, isInterestStatic);
        }

        @Override
        public boolean addInterest(double value, Date startDate)
        {
            return false;
        }

        @Override
        public double getInterest()
        {
            return 0;
        }

        @Override
        public double getInterest(Date date)
        {
            return 0;
        }

        private static final long serialVersionUID = 1L;
    }

    private final static String bankNames[] = {"BT.test", "ING.test"};
}
