package repository;

public interface BankRepository extends java.io.Serializable
{
    /*
     * Adds a bank to the repository. Throws RepositoryException when the bank
     * name already exists in the repository. The user is recommended to use
     * this method directly and not use the find() method to check if the bank
     * can be safely added or not because the same check is done by this
     * add()method.
     */
    void add(domain.Bank bank) throws RepositoryException;

    /*
     * Finds a bank with the given name. If there is none null is returned.
     * Throws RepositoryException when the data is not accessible. This is
     * usually happens when the repository implementation uses a database or
     * files to store information.
     */
    domain.Bank find(String name) throws RepositoryException;

    /*
     * Throws RepositoryException when a reader could not be created. This issue
     * may arise when using a database or files to store data because at a given
     * moment the database or file may not be accessible
     */
    controller.EntityReader<domain.Bank> getReader();
}
