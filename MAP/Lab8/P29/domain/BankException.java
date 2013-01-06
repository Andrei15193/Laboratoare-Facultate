package domain;

public class BankException extends Exception
{
    public BankException(final String message)
    {
        super(message);
    }

    private static final long serialVersionUID = 1L;
}
