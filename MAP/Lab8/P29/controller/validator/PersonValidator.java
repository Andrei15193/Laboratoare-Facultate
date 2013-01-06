package controller.validator;

import java.util.LinkedList;

import utils.Utils;
import domain.Person;

public class PersonValidator implements Validator<Person>
{
    @Override
    public Iterable<String> validate(Person person)
    {
        final String personId = person.getId();
        final String personName = person.getName();
        LinkedList<String> errors = new LinkedList<String>();
        if (personName.length() == 0)
            errors.add("There is no person name!");
        else
            if (!personName.matches(Utils.nameRegEx))
                errors.add("The person name is invalid! The name can contain letters, dashes or spaces!");
        if (personId.matches("^[01]([0-9]{2})("
                        + "((0[13578]|10|12)(([0-2][1-9])|([1-3]0)|(31)))"
                        + "|((0[469]|11)(([0-2][1-9])|([1-3]0)))"
                        + "|(02(([0-2][0-9])|([12]0)))" + ")[0-9]{6}$"))
        {
            if (Integer.parseInt(personId.substring(1, 3)) % 4 != 0
                            && Integer.parseInt(personId.substring(3, 5)) == 2
                            && Integer.parseInt(personId.substring(5, 7)) == 29)
                errors.add("A person can only be born on the 29th of February if the year is a leap year!");
        }
        else
            errors.add("The person id is invalid! Only numerical characters are allowed and must respect the personal identification number rules!");
        return errors;
    }
}
