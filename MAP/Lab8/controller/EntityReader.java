package controller;

public interface EntityReader<T>
{
    T getCurrentEntity();

    Boolean next();

    void close();
}
