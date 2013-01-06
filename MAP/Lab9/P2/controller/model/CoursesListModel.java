package controller.model;

import java.util.ArrayList;

import javax.swing.AbstractListModel;

import data.iterators.StreamIterator;
import domain.Course;

public class CoursesListModel extends AbstractListModel<String>
{
    public CoursesListModel()
    {
        this.lines = new ArrayList<String>();
    }

    public CoursesListModel(final StreamIterator<Course> iterator)
    {
        this.lines = new ArrayList<String>();
        while (iterator.hasNext())
            this.lines.add(iterator.next().getName());
    }

    public void addCourses(final StreamIterator<Course> iterator)
    {
        final int insertedLineIndex = this.lines.size() + 1;
        while (iterator.hasNext())
            this.lines.add(iterator.next().getName());
        this.fireIntervalAdded(this, insertedLineIndex, this.lines.size());
    }

    public void addCourse(final Course course)
    {
        final int insertedLineIndex = this.lines.size() + 1;
        this.lines.add(course.getName());
        this.fireIntervalAdded(this, insertedLineIndex, insertedLineIndex);
    }

    @Override
    public String getElementAt(int index)
    {
        return this.lines.get(index);
    }

    @Override
    public int getSize()
    {
        return this.lines.size();
    }

    private final ArrayList<String> lines;
    private static final long serialVersionUID = 1L;
}
