package persistence.file;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import persistence.AllMarks;
import persistence.PersistenceException;
import utils.AppendingObjectOutputStream;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Mark;
import domain.Student;

public class AllMarksInFile implements AllMarks
{
    public AllMarksInFile(final String fileName)
    {
        this.fileName = fileName;
    }

    @Override
    public StreamIterator<Mark> from(final Student student)
                    throws PersistenceException
    {
        StreamIterator<Mark> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Mark>(new ObjectInputStream(
                            new FileInputStream(this.fileName)))
            {
                @Override
                public Mark next()
                {
                    Mark found;
                    do
                        found = super.next();
                    while (super.hasNext()
                                    && !this.name.equals(found.getStudentName()));
                    return found;
                }

                private final String name = new String(student.getName());
            };
        }
        catch (FileNotFoundException e)
        {
            iterator = Utils.<Mark>buildStreamIteratorOverEmptyCollection();
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
        return iterator;
    }

    @Override
    public void nowInclude(final Mark newMark) throws PersistenceException
    {
        ObjectOutputStream output;
        try
        {
            if (Utils.checkIfFileExists(this.fileName))
                output = new AppendingObjectOutputStream(new FileOutputStream(
                                this.fileName, true));
            else
                output = new ObjectOutputStream(new FileOutputStream(
                                this.fileName));
            output.writeObject(newMark);
            Utils.closeStream(output);
        }
        catch (Exception e)
        {
            throw new PersistenceException("Inaccessible file", e);
        }
    }

    private final String fileName;
}
