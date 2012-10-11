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
        this.menu.addItem(new GetAgeInDaysMenuItem(controller));
        this.menu.addItem(new GetFirstTwinPrimesMenuItem(controller));
        this.menu.addItem(new GetProductMenuItem(controller));
        this.menu.addItem(new GetMirroredMenuItem(controller));
        this.menu.addItem(new GetFirstPerfectNumberMenuItem(controller));
        this.menu.addItem(new GetPreviousPrimeMenuItem(controller));
        this.menu.addItem(new GetPreviousPerfectNumberMenuItem(controller));
        this.menu.addItem(new GetGreaterFibonacciMenuItem(controller));
        this.menu.addItem(new GetProductOfPrimesMenuItem(controller));
        this.menu.addItem(new GetSumOfPrimesMenuItem(controller));
        this.menu.addItem(new GetHighestPrimeMenuItem(controller));
        this.menu.addItem(new GetLowestPrimeMenuItem(controller));
        this.menu.addItem(new GetSumOfNonPrimesMenuItem(controller));
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
