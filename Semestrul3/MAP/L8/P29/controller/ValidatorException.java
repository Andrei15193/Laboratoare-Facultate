package controller;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

public class ValidatorException extends Exception implements Iterable<String>
{
    public ValidatorException(final String message)
    {
        final Collection<String> col = new LinkedList<String>();
        col.add(message);
        this.errors = col;
    }

    public ValidatorException(final Iterable<String> errors)
    {
        this.errors = errors;
    }

    @Override
    public String getMessage()
    {
        StringBuilder message = new StringBuilder();
        for (final String error: this)
            message.append(error + " ");
        return message.toString().trim();
    }

    @Override
    public Iterator<String> iterator()
    {
        return this.errors.iterator();
    }

    private final Iterable<String> errors;
    private static final long serialVersionUID = 1L;
}
