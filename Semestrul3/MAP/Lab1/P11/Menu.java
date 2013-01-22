// Represents a menu, extends the MenuItem class to allow a Menu to contain another
// Menu.
public class Menu extends MenuItem{    
    // Creates a new Menu.
    // Preconditions:
    //      title: a String representing the title of the menu (displayed if
    //             the new menu becomes a sub-menu).
    public Menu(String title){
        super(title);
        this.menuItems = new java.util.ArrayList<MenuItem>();
    }

    // Adds the specified MenuItem to the menu.
    // Preconditions:
    //      item: a MenuItem that will be stored into the Menu.
    //            NOTE! The item is not copied! Any change to the MenuItem outside
    //                  the Menu instance will affect the given MenuItem instance!
    // Postconditions:
    //      The given MenuItem instance will be stored inside the Menu.
    public void addItem(MenuItem item){
        this.menuItems.add(item);
    }

    @Override
    // Runs the menu by displaying it's content to the given PrintStream (output)
    // instance and reads from the given BufferedReader (input) instance.
    // Preconditions:
    //      input:  a BufferedReader from which instructions are read.
    //              Can be a file or the standard input wrapped around an
    //              InputStreamReader and a BufferedReader (see Java doc for details).
    //      output: a PrintStream to which the MenuItem prints messages, can be
    //              the standard output (System.out) itself.
    // Postconditions:
    //              Prints messages to the given PrintStream. 
    //              Throws IOError if an I/O error occurs.
    public void run(java.io.BufferedReader input, java.io.PrintStream output) throws java.io.IOException{
        boolean runMenu = true;
        int option;
        do
            try{
                print(output);
                output.print("Your option is: ");
                option = Integer.parseInt(input.readLine());
                if (option != menuItems.size())
                    this.menuItems.get(option).run(input, output);
                else
                    runMenu = false;
            }
            catch (java.lang.NumberFormatException exception){
                output.println("Invalid option! You must enter a number.");
            }
            catch (java.lang.IndexOutOfBoundsException exception){
                output.println("Invalid option! You must enter a natural number that is less or equal to " + this.menuItems.size());
            }
        while (runMenu);
    }

    @Override
    // Prints the contained MenuItems title to the specified PrintStream instance.
    // This is a protected method because it is intended to be used, internally,
    // by derived type instances and not from outside the class.
    // Postconditions:
    //      Prints messages to the given PrintStream instance.
    protected void print(java.io.PrintStream output){
        int size = this.menuItems.size();
        for (int i = 0; i < size; i++)
            output.println(i + ". " + this.menuItems.get(i).getTitle());
        output.println(size + ". Back");
    }

    private java.util.AbstractList<MenuItem> menuItems;
}
