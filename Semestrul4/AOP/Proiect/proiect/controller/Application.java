package proiect.controller;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.logging.LogManager;

import javax.swing.SwingUtilities;

import proiect.repository.FirmRepositoryFile;
import proiect.userInterface.MainWindow;

// Represents the point of entry into the application. It's only method is the
// main method.
public class Application
{
    // Starts the application. Creates all necessary resources. If a first
    // parameter
    // is set then that is used as a filename where to load and store all Firms.
    public static void main(String[] args)
    {
        final String filename;
        FileInputStream file = null;
        try
        {
            file = new FileInputStream("src/logging.properties");
            LogManager.getLogManager().readConfiguration(file);
        }
        catch (SecurityException | IOException e)
        {
            System.out.println("Invalid logger properties file!");
            e.printStackTrace();
        }
        finally
        {
            if (file != null)
                try
                {
                    file.close();
                }
                catch (IOException e)
                {
                }
        }
        if (args.length > 0)
            filename = args[0];
        else
            filename = "firmsFile";
        SwingUtilities.invokeLater(new Runnable()
        {
            @Override
            public void run()
            {
                MainWindow mainWindow = new MainWindow(
                                new ApplicationController(
                                                new FirmRepositoryFile(filename)));
                mainWindow.setSize(300, 300);
                mainWindow.setLocation(100, 100);
                mainWindow.setVisible(true);
            }
        });
    }
}
