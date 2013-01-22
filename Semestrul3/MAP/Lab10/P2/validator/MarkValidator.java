package validator;

import java.util.LinkedList;
import java.util.List;

import domain.Mark;

public class MarkValidator implements Validator<Mark>
{
    @Override
    public String[] validate(Mark entity)
    {
        List<String> errors = new LinkedList<String>();
        if (entity.getMark() <= 0)
            errors.add("The mark cannot be negative");
        else
            if (10 < entity.getMark())
                errors.add("The mark cannot be greater than 10");
        return errors.toArray(new String[errors.size()]);
    }
}
