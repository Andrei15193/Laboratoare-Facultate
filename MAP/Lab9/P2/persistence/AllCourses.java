package persistence;

import data.iterators.StreamIterator;
import domain.Course;

public interface AllCourses
{
    Course with(final String name) throws PersistenceException;

    StreamIterator<Course> iterator() throws PersistenceException;

    void nowInclude(final Course newCourse) throws PersistenceException;
}
