package controller;

// Represents the point of entry into the application. It's only method is the
// main method.
public class Application{
    // Starts the application. Creates all necessary resources. If a first parameter
    // is set then that is used as a filename where to load and store all Firms.
    public static void main(String[] args){
        String filename = "firmsFile";
        if (args.length > 0)
            filename = args[0];
        (new userInterface.Console(new controller.ApplicationController(new repository.FirmRepositoryFile(filename)))).run();
    }
}
