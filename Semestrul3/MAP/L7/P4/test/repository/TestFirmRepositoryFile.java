package test.repository;

import static org.junit.Assert.*;
import org.junit.Test;

public class TestFirmRepositoryFile{
    @Test
    public void testCreate(){
        repository.FirmRepositoryFile repository = new repository.FirmRepositoryFile("firms.test");
        assertNotNull(repository);
    }

    @Test
    public void testStoreFirm(){
        final String testFileName = "storeFirm.test";
        try{
            repository.FirmRepositoryFile repository = new repository.FirmRepositoryFile(testFileName);
            try{
                repository.storeFirm(new domain.Firm("ING", 1));
            }
            catch (repository.RepositoryException e){
                fail("Firm ING is not memorized!");
            }
            try{
                repository.storeFirm(new domain.Firm("ING", 1));
                fail("Firm ING is memorized!");
            }
            catch (repository.RepositoryException e){
            }
            try{
                repository.storeFirm(new domain.Firm("BT", 1));
            }
            catch (repository.RepositoryException e){
                fail("Firm BT is not memorized!");
            }
            try{
                repository.storeFirm(new domain.Firm("BT", 1));
                fail("Firm BT is memorized!");
            }
            catch (repository.RepositoryException e){
            }
            try{
                repository.storeFirm(new domain.Firm("ING", 2));
                fail("Firm ING is memorized!");
            }
            catch (repository.RepositoryException e){
            }
        }
        catch (domain.FirmException e){
            fail("Firm(\"ING\", 1) is valid!");
        }
        finally{
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }
    
    @Test
    public void testReadFirms(){
        final String testFileName = "firmReader.test";
        try{
            domain.Firm firm;
            controller.EntityReader<domain.Firm> reader;
            repository.FirmRepositoryFile repository = new repository.FirmRepositoryFile(testFileName);
            try{
                repository.storeFirm(new domain.Firm("ING", 1));
            }
            catch (repository.RepositoryException e){
                fail("Firm ING is not memorized!");
            }
            reader = repository.getFirmsReader();
            assertNull(reader.getCurrentEntity());
            assertTrue(reader.next());
            assertEquals(reader.getCurrentEntity(), new domain.Firm("ING", 1));
            assertFalse(reader.next());
            assertNull(reader.getCurrentEntity());

            try{
                repository.storeFirm(new domain.Firm("BT", 1));
            }
            catch (repository.RepositoryException e){
                fail("Firm BT is not memorized!");
            }
            reader = repository.getFirmsReader();
            assertNull(reader.getCurrentEntity());
            assertTrue(reader.next());
            firm = reader.getCurrentEntity();
            assertNotNull(firm);
            if (!firm.equals(new domain.Firm("ING", 1)) && !firm.equals(new domain.Firm("BT", 1)))
                fail("ING or BT has not been memorized!");
            assertTrue(reader.next());
            firm = reader.getCurrentEntity();
            assertNotNull(firm);
            if (!firm.equals(new domain.Firm("ING", 1)) && !firm.equals(new domain.Firm("BT", 1)))
                fail("ING or BT has not been memorized!");
            assertFalse(reader.next());
            assertNull(reader.getCurrentEntity());
        }
        catch (domain.FirmException e){
            fail("Firm(\"ING\", 1) is valid!");
        }
        finally{
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }
    
    @Test
    public void testFilterByTurnover(){
        final String[] testFileNames = {"source.test", "filter1.test", "filter2.test", "filter3.test", "filter4.test"};
        try{
            controller.EntityReader<domain.Firm> reader;
            Integer[][] bounds = {{null, 10}, {1, 10}, {1, null}, {null, null}};
            repository.FirmRepositoryFile[] repositories = new repository.FirmRepositoryFile[testFileNames.length];
            repositories[0] = new repository.FirmRepositoryFile(testFileNames[0]);
            try{
                repositories[0].storeFirm(new domain.Firm("ING", 0));
                repositories[0].storeFirm(new domain.Firm("BT", 1));
                repositories[0].storeFirm(new domain.Firm("BRD", 5));
                repositories[0].storeFirm(new domain.Firm("BCC", 9));
                repositories[0].storeFirm(new domain.Firm("BCR", 10));
                repositories[0].storeFirm(new domain.Firm("BP", 11));
            }
            catch (repository.RepositoryException e){
                fail("No firms are memorized!");
            }
            
            try{
                for (int i = 0; i < 4; i++){
                    repositories[0].filterFirmsByTurnover(testFileNames[i + 1], bounds[i][0], bounds[i][1]);
                    repositories[i + 1] = new repository.FirmRepositoryFile(testFileNames[i + 1]);
                    
                    reader = repositories[i + 1].getFirmsReader();
                    assertTrue(reader.next());
                    do{
                        int turnover = reader.getCurrentEntity().getTurnover();
                        assertTrue((bounds[i][0] == null || bounds[i][0] <= turnover) && (bounds[i][1] == null || turnover < bounds[i][1]));
                    }while (reader.next());
                }
            }
            catch (repository.RepositoryException e){
                fail("cannot create temporary test files!");
            }
        }
        catch (domain.FirmException e){
            fail("Firm(\"ING\", 1) is valid!");
        }
        finally{
            for (String testFileName:testFileNames){
                java.io.File deleteFile = new java.io.File(testFileName);
                deleteFile.delete();
            }
        }
    }
    
    @Test
    public void testGetFirmByName(){
        final String testFileName = "getFirmByName.test";
        try{
            repository.FirmRepositoryFile repository = new repository.FirmRepositoryFile(testFileName);
            try{
                repository.storeFirm(new domain.Firm("ING", 1));
                repository.storeFirm(new domain.Firm("BT", 2));
                repository.storeFirm(new domain.Firm("BCR", 3));
                repository.storeFirm(new domain.Firm("BCC", 4));
            }
            catch (repository.RepositoryException e){
                fail("No firms are memorized!");
            }
            assertNotNull(repository.getFirmByName("ING"));
            assertNotNull(repository.getFirmByName("BT"));
            assertNotNull(repository.getFirmByName("BCR"));
            assertNotNull(repository.getFirmByName("BCC"));
            assertNull(repository.getFirmByName("BP"));
            assertEquals(repository.getFirmByName("ING"), new domain.Firm("ING", 1));
            assertEquals(repository.getFirmByName("BCR"), new domain.Firm("BCR", 3));
            assertEquals(repository.getFirmByName("BT"), new domain.Firm("BT", 2));
            assertEquals(repository.getFirmByName("BCC"), new domain.Firm("BCC", 4));
        }
        catch (domain.FirmException e){
            fail("Firm(\"ING\", 1) is valid!");
        }
        finally{
            java.io.File deleteFile = new java.io.File(testFileName);
            deleteFile.delete();
        }
    }
}
