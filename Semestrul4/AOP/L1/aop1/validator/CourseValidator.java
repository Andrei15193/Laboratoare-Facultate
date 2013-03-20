package aop1.validator;

import java.util.LinkedList;
import java.util.List;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Course;

public class CourseValidator implements Validator<Course>
{
    @Override
    public String[] validate(Course entity)
    {
        Application.logger.log(Level.INFO, "CourseValidator.validate(" + entity
                        + " :Course)");
        List<String> errors = new LinkedList<String>();
        if (!entity.getName().matches("^[a-zA-Z ]+$"))
            errors.add("The name can contain only letters and spaces");
        return errors.toArray(new String[errors.size()]);
    }
}
