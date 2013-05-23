package aop4.validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
