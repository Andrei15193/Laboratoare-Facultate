// Genereaza cel mai mare numar perfect mai mic decat un numar n dat. In cazul in 
// care nu exista, se afiseaza mesaj. Un numar este perfect daca este egal cu suma 
// divizorilor sai, exeptandu-l pe el insusi. (6=1+2+3).

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

