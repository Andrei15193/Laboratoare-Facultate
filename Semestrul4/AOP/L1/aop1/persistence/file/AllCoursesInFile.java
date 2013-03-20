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
import aop1.domain.Course;
import aop1.persistence.AllCourses;
import aop1.persistence.RepositoryException;
import aop1.utils.AppendingObjectOutputStream;
import aop1.utils.Utils;

public class AllCoursesInFile implements AllCourses
{
    public AllCoursesInFile(final String fileName)
    {
        Application.logger.log(Level.INFO, "AllCoursesInFile.AllCoursesInFile("
                        + fileName + " :String)");
        this.fileName = fileName;
    }

    @Override
    public Course with(final String name) throws RepositoryException
    {
        Application.logger.log(Level.INFO, "AllCoursesInFile.with(" + name
                        + " :String)");
        boolean eof = false;
        Course course = null;
        ObjectInputStream input = null;
        try
        {
            input = new ObjectInputStream(new FileInputStream(this.fileName));
            do
                try
                {
                    course = (Course)input.readObject();
                }
                catch (EOFException e)
                {
                    course = null;
                    eof = true;
                }
            while (!eof && !name.equals(course.getName()));
        }
        catch (ClassNotFoundException e)
        {
            course = null;
        }
        catch (IOException e)
        {
            new RepositoryException("Data file inaccessible", e);
        }
        finally
        {
            Utils.closeStream(input);
        }
        return course;
    }

    @Override
    public Course[] get() throws RepositoryException
    {
        Application.logger.log(Level.INFO, "AllCoursesInFile.get()");
        boolean eof = false;
        ObjectInputStream input = null;
        List<Course> courses = new LinkedList<Course>();
        try
        {
            input = new ObjectInputStream(new FileInputStream(this.fileName));
            do
                try
                {
                    courses.add((Course)input.readObject());
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
        return courses.toArray(new Course[courses.size()]);
    }

    @Override
    public void nowInclude(final Course course) throws RepositoryException
    {
        Application.logger.log(Level.INFO, "AllCoursesInFile.nowInclude("
                        + course + " :Course)");
        ObjectOutputStream output = null;
        if (this.with(course.getName()) == null)
            try
            {
                if (Utils.checkIfFileExists(this.fileName))
                    output = new AppendingObjectOutputStream(
                                    new FileOutputStream(this.fileName, true));
                else
                    output = new ObjectOutputStream(new FileOutputStream(
                                    this.fileName));
                output.writeObject(course);
            }
            catch (IOException e)
            {
                throw new RepositoryException("Data file inaccessible.", e);
            }
            finally
            {
                Utils.closeStream(output);
            }
        else
            throw new RepositoryException("The course already exists!");
    }

    final String fileName;
}
