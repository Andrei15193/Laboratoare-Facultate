package proiect.domain;

// Represents a specialized Exception for the Firm domain entity.
public class FirmException extends Exception{
    // Creates a new FirmException instance holding the specified message.
    public FirmException(String message){
        super(message);
    }

    private static final long serialVersionUID = 1L;
}
