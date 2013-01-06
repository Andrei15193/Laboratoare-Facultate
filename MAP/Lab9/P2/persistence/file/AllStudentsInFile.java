package persistence.file;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import persistence.AllStudents;
import persistence.PersistenceException;
import utils.AppendingObjectOutputStream;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Student;

public class AllStudentsInFile implements AllStudents
{
    public AllStudentsInFile(final String fileName)
    {
        this.fileName = fileName;
    }

    @Override
    public Student with(String name) throws PersistenceException
    {
        Student found = null;
        StreamIterator<Student> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Student>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
            while (iterator.hasNext() && found == null)
            {
                found = iterator.next();
                if (!name.equals(found.getName()))
                    found = null;
            }
            iterator.close();
        }
        catch (FileNotFoundException e)
        {
            iterator = Utils.<Student>buildStreamIteratorOverEmptyCollection();
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
        return found;
    }

    @Override
    public Student with(final String name, final String password)
                    throws PersistenceException
    {
        Student found = null;
        StreamIterator<Student> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Student>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
            while (iterator.hasNext() && found == null)
            {
                found = iterator.next();
                if (!(name.equals(found.getName()) && password.equals(found
                                .getPassword())))
                    found = null;
            }
            iterator.close();
        }
        catch (FileNotFoundException e)
        {
            iterator = Utils.<Student>buildStreamIteratorOverEmptyCollection();
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
        return found;
    }

    @Override
    public void nowInclude(final Student newStudent)
                    throws PersistenceException
    {
        ObjectOutputStream output;
        if (this.with(newStudent.getName()) == null)
            try
            {
                if (Utils.checkIfFileExists(this.fileName))
                    output = new AppendingObjectOutputStream(
                                    new FileOutputStream(this.fileName, true));
                else
                    output = new ObjectOutputStream(new FileOutputStream(
                                    this.fileName));
                output.writeObject(newStudent);
                Utils.closeStream(output);
            }
            catch (Exception e)
            {
                throw new PersistenceException("Inaccessible file", e);
            }
        else
            throw new PersistenceException(
                            "The new student could not be included because he is already present.",
                            null);
    }

    private final String fileName;
}
