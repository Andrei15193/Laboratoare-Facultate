package test.repository;

import junit.framework.Assert;

import org.junit.Test;

import repository.BankFile;
import repository.BankRepositoryFile;
import repository.RepositoryException;
import utils.Utils;
import data.iterators.StreamIterator;
import domain.Bank;
import domain.BankException;

public class TestBankRepositoryFileLow
{
    @Test
    public void testAddLow()
    {
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            Bank bank3 = null;
            // Adding bank1
            Assert.assertTrue(repo.add(bank1));
            Assert.assertTrue(repo.contains(bank1));
            bank3 = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNotNull(bank3);
            Assert.assertTrue(bank1.equals(bank3));
            Assert.assertEquals(1, repo.size());
            // Attempting to add bank1 once again
            Assert.assertFalse(repo.add(bank1));
            Assert.assertEquals(1, repo.size());
            // Adding bank2
            Assert.assertTrue(repo.add(bank2));
            Assert.assertTrue(repo.contains(bank2));
            bank3 = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertNotNull(bank3);
            Assert.assertTrue(bank2.equals(bank3));
            Assert.assertEquals(2, repo.size());
            // Attempting to add bank2 once again
            Assert.assertFalse(repo.add(bank2));
            Assert.assertEquals(2, repo.size());
            // Testing for persistence
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Assert.assertTrue(repo.contains(bank1));
            bank3 = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNotNull(bank3);
            Assert.assertTrue(bank1.equals(bank3));
            Assert.assertTrue(repo.contains(bank2));
            bank3 = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertNotNull(bank3);
            Assert.assertTrue(bank2.equals(bank3));
            Assert.assertEquals(2, repo.size());
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testClearLow()
    {
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            // Adding banks
            Assert.assertTrue(repo.add(bank1));
            Assert.assertEquals(1, repo.size());
            Assert.assertTrue(repo.add(bank2));
            Assert.assertEquals(2, repo.size());
            Assert.assertTrue(repo.contains(bank1));
            Assert.assertTrue(repo.contains(bank2));
            Assert.assertNotNull(repo
                            .find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNotNull(repo
                            .find(TestBankRepositoryFileLow.bankNames[1]));
            repo.clear();
            // Testing if the repository is clear
            Assert.assertEquals(0, repo.size());
            Assert.assertFalse(repo.contains(bank1));
            Assert.assertFalse(repo.contains(bank2));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[1]));
            // Adding banks
            Assert.assertTrue(repo.add(bank1));
            Assert.assertTrue(repo.add(bank2));
            repo.clear();
            // Testing if the repository is clear
            Assert.assertEquals(0, repo.size());
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[1]));
            // Testing persistence
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Assert.assertEquals(0, repo.size());
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[1]));
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testContainsLow()
    {
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            // Testing on empty repository
            Assert.assertFalse(repo.contains(bank1));
            Assert.assertFalse(repo.contains(bank2));
            // Adding banks
            repo.add(bank1);
            Assert.assertTrue(repo.contains(bank1));
            Assert.assertFalse(repo.contains(bank2));
            repo.add(bank2);
            Assert.assertTrue(repo.contains(bank1));
            Assert.assertTrue(repo.contains(bank2));
            // Testing persistence
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Assert.assertTrue(repo.contains(bank1));
            Assert.assertTrue(repo.contains(bank2));
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testFindLow()
    {
        Bank found;
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            // Testing on empty repository
            found = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNull(found);
            found = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertNull(found);
            // Adding banks
            repo.add(bank1);
            found = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNotNull(found);
            Assert.assertEquals(bank1, found);
            found = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertNull(found);
            repo.add(bank2);
            found = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNotNull(found);
            Assert.assertEquals(bank1, found);
            found = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertEquals(bank2, found);
            // Testing persistence
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            found = repo.find(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertNotNull(found);
            Assert.assertEquals(bank1, found);
            found = repo.find(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertNotNull(found);
            Assert.assertEquals(bank2, found);
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testIsEmptyLow()
    {
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            Assert.assertTrue(repo.isEmpty());
            repo.add(bank1);
            Assert.assertFalse(repo.isEmpty());
            repo.clear();
            Assert.assertTrue(repo.isEmpty());
            repo.add(bank2);
            Assert.assertFalse(repo.isEmpty());
            // Testing persistence
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Assert.assertFalse(repo.isEmpty());
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testIteratorLow()
    {
        int i, count = 0;
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            Bank bank3 = new BankFile(TestBankRepositoryFileLow.bankNames[2],
                            true, 0.1);
            Bank[] banks1 = {bank1, bank2};
            // Testing on empty repo
            i = 0;
            for (Bank it: repo)
                Assert.assertEquals(banks1[i++], it);
            Assert.assertEquals(i, 0);
            // Testing with one entity in repo
            i = 0;
            Assert.assertTrue(repo.add(bank1));
            for (Bank it: repo)
                Assert.assertEquals(banks1[i++], it);
            Assert.assertEquals(i, 1);
            // Testing with two entities in repo
            Assert.assertTrue(repo.add(bank2));
            for (Bank it: repo)
            {
                i = 0;
                while (i < banks1.length && !banks1[i].equals(it))
                    i++;
                if (i == banks1.length)
                    Assert.fail("The bank: " + it + " is invalid!");
                else
                    count++;
            }
            Assert.assertEquals(count, 2);
            // Issuing a search for a element that does not exist
            {
                StreamIterator<Bank> it = repo.iterator();
                while (it.hasNext())
                    Assert.assertFalse(it.next().equals(bank3));
            }
            // Testing for persistence
            count = 0;
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            for (Bank it: repo)
            {
                i = 0;
                while (i < banks1.length && !banks1[i].equals(it))
                    i++;
                if (i == banks1.length)
                    Assert.fail("The bank: " + it + " is invalid!");
                else
                    count++;
            }
            Assert.assertEquals(count, 2);
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testRemoveLow()
    {
        BankRepositoryFile repo = null;
        TestBankRepositoryFileLow.removeTestFiles();
        try
        {
            repo = new BankRepositoryFile(
                            TestBankRepositoryFileLow.fileNames[0]);
            Bank removed;
            Bank bank1 = new BankFile(TestBankRepositoryFileLow.bankNames[0],
                            true, 0.3);
            Bank bank2 = new BankFile(TestBankRepositoryFileLow.bankNames[1],
                            true, 0.2);
            // Testing with one entity
            Assert.assertEquals(0, repo.size());
            Assert.assertNull(repo
                            .remove(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(bank1));
            Assert.assertEquals(1, repo.size());
            removed = repo.remove(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertEquals(0, repo.size());
            Assert.assertNotNull(removed);
            Assert.assertNull(repo
                            .remove(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertEquals(0, repo.size());
            Assert.assertFalse(repo.contains(bank1));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertEquals(removed, bank1);
            // Testing with two banks
            Assert.assertNull(repo
                            .remove(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNull(repo
                            .remove(TestBankRepositoryFileLow.bankNames[1]));
            Assert.assertTrue(repo.add(bank1));
            Assert.assertTrue(repo.add(bank2));
            Assert.assertEquals(2, repo.size());
            removed = repo.remove(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertEquals(1, repo.size());
            Assert.assertNotNull(removed);
            Assert.assertEquals(removed, bank1);
            Assert.assertFalse(repo.contains(bank1));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertTrue(repo.contains(bank2));
            Assert.assertTrue(repo.add(bank1));
            removed = repo.remove(TestBankRepositoryFileLow.bankNames[1]);
            Assert.assertEquals(1, repo.size());
            Assert.assertNotNull(removed);
            Assert.assertEquals(removed, bank2);
            Assert.assertFalse(repo.contains(bank2));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[1]));
            Assert.assertTrue(repo.contains(bank1));
            // Removing all banks
            removed = repo.remove(TestBankRepositoryFileLow.bankNames[0]);
            Assert.assertEquals(0, repo.size());
            Assert.assertNotNull(removed);
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[0]));
            Assert.assertNull(repo.find(TestBankRepositoryFileLow.bankNames[1]));
        }
        catch (RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestBankRepositoryFileLow.removeTestFiles();
        }
    }

    private static void removeTestFiles()
    {
        for (String fileName: TestBankRepositoryFileLow.fileNames)
            Utils.removeFile(fileName);
        for (String fileName: TestBankRepositoryFileLow.bankNames)
            Utils.removeFile(fileName);
    }

    private static final String fileNames[] = {"bankRepository.test"};
    private static final String bankNames[] = {"ING", "BT", "BCC"};
}
