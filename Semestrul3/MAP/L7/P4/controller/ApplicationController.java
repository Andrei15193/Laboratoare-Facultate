package controller;

import javax.swing.AbstractListModel;

// Represents the application controller. All application logic is done by
// instances of this type.
public class ApplicationController{
    // Creates a new ApplicationController instance that retains a reference to
    // the given repository.
    public ApplicationController(repository.FirmRepository firmRepository){
        this.firmRepository = firmRepository;
        this.firmsListModel = new controller.FirmsListModel(this.firmRepository.getFirmsReader());
    }
    
    // Returns a FirmReader that will iterate through all Firms.
    public controller.EntityReader<domain.Firm> getAllFirms(){
        return this.firmRepository.getFirmsReader();
    }
    
    // Tries to create and retain a new Firm with the specified data. If any
    // operation fails a new FirmException instance is thrown that holds
    // extended information about the nature of the error.
    public void addFirm(String name, Integer turnover) throws domain.FirmException{
        try{
            domain.Firm firm = new domain.Firm(name, turnover);
            this.firmRepository.storeFirm(firm);
            this.firmsListModel.addFirm(firm);
        }
        catch (repository.RepositoryException exception){
            throw new domain.FirmException("The Firm could not be stored! " + exception.getMessage());
        }
    }
    
    // Creates three files that are prefixed with the fileName. The first file ends
    // with Low and contains a list of all Firms that have the turnover lower than
    // 50. The second file ends with Medium and contains a list of all Firms that
    // have the turnover lower than 100 and greater than 50. The last file ends
    // with High and contains a list of all Firms that have the turnover higher
    // than 100.
    // The method will try and write all files. If at least one fails an IOException
    // is thrown that holds the names of the files that could not be created. 
    public void filterFirms(String fileName) throws java.io.IOException{
        String errors = "";
        
        try{
            this.firmRepository.filterFirmsByTurnover(fileName + "Low", null, 50);
        }
        catch (repository.RepositoryException exception){
            errors += fileName + "Low could not be created! ";
        }
        try{
            this.firmRepository.filterFirmsByTurnover(fileName + "Medium", 50, 100);
        }
        catch (repository.RepositoryException exception){
            errors += fileName + "Medium could not be created! ";
        }
        try{
            this.firmRepository.filterFirmsByTurnover(fileName + "High", 100, null);
        }
        catch (repository.RepositoryException exception){
            errors += fileName + "High could not be created! ";
        }
        
        if (errors.length() > 0)
            throw new java.io.IOException(errors);
    }

    // Gets an abstract model for the list.
    public AbstractListModel getModel(){
        return this.firmsListModel;
    }
    
    private controller.FirmsListModel firmsListModel;
    private repository.FirmRepository firmRepository;
}
