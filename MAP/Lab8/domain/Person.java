package domain;

import java.io.Serializable;

public class Person implements Serializable
{
    public Person(String name, String personalIdentificationNumber)
    {
        this.name = name;
        this.id = personalIdentificationNumber;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public String getName()
    {
        return this.name;
    }

    public String getPersonalIdentificationNumber()
    {
        return this.id;
    }

    private String name;
    private final String id;
    private static final long serialVersionUID = 1L;
}
