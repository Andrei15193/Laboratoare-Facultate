package aop4.presentation;

import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;
import javax.swing.WindowConstants;

import aop4.controller.StudentController;
import aop4.persistence.RepositoryException;


public class StudentFrame extends JFrame
{
    public StudentFrame(final StudentController controller)
    {
        super("Student application");
        this.controller = controller;
        this.initialize();
        this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
    }

    private void initialize()
    {
        this.setResizable(false);
        this.getContentPane().setPreferredSize(new Dimension(300, 300));
        this.setLayout(null);
        this.setLayout(new GridLayout(1, 1));
        this.setJMenuBar(this.buildMenuBar());
        this.add(new JScrollPane(new JTable(this.controller
                        .getMarksTableModel())));
        this.pack();
    }

    private JMenuBar buildMenuBar()
    {
        JMenuBar menu = new JMenuBar();
        JMenuItem logInMenuItem = new JMenuItem("Log in");
        logInMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                new LogInDialog().setVisible(true);
            }
        });
        menu.add(logInMenuItem);
        return menu;
    }

    private class LogInDialog extends JDialog
    {
        public LogInDialog()
        {
            super(StudentFrame.this, "Log in");
            this.setResizable(false);
            this.setModal(true);
            this.buildForm();
            this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        }

        private void buildForm()
        {
            this.content = new JPanel(null);
            this.add(this.content);
            this.getContentPane().setPreferredSize(new Dimension(230, 100));
            this.content.add(this.buildLabels());
            this.content.add(this.buildTextFields());
            this.content.add(this.buildSubmitButton());
            this.pack();
        }

        private void errorMessage(String message)
        {
            JOptionPane.showMessageDialog(this, message, "Error!",
                            JOptionPane.ERROR_MESSAGE);
        }

        private void confirmationMessage(String message)
        {
            JOptionPane.showMessageDialog(this, message, "Success!",
                            JOptionPane.INFORMATION_MESSAGE);
        }

        private JButton buildSubmitButton()
        {
            JButton submitButton = new JButton("Submit");
            submitButton.setLocation(65, 70);
            submitButton.setSize(100, 20);
            submitButton.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent arg0)
                {
                    try
                    {
                        if (StudentFrame.this.controller.logIn(
                                        LogInDialog.this.nameTextField
                                                        .getText(),
                                        LogInDialog.this.passwordTextField
                                                        .getText()))
                        {
                            LogInDialog.this.confirmationMessage("You have successfully logged in!");
                            LogInDialog.this.dispose();
                        }
                        else
                            LogInDialog.this.errorMessage("Invalid name or password. Could not log in!");
                    }
                    catch (RepositoryException exception)
                    {
                        exception.printStackTrace();
                        String message = exception.getMessage();
                        final Throwable cause = exception.getCause();
                        if (cause != null)
                            message += " " + cause.getMessage();
                        LogInDialog.this.errorMessage(message);
                    }
                }
            });
            return submitButton;
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Student name:");
            final JLabel passwordLabel = new JLabel("Student pasword:");
            panel.setLocation(10, 10);
            panel.setSize(100, 50);
            nameLabel.setSize(100, 20);
            passwordLabel.setLocation(0, 30);
            passwordLabel.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(passwordLabel);
            return panel;
        }

        private JPanel buildTextFields()
        {
            final JPanel panel = new JPanel(null);
            this.nameTextField = new JTextField();
            this.passwordTextField = new JPasswordField();
            panel.setLocation(120, 10);
            panel.setSize(100, 50);
            this.nameTextField.setSize(100, 20);
            this.passwordTextField.setLocation(0, 30);
            this.passwordTextField.setSize(100, 20);
            panel.add(this.nameTextField);
            panel.add(this.passwordTextField);
            return panel;
        }

        private JPanel content;
        private JTextField nameTextField;
        private JTextField passwordTextField;
        private static final long serialVersionUID = 1L;
    }

    private final StudentController controller;
    private static final long serialVersionUID = 1L;
}
