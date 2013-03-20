package aop1.domain;

import java.io.Serializable;

import org.apache.log4j.Level;

import aop1.Application;

public class Student implements Serializable
{
    public Student(final String name, final String password)
    {
        Application.logger.log(Level.INFO, "Student.Student(" + name
                        + " :String, " + password + " :String)");
        this.name = name;
        this.password = password;
    }

    public String getName()
    {
        Application.logger.log(Level.INFO, "Student.getName()");
        return this.name;
    }

    public String getPassword()
    {
        Application.logger.log(Level.INFO, "Student.getPassword()");
        return this.password;
    }

    public void setPassword(String password)
    {
        Application.logger.log(Level.INFO, "Student.setPassword(" + password
                        + " :String)");
        this.password = password;
    }

    @Override
    public boolean equals(Object object)
    {
        Application.logger.log(Level.INFO, "Student.equals(" + object
                        + " :Object)");
        return object instanceof Student
                        && this.name.equals(((Student)object).name);
    }

    @Override
    public String toString()
    {
        Application.logger.log(Level.INFO, "Student.toString()");
        return this.name;
    }

    private final String name;
    private String password;
    private static final long serialVersionUID = 1L;
}
