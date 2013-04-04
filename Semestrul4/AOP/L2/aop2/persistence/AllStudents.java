package aop2.persistence;

import aop2.domain.Student;

public interface AllStudents
{
    Student with(final String name) throws RepositoryException;

    Student with(final String name, final String password)
                    throws RepositoryException;

    void nowInclude(final Student student) throws RepositoryException;
}
