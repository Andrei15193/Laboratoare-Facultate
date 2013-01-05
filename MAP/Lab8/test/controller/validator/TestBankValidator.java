package test.controller.validator;

import junit.framework.Assert;

import org.junit.Test;

import repository.file.BankFile;
import utils.Utils;
import controller.validator.BankValidator;
import domain.Bank;
import domain.BankException;

public class TestBankValidator
{
    @Test
    public void testInterestValidation()
    {
        Bank bank;
        BankValidator validator = new BankValidator();
        TestBankValidator.removeTestFiles();
        try
        {
            bank = new BankFile(TestBankValidator.bankName, true, 0.3);
            Assert.assertFalse(validator.validate(bank).iterator().hasNext());
            bank = new BankFile(TestBankValidator.bankName, true, 0);
            Assert.assertTrue(validator.validate(bank).iterator().hasNext());
            bank = new BankFile(TestBankValidator.bankName, true, 1);
            Assert.assertTrue(validator.validate(bank).iterator().hasNext());
            bank = new BankFile(TestBankValidator.bankName, true, -1);
            Assert.assertTrue(validator.validate(bank).iterator().hasNext());
            bank = new BankFile(TestBankValidator.bankName, true, 2);
            Assert.assertTrue(validator.validate(bank).iterator().hasNext());
        }
        catch (BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankValidator.removeTestFiles();
        }
    }

    private static void removeTestFiles()
    {
        Utils.removeFile(TestBankValidator.bankName);
    }

    private static final String bankName = "ING";
}
