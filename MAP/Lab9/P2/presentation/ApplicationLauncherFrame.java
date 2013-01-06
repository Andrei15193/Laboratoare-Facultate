package presentation;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.LinkedList;
import java.util.Observable;
import java.util.Observer;

import javax.swing.JButton;
import javax.swing.JFrame;

import persistence.AllCourses;
import persistence.AllMarks;
import persistence.AllStudents;
import controller.SecretaryController;
import controller.StudentController;

public class ApplicationLauncherFrame extends JFrame implements
                ApplicationLauncher
{
    public ApplicationLauncherFrame(AllCourses allCourses, AllMarks allMarks,
                    AllStudents allStudents)
    {
        super("Application launcher");
        this.allCourses = allCourses;
        this.allMarks = allMarks;
        this.allStudents = allStudents;
        this.controllers = new LinkedList<Observable>();
        this.buildForm();
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private void buildForm()
    {
        final JButton launchSecretaryApplicationButton = new JButton(
                        "Launch secretary application");
        final JButton launchStudentApplicationButton = new JButton(
                        "Launch student application");
        this.setResizable(false);
        this.setLocation(200, 200);
        this.getContentPane().setPreferredSize(new Dimension(220, 90));
        this.setLayout(null);
        launchSecretaryApplicationButton.setLocation(10, 10);
        launchSecretaryApplicationButton.setSize(200, 30);
        launchSecretaryApplicationButton.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                final SecretaryController newSecretaryController = new SecretaryController(
                                ApplicationLauncherFrame.this.allCourses,
                                ApplicationLauncherFrame.this.allMarks,
                                ApplicationLauncherFrame.this.allStudents);
                for (final Observable subject: ApplicationLauncherFrame.this.controllers)
                {
                    subject.addObserver(newSecretaryController);
                    if (subject instanceof Observer)
                        newSecretaryController.addObserver((Observer)subject);
                }
                ApplicationLauncherFrame.this.controllers
                                .addLast(newSecretaryController);
                new SecretaryApplicationFrame(ApplicationLauncherFrame.this,
                                newSecretaryController).setVisible(true);
            }
        });
        launchStudentApplicationButton.setLocation(10, 50);
        launchStudentApplicationButton.setSize(200, 30);
        launchStudentApplicationButton.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                final StudentController newStudentController = new StudentController(
                                ApplicationLauncherFrame.this.allMarks,
                                ApplicationLauncherFrame.this.allStudents);
                for (final Observable subject: ApplicationLauncherFrame.this.controllers)
                {
                    subject.addObserver(newStudentController);
                    if (subject instanceof Observer)
                        newStudentController.addObserver((Observer)subject);
                }
                ApplicationLauncherFrame.this.controllers
                                .addLast(newStudentController);
                new StudentApplicationFrame(ApplicationLauncherFrame.this,
                                newStudentController).setVisible(true);
            }
        });
        this.add(launchSecretaryApplicationButton);
        this.add(launchStudentApplicationButton);
        this.pack();
    }

    @Override
    public void closing(ApplicationFrame frame)
    {
        Observer observer = frame.getController();
        for (final Observable subject: ApplicationLauncherFrame.this.controllers)
            subject.deleteObserver(observer);
        if (observer instanceof Observable)
            ((Observable)observer).deleteObservers();
    }

    private final AllCourses allCourses;
    private final AllMarks allMarks;
    private final AllStudents allStudents;
    private final LinkedList<Observable> controllers;
    private static final long serialVersionUID = 1L;
}
