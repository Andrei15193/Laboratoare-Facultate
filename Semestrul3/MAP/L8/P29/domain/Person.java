package domain;

public class Person implements java.io.Serializable
{
    public Person(final String personName, final String personId)
    {
        this.name = personName;
        this.id = personId;
    }

    public final String getName()
    {
        return this.name;
    }

    public final String getId()
    {
        return this.id;
    }

    @Override
    public boolean equals(Object object)
    {
        return object instanceof Person && this.id.equals(((Person)object).id);
    }

    @Override
    public String toString()
    {
        return this.name + " " + this.id;
    }

    private final String name;
    private final String id;
    private static final long serialVersionUID = 1L;
}
