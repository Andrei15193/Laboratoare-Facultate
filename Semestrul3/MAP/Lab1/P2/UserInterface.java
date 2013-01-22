// Represents a basic user interface. Can read and write data to and from the
// console.
public class UserInterface {
    // Creates a new UserInterface.
    // Preconditions:
    //      controller: a Controller
    public UserInterface(Controller controller){
        // Initialize menu
        this.menu = new Menu("Main menu");
        this.menu.addItem(new GetFirstPrimeMenuItem(controller));
        this.menu.addItem(new GetGoldbachsNumbersMenuItem(controller));
    }
    
    // Runs the interface.
    // Postconditions:
    //      Displays messages to the console.
    public void run(){
        try{
            this.menu.run(new java.io.BufferedReader(new java.io.InputStreamReader(System.in)), System.out);
        }
        catch (java.io.IOException exception){
            System.out.println("An unexpected I/O error occurred, terminating");
        }
    }
    
    private Menu menu;
}
