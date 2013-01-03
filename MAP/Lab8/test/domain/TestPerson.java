package test.domain;

import junit.framework.Assert;

import org.junit.Test;

import domain.Person;

public class TestPerson
{
    @Test
    public void testGetters()
    {
        Person person = new Person("Andrei", "1234567890123");
        Assert.assertEquals("Andrei", person.getName());
        Assert.assertEquals("1234567890123", person.getId());
        person = new Person("", "");
        Assert.assertEquals("", person.getName());
        Assert.assertEquals("", person.getId());
    }

    @Test
    public void testEquals()
    {
        Person person1 = new Person("Andrei", "12345");
        Person person2 = new Person("Alex", "12345");
        Person person3 = new Person("Florin", "123");
        Person person4 = new Person("Andrei", "123");
        Assert.assertEquals(person1, person2);
        Assert.assertNotSame(person1, person3);
        Assert.assertNotSame(person1, person4);
    }
}
