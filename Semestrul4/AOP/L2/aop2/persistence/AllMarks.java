package aop2.persistence;

import aop2.domain.Mark;
import aop2.domain.Student;

public interface AllMarks
{
    Mark[] from(final Student student) throws RepositoryException;

    void nowInclude(final Mark mark) throws RepositoryException;
}
