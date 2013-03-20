package aop1.controller;

import javax.swing.JOptionPane;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.domain.MarksApplication;
import aop1.persistence.RepositoryException;
import aop1.presentation.SecretaryFrame;
import aop1.presentation.StudentFrame;

public class LauncherController
{
    public LauncherController(final MarksApplication marksApplication)
    {
        Application.logger.log(Level.INFO,
                        "LauncherController.LauncherController("
                                        + marksApplication
                                        + " :MarksApplication)");
        this.marksApplication = marksApplication;
    }

    public void openSecretaryFrame()
    {
        Application.logger.log(Level.INFO,
                        "LauncherController.openSecretaryFrame()");
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
        Application.logger.log(Level.INFO,
                        "LauncherController.openStudentFrame()");
        new StudentFrame(new StudentController(this.marksApplication))
                        .setVisible(true);
    }

    private final MarksApplication marksApplication;
}
