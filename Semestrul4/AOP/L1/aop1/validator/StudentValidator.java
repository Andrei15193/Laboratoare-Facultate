package aop1.validator;

import java.util.LinkedList;
import java.util.List;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Student;

public class StudentValidator implements Validator<Student>
{
    @Override
    public String[] validate(Student entity)
    {
        Application.logger.log(Level.INFO, "StudentValidator.validate("
                        + entity + " :Student)");
        List<String> errors = new LinkedList<String>();
        if (!entity.getName().matches("^[a-zA-Z -]+$"))
            errors.add("The name can contain only letters, spaces and dashes");
        return errors.toArray(new String[errors.size()]);
    }
}
