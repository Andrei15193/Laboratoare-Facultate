package controller.validator;

import java.util.LinkedList;

import domain.Deposit;

public class DepositValidator implements Validator<Deposit>
{
    @Override
    public Iterable<String> validate(Deposit deposit)
    {
        LinkedList<String> errors = new LinkedList<String>();
        if (deposit.getSum() <= 0)
            errors.add("The deposit sum cannot be negative!");
        return errors;
    }
}
