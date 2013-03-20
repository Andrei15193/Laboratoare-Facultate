package aop1.persistence;

import aop1.domain.Mark;
import aop1.domain.Student;

public interface AllMarks
{
    Mark[] from(final Student student) throws RepositoryException;

    void nowInclude(final Mark mark) throws RepositoryException;
}
