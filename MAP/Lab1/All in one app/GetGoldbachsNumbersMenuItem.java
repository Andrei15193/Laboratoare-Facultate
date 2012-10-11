// Represents a specialized MenuItem that is destined to read a number from a given
// input and prints the result to the given output.
public class GetGoldbachsNumbersMenuItem extends MenuItem {
    // Creates a new GetGoldbachsNumbersMenuItem instance.
    // Preconditions:
    //      controller: a Controller that is used for business logic.
    public GetGoldbachsNumbersMenuItem(Controller controller) {
        super("Get Goldbach's number for n");
        this.controller = controller;
    }

    @Override
    // Reads a number from the given input and prints two prime numbers that summed
    // are equal to the red number. If there is no such number a corresponding
    // message will be printed.
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
        int[] result;
        do
            try{
                output.print("Enter a number: ");
                number = new Integer(input.readLine());
            }
            catch (java.lang.NumberFormatException exception){
                output.println("Invalid number entered!");
            }
        while (number == null);
        result = controller.getGoldbachsNumbers(number);
        if (result.length != 0)
            output.println(result[0] + " + " + result[1] + " = " + number);
        else
            output.println("The number you entered cannot be a sum of two prime numbers");
    }

    private Controller controller;
}
