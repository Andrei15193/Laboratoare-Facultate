package aop3.persistence;

public class RepositoryException extends Exception
{
    public RepositoryException(final String message)
    {
        super(message);
    }

    public RepositoryException(final String message, final Throwable cause)
    {
        super(message, cause);
    }

    public RepositoryException(final Throwable cause)
    {
        super(cause);
    }

    private static final long serialVersionUID = 1L;
}
