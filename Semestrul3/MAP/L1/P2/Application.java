// Dandu-se numarul natural n, determina numerele prime p1 si p2 astfel ca
// n  = p1 + p2 (verificarea ipotezei lui Goldbach).

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
