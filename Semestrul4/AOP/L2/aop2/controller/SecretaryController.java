package aop2.controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.AbstractListModel;

import aop2.domain.Course;
import aop2.domain.MarksApplication;
import aop2.persistence.RepositoryException;
import aop2.validator.ValidatorException;


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
