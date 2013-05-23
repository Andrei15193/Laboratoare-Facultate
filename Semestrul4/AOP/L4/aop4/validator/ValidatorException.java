package aop4.validator;

public class ValidatorException extends Exception
{
    public ValidatorException(final String[] errors)
    {
        this.errors = errors;
    }

    @Override
    public String getMessage()
    {
        final StringBuilder builder = new StringBuilder();
        for (final String error: this.errors)
            builder.append(error + ", ");
        return builder.toString().substring(0, builder.length() - 2);
    }

    public String[] getErrors()
    {
        return this.errors;
    }

    private final String[] errors;
    private static final long serialVersionUID = 1L;
}
