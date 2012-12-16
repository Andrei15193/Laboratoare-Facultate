package test.repository;

import java.io.File;

import junit.framework.Assert;

import org.junit.Test;

import repository.BankRepository;
import repository.RepositoryException;
import repository.file.BankFile;
import repository.file.BankRepositoryFile;
import controller.EntityReader;
import domain.Bank;

public class TestBankRepositoryFile
{
    private static final String testFile = "bankTestFile.test";
    private static final String[] testBankNames = new String[] {"bank0.test",
                    "bank1.test", "bank2.test", "bank3.test", "bank4.test",
                    "bank5.test", "bank6.test", "bank7.test", "bank8.test",
                    "bank9.test"};

    private static void removeTestFiles()
    {
        File file = null;
        try
        {
            file = new File(TestBankRepositoryFile.testFile);
            file.delete();
        }
        catch (Exception e)
        {
        }
        for (String fileName: TestBankRepositoryFile.testBankNames)
            try
            {
                file = new File(fileName);
                file.delete();
            }
            catch (Exception e)
            {
            }
    }

    @Test
    public void testAdd()
    {
        TestBankRepositoryFile.removeTestFiles();
        BankRepository repo = new BankRepositoryFile(
                        TestBankRepositoryFile.testFile);
        try
        {
            // First Bank
            try
            {
                repo.add(new BankFile(TestBankRepositoryFile.testBankNames[0],
                                true, 0.1));
            }
            catch (RepositoryException e)
            {
                Assert.fail(e.getMessage());
            }
            try
            {
                Bank bank = repo.find(TestBankRepositoryFile.testBankNames[0]);
                if (bank == null)
                    Assert.fail("the bank was not memorized!");
                Assert.assertEquals(bank.getName(),
                                TestBankRepositoryFile.testBankNames[0]);
                Assert.assertEquals(bank.getInterest(), 0.1);
                Assert.assertEquals(bank.getStaticInterest(), true);
            }
            catch (RepositoryException e)
            {
                Assert.fail(e.getMessage());
            }
            // Second bank.
            try
            {
                repo.add(new BankFile(TestBankRepositoryFile.testBankNames[1],
                                true, 0.1));
                EntityReader<Bank> reader = repo.getReader();
                while (reader.next())
                    System.out.println(reader.getCurrentEntity().getName());
            }
            catch (RepositoryException e)
            {
                Assert.fail(e.getMessage());
            }
            try
            {
                Bank bank = repo.find(TestBankRepositoryFile.testBankNames[1]);
                if (bank == null)
                    Assert.fail("the bank was not memorized!");
                Assert.assertEquals(bank.getName(),
                                TestBankRepositoryFile.testBankNames[1]);
                Assert.assertEquals(bank.getInterest(), 0.1);
                Assert.assertEquals(bank.getStaticInterest(), true);
            }
            catch (RepositoryException e)
            {
                Assert.fail(e.getMessage());
            }
        }
        finally
        {
            TestBankRepositoryFile.removeTestFiles();
        }
    }
}
