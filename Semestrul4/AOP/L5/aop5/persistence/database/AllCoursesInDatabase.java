package aop5.persistence.database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.LinkedList;
import java.util.List;

import aop5.domain.Course;
import aop5.persistence.AllCourses;
import aop5.persistence.RepositoryException;

public class AllCoursesInDatabase implements AllCourses
{
    @Override
    public Course with(String name) throws RepositoryException
    {
        Course course = null;
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("select name from Courses where name=?");
            stmt.setString(1, name);
            ResultSet result = stmt.executeQuery();
            if (result.next())
                course = new Course(name);
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
        return course;
    }

    @Override
    public Course[] get() throws RepositoryException
    {
        List<Course> courses = new LinkedList<Course>();
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("select name from Courses");
            ResultSet result = stmt.executeQuery();
            while (result.next())
                courses.add(new Course(result.getString("name")));
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
        return courses.toArray(new Course[courses.size()]);
    }

    @Override
    public void nowInclude(Course course) throws RepositoryException
    {
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("insert into Courses(name) values  (?)");
            stmt.setString(1, course.getName());
            int ok = stmt.executeUpdate();
            if (ok != 1)
                throw new RepositoryException("Could not add new course!");
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
