import static org.junit.Assert.assertEquals;
import org.junit.Test;

// Represents the Application controller. Does business logic upon request.
public class Controller {
    // Gets the age of a person in days.
    // Preconditions:
    //      birthDate: a Date that represents the birth dare of the person.
    // Postconditions:
    //      Returns an integer representing the number of days if the given
    //      birth date predates the current date, -1 otherwise.
    public int getAgeInDays(java.util.Date birthDate){
        java.util.Date today = new java.util.Date();
        if (today.after(birthDate))
            return (int)((today.getTime() - birthDate.getTime()) / (1000 * 3600 * 24));
        else
            return -1;
    }

    @Test
    public void test_getAgeInDays(){
        try{
            assertEquals(-1, getAgeInDays((new java.text.SimpleDateFormat("dd/MM/yyyy")).parse("1/1/9999")));
            assertEquals(true, -1 != getAgeInDays((new java.text.SimpleDateFormat("dd/MM/yyyy")).parse("1/1/2000")));
        }
        catch (java.text.ParseException exception){
            assertEquals(true, false);
        }
    }
}
