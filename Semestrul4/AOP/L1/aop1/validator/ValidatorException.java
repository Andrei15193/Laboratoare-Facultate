package aop1.validator;

import org.apache.log4j.Level;

import aop1.Application;

public class ValidatorException extends Exception
{
    public ValidatorException(final String[] errors)
    {
        Application.logger.log(Level.INFO,
                        "ValidatorException.ValidatorException(" + errors
                                        + " :String[])");
        this.errors = errors;
    }

    @Override
    public String getMessage()
    {
        Application.logger.log(Level.INFO, "ValidatorException.getMessage()");
        final StringBuilder builder = new StringBuilder();
        for (final String error: this.errors)
            builder.append(error + ", ");
        return builder.toString().substring(0, builder.length() - 2);
    }

    public String[] getErrors()
    {
        Application.logger.log(Level.INFO, "ValidatorException.getErrors()");
        return this.errors;
    }

    private final String[] errors;
    private static final long serialVersionUID = 1L;
}
