package persistence;

import domain.Mark;
import domain.Student;

public interface AllMarks
{
    Mark[] from(final Student student) throws RepositoryException;

    void nowInclude(final Mark mark) throws RepositoryException;
}
