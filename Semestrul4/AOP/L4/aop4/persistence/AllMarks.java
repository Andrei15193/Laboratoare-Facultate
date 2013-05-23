package aop4.persistence;

import aop4.domain.Mark;
import aop4.domain.Student;

public interface AllMarks
{
    Mark[] from(final Student student) throws RepositoryException;

    void nowInclude(final Mark mark) throws RepositoryException;
}
