package aop1.validator;

import java.util.LinkedList;
import java.util.List;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Mark;

public class MarkValidator implements Validator<Mark>
{
    @Override
    public String[] validate(Mark entity)
    {
        Application.logger.log(Level.INFO, "MarkValidator.validate(" + entity
                        + " :Mark)");
        List<String> errors = new LinkedList<String>();
        if (entity.getMark() <= 0)
            errors.add("The mark cannot be negative");
        else
            if (10 < entity.getMark())
                errors.add("The mark cannot be greater than 10");
        return errors.toArray(new String[errors.size()]);
    }
}
