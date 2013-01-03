package test.repository;

import junit.framework.Assert;

import org.junit.Test;

import repository.DepositRepositoryFile;
import repository.RepositoryException;
import utils.Pair;
import utils.Utils;
import domain.Deposit;

public class TestDepositRepositoryFileLow
{
    @Test
    public void testAddLow()
    {
        this.removeTestFiles();
        try
        {
            DepositRepositoryFile repo = new DepositRepositoryFile(
                            TestDepositRepositoryFileLow.testFileName);
            Deposit depo1 = new Deposit("ING", "12345", 1000, false);
            Deposit depo2 = new Deposit("BT", "12345", 1000, false);
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(depo1));
            Assert.assertEquals(1, repo.size());
            Assert.assertFalse(repo.add(depo1));
            Assert.assertEquals(1, repo.size());
            Assert.assertEquals(depo1,
                            repo.find(new Pair<String, String>("ING", "12345")));
            Assert.assertTrue(repo.add(depo2));
            Assert.assertEquals(depo2,
                            repo.find(new Pair<String, String>("BT", "12345")));
            Assert.assertEquals(2, repo.size());
            Assert.assertFalse(repo.add(depo2));
            Assert.assertEquals(2, repo.size());
            // Testing persistence
            repo = new DepositRepositoryFile(
                            TestDepositRepositoryFileLow.testFileName);
            Assert.assertFalse(repo.add(depo1));
            Assert.assertFalse(repo.add(depo2));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            this.removeTestFiles();
        }
    }

    @Test
    public void testContainsLow()
    {
        this.removeTestFiles();
        try
        {
            DepositRepositoryFile repo = new DepositRepositoryFile(
                            TestDepositRepositoryFileLow.testFileName);
            Deposit depo1 = new Deposit("ING", "12345", 1000, false);
            Deposit depo2 = new Deposit("BT", "12345", 1000, false);
            Assert.assertFalse(repo.contains(depo1));
            Assert.assertFalse(repo.contains(depo2));
            Assert.assertTrue(repo.add(depo1));
            Assert.assertTrue(repo.contains(depo1));
            Assert.assertFalse(repo.contains(depo2));
            Assert.assertTrue(repo.add(depo2));
            Assert.assertTrue(repo.contains(depo1));
            Assert.assertTrue(repo.contains(depo2));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            this.removeTestFiles();
        }
    }

    @Test
    public void testFindLow()
    {
        this.removeTestFiles();
        try
        {
            DepositRepositoryFile repo = new DepositRepositoryFile(
                            TestDepositRepositoryFileLow.testFileName);
            Deposit found;
            Deposit depo1 = new Deposit("ING", "12345", 1000, false);
            Deposit depo2 = new Deposit("BT", "12345", 1000, false);
            Pair<String, String> pair1 = new Pair<String, String>("ING",
                            "12345");
            Pair<String, String> pair2 = new Pair<String, String>("BT", "12345");
            Assert.assertNull(repo.find(pair1));
            Assert.assertNull(repo.find(pair2));
            Assert.assertTrue(repo.add(depo1));
            found = repo.find(pair1);
            Assert.assertNotNull(found);
            Assert.assertEquals(depo1, found);
            Assert.assertTrue(repo.add(depo2));
            found = repo.find(pair2);
            Assert.assertNotNull(found);
            Assert.assertEquals(depo2, found);
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            this.removeTestFiles();
        }
    }

    @Test
    public void testRemoveLow()
    {
        this.removeTestFiles();
        try
        {
            DepositRepositoryFile repo = new DepositRepositoryFile(
                            TestDepositRepositoryFileLow.testFileName);
            Deposit depo1 = new Deposit("ING", "12345", 1000, false);
            Deposit depo2 = new Deposit("BT", "12345", 1000, false);
            Pair<String, String> pair1 = new Pair<String, String>("ING",
                            "12345");
            Pair<String, String> pair2 = new Pair<String, String>("BT", "12345");
            Assert.assertNull(repo.remove(pair1));
            Assert.assertNull(repo.remove(pair2));
            Assert.assertTrue(repo.add(depo1));
            Assert.assertEquals(depo1, repo.remove(pair1));
            Assert.assertNull(repo.find(pair1));
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(depo1));
            Assert.assertTrue(repo.add(depo2));
            Assert.assertEquals(depo2, repo.remove(pair2));
            Assert.assertEquals(1, repo.size());
            Assert.assertNull(repo.find(pair2));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            this.removeTestFiles();
        }
    }

    private void removeTestFiles()
    {
        Utils.removeFile(TestDepositRepositoryFileLow.testFileName);
    }

    private static final String testFileName = "deosits.test";
}
