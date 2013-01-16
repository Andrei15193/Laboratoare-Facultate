package domain;

import java.io.Serializable;

public class Course implements Serializable
{
    public Course(final String name)
    {
        this.name = name;
    }

    public String getName()
    {
        return this.name;
    }

    @Override
    public boolean equals(Object object)
    {
        return object instanceof Course
                        && this.name.equals(((Course)object).name);
    }

    @Override
    public String toString()
    {
        return this.name;
    }

    private final String name;
    private static final long serialVersionUID = 1L;
}
