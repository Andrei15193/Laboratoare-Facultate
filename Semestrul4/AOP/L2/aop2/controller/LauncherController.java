package aop2.controller;

import javax.swing.JOptionPane;

import aop2.domain.MarksApplication;
import aop2.persistence.RepositoryException;
import aop2.presentation.SecretaryFrame;
import aop2.presentation.StudentFrame;


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
