package aop2.domain;

import java.io.Serializable;

public class Student implements Serializable
{
    public Student(final String name, final String password)
    {
        this.name = name;
        this.password = password;
    }

    public String getName()
    {
        return this.name;
    }

    public String getPassword()
    {
        return this.password;
    }

    public void setPassword(String password)
    {
        this.password = password;
    }

    @Override
    public boolean equals(Object object)
    {
        return object instanceof Student
                        && this.name.equals(((Student)object).name);
    }

    @Override
    public String toString()
    {
        return this.name;
    }

    private final String name;
    private String password;
    private static final long serialVersionUID = 1L;
}
