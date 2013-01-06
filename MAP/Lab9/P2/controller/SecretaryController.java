package controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.AbstractListModel;

import persistence.AllCourses;
import persistence.AllMarks;
import persistence.AllStudents;
import persistence.PersistenceException;
import controller.model.CoursesListModel;
import domain.Course;
import domain.Mark;
import domain.Student;

public class SecretaryController extends Observable implements Observer
{
    public SecretaryController(final AllCourses allCourses,
                    final AllMarks allMarks, final AllStudents allStudents)
    {
        this.allCourses = allCourses;
        this.allMarks = allMarks;
        this.allStudents = allStudents;
        this.coursesListModel = new CoursesListModel();
        try
        {
            this.coursesListModel.addCourses(this.allCourses.iterator());
        }
        catch (PersistenceException e)
        {
        }
    }

    @Override
    public void update(Observable sender, Object object)
    {
        if (object instanceof Course)
            this.coursesListModel.addCourse((Course)object);
    }

    public AbstractListModel<String> getCoursesListModel()
    {
        return this.coursesListModel;
    }

    public void addCourse(final String courseName) throws PersistenceException
    {
        Course newCourse = new Course(courseName);
        this.allCourses.nowInclude(newCourse);
        this.coursesListModel.addCourse(newCourse);
        this.setChanged();
        this.notifyObservers(newCourse);
    }

    public void addStudent(final String studentName,
                    final String studentPassword) throws PersistenceException
    {
        this.allStudents.nowInclude(new Student(studentName, studentPassword));
    }

    public void addMark(final String courseName, final String studentName,
                    final int mark) throws PersistenceException
    {
        Mark newMark;
        if (this.allStudents.with(studentName) == null)
            throw new PersistenceException(
                            "The student does not exist in the repository!",
                            null);
        else
            if (this.allCourses.with(courseName) == null)
                throw new PersistenceException(
                                "The specified course does not exist in the repository!",
                                null);
            else
            {
                newMark = new Mark(courseName, studentName, mark);
                this.allMarks.nowInclude(newMark);
                this.setChanged();
                this.notifyObservers(newMark);
            }
    }

    private final AllCourses allCourses;
    private final AllMarks allMarks;
    private final AllStudents allStudents;
    private final CoursesListModel coursesListModel;
}
