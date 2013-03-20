package aop1.presentation;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JFrame;

import org.apache.log4j.Level;

import aop1.Application;
import aop1.controller.LauncherController;

public class ApplicationLauncherFrame extends JFrame
{
    public ApplicationLauncherFrame(final LauncherController controller)
    {
        super("Launcher");
        Application.logger.log(Level.INFO,
                        "ApplicationLauncherFrame.ApplicationLauncherFrame("
                                        + controller + "LauncherController)");
        this.controller = controller;
        this.initialize();
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private void initialize()
    {
        Application.logger.log(Level.INFO,
                        "ApplicationLauncherFrame.initialize()");
        final JButton launchSecreateryApplicationButton = new JButton(
                        "Launch secretary application");
        final JButton launchStudentApplicationButton = new JButton(
                        "Launch student application");
        this.setResizable(false);
        this.setLayout(null);
        this.getContentPane().setPreferredSize(new Dimension(220, 90));
        launchSecreateryApplicationButton.setLocation(10, 10);
        launchSecreateryApplicationButton.setSize(200, 30);
        launchSecreateryApplicationButton
                        .addActionListener(new ActionListener()
                        {
                            @Override
                            public void actionPerformed(ActionEvent arg0)
                            {
                                ApplicationLauncherFrame.this.controller
                                                .openSecretaryFrame();
                            }
                        });
        launchStudentApplicationButton.setLocation(10, 50);
        launchStudentApplicationButton.setSize(200, 30);
        launchStudentApplicationButton.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                ApplicationLauncherFrame.this.controller.openStudentFrame();
            }
        });
        this.add(launchSecreateryApplicationButton);
        this.add(launchStudentApplicationButton);
        this.pack();
    }

    private final LauncherController controller;
    private static final long serialVersionUID = 1L;
}
