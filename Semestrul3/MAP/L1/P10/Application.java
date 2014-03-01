// Gaseste cel mai mic numar m din sirul lui Fibonacci definit de
// f[0]=f[1]=1, f[n]=f[n-1]+f[n-2], pentru n>2, 
// mai mare decat numarul natural n dat, deci exista k astfel ca f[k]=m si m>n.

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



