package aop3.validator;

import java.util.LinkedList;
import java.util.List;

import aop3.domain.Student;


public class StudentValidator implements Validator<Student>
{
    @Override
    public String[] validate(Student entity)
    {
        List<String> errors = new LinkedList<String>();
        if (!entity.getName().matches("^[a-zA-Z -]+$"))
            errors.add("The name can contain only letters, spaces and dashes");
        return errors.toArray(new String[errors.size()]);
    }
}
