package repository;

// Instances of this type are in charge of obtaining a Firm instance out of a string,
// when possible, and to return a String having a Firm instance. Applying the
// "destring" operation on a string returned by the "stringify" operation returns
// a new Firm instance that is equal in value with the original.
public class FirmStringifier{
    // Stringifies a Firm instance.
    public String stringify(domain.Firm instance){
        return instance.getName() + "|" + instance.getTurnover();
    }
    
    // Destringifies a given instance. If there are errors (not enough fields,
    // a field is invalid) a FirmException instance is thrown containing a message
    // with extended information about the nature of the error.
    public domain.Firm destring(String string) throws domain.FirmException{
        String[] fields = string.split("\\|");
        Integer turnover = null;
        if (fields.length >= 2){
            try{
                turnover = new Integer(fields[1]);
            }
            catch (NumberFormatException exception){
                throw new domain.FirmException("The specified turnover is not a number! ");
            }
            return new domain.Firm(fields[0], turnover);
        }
        else
            throw new domain.FirmException("Insufficient arguments! ");
    }
}
