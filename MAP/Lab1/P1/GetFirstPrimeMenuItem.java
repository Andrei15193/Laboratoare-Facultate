// Represents a specialized MenuItem that is destined to read a number from a given
// input and prints the result to the given output.
public class GetFirstPrimeMenuItem extends MenuItem {
    // Creates a new GetFirstPrimeMenuItem instance.
    // Preconditions:
    //      controller: a Controller that is used for business logic.
    public GetFirstPrimeMenuItem(Controller controller) {
        super("Get the first prime number greater than n");
        this.controller = controller;
    }

    @Override
    // Reads a number from the given input and prints the first prime number after
    // it. If a number could not be read, the user will be asked again to enter a
    // valid number.
    // Preconditions:
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Prints messages to the given PrintStream. 
    //              Throws IOError if an I/O error occurs.
    public void run(java.io.BufferedReader input, java.io.PrintStream output) throws java.io.IOException {
        Integer number = null;
        do
            try{
                output.print("Enter a number: ");
                number = new Integer(input.readLine());
            }
            catch (java.lang.NumberFormatException exception){
                output.println("Invalid number entered!");
            }
        while (number == null);
        output.println("The first prime number after " + number + " is " + this.controller.getFirstPrimeAfterN(number));
    }

    private Controller controller;
}
