package aop5.persistence.database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import aop5.domain.Course;
import aop5.domain.Mark;
import aop5.domain.Student;
import aop5.persistence.AllMarks;
import aop5.persistence.RepositoryException;

public class AllMarksInDatabase implements AllMarks
{
    @Override
    public Mark[] from(Student student) throws RepositoryException
    {
        List<Mark> marks = new LinkedList<Mark>();
        Map<String, Course> loadedCourses = new HashMap<String, Course>();
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("select student, course, mark from Marks where student=?");
            stmt.setString(1, student.getName());
            ResultSet result = stmt.executeQuery();
            while (result.next())
            {
                String courseName = result.getString("course");
                if (loadedCourses.containsKey(courseName))
                    marks.add(new Mark(student, loadedCourses.get(courseName),
                                    result.getInt("mark")));
                else
                {
                    Course course = new Course(courseName);
                    loadedCourses.put(courseName, course);
                    marks.add(new Mark(student, course, result.getInt("mark")));
                }
            }
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
        return marks.toArray(new Mark[marks.size()]);
    }

    @Override
    public void nowInclude(Mark mark) throws RepositoryException
    {
        Connection con = JdbcUtils.getInstance().getConnection();
        PreparedStatement stmt = null;
        try
        {
            stmt = con.prepareStatement("insert into Marks(student, course, mark) values  (?,?,?)");
            stmt.setString(1, mark.getStudent().getName());
            stmt.setString(2, mark.getCourse().getName());
            stmt.setInt(3, mark.getMark());
            int ok = stmt.executeUpdate();
            if (ok != 1)
                throw new RepositoryException("Could not add new mark!");
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
