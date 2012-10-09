// Represents a menu item that can be displayed in a console.
public abstract class MenuItem {    
    // Creates a new MenuItem.
    // Preconditions:
    //      title: a String representing the MenuItem title
    public MenuItem(String title){
        this.title = title;
    }
    
    // Runs the actions assigned to the MenuItem instance. This method must be
    // overridden in subclasses. (E.g.: call a method of a controller).
    // Preconditions:
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Prints messages to the given PrintStream. 
    //              Throws IOError if an I/O error occurs.
    public abstract void run(java.io.BufferedReader input, java.io.PrintStream output) throws java.io.IOException;
    
    // Gets the menu item title.
    // Postconditions:
    //      Returns a String that represents the MenuItem title.
    public final String getTitle(){
        return this.title;
    }
    
    // Prints the menu item to the specified PrintStream instance. This is a
    // protected method because it is intended to be used, internally, by derived
    // type instances and not from outside the class.
    // Postconditions:
    //      Prints messages to the given PrintStream instance.
    protected void print(java.io.PrintStream output){
        output.println(this.title);
    }
    
    private final String title;
}
