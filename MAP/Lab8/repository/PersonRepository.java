package repository;

public interface PersonRepository extends java.io.Serializable
{
    void add(domain.Person person);

    domain.Person find(String personId);

    controller.EntityReader<domain.Person> getReader();
}
