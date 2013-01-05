package repository;

import domain.Person;

public interface PersonRepository extends Iterable<Person>
{
    boolean add(Person entity);

    void clear();

    boolean contains(Person entity);

    Person find(String key);

    boolean isEmpty();

    @Override
    data.iterators.StreamIterator<Person> iterator();

    Person remove(String key);

    int size();
}
