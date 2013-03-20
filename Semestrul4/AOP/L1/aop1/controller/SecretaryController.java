package aop1.controller;

import java.util.Observable;
import java.util.Observer;

import javax.swing.AbstractListModel;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Course;
import aop1.domain.MarksApplication;
import aop1.persistence.RepositoryException;
import aop1.validator.ValidatorException;

public class SecretaryController implements Observer
{
    public SecretaryController(final MarksApplication marksApplication)
                    throws RepositoryException
    {
        Application.logger.log(Level.INFO,
                        "SecretaryController.SecretaryController("
                                        + marksApplication
                                        + " :MarksApplication)");
        this.marksApplication = marksApplication;
        this.courseListModel = new CourseListModel(
                        this.marksApplication.getAllCourses());
        this.marksApplication.addObserver(this);
    }

    public void addCourse(final String name) throws RepositoryException,
                    ValidatorException
    {
        Application.logger.log(Level.INFO, "SecretaryController.addCourse("
                        + name + " :String)");
        this.marksApplication.addCourse(name);
    }

    public void addStudent(final String name, final String password)
                    throws RepositoryException, ValidatorException
    {
        Application.logger.log(Level.INFO, "SecretaryController.addStudent("
                        + name + " :String, " + password + " :String)");
        this.marksApplication.addStudent(name, password);
    }

    public void addMark(final String studentName, final String courseName,
                    final int mark) throws RepositoryException,
                    ValidatorException
    {
        Application.logger.log(Level.INFO, "SecretaryController.addMark("
                        + studentName + " :String, " + courseName
                        + " :String, " + mark + " :int)");
        this.marksApplication.addMark(studentName, courseName, mark);
    }

    @Override
    public void update(Observable sender, Object added)
    {
        Application.logger.log(Level.INFO, "SecretaryController.update("
                        + sender + " :Observable, " + added + " :Object)");
        if (added instanceof Course)
            this.courseListModel.addCourse((Course)added);
    }

    public void unsubscribe()
    {
        Application.logger.log(Level.INFO, "SecretaryController.unsubscribe()");
        this.marksApplication.deleteObserver(this);
    }

    public AbstractListModel<Course> getCourseListModel()
    {
        Application.logger.log(Level.INFO,
                        "SecretaryController.getCourseListModel()");
        return this.courseListModel;
    }

    private final CourseListModel courseListModel;
    private final MarksApplication marksApplication;
}
