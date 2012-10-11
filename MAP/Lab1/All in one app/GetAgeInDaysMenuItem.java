// Represents a specialized MenuItem that is destined to read a number from a given
// input and prints the result to the given output.
public class GetAgeInDaysMenuItem extends MenuItem {
    // Creates a new GetFirstPrimeMenuItem instance.
    // Preconditions:
    //      controller: a Controller that is used for business logic.
    public GetAgeInDaysMenuItem(Controller controller) {
        super("Get the age in days with a given birth date");
        this.controller = controller;
    }

    @Override
    // Reads a date from the given input and outputs the difference between the
    // current date and given date in days. If the red date does not fit the
    // required format or does not predate the current date an according
    // message is displayed.
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
        int days = -1;
        boolean ok = false;
        java.util.Date givenDate = null;
        java.text.SimpleDateFormat dateParser = new java.text.SimpleDateFormat("dd/MM/yyyy");
        do{
            try{
                output.print("Enter a date (format: dd/mm/yyyy): ");
                givenDate = dateParser.parse(input.readLine());
                days = this.controller.getAgeInDays(givenDate);
                if (days == -1)
                    output.println("Invalid date! The entered date must predate the current date");
                else
                    ok = true;
            }
            catch (java.text.ParseException exception){
                output.println("Invalid date format entered!");
            }
        }while (!ok);
        output.println("The difference in days between the current date and the given one is " + days);
    }

    private Controller controller;
}
