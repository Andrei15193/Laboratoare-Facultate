package test.controller;

import org.junit.Assert;
import org.junit.Test;

import repository.RepositoryException;
import repository.file.BankFile;
import repository.file.BankRepositoryFile;
import repository.file.DepositRepositoryFile;
import repository.file.PersonRepositoryFile;
import utils.Utils;
import controller.Controller;
import controller.ValidatorException;
import domain.BankException;
import domain.Person;

public class TestController
{
    @Test
    public void testAddPerson()
    {
        TestController.removeTestFiles();
        try
        {
            final BankRepositoryFile bankRepo = new BankRepositoryFile(
                            TestController.fileNames[0]);
            final DepositRepositoryFile depositRepo = new DepositRepositoryFile(
                            TestController.fileNames[1]);
            final PersonRepositoryFile personRepo = new PersonRepositoryFile(
                            TestController.fileNames[2]);
            final Controller ctrl = new Controller(bankRepo, depositRepo,
                            personRepo);
            Assert.assertTrue(ctrl.addPerson("Andrei", "1000229123456"));
            Assert.assertFalse(ctrl.addPerson("Andrei", "1000229123456"));
            try
            {
                ctrl.addPerson("", "1000229123456");
                Assert.fail("No name valdiation!");
            }
            catch (ValidatorException e)
            {
            }
            try
            {
                ctrl.addPerson("Andrei", "");
                Assert.fail("No id valdiation!");
            }
            catch (ValidatorException e)
            {
            }
        }
        catch (ValidatorException | RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestController.removeTestFiles();
        }
    }

    @Test
    public void testAddDeposit()
    {
        TestController.removeTestFiles();
        try
        {
            final BankRepositoryFile bankRepo = new BankRepositoryFile(
                            TestController.fileNames[0]);
            final DepositRepositoryFile depositRepo = new DepositRepositoryFile(
                            TestController.fileNames[1]);
            final PersonRepositoryFile personRepo = new PersonRepositoryFile(
                            TestController.fileNames[2]);
            final Controller ctrl = new Controller(bankRepo, depositRepo,
                            personRepo);
            personRepo.add(new Person("Andrei", "12345"));
            bankRepo.add(new BankFile(TestController.bankNames[0], true, 0.3));
            Assert.assertTrue(ctrl.logIn("Andrei", "12345"));
            Assert.assertTrue(ctrl.addDeposit(TestController.bankNames[0],
                            1000, true));
            Assert.assertFalse(ctrl.addDeposit(TestController.bankNames[0],
                            1000, true));
            Assert.assertFalse(ctrl.addDeposit(TestController.bankNames[1],
                            1000, true));
            Assert.assertTrue(ctrl.logOut());
            bankRepo.add(new BankFile(TestController.bankNames[1], false, 0.2));
            Assert.assertFalse(ctrl.addDeposit(TestController.bankNames[1],
                            1000, true));
            bankRepo.add(new BankFile(TestController.bankNames[2], false, 0.1));
            Assert.assertTrue(ctrl.logIn("Andrei", "12345"));
            try
            {
                ctrl.addDeposit(TestController.bankNames[2], 0, true);
                Assert.fail("No deposit validation!");
            }
            catch (ValidatorException e)
            {
            }
        }
        catch (ValidatorException | RepositoryException | BankException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestController.removeTestFiles();
        }
    }

    @Test
    public void testLogIn()
    {
        TestController.removeTestFiles();
        try
        {
            final BankRepositoryFile bankRepo = new BankRepositoryFile(
                            TestController.fileNames[0]);
            final DepositRepositoryFile depositRepo = new DepositRepositoryFile(
                            TestController.fileNames[1]);
            final PersonRepositoryFile personRepo = new PersonRepositoryFile(
                            TestController.fileNames[2]);
            final Controller ctrl = new Controller(bankRepo, depositRepo,
                            personRepo);
            Assert.assertFalse(ctrl.isPersonLoggedIn());
            Assert.assertTrue(personRepo.add(new Person("Andrei", "12345")));
            Assert.assertFalse(ctrl.logIn("Alex", "12345"));
            Assert.assertFalse(ctrl.isPersonLoggedIn());
            Assert.assertFalse(ctrl.logIn("Andrei", "123"));
            Assert.assertFalse(ctrl.isPersonLoggedIn());
            Assert.assertTrue(ctrl.logIn("Andrei", "12345"));
            Assert.assertTrue(ctrl.isPersonLoggedIn());
            Assert.assertEquals(new Person("Andrei", "12345"),
                            ctrl.getLoggedInPerson());
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestController.removeTestFiles();
        }
    }

    @Test
    public void testLogOut()
    {
        TestController.removeTestFiles();
        try
        {
            final BankRepositoryFile bankRepo = new BankRepositoryFile(
                            TestController.fileNames[0]);
            final DepositRepositoryFile depositRepo = new DepositRepositoryFile(
                            TestController.fileNames[1]);
            final PersonRepositoryFile personRepo = new PersonRepositoryFile(
                            TestController.fileNames[2]);
            final Controller ctrl = new Controller(bankRepo, depositRepo,
                            personRepo);
            Assert.assertFalse(ctrl.logOut());
            Assert.assertTrue(personRepo.add(new Person("Andrei", "12345")));
            Assert.assertTrue(ctrl.logIn("Andrei", "12345"));
            Assert.assertEquals(new Person("Andrei", "12345"),
                            ctrl.getLoggedInPerson());
            Assert.assertTrue(ctrl.logOut());
            Assert.assertFalse(ctrl.logOut());
        }
        catch (RepositoryException e)
        {
            Assert.fail(e.getMessage());
        }
        finally
        {
            TestController.removeTestFiles();
        }
    }

    private static void removeTestFiles()
    {
        for (final String fileName: TestController.fileNames)
            Utils.removeFile(fileName);
        for (final String fileName: TestController.bankNames)
            Utils.removeFile(fileName);
    }

    private static final String[] bankNames = {"ING", "BT", "BCR"};
    private static final String[] fileNames = {"banks.test", "deposits.test",
                    "persons.test"};
}
