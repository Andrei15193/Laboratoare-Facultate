package aop2.validator;

import java.util.LinkedList;
import java.util.List;

import aop2.domain.Course;


public class CourseValidator implements Validator<Course>
{
    @Override
    public String[] validate(Course entity)
    {
        List<String> errors = new LinkedList<String>();
        if (!entity.getName().matches("^[a-zA-Z ]+$"))
            errors.add("The name can contain only letters and spaces");
        return errors.toArray(new String[errors.size()]);
    }
}
