package persistence;

public class PersistenceException extends Exception
{
    public PersistenceException(final String message, final Throwable cause)
    {
        super(message, cause);
    }

    private static final long serialVersionUID = 1L;
}
