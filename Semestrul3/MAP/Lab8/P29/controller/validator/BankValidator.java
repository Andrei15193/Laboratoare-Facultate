package controller.validator;

import java.util.LinkedList;

import utils.Utils;
import domain.Bank;

public class BankValidator implements Validator<Bank>
{
    @Override
    public Iterable<String> validate(Bank bank)
    {
        final double interest = bank.getInterest();
        final String bankName = bank.getName();
        LinkedList<String> errors = new LinkedList<String>();
        if (bankName.length() == 0)
            errors.add("There is no bank name!");
        else
            if (!bankName.matches(Utils.nameRegEx))
                errors.add("The bank name is invalid! The name can contain letters, dashes or spaces!");
        if (!(0 < interest && interest < 1))
            errors.add("The bank interest is invalid! The interest must be in the (0, 1) interval.");
        return errors;
    }
}
