package aop1.persistence;

import org.apache.log4j.Level;

import aop1.Application;

public class RepositoryException extends Exception
{
    public RepositoryException(final String message)
    {
        super(message);
        Application.logger.log(Level.INFO,
                        "RepositoryException.RepositoryException(" + message
                                        + " :String)");
    }

    public RepositoryException(final String message, final Throwable cause)
    {
        super(message, cause);
        Application.logger
                        .log(Level.INFO,
                                        "RepositoryException.RepositoryException("
                                                        + message
                                                        + " :String, " + cause
                                                        + " :Throwable)");
    }

    public RepositoryException(final Throwable cause)
    {
        super(cause);
        Application.logger.log(Level.INFO,
                        "RepositoryException.RepositoryException(" + cause
                                        + " :Throwable)");
    }

    private static final long serialVersionUID = 1L;
}
