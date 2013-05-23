package aop4.persistence;

import aop4.domain.Course;

public interface AllCourses
{
    Course with(final String name) throws RepositoryException;

    Course[] get() throws RepositoryException;

    void nowInclude(final Course course) throws RepositoryException;
}
