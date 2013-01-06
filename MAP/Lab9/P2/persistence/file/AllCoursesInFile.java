package persistence.file;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import persistence.AllCourses;
import persistence.PersistenceException;
import utils.AppendingObjectOutputStream;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Course;

public class AllCoursesInFile implements AllCourses
{
    public AllCoursesInFile(final String fileName)
    {
        this.fileName = fileName;
    }

    @Override
    public Course with(String name) throws PersistenceException
    {
        Course found = null;
        StreamIterator<Course> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Course>(new ObjectInputStream(
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
            iterator = Utils.<Course>buildStreamIteratorOverEmptyCollection();
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
        return found;
    }

    @Override
    public StreamIterator<Course> iterator() throws PersistenceException
    {
        StreamIterator<Course> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Course>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
        }
        catch (FileNotFoundException e)
        {
            iterator = Utils.<Course>buildStreamIteratorOverEmptyCollection();
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
        return iterator;
    }

    @Override
    public void nowInclude(Course newCourse) throws PersistenceException
    {
        ObjectOutputStream output;
        if (this.with(newCourse.getName()) == null)
            try
            {
                if (Utils.checkIfFileExists(this.fileName))
                    output = new AppendingObjectOutputStream(
                                    new FileOutputStream(this.fileName, true));
                else
                    output = new ObjectOutputStream(new FileOutputStream(
                                    this.fileName));
                output.writeObject(newCourse);
                Utils.closeStream(output);
            }
            catch (Exception e)
            {
                throw new PersistenceException("Inaccessible file", e);
            }
        else
            throw new PersistenceException(
                            "The new course could not be included because it is already present.",
                            null);
    }

    private final String fileName;
}
