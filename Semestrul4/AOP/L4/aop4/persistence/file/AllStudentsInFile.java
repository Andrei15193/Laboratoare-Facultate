package aop4.persistence.file;

import java.io.EOFException;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import aop4.domain.Student;
import aop4.persistence.AllStudents;
import aop4.persistence.RepositoryException;
import aop4.utils.AppendingObjectOutputStream;
import aop4.utils.Utils;


public class AllStudentsInFile implements AllStudents
{
    public AllStudentsInFile(final String fileName)
    {
        this.fileName = fileName;
    }

    @Override
    public Student with(final String name) throws RepositoryException
    {
        boolean eof = false;
        Student student = null;
        ObjectInputStream input = null;
        try
        {
            input = new ObjectInputStream(new FileInputStream(this.fileName));
            do
                try
                {
                    student = (Student)input.readObject();
                }
                catch (EOFException e)
                {
                    student = null;
                    eof = true;
                }
            while (!eof && !name.equals(student.getName()));
        }
        catch (ClassNotFoundException e)
        {
        }
        catch (IOException e)
        {
            new RepositoryException("File inaccessible", e);
        }
        finally
        {
            Utils.closeStream(input);
        }
        return student;
    }

    @Override
    public Student with(final String name, final String password)
                    throws RepositoryException
    {
        boolean eof = false;
        Student student = null;
        ObjectInputStream input = null;
        try
        {
            input = new ObjectInputStream(new FileInputStream(this.fileName));
            do
                try
                {
                    student = (Student)input.readObject();
                }
                catch (EOFException e)
                {
                    student = null;
                    eof = true;
                }
            while (!eof
                            && !(name.equals(student.getName()) && password
                                            .equals(student.getPassword())));
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
        return student;
    }

    @Override
    public void nowInclude(final Student student) throws RepositoryException
    {
        ObjectOutputStream output = null;
        if (this.with(student.getName()) == null)
            try
            {
                if (Utils.checkIfFileExists(this.fileName))
                    output = new AppendingObjectOutputStream(
                                    new FileOutputStream(this.fileName, true));
                else
                    output = new ObjectOutputStream(new FileOutputStream(
                                    this.fileName));
                output.writeObject(student);
            }
            catch (IOException e)
            {
                throw new RepositoryException("File inaccessible.", e);
            }
            finally
            {
                Utils.closeStream(output);
            }
        else
            throw new RepositoryException("The student already exists!");
    }

    private final String fileName;
}
