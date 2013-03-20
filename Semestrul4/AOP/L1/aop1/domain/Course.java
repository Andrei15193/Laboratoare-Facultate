package aop1.domain;

import java.io.Serializable;

import org.apache.log4j.Level;

import aop1.Application;

public class Course implements Serializable
{
    public Course(final String name)
    {
        Application.logger.log(Level.INFO, "Course.Course(" + name
                        + " :String)");
        this.name = name;
    }

    public String getName()
    {
        Application.logger.log(Level.INFO, "Course.getName()");
        return this.name;
    }

    @Override
    public boolean equals(Object object)
    {
        Application.logger.log(Level.INFO, "Course.equals(" + object
                        + " :Object)");
        return object instanceof Course
                        && this.name.equals(((Course)object).name);
    }

    @Override
    public String toString()
    {
        Application.logger.log(Level.INFO, "Course.toString()");
        return this.name;
    }

    private final String name;
    private static final long serialVersionUID = 1L;
}
