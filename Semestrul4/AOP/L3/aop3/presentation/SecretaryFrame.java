package aop3.presentation;

import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.WindowConstants;

import aop3.controller.SecretaryController;
import aop3.domain.Course;
import aop3.persistence.RepositoryException;
import aop3.validator.ValidatorException;


public class SecretaryFrame extends JFrame
{
    public SecretaryFrame(final SecretaryController controller)
    {
        super("Secretary application");
        this.controller = controller;
        this.courseList = new JList<Course>(
                        this.controller.getCourseListModel());
        this.initialize();
        this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
    }

    private void initialize()
    {
        this.setResizable(false);
        this.getContentPane().setPreferredSize(new Dimension(300, 300));
        this.setLayout(new GridLayout(1, 1));
        this.setJMenuBar(this.buildMenuBar());
        this.add(new JScrollPane(this.courseList));
        this.pack();
    }

    private JMenuBar buildMenuBar()
    {
        JMenuBar menuBar = new JMenuBar();
        JMenu menu = new JMenu("Add");
        JMenuItem addCourseMenuItem = new JMenuItem("Course");
        JMenuItem addStudentMenuItem = new JMenuItem("Student");
        JMenuItem addMarkMenuItem = new JMenuItem("Mark");
        addCourseMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                new AddCourseDialog().setVisible(true);
            }
        });
        addStudentMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                new AddStudentDialog().setVisible(true);
            }
        });
        addMarkMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                final Course course = SecretaryFrame.this.courseList
                                .getSelectedValue();
                if (course != null)
                    new AddMarkDialog(course.getName()).setVisible(true);
                else
                    SecretaryFrame.this
                                    .errorMessage("There is no course selected.");
            }
        });
        menu.add(addCourseMenuItem);
        menu.add(addStudentMenuItem);
        menu.add(addMarkMenuItem);
        menuBar.add(menu);
        return menuBar;
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

    private class AddCourseDialog extends JDialog
    {
        public AddCourseDialog()
        {
            super(SecretaryFrame.this, "Add course");
            this.setResizable(false);
            this.setModal(true);
            this.buildForm();
            this.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
        }

        private void buildForm()
        {
            this.content = new JPanel(null);
            this.add(this.content);
            this.getContentPane().setPreferredSize(new Dimension(230, 70));
            this.content.add(this.buildLabels());
            this.content.add(this.buildTextFields());
            this.content.add(this.buildSubmitButton());
            this.pack();
        }

        private JButton buildSubmitButton()
        {
            JButton submitButton = new JButton("Submit");
            submitButton.setLocation(65, 40);
            submitButton.setSize(100, 20);
            submitButton.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent arg0)
                {
                    try
                    {
                        SecretaryFrame.this.controller
                                        .addCourse(AddCourseDialog.this.nameTextField
                                                        .getText());
                        SecretaryFrame.this
                                        .confirmationMessage("The course has been successfully added!");
                        AddCourseDialog.this.dispose();
                    }
                    catch (RepositoryException exception)
                    {
                        exception.printStackTrace();
                        String message = exception.getMessage();
                        final Throwable cause = exception.getCause();
                        if (cause != null)
                            message += " " + cause.getMessage();
                        SecretaryFrame.this.errorMessage(message);
                    }
                    catch (ValidatorException exception)
                    {
                        SecretaryFrame.this.errorMessage(exception.getMessage());
                    }
                }
            });
            return submitButton;
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Course name:");
            panel.setLocation(10, 10);
            panel.setSize(100, 20);
            nameLabel.setSize(100, 20);
            panel.add(nameLabel);
            return panel;
        }

        private JPanel buildTextFields()
        {
            final JPanel panel = new JPanel(null);
            this.nameTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(100, 20);
            this.nameTextField.setSize(100, 20);
            panel.add(this.nameTextField);
            return panel;
        }

        private JPanel content;
        private JTextField nameTextField;
        private static final long serialVersionUID = 1L;
    }

    private class AddStudentDialog extends JDialog
    {
        public AddStudentDialog()
        {
            super(SecretaryFrame.this, "Add student");
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
                        SecretaryFrame.this.controller.addStudent(
                                        AddStudentDialog.this.nameTextField
                                                        .getText(),
                                        AddStudentDialog.this.passwordTextField
                                                        .getText());
                        SecretaryFrame.this
                                        .confirmationMessage("The student has been successfully added!");
                        AddStudentDialog.this.dispose();
                    }
                    catch (RepositoryException exception)
                    {
                        exception.printStackTrace();
                        String message = exception.getMessage();
                        final Throwable cause = exception.getCause();
                        if (cause != null)
                            message += " " + cause.getMessage();
                        SecretaryFrame.this.errorMessage(message);
                    }
                    catch (ValidatorException exception)
                    {
                        SecretaryFrame.this.errorMessage(exception.getMessage());
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
            this.passwordTextField = new JTextField();
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

    private class AddMarkDialog extends JDialog
    {
        public AddMarkDialog(final String courseName)
        {
            super(SecretaryFrame.this, "Add mark");
            this.courseName = courseName;
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
                        SecretaryFrame.this.controller.addMark(
                                        AddMarkDialog.this.nameTextField
                                                        .getText(),
                                        AddMarkDialog.this.courseName,
                                        new Integer(
                                                        AddMarkDialog.this.markTextField
                                                                        .getText()));
                        SecretaryFrame.this
                                        .confirmationMessage("The mark has been successfully added!");
                        AddMarkDialog.this.dispose();
                    }
                    catch (NumberFormatException exception)
                    {
                        SecretaryFrame.this
                                        .errorMessage("The mark must be a number!");
                    }
                    catch (RepositoryException exception)
                    {
                        exception.printStackTrace();
                        String message = exception.getMessage();
                        final Throwable cause = exception.getCause();
                        if (cause != null)
                            message += " " + cause.getMessage();
                        SecretaryFrame.this.errorMessage(message);
                    }
                    catch (ValidatorException exception)
                    {
                        SecretaryFrame.this.errorMessage(exception.getMessage());
                    }
                }
            });
            return submitButton;
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Student name:");
            final JLabel markLabel = new JLabel("Student mark:");
            panel.setLocation(10, 10);
            panel.setSize(100, 50);
            nameLabel.setSize(100, 20);
            markLabel.setLocation(0, 30);
            markLabel.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(markLabel);
            return panel;
        }

        private JPanel buildTextFields()
        {
            final JPanel panel = new JPanel(null);
            this.nameTextField = new JTextField();
            this.markTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(100, 50);
            this.nameTextField.setSize(100, 20);
            this.markTextField.setLocation(0, 30);
            this.markTextField.setSize(100, 20);
            panel.add(this.nameTextField);
            panel.add(this.markTextField);
            return panel;
        }

        private final String courseName;
        private JPanel content;
        private JTextField nameTextField;
        private JTextField markTextField;
        private static final long serialVersionUID = 1L;
    }

    private final JList<Course> courseList;
    private final SecretaryController controller;
    private static final long serialVersionUID = 1L;
}
