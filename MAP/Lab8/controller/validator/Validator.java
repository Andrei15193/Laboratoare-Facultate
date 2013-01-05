package controller.validator;

public interface Validator<E>
{
    public Iterable<String> validate(E instance);
}
