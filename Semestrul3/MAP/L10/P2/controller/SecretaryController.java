package controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.AbstractListModel;

import persistence.RepositoryException;
import validator.ValidatorException;
import domain.Course;
import domain.MarksApplication;

public class SecretaryController implements Observer
{
    public SecretaryController(final MarksApplication marksApplication)
                    throws RepositoryException
    {
        this.marksApplication = marksApplication;
        this.courseListModel = new CourseListModel(
                        this.marksApplication.getAllCourses());
        this.marksApplication.addObserver(this);
    }

    public void addCourse(final String name) throws RepositoryException,
                    ValidatorException
    {
        this.marksApplication.addCourse(name);
    }

    public void addStudent(final String name, final String password)
                    throws RepositoryException, ValidatorException
    {
        this.marksApplication.addStudent(name, password);
    }

    public void addMark(final String studentName, final String courseName,
                    final int mark) throws RepositoryException,
                    ValidatorException
    {
        this.marksApplication.addMark(studentName, courseName, mark);
    }

    @Override
    public void update(Observable sender, Object added)
    {
        if (added instanceof Course)
            this.courseListModel.addCourse((Course)added);
    }

    public void unsubscribe()
    {
        this.marksApplication.deleteObserver(this);
    }

    public AbstractListModel<Course> getCourseListModel()
    {
        return this.courseListModel;
    }

    private final CourseListModel courseListModel;
    private final MarksApplication marksApplication;
}
