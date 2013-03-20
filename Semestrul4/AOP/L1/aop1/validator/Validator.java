package aop1.validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
