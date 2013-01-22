package controller;

import java.util.ArrayList;

import javax.swing.AbstractListModel;
import javax.swing.table.AbstractTableModel;

import repository.BankRepository;
import repository.DepositRepository;
import repository.PersonRepository;
import repository.file.BankFile;
import utils.Utils;
import controller.DepositTableModel.DisplayData;
import controller.validator.BankValidator;
import controller.validator.DepositValidator;
import controller.validator.PersonValidator;
import controller.validator.Validator;
import domain.Bank;
import domain.BankException;
import domain.Deposit;
import domain.Person;

public class Controller
{
    public Controller(final BankRepository bankRepository,
                    final DepositRepository depositRepository,
                    final PersonRepository personRepository)
    {
        this.bankRepository = bankRepository;
        this.depositRepository = depositRepository;
        this.personRepository = personRepository;
        this.personValidator = new PersonValidator();
        this.bankValidator = new BankValidator();
        this.depositValidator = new DepositValidator();
        this.depositTableModel = new DepositTableModel();
        this.bankListModel = new BankListModel(this.bankRepository.iterator());
        this.loggedIn = null;
    }

    public boolean addBank(final String bankName, final boolean staticInterest,
                    final double interest) throws ValidatorException,
                    BankException
    {
        final boolean result;
        final Bank bank = new BankFile(bankName, staticInterest, interest);
        final Iterable<String> errors = this.bankValidator.validate(bank);
        if (errors.iterator().hasNext())
        {
            Utils.removeFile(bankName);
            throw new ValidatorException(errors);
        }
        else
        {
            result = this.bankRepository.add(bank);
            if (result == true)
                this.bankListModel.addBank(bank);
            return result;
        }
    }

    public boolean addDeposit(final String bankName, final double sum,
                    final boolean capitalisation) throws ValidatorException
    {
        final boolean result;
        final Deposit deposit;
        final Iterable<String> errors;
        if (this.bankRepository.find(bankName) == null)
        {
            ArrayList<String> error = new ArrayList<String>();
            error.add("The bank does not exist in the repository! Please register the bank if you wish to use it's name!");
            throw new ValidatorException(error);
        }
        else
            if (this.loggedIn == null)
                return false;
            else
            {
                deposit = new Deposit(bankName, this.loggedIn.getId(), sum,
                                capitalisation);
                errors = this.depositValidator.validate(deposit);
                if (errors.iterator().hasNext())
                    throw new ValidatorException(errors);
                result = this.depositRepository.add(deposit);
                if (result == true)
                    this.depositTableModel
                                    .setDisplayData(DisplayData.bankNameAndSum,
                                                    this.bankRepository
                                                                    .getBanksForDeposits(this.depositRepository
                                                                                    .getDepositsForPerson(this.loggedIn
                                                                                                    .getId())));
                return result;
            }
    }

    public boolean addPerson(final String personName, final String personId)
                    throws ValidatorException
    {
        Person person = new Person(personName, personId);
        Iterable<String> errors = this.personValidator.validate(person);
        if (errors.iterator().hasNext())
            throw new ValidatorException(errors);
        else
            return this.personRepository.add(person);
    }

    public boolean showDepositsWithSums()
    {
        if (this.loggedIn != null)
        {
            this.depositTableModel
                            .setDisplayData(DisplayData.bankNameAndSum,
                                            this.bankRepository
                                                            .getBanksForDeposits(this.depositRepository
                                                                            .getDepositsForPerson(this.loggedIn
                                                                                            .getId())));
            return true;
        }
        else
            return false;
    }

    public boolean showDepositsWithInterest()
    {
        if (this.loggedIn != null)
        {
            this.depositTableModel
                            .setDisplayData(DisplayData.bankNameAndInterest,
                                            this.bankRepository
                                                            .getBanksForDeposits(this.depositRepository
                                                                            .getDepositsForPerson(this.loggedIn
                                                                                            .getId())));
            return true;
        }
        else
            return false;
    }

    public boolean showAllDepositData()
    {
        if (this.loggedIn != null)
        {
            this.depositTableModel
                            .setDisplayData(DisplayData.allData,
                                            this.bankRepository
                                                            .getBanksForDeposits(this.depositRepository
                                                                            .getDepositsForPerson(this.loggedIn
                                                                                            .getId())));
            return true;
        }
        else
            return false;
    }

    public boolean logIn(final String personName, final String personId)
    {
        this.loggedIn = this.personRepository.find(personId);
        if (this.loggedIn != null && personName.equals(this.loggedIn.getName()))
        {
            this.depositTableModel
                            .setDisplayData(DisplayData.bankNameAndSum,
                                            this.bankRepository
                                                            .getBanksForDeposits(this.depositRepository
                                                                            .getDepositsForPerson(this.loggedIn
                                                                                            .getId())));
            return true;
        }
        else
        {
            this.loggedIn = null;
            return false;
        }
    }

    public boolean logOut()
    {
        if (this.loggedIn == null)
            return false;
        else
        {
            this.loggedIn = null;
            return true;
        }
    }

    public Person getLoggedInPerson()
    {
        return this.loggedIn;
    }

    public AbstractTableModel getDepositTableModel()
    {
        return this.depositTableModel;
    }

    public AbstractListModel<String> getBankListModel()
    {
        return this.bankListModel;
    }

    public boolean isPersonLoggedIn()
    {
        return this.loggedIn != null;
    }

    private Person loggedIn;
    private final BankRepository bankRepository;
    private final DepositRepository depositRepository;
    private final PersonRepository personRepository;
    private final Validator<Person> personValidator;
    private final Validator<Bank> bankValidator;
    private final Validator<Deposit> depositValidator;
    private final DepositTableModel depositTableModel;
    private final BankListModel bankListModel;
}
