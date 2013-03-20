package aop1.controller;

import java.util.ArrayList;
import java.util.List;

import javax.swing.AbstractListModel;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.Course;

public class CourseListModel extends AbstractListModel<Course>
{
    public CourseListModel(final Course[] courses)
    {
        Application.logger.log(Level.INFO, "CourseListModel.CourseListModel("
                        + courses + " :Course[])");
        this.courses = new ArrayList<Course>();
        for (Course course: courses)
            this.courses.add(course);
    }

    public void addCourse(final Course course)
    {
        Application.logger.log(Level.INFO, "CourseListModel.addCourse("
                        + course + " :Course)");
        final int newIndex = this.courses.size();
        this.courses.add(course);
        this.fireIntervalAdded(this, newIndex, newIndex);
    }

    @Override
    public Course getElementAt(int index)
    {
        Application.logger.log(Level.INFO, "CourseListModel.getElementAt("
                        + index + " :int)");
        return this.courses.get(index);
    }

    @Override
    public int getSize()
    {
        Application.logger.log(Level.INFO, "CourseListModel.getSize()");
        return this.courses.size();
    }

    private final List<Course> courses;
    private static final long serialVersionUID = 1L;
}
