package proiect.controller;

import javax.swing.AbstractListModel;

import proiect.domain.Firm;
import proiect.domain.FirmException;
import proiect.repository.FirmRepository;
import proiect.repository.RepositoryException;
import aspects.ChangesState;

// Represents the application controller. All application logic is done by
// instances of this type.
public class ApplicationController
{
    // Creates a new ApplicationController instance that retains a reference to
    // the given repository.
    public ApplicationController(FirmRepository firmRepository)
    {
        this.firmRepository = firmRepository;
        this.firmsListModel = new FirmsListModel(
                        this.firmRepository.getFirmsReader());
    }

    // Returns a FirmReader that will iterate through all Firms.
    public EntityReader<Firm> getAllFirms()
    {
        return this.firmRepository.getFirmsReader();
    }

    // Tries to create and retain a new Firm with the specified data. If any
    // operation fails a new FirmException instance is thrown that holds
    // extended information about the nature of the error.
    @ChangesState
    public void addFirm(String name, Integer turnover) throws FirmException
    {
        try
        {
            Firm firm = new Firm(name, turnover);
            this.firmRepository.storeFirm(firm);
            this.firmsListModel.addFirm(firm);
        }
        catch (RepositoryException exception)
        {
            throw new FirmException("The Firm could not be stored! "
                            + exception.getMessage());
        }
    }

    // Creates three files that are prefixed with the fileName. The first file
    // ends
    // with Low and contains a list of all Firms that have the turnover lower
    // than
    // 50. The second file ends with Medium and contains a list of all Firms
    // that
    // have the turnover lower than 100 and greater than 50. The last file ends
    // with High and contains a list of all Firms that have the turnover higher
    // than 100.
    // The method will try and write all files. If at least one fails an
    // IOException
    // is thrown that holds the names of the files that could not be created.
    @ChangesState
    public void filterFirms(String fileName) throws java.io.IOException
    {
        String errors = "";
        try
        {
            this.firmRepository.filterFirmsByTurnover(fileName + "Low", null,
                            50);
        }
        catch (RepositoryException exception)
        {
            errors += fileName + "Low could not be created! ";
        }
        try
        {
            this.firmRepository.filterFirmsByTurnover(fileName + "Medium", 50,
                            100);
        }
        catch (RepositoryException exception)
        {
            errors += fileName + "Medium could not be created! ";
        }
        try
        {
            this.firmRepository.filterFirmsByTurnover(fileName + "High", 100,
                            null);
        }
        catch (RepositoryException exception)
        {
            errors += fileName + "High could not be created! ";
        }
        if (errors.length() > 0)
            throw new java.io.IOException(errors);
    }

    // Gets an abstract model for the list.
    public AbstractListModel<String> getModel()
    {
        return this.firmsListModel;
    }

    private final FirmsListModel firmsListModel;
    private final FirmRepository firmRepository;
}
