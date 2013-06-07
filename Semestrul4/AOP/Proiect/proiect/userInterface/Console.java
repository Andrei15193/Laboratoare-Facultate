package proiect.userInterface;

import proiect.controller.ApplicationController;
import proiect.controller.EntityReader;
import proiect.domain.Firm;
import proiect.domain.FirmException;

// Represents the console user interface (command line).
public class Console
{
    // Creates a new Console instance having a reference to the specified
    // ApplicationController.
    public Console(ApplicationController controller)
    {
        this.input = new java.io.BufferedReader(new java.io.InputStreamReader(
                        System.in));
        this.output = System.out;
        this.applicationController = controller;
    }

    // Runs the console.
    public void run()
    {
        try
        {
            int option;
            do
            {
                this.showFirms();
                this.showMenu();
                this.output.print("Your option is: ");
                option = this.readOption() - 1;
                if (FirmMenuOptions.Add_Firm.ordinal() == option)
                    this.addFirm();
                else
                    if (FirmMenuOptions.Classify_Firms.ordinal() == option)
                        this.classifyFirms();
            }
            while (option != FirmMenuOptions.Exit.ordinal());
            this.output.println("Application is closing");
        }
        catch (Exception e)
        {
            this.printError("An unhandled exception occurred! "
                            + e.getMessage());
        }
    }

    // Adds a firm.
    public void addFirm()
    {
        String firmName;
        Integer firmTurnover;
        try
        {
            this.output.println("Adding a Firm:");
            this.output.print("Firm name:     ");
            firmName = this.input.readLine();
            this.output.print("Firm turnover: ");
            try
            {
                firmTurnover = new Integer(this.input.readLine());
            }
            catch (NumberFormatException exception)
            {
                firmTurnover = null;
            }
            this.applicationController.addFirm(firmName, firmTurnover);
        }
        catch (FirmException e)
        {
            this.printError(e.getMessage());
        }
        catch (java.io.IOException e1)
        {
            this.printError("An I/O error occurred. Please try again. ");
        }
    }

    // Classifies the firms
    public void classifyFirms()
    {
        this.output.println("Classify Firms:");
        this.output.println("This operation will create 3 (three) files that end "
                        + "with Low, Medium, and Hight. The rest of the file name is the "
                        + "same for all. In the file ended with Low you will find all "
                        + "firms with the turnover lower than 50, in the file ended with "
                        + "Medium you will find all firms with the turnover between 50 and "
                        + "100. In the file eneded with High you will find all other firms.");
        this.output.print("Enter common file name part: ");
        try
        {
            this.applicationController.filterFirms(this.input.readLine());
        }
        catch (java.io.IOException e)
        {
            this.printError("An I/O error occurred. Please try again. ");
        }
    }

    // Prints the specified error message.
    private void printError(String message)
    {
        this.output.println(" > ERROR! <");
        this.output.println(" > " + message);
        this.output.println();
    }

    // Prints all the firms contained.
    private void showFirms()
    {
        String firms = "";
        EntityReader<Firm> firmReader = this.applicationController
                        .getAllFirms();
        this.output.print("The registered firms are: ");
        while (firmReader.next())
            firms += firmReader.getCurrentEntity().toString() + ", ";
        this.output.println(firms.substring(0, firms.length() - 2));
    }

    // Prints the menu.
    private void showMenu()
    {
        for (FirmMenuOptions item: FirmMenuOptions.values())
            this.output.println(item.ordinal() + 1 + ". "
                            + item.name().replaceAll("_", " "));
    }

    // Reads an option (Integer). If an invalid number format is entered -1 will
    // be
    // returned.
    private int readOption()
    {
        try
        {
            return new Integer(this.input.readLine());
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    private final java.io.BufferedReader input;
    private final java.io.PrintStream output;
    private final ApplicationController applicationController;
}
