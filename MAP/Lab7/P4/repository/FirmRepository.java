package repository;

// Represents the interface of a FirmRepository. Any Firm repository has to provide
// the features found in this interface. All Firms in a repository are uniquely
// found by their name.
public interface FirmRepository{
    // Returns a FirmReader that will iterate through all Firms in the repository.
    controller.EntityReader<domain.Firm> getFirmsReader();
    
    // Stores a new Firm in the repository. All Firms are uniquely determined by
    // their name! If a Firm with the same name as the specified one already exists
    // then a RepositoryException is thrown.
    void storeFirm(domain.Firm firm) throws repository.RepositoryException;
    
    // Filters all Firms in the repository that have the turnover between the
    // specified bounds into a file having the specified name. To ignore at least
    // one of the bounds simply call the method with null.
    // NOTE! If both bounds are null then the result is equivalent to a bit by bit
    // copy into the specified file.
    // Throws RepositoryException if the file could not be created.
    void filterFirmsByTurnover(String fileName, Integer lowerBound, Integer higherBound) throws repository.RepositoryException;
    
    // Returns a firm from the repository having the given name. If there is no such
    // firm null is returned.
    domain.Firm getFirmByName(String name);
}
