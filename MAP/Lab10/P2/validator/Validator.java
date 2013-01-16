package validator;

public interface Validator<E>
{
    String[] validate(E entity);
}
