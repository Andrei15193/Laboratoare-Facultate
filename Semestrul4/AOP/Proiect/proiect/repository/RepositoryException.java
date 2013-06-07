package proiect.repository;

// Represents a specialized exception class.
public class RepositoryException extends Exception{
    // Creates a new RepositoryException instance having the specified message.
    public RepositoryException(String message){
        super(message);
    }
    
    private static final long serialVersionUID = 1L;
}
