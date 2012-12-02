package domain;

// Represents a Firm. All Firms have a name and a turnover.
public class Firm{
    // Creates a new Firm with the specified name and turnover. If the name does
    // not contain only alpha numerical, spaces, dashes or any of the two
    // arguments are null an exception is thrown.
    public Firm(String name, Integer turnover) throws domain.FirmException{
        this.name = name;
        this.turnover = turnover;
        this.validate();
    }
    
    // Validates the calling instance. See the constructor constraints to know when
    // this method throws an exception.
    public void validate() throws domain.FirmException{
        String errors = this.validateName() + this.validateTurnover();
        if (errors.length() > 0)
            throw new domain.FirmException(errors);
    }
    
    @Override
    // Checks if two Firm instances are equal.
    public boolean equals(Object object){
        if (object instanceof domain.Firm){
            domain.Firm firm = (domain.Firm)object;
            return this.name.equals(firm.name) && this.turnover.equals(firm.turnover);
        }
        else
            return false;
    }
    
    @Override
    // Returns the string representation of the calling instance. In this case it's
    // the firm name followed by the firm turnover in brackets.
    public String toString(){
        return this.name + "(" + this.turnover + ")";
    }
    
    // Gets the name of the calling Firm instance (same as toString()).
    public String getName(){
        return this.name;
    }
    
    // Sets the name of the calling Firm instance. See constructor parameter
    // constraints to know when this method throws an Exception.
    public void setName(String name) throws domain.FirmException{
        String errors;
        this.name = name;
        errors = this.validateName();
        if (errors.length() > 0)
            throw new domain.FirmException(errors);
    }
    
    // Gets the turnover of the calling Firm instance.
    public Integer getTurnover(){
        return this.turnover;
    }
    
    // Sets the turnover of the calling Firm instance. See constructor parameter
    // constraints to know when this method throws an Exception.
    public void setTurnover(Integer turnover) throws domain.FirmException{
        String errors;
        this.turnover = turnover;
        errors = this.validateTurnover();
        if (errors.length() > 0)
            throw new domain.FirmException(errors);
    }
    
    // Returns a string of errors after validating the calling instance's name.
    // If no errors occur the return string is empty (it's length is 0).
    private String validateName(){
        String errors = "";
        if (this.name == null)
            errors += "Invalid firm name! There must be a firm name! ";
        else if (!this.name.matches("^[a-zA-Z0-9 -]+$"))
            errors += "Invalid firm name! The firm name can contain only alphanumerical characters, spaces or dashes! ";
        return errors;
    }

    // Returns a string of errors after validating the calling instance's turnover.
    // If no errors occur the return string is empty (it's length is 0).
    private String validateTurnover(){
        String errors = "";
        if (this.turnover == null)
            errors += "Invalid firm turnover! The turnover must be a number! ";
        return errors;
    }
    
    private Integer turnover;
    private String name;
}
