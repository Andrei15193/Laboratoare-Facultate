package proiect.test.repository;

import org.junit.Assert;
import org.junit.Test;

import proiect.controller.EntityReader;
import proiect.domain.Firm;
import proiect.domain.FirmException;
import proiect.repository.FirmRepositoryFile;
import proiect.repository.RepositoryException;

public class TestFirmRepositoryFile
{
    @Test
    public void testCreate()
    {
        FirmRepositoryFile repository = new FirmRepositoryFile("firms.test");
        Assert.assertNotNull(repository);
    }

    @Test
    public void testStoreFirm()
    {
        final String testFileName = "storeFirm.test";
        try
        {
            FirmRepositoryFile repository = new FirmRepositoryFile(testFileName);
            try
            {
                repository.storeFirm(new Firm("ING", 1));
            }
            catch (RepositoryException e)
            {
                Assert.fail("Firm ING is not memorized!");
            }
            try
            {
                repository.storeFirm(new Firm("ING", 1));
                Assert.fail("Firm ING is memorized!");
            }
            catch (RepositoryException e)
            {
            }
            try
            {
                repository.storeFirm(new Firm("BT", 1));
            }
            catch (RepositoryException e)
            {
                Assert.fail("Firm BT is not memorized!");
            }
            try
            {
                repository.storeFirm(new Firm("BT", 1));
                Assert.fail("Firm BT is memorized!");
            }
            catch (RepositoryException e)
            {
            }
            try
            {
                repository.storeFirm(new Firm("ING", 2));
                Assert.fail("Firm ING is memorized!");
            }
            catch (RepositoryException e)
            {
            }
        }
        catch (FirmException e)
        {
            Assert.fail("Firm(\"ING\", 1) is valid!");
        }
        finally
        {
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }

    @Test
    public void testReadFirms()
    {
        final String testFileName = "firmReader.test";
        try
        {
            Firm firm;
            EntityReader<Firm> reader;
            FirmRepositoryFile repository = new FirmRepositoryFile(testFileName);
            try
            {
                repository.storeFirm(new Firm("ING", 1));
            }
            catch (RepositoryException e)
            {
                Assert.fail("Firm ING is not memorized!");
            }
            reader = repository.getFirmsReader();
            Assert.assertNull(reader.getCurrentEntity());
            Assert.assertTrue(reader.next());
            Assert.assertEquals(reader.getCurrentEntity(), new Firm("ING", 1));
            Assert.assertFalse(reader.next());
            Assert.assertNull(reader.getCurrentEntity());
            try
            {
                repository.storeFirm(new Firm("BT", 1));
            }
            catch (RepositoryException e)
            {
                Assert.fail("Firm BT is not memorized!");
            }
            reader = repository.getFirmsReader();
            Assert.assertNull(reader.getCurrentEntity());
            Assert.assertTrue(reader.next());
            firm = reader.getCurrentEntity();
            Assert.assertNotNull(firm);
            if (!firm.equals(new Firm("ING", 1))
                            && !firm.equals(new Firm("BT", 1)))
                Assert.fail("ING or BT has not been memorized!");
            Assert.assertTrue(reader.next());
            firm = reader.getCurrentEntity();
            Assert.assertNotNull(firm);
            if (!firm.equals(new Firm("ING", 1))
                            && !firm.equals(new Firm("BT", 1)))
                Assert.fail("ING or BT has not been memorized!");
            Assert.assertFalse(reader.next());
            Assert.assertNull(reader.getCurrentEntity());
        }
        catch (FirmException e)
        {
            Assert.fail("Firm(\"ING\", 1) is valid!");
        }
        finally
        {
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }

    @Test
    public void testFilterByTurnover()
    {
        final String[] testFileNames = {"source.test", "filter1.test",
                        "filter2.test", "filter3.test", "filter4.test"};
        try
        {
            EntityReader<Firm> reader;
            Integer[][] bounds = { {null, 10}, {1, 10}, {1, null}, {null, null}};
            FirmRepositoryFile[] repositories = new FirmRepositoryFile[testFileNames.length];
            repositories[0] = new FirmRepositoryFile(testFileNames[0]);
            try
            {
                repositories[0].storeFirm(new Firm("ING", 0));
                repositories[0].storeFirm(new Firm("BT", 1));
                repositories[0].storeFirm(new Firm("BRD", 5));
                repositories[0].storeFirm(new Firm("BCC", 9));
                repositories[0].storeFirm(new Firm("BCR", 10));
                repositories[0].storeFirm(new Firm("BP", 11));
            }
            catch (RepositoryException e)
            {
                Assert.fail("No firms are memorized!");
            }
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    repositories[0].filterFirmsByTurnover(testFileNames[i + 1],
                                    bounds[i][0], bounds[i][1]);
                    repositories[i + 1] = new FirmRepositoryFile(
                                    testFileNames[i + 1]);
                    reader = repositories[i + 1].getFirmsReader();
                    Assert.assertTrue(reader.next());
                    do
                    {
                        int turnover = reader.getCurrentEntity().getTurnover();
                        Assert.assertTrue((bounds[i][0] == null || bounds[i][0] <= turnover)
                                        && (bounds[i][1] == null || turnover < bounds[i][1]));
                    }
                    while (reader.next());
                }
            }
            catch (RepositoryException e)
            {
                Assert.fail("cannot create temporary test files!");
            }
        }
        catch (FirmException e)
        {
            Assert.fail("Firm(\"ING\", 1) is valid!");
        }
        finally
        {
            for (String testFileName: testFileNames)
            {
                java.io.File deleteFile = new java.io.File(testFileName);
                deleteFile.delete();
            }
        }
    }

    @Test
    public void testGetFirmByName()
    {
        final String testFileName = "getFirmByName.test";
        try
        {
            FirmRepositoryFile repository = new FirmRepositoryFile(testFileName);
            try
            {
                repository.storeFirm(new Firm("ING", 1));
                repository.storeFirm(new Firm("BT", 2));
                repository.storeFirm(new Firm("BCR", 3));
                repository.storeFirm(new Firm("BCC", 4));
            }
            catch (RepositoryException e)
            {
                Assert.fail("No firms are memorized!");
            }
            Assert.assertNotNull(repository.getFirmByName("ING"));
            Assert.assertNotNull(repository.getFirmByName("BT"));
            Assert.assertNotNull(repository.getFirmByName("BCR"));
            Assert.assertNotNull(repository.getFirmByName("BCC"));
            Assert.assertNull(repository.getFirmByName("BP"));
            Assert.assertEquals(repository.getFirmByName("ING"), new Firm(
                            "ING", 1));
            Assert.assertEquals(repository.getFirmByName("BCR"), new Firm(
                            "BCR", 3));
            Assert.assertEquals(repository.getFirmByName("BT"), new Firm("BT",
                            2));
            Assert.assertEquals(repository.getFirmByName("BCC"), new Firm(
                            "BCC", 4));
        }
        catch (FirmException e)
        {
            Assert.fail("Firm(\"ING\", 1) is valid!");
        }
        finally
        {
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }
}
