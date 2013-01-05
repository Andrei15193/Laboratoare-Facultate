package test.controller.validator;

import junit.framework.Assert;

import org.junit.Test;

import controller.validator.PersonValidator;
import domain.Person;

public class TestPersonValidator
{
    @Test
    public void testValidateName()
    {
        final Person[] valid = {new Person("Andrei", "1000229123456"),
                        new Person("Andrei Alex", "1000229123456"),
                        new Person("Andrei-Alex", "1000229123456"),
                        new Person("Andrei-Alex Florin", "1000229123456"),
                        new Person("Andrei Alex-Florin", "1000229123456")};
        final Person[] notValid = {new Person("-Andrei", "1000229123456"),
                        new Person("Andrei-", "1000229123456"),
                        new Person("-Andrei-", "1000229123456"),
                        new Person(" Andrei", "1000229123456"),
                        new Person("Andrei ", "1000229123456"),
                        new Person(" Andrei ", "1000229123456"),
                        new Person("Andrei- Alex", "1000229123456"),
                        new Person("Andrei -Alex", "1000229123456"),
                        new Person("Andrei Alex-", "1000229123456"),
                        new Person("-Andrei Alex", "1000229123456"),
                        new Person("-Andrei Alex-", "1000229123456"),
                        new Person("Andrei Alex ", "1000229123456"),
                        new Person(" Andrei Alex", "1000229123456"),
                        new Person(" Andrei Alex ", "1000229123456")};
        final PersonValidator validator = new PersonValidator();
        Assert.assertFalse(validator.validate(valid[0]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[1]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[2]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[3]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[4]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[0]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[1]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[2]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[3]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[4]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[5]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[6]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[7]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[8]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[9]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[10]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[11]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[12]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[13]).iterator().hasNext());
    }

    @Test
    public void testValdiateId()
    {
        final Person[] valid = {new Person("Andrei", "1000131123456"),
                        new Person("Andrei", "1000229123456"),
                        new Person("Andrei", "1010228123456"),
                        new Person("Andrei", "1000331123456"),
                        new Person("Andrei", "1000430123456"),
                        new Person("Andrei", "1000531123456"),
                        new Person("Andrei", "1000630123456"),
                        new Person("Andrei", "1000731123456"),
                        new Person("Andrei", "1000831123456"),
                        new Person("Andrei", "1000930123456"),
                        new Person("Andrei", "1001031123456"),
                        new Person("Andrei", "1001130123456"),
                        new Person("Andrei", "1001231123456"),
                        new Person("Ioana", "0000131123456"),
                        new Person("Ioana", "0000229123456"),
                        new Person("Ioana", "0010228123456"),
                        new Person("Ioana", "0000331123456"),
                        new Person("Ioana", "0000430123456"),
                        new Person("Ioana", "0000531123456"),
                        new Person("Ioana", "0000630123456"),
                        new Person("Ioana", "0000731123456"),
                        new Person("Ioana", "0000831123456"),
                        new Person("Ioana", "0000930123456"),
                        new Person("Ioana", "0001031123456"),
                        new Person("Ioana", "0001130123456"),
                        new Person("Ioana", "0001231123456")};
        final Person[] notValid = {new Person("Andrei", "1000132123456"),
                        new Person("Andrei", "1000230123456"),
                        new Person("Andrei", "1010229123456"),
                        new Person("Andrei", "1000332123456"),
                        new Person("Andrei", "1000431123456"),
                        new Person("Andrei", "1000532123456"),
                        new Person("Andrei", "1000631123456"),
                        new Person("Andrei", "1000732123456"),
                        new Person("Andrei", "1000832123456"),
                        new Person("Andrei", "1000931123456"),
                        new Person("Andrei", "1001032123456"),
                        new Person("Andrei", "1001131123456"),
                        new Person("Andrei", "1001232123456"),
                        new Person("Florin", "10012311234567"),
                        new Person("Florin", "1001231123456a"),
                        new Person("Florin", "100123112345a"),
                        new Person("Florin", "100123112345 "),
                        new Person("Florin", "2001231123456")};
        final PersonValidator validator = new PersonValidator();
        Assert.assertFalse(validator.validate(valid[0]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[1]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[2]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[3]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[4]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[5]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[6]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[7]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[8]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[9]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[10]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[11]).iterator().hasNext());
        Assert.assertFalse(validator.validate(valid[12]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[0]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[1]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[2]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[3]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[4]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[5]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[6]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[7]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[8]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[9]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[10]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[11]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[12]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[13]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[14]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[15]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[16]).iterator().hasNext());
        Assert.assertTrue(validator.validate(notValid[17]).iterator().hasNext());
    }
}
