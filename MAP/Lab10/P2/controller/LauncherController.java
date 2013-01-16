package controller;

import javax.swing.JOptionPane;

import persistence.RepositoryException;
import presentation.SecretaryFrame;
import presentation.StudentFrame;
import domain.MarksApplication;

public class LauncherController
{
    public LauncherController(final MarksApplication marksApplication)
    {
        this.marksApplication = marksApplication;
    }

    public void openSecretaryFrame()
    {
        try
        {
            new SecretaryFrame(new SecretaryController(this.marksApplication))
                            .setVisible(true);
        }
        catch (RepositoryException e)
        {
            JOptionPane.showMessageDialog(null, e.getMessage() + "\n"
                            + e.getCause().getMessage(), "Error!",
                            JOptionPane.ERROR_MESSAGE);
        }
    }

    public void openStudentFrame()
    {
        new StudentFrame(new StudentController(this.marksApplication))
                        .setVisible(true);
    }

    private final MarksApplication marksApplication;
}
