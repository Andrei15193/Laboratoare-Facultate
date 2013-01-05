package repository.file;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import repository.PersonRepository;
import repository.RepositoryException;
import utils.AppendingObjectOutputStream;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Person;

public class PersonRepositoryFile implements PersonRepository
{
    public PersonRepositoryFile(final String fileName)
                    throws RepositoryException
    {
        this.fileName = fileName;
        this.count = 0;
        for (@SuppressWarnings("unused")
        Person person: this)
            this.count++;
    }

    @Override
    public boolean add(Person person)
    {
        boolean result = true;
        if (!this.contains(person))
            result = this.storePerson(person);
        else
            result = false;
        return result;
    }

    @Override
    public void clear()
    {
        this.count = 0;
        Utils.removeFile(this.fileName);
    }

    @Override
    public boolean contains(Person person)
    {
        boolean found = false;
        StreamIterator<Person> it = this.iterator();
        while (it.hasNext() && !found)
            found = person.equals(it.next());
        it.close();
        return found;
    }

    @Override
    public Person find(String personId)
    {
        Person found = null;
        StreamIterator<Person> it = this.iterator();
        while (it.hasNext() && found == null)
        {
            found = it.next();
            if (!personId.equals(found.getId()))
                found = null;
        }
        it.close();
        return found;
    }

    @Override
    public boolean isEmpty()
    {
        return this.count == 0;
    }

    @Override
    public StreamIterator<Person> iterator()
    {
        StreamIterator<Person> it;
        try
        {
            it = new ObjectStreamIterator<Person>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
        }
        catch (IOException e)
        {
            it = Utils.<Person>buildStreamIteratorOverEmptyCollection();
        }
        return it;
    }

    @Override
    public Person remove(String personId)
    {
        Person removed = null;
        ObjectOutputStream output = null;
        final String tempFileName = this.fileName + ".temp";
        try
        {
            output = new ObjectOutputStream(new FileOutputStream(tempFileName));
            for (Person person: this)
                if (!person.getId().equals(personId))
                    output.writeObject(person);
                else
                    removed = person;
            output.close();
            Utils.removeFile(this.fileName);
            Utils.renameFile(tempFileName, this.fileName);
            if (removed != null)
                this.count--;
        }
        catch (IOException e)
        {
            Utils.removeFile(tempFileName);
        }
        finally
        {
            Utils.closeStream(output);
        }
        return removed;
    }

    @Override
    public int size()
    {
        return this.count;
    }

    private boolean storePerson(Person person)
    {
        boolean result = true;
        ObjectOutputStream output = null;
        try
        {
            if (this.count == 0)
                output = new ObjectOutputStream(new FileOutputStream(
                                this.fileName));
            else
                output = new AppendingObjectOutputStream(new FileOutputStream(
                                this.fileName, true));
            output.writeObject(person);
            this.count++;
        }
        catch (IOException e)
        {
            result = false;
        }
        finally
        {
            Utils.closeStream(output);
        }
        return result;
    }

    private int count;
    private final String fileName;
}
