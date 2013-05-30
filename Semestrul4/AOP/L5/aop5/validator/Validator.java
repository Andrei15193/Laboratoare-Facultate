package aop5.validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
