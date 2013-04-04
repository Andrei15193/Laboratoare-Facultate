package aop2.validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
