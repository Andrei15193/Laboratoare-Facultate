package test.repository;

import java.util.Date;

import junit.framework.Assert;

import org.junit.Test;

import utils.Utils;
import domain.BankException;

public class TestBankFileLow
{
    @Test
    public void testInterestLow()
    {
        TestBankFileLow.removeTestFiles();
        try
        {
            final Date date = new Date();
            repository.file.BankFile bank = null;
            Assert.assertNotNull(bank = new repository.file.BankFile(
                            TestBankFileLow.bankNames[0], true, 0.1));
            Assert.assertEquals(bank.getInterest(), 0.1);
            bank.addInterest(0.2, new Date(date.getTime() + 10000));
            Assert.assertEquals(
                            bank.getInterest(new Date(date.getTime() + 10001)),
                            0.2);
            Assert.assertEquals(bank.getInterest(), 0.1);
            Assert.assertEquals(
                            bank.getInterest(new Date(date.getTime() - 10001)),
                            0.1);
        }
        catch (BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankFileLow.removeTestFiles();
        }
    }

    private static void removeTestFiles()
    {
        for (String it: TestBankFileLow.bankNames)
            Utils.removeFile(it);
    }

    private final static String bankNames[] = {"BT.test", "ING.test"};
}
