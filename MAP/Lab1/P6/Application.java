// Palindromul unui numar este numarul obtinut prin scrierea cifrelor in ordine  
// inversa (Ex. palindrom(237) = 732). Pentru un n dat calculati palindromul sau.

// Represents the application
public class Application {
    // Point of entry. The program starts here.
    // Preconditions:
    //      args: refers an array of Strings that represent the command line
    //            arguments.
    // Postconditions:
    //      Solves the given problem by requesting data (the n number) and prints
    //      the solution to the standard output.
    public static void main(String[] args){
        UserInterface ui = new UserInterface(new Controller());
        ui.run();
    }
}