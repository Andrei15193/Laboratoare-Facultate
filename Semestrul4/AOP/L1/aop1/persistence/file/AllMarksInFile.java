package aop1.persistence.file;

import java.io.EOFException;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.LinkedList;
import java.util.List;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Mark;
import aop1.domain.Student;
import aop1.persistence.AllMarks;
import aop1.persistence.RepositoryException;
import aop1.utils.AppendingObjectOutputStream;
import aop1.utils.Utils;

public class AllMarksInFile implements AllMarks
{
    public AllMarksInFile(final String fileName)
    {
        Application.logger.log(Level.INFO, "AllMarksInFile.AllMarksInFile("
                        + fileName + " :String)");
        this.fileName = fileName;
    }

    @Override
    public Mark[] from(final Student student) throws RepositoryException
    {
        Application.logger.log(Level.INFO, "AllMarksInFile.from(" + student
                        + " :Student)");
        Mark mark;
        boolean eof = false;
        ObjectInputStream input = null;
        List<Mark> marks = new LinkedList<Mark>();
        try
        {
            input = new ObjectInputStream(new FileInputStream(this.fileName));
            do
                try
                {
                    mark = (Mark)input.readObject();
                    if (student.equals(mark.getStudent()))
                        marks.add(mark);
                }
                catch (EOFException e)
                {
                    eof = true;
                }
            while (!eof);
        }
        catch (ClassNotFoundException e)
        {
            new RepositoryException("Corrupted data file", e);
        }
        catch (IOException e)
        {
            new RepositoryException("Data file inaccessible", e);
        }
        finally
        {
            Utils.closeStream(input);
        }
        return marks.toArray(new Mark[marks.size()]);
    }

    @Override
    public void nowInclude(final Mark mark) throws RepositoryException
    {
        Application.logger.log(Level.INFO, "AllMarksInFile.nowInclude(" + mark
                        + " :Mark)");
        ObjectOutputStream output = null;
        try
        {
            if (Utils.checkIfFileExists(this.fileName))
                output = new AppendingObjectOutputStream(new FileOutputStream(
                                this.fileName, true));
            else
                output = new ObjectOutputStream(new FileOutputStream(
                                this.fileName));
            output.writeObject(mark);
        }
        catch (IOException e)
        {
            throw new RepositoryException("Data file inaccessible.", e);
        }
        finally
        {
            Utils.closeStream(output);
        }
    }

    private final String fileName;
}
