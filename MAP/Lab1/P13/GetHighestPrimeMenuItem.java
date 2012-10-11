import java.io.IOException;

// Represents a specialized MenuItem that is destined to read a number from a given
// input and prints the result to the given output.
public class GetHighestPrimeMenuItem extends MenuItem {
    // Creates a new GetFirstPrimeMenuItem instance.
    // Preconditions:
    //      controller: a Controller that is used for business logic.
    public GetHighestPrimeMenuItem(Controller controller) {
        super("Get the highest prime from an array");
        this.controller = controller;
    }

    @Override
    // Reads an array from the given input and prints to the given output the highest prime number found.
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
        int number = this.controller.getHighestPrime(readArray(input, output));
        if (number != -1)
            output.println("The highest prime number from the given array is " + number);
        else
            output.println("There is no prime number in the given array");
    }

    // Reads a number from the specified input. If the user does not enter an number he is kept in the loop
    // until the postconditions are specified.
    // Preconditions:
    //      n:      an Integer representing the lower bound of the number that will be returned.
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Returns a number from the given input.
    //              Prints messages to the given PrintStream.
    //              Throws IOError if an I/O error occurs.
    private int readNumber(java.io.BufferedReader input, java.io.PrintStream output) throws IOException{
        Integer number = null;
        do
            try{
                number = new Integer(input.readLine());
            }
        catch (java.lang.NumberFormatException exception){
            output.println("Invalid number entered!");
        }
        while (number == null);
        return number;
    }

    // Reads a number greater than the specified one. If the user does not enter a number or the number entered
    // is bellow or equal to the given one he is kept in the loop until the postconditions are satisfied.
    // Preconditions:
    //      n:      an Integer representing the lower bound of the number that will be returned.
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Returns a number greater than n from the specified input.
    //              Prints messages to the given PrintStream.
    //              Throws IOError if an I/O error occurs.
    private int readNumberGreaterThanN(int n, java.io.BufferedReader input, java.io.PrintStream output) throws IOException{
        int number;
        boolean ok = false;
        do{
            number = readNumber(input, output);
            if (number <= n)
                output.println("The number must be greater than " + n);
            else
                ok = true;
        }while (!ok);
        return number;
    }

    // Reads an array of minimum length 1 and returns it. If the user does not enter a valid array length (a
    // number greater than 0) or number he is kept in the loop until he does so.
    // Preconditions:
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Returns an array of minimum length 1 containing numbers.
    //              Prints messages to the given PrintStream.
    //              Throws IOError if an I/O error occurs.
    private int[] readArray(java.io.BufferedReader input, java.io.PrintStream output) throws IOException{
        output.print("Enter the length of the array: ");
        int[] newArray = new int[readNumberGreaterThanN(0, input, output)];
        for (int i = 0; i < newArray.length; i++){
            output.print("array[" + i + "]=");
            newArray[i] = readNumber(input, output);
        }
        return newArray;
    }

    private Controller controller;
}
