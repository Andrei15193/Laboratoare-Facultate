package aop5.persistence.database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import aop5.domain.Student;
import aop5.persistence.AllStudents;
import aop5.persistence.RepositoryException;

public class AllStudentsInDatabase implements AllStudents
{
    @Override
    public Student with(String name) throws RepositoryException
    {
        Student student = null;
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("select name, password from Students where name=?");
            stmt.setString(1, name);
            ResultSet result = stmt.executeQuery();
            if (result.next())
                student = new Student(name, result.getString("password"));
        }
        catch (SQLException e)
        {
            throw new RepositoryException("Database is not accessible", e);
        }
        finally
        {
            try
            {
                if (stmt != null)
                    stmt.close();
            }
            catch (SQLException e)
            {
                throw new RepositoryException(
                                "Could not prepare SQL Statement.", e);
            }
        }
        return student;
    }

    @Override
    public Student with(String name, String password)
                    throws RepositoryException
    {
        Student student = null;
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("select name, password from Students where name=? and password=?");
            stmt.setString(1, name);
            stmt.setString(2, password);
            ResultSet result = stmt.executeQuery();
            if (result.next())
                student = new Student(name, password);
        }
        catch (SQLException e)
        {
            throw new RepositoryException("Database is not accessible", e);
        }
        finally
        {
            try
            {
                if (stmt != null)
                    stmt.close();
            }
            catch (SQLException e)
            {
                throw new RepositoryException(
                                "Could not prepare SQL Statement.", e);
            }
        }
        return student;
    }

    @Override
    public void nowInclude(Student student) throws RepositoryException
    {
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("insert into Students(name, password) values  (?,?)");
            stmt.setString(1, student.getName());
            stmt.setString(2, student.getPassword());
            int ok = stmt.executeUpdate();
            if (ok != 1)
                throw new RepositoryException("Could not add new student!");
        }
        catch (SQLException e)
        {
            throw new RepositoryException("Database is not accessible", e);
        }
        finally
        {
            if (stmt != null)
                try
                {
                    stmt.close();
                }
                catch (SQLException e)
                {
                    throw new RepositoryException(
                                    "Could not prepare SQL Statement.", e);
                }
        }
    }
}
