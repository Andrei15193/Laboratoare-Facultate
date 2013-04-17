package aop3.validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
