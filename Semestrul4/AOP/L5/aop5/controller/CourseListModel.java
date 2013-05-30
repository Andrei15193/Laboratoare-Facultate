package aop5.controller;

import java.util.ArrayList;
import java.util.List;

import javax.swing.AbstractListModel;

import aop5.domain.Course;

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

    public void setCourses(Course[] allCourses)
    {
        this.courses.clear();
        for (Course course: allCourses)
            this.courses.add(course);
        this.fireContentsChanged(this, 0, this.courses.size());
    }

    private final List<Course> courses;
    private static final long serialVersionUID = 1L;
}
