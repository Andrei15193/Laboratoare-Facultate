package aop2.controller;

import java.util.ArrayList;
import java.util.List;

import javax.swing.AbstractListModel;

import aop2.domain.Course;


public class CourseListModel extends AbstractListModel<Course>
{
    public CourseListModel(final Course[] courses)
    {
        this.courses = new ArrayList<Course>();
        for (Course course: courses)
            this.courses.add(course);
    }

    public void addCourse(final Course course)
    {
        final int newIndex = this.courses.size();
        this.courses.add(course);
        this.fireIntervalAdded(this, newIndex, newIndex);
    }

    @Override
    public Course getElementAt(int index)
    {
        return this.courses.get(index);
    }

    @Override
    public int getSize()
    {
        return this.courses.size();
    }

    private final List<Course> courses;
    private static final long serialVersionUID = 1L;
}
