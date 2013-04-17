package aop3.persistence;

import aop3.domain.Mark;
import aop3.domain.Student;

public interface AllMarks
{
    Mark[] from(final Student student) throws RepositoryException;

    void nowInclude(final Mark mark) throws RepositoryException;
}
