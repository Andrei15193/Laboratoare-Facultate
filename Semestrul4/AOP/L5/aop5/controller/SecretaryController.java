package aop5.controller;

import javax.swing.AbstractListModel;

import aop5.domain.Course;
import aop5.domain.MarksApplication;
import aop5.persistence.RepositoryException;
import aop5.validator.ValidatorException;

public class SecretaryController/* implements Observer */
{
    public SecretaryController(final MarksApplication marksApplication)
                    throws RepositoryException
    {
        this.marksApplication = marksApplication;
        this.courseListModel = new CourseListModel(
                        this.marksApplication.getAllCourses());
        // this.marksApplication.addObserver(this);
    }

    public void addCourse(final String name) throws RepositoryException,
                    ValidatorException
    {
        System.out.println("Adding course");
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

    // @Override
    // public void update(Observable sender, Object added)
    // {
    // if (added instanceof Course)
    // this.courseListModel.addCourse((Course)added);
    // }
    // public void unsubscribe()
    // {
    // this.marksApplication.deleteObserver(this);
    // }
    public AbstractListModel<Course> getCourseListModel()
    {
        return this.courseListModel;
    }

    public void setCoursesInModel() throws RepositoryException
    {
        System.out.println("Subject notified!");
        this.courseListModel.setCourses(this.marksApplication.getAllCourses());
    }

    private final CourseListModel courseListModel;
    private final MarksApplication marksApplication;
}
