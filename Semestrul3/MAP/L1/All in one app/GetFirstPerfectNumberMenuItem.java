// Represents a specialized MenuItem that is destined to read a number from a given
// input and prints the result to the given output.
public class GetFirstPerfectNumberMenuItem extends MenuItem {
    // Creates a new GetFirstPrimeMenuItem instance.
    // Preconditions:
    //      controller: a Controller that is used for business logic.
    public GetFirstPerfectNumberMenuItem(Controller controller) {
        super("Get the first perfect number greater than n");
        this.controller = controller;
    }

    @Override
    // Reads a number from the given input and prints the first perfect number
    // greater than the red number and it's divisors excepts itself.
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
        String message;
        java.util.AbstractList<Integer> divisors;
        do
            try{
                output.print("Enter a number: ");
                number = new Integer(input.readLine());
            }
            catch (java.lang.NumberFormatException exception){
                output.println("Invalid number entered!");
            }
        while (number == null);
        divisors = this.controller.getPerfectNumberDivisors(number);
        message = divisors.get(divisors.size() - 1) + " = ";
        java.util.Iterator<Integer> it = divisors.iterator();
        number = it.next();
        while(it.hasNext()){
            message += number + " + ";
            number = it.next();
        }
        output.println(message.substring(0, message.length() - 3));
    }

    private Controller controller;
}
