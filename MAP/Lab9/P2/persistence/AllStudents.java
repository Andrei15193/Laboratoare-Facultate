package persistence;

import domain.Student;

public interface AllStudents
{
    Student with(final String name) throws PersistenceException;

    Student with(final String name, final String password)
                    throws PersistenceException;

    void nowInclude(final Student newStudent) throws PersistenceException;
}
