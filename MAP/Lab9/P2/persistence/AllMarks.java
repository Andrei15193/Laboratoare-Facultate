package persistence;

import data.iterators.StreamIterator;
import domain.Mark;
import domain.Student;

public interface AllMarks
{
    StreamIterator<Mark> from(final Student student)
                    throws PersistenceException;

    void nowInclude(final Mark newMark) throws PersistenceException;
}
