package test.repository;

import junit.framework.Assert;

import org.junit.Test;

import repository.PersonRepositoryFile;
import repository.RepositoryException;
import utils.Utils;
import data.iterators.StreamIterator;
import domain.Person;

public class TestPersonRepositoryFileLow
{
    @Test
    public void testAddLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertTrue(repo.isEmpty());
            Assert.assertTrue(repo.add(person1));
            Assert.assertFalse(repo.add(person1));
            Assert.assertEquals(1, repo.size());
            Assert.assertEquals(person1, repo.find("12345"));
            Assert.assertTrue(repo.add(person2));
            Assert.assertFalse(repo.add(person2));
            Assert.assertEquals(2, repo.size());
            Assert.assertEquals(person1, repo.find("12345"));
            Assert.assertEquals(person2, repo.find("123"));
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(person1, repo.find("12345"));
            Assert.assertEquals(person2, repo.find("123"));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testClearLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(person1));
            Assert.assertEquals(1, repo.size());
            repo.clear();
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(person1));
            Assert.assertEquals(1, repo.size());
            Assert.assertTrue(repo.add(person2));
            Assert.assertEquals(2, repo.size());
            repo.clear();
            Assert.assertEquals(0, repo.size());
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(0, repo.size());
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testContainsLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertFalse(repo.contains(person1));
            Assert.assertFalse(repo.contains(person2));
            Assert.assertTrue(repo.add(person1));
            Assert.assertTrue(repo.contains(person1));
            Assert.assertFalse(repo.contains(person2));
            Assert.assertTrue(repo.add(person2));
            Assert.assertTrue(repo.contains(person1));
            Assert.assertTrue(repo.contains(person2));
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertTrue(repo.contains(person1));
            Assert.assertTrue(repo.contains(person2));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testFindLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person found;
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertNull(repo.find("12345"));
            Assert.assertNull(repo.find("123"));
            Assert.assertTrue(repo.add(person1));
            found = repo.find("12345");
            Assert.assertNotNull(found);
            Assert.assertEquals(person1, found);
            Assert.assertNull(repo.find("123"));
            Assert.assertTrue(repo.add(person2));
            found = repo.find("123");
            Assert.assertNotNull(found);
            Assert.assertEquals(person2, found);
            Assert.assertNull(repo.find("1234567"));
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            found = repo.find("12345");
            Assert.assertNotNull(found);
            Assert.assertEquals(person1, found);
            found = repo.find("123");
            Assert.assertNotNull(found);
            Assert.assertEquals(person2, found);
            Assert.assertNull(repo.find("1234567"));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testIteratorLow()
    {
        int i, count;
        Person current;
        Person person1 = new Person("Andrei", "12345");
        Person person2 = new Person("Andrei", "123");
        Person[] persons = {person1, person2};
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            StreamIterator<Person> it;
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertFalse(repo.iterator().hasNext());
            Assert.assertTrue(repo.add(person1));
            it = repo.iterator();
            Assert.assertTrue(it.hasNext());
            Assert.assertEquals(it.next(), person1);
            Assert.assertTrue(repo.add(person2));
            it = repo.iterator();
            count = 0;
            while (it.hasNext())
            {
                i = 0;
                current = it.next();
                while (i < persons.length && persons[i].equals(current))
                    i++;
                if (i != persons.length)
                    count++;
            }
            Assert.assertEquals(2, count);
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            it = repo.iterator();
            count = 0;
            while (it.hasNext())
            {
                i = 0;
                current = it.next();
                while (i < persons.length && persons[i].equals(current))
                    i++;
                if (i != persons.length)
                    count++;
            }
            Assert.assertEquals(2, count);
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testRemoveLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertNull(repo.remove("12345"));
            Assert.assertNull(repo.remove("123"));
            Assert.assertTrue(repo.add(person1));
            Assert.assertEquals(1, repo.size());
            Assert.assertEquals(person1, repo.find("12345"));
            Assert.assertEquals(person1, repo.remove("12345"));
            Assert.assertNull(repo.find("12345"));
            Assert.assertTrue(repo.add(person2));
            Assert.assertEquals(1, repo.size());
            Assert.assertEquals(person2, repo.remove("123"));
            Assert.assertNull(repo.find("123"));
            Assert.assertEquals(0, repo.size());
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertNull(repo.find("12345"));
            Assert.assertNull(repo.find("123"));
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(person1));
            Assert.assertTrue(repo.add(person2));
            Assert.assertEquals(2, repo.size());
            Assert.assertEquals(person1, repo.find("12345"));
            Assert.assertEquals(person2, repo.find("123"));
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(2, repo.size());
            Assert.assertEquals(person1, repo.remove("12345"));
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(1, repo.size());
            Assert.assertNull(repo.remove("12345"));
            Assert.assertEquals(person2, repo.remove("123"));
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    @Test
    public void testSizeLow()
    {
        TestPersonRepositoryFileLow.removeTestFiles();
        try
        {
            PersonRepositoryFile repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Person person1 = new Person("Andrei", "12345");
            Person person2 = new Person("Ioana", "123");
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(person1));
            Assert.assertEquals(1, repo.size());
            Assert.assertTrue(repo.add(person2));
            Assert.assertEquals(2, repo.size());
            repo.clear();
            Assert.assertEquals(0, repo.size());
            // Testing persistence
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(0, repo.size());
            Assert.assertTrue(repo.add(person1));
            Assert.assertTrue(repo.add(person2));
            Assert.assertEquals(2, repo.size());
            repo = new PersonRepositoryFile(
                            TestPersonRepositoryFileLow.testFileName);
            Assert.assertEquals(2, repo.size());
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestPersonRepositoryFileLow.removeTestFiles();
        }
    }

    private static void removeTestFiles()
    {
        Utils.removeFile(TestPersonRepositoryFileLow.testFileName);
    }

    private static final String testFileName = "personRepository.test";
}
