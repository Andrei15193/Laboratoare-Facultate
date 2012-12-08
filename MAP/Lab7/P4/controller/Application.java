package controller;

import javax.swing.SwingUtilities;

// Represents the point of entry into the application. It's only method is the
// main method.
public class Application{
    // Starts the application. Creates all necessary resources. If a first parameter
    // is set then that is used as a filename where to load and store all Firms.
    public static void main(String[] args){
        final String filename;
        if (args.length > 0)
            filename = args[0];
        else
            filename = "firmsFile";
        SwingUtilities.invokeLater(new Runnable(){
            @Override
            public void run(){
                userInterface.MainWindow mainWindow = new userInterface.MainWindow(new controller.ApplicationController(new repository.FirmRepositoryFile(filename)));
                mainWindow.setSize(300, 300);
                //mainWindow.setLocation(true);
                mainWindow.setVisible(true);
            }
        });
    }
}
