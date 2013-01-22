package userInterface;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.Point;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;

import controller.Controller;
import controller.ValidatorException;
import domain.BankException;

public class MainWindow extends JFrame
{
    public MainWindow(final Controller controller)
    {
        super("Main window");
        this.controller = controller;
        this.buildForm();
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    @Override
    public void setSize(int width, int height)
    {
        this.getContentPane().setPreferredSize(new Dimension(width, height));
        this.pack();
    }

    @Override
    public void setSize(Dimension dimension)
    {
        this.getContentPane().setPreferredSize(dimension);
        this.pack();
    }

    private void buildForm()
    {
        this.setLayout(null);
        this.setResizable(false);
        this.setLocation(150, 150);
        this.setSize(400, 200);
        this.setJMenuBar(this.buildMenuBar());
        this.add(this.buildTableView());
        this.pack();
    }

    private Component buildTableView()
    {
        this.depositTablePanel = new JPanel(new GridLayout(1, 1));
        this.depositTablePanel.setVisible(false);
        this.depositTablePanel.setLocation(10, 10);
        this.depositTablePanel.setSize(380, 180);
        this.depositTablePanel.add(new JScrollPane(new JTable(this.controller
                        .getDepositTableModel())));
        return this.depositTablePanel;
    }

    private JMenuBar buildMenuBar()
    {
        final JMenuBar menu = new JMenuBar();
        menu.setLayout(new FlowLayout(FlowLayout.LEFT, 5, 0));
        menu.add(this.buildEditMenu());
        menu.add(this.buildLogInMenuItem());
        menu.add(this.buildLoggedInMenu());
        return menu;
    }

    private JMenuItem buildLogInMenuItem()
    {
        this.logInMenuItem = new JMenuItem("Log in");
        this.logInMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                new LogInDialog().setVisible(true);
            }
        });
        return this.logInMenuItem;
    }

    private JMenu buildLoggedInMenu()
    {
        this.loggedInMenu = new JMenu("Deposit operations");
        this.logOutMenuItem = new JMenuItem("Log out");
        final JMenuItem addDepositMenuItem = new JMenuItem("Add deposit");
        final JMenuItem showDepositsAndSumsMenuItem = new JMenuItem(
                        "Show deposits and sums");
        final JMenuItem showDepositsAndInterestsMenuItem = new JMenuItem(
                        "Show deposits and bank interests");
        final JMenuItem showAllDepositInformationMenuItem = new JMenuItem(
                        "Show all deposit information");
        this.logOutMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                MainWindow.this.controller.logOut();
                MainWindow.this.loggedInMenu.setVisible(false);
                MainWindow.this.logInMenuItem.setVisible(true);
                MainWindow.this.depositTablePanel.setVisible(false);
            }
        });
        addDepositMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent arg0)
            {
                new AddDepositDialog().setVisible(true);
            }
        });
        showDepositsAndSumsMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                MainWindow.this.controller.showDepositsWithSums();
            }
        });
        showDepositsAndInterestsMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                MainWindow.this.controller.showDepositsWithInterest();
            }
        });
        showAllDepositInformationMenuItem
                        .addActionListener(new ActionListener()
                        {
                            @Override
                            public void actionPerformed(ActionEvent e)
                            {
                                MainWindow.this.controller.showAllDepositData();
                            }
                        });
        this.loggedInMenu.add(this.logOutMenuItem);
        this.loggedInMenu.add(addDepositMenuItem);
        this.loggedInMenu.add(showDepositsAndSumsMenuItem);
        this.loggedInMenu.add(showDepositsAndInterestsMenuItem);
        this.loggedInMenu.add(showAllDepositInformationMenuItem);
        this.loggedInMenu.setVisible(false);
        return this.loggedInMenu;
    }

    private JMenu buildEditMenu()
    {
        final JMenu editMenu = new JMenu("Edit");
        final JMenuItem addBankMenuItem = new JMenuItem("Add bank");
        final JMenuItem addPersonMenuItem = new JMenuItem("Add person");
        addBankMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                new AddBankDialog().setVisible(true);
            }
        });
        addPersonMenuItem.addActionListener(new ActionListener()
        {
            @Override
            public void actionPerformed(ActionEvent e)
            {
                new AddPersonDialog().setVisible(true);
            }
        });
        editMenu.add(addBankMenuItem);
        editMenu.add(addPersonMenuItem);
        return editMenu;
    }

    private class AddBankDialog extends JDialog
    {
        public AddBankDialog()
        {
            super(MainWindow.this, "Add bank");
            this.buildForm();
            this.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            this.setModalityType(ModalityType.APPLICATION_MODAL);
        }

        @Override
        public void setSize(int width, int height)
        {
            this.getContentPane()
                            .setPreferredSize(new Dimension(width, height));
            this.pack();
        }

        @Override
        public void setSize(Dimension dimension)
        {
            this.getContentPane().setPreferredSize(dimension);
            this.pack();
        }

        private void buildForm()
        {
            final JPanel content = new JPanel(null);
            final Point location = MainWindow.this.getLocation();
            this.setResizable(false);
            this.setSize(280, 160);
            this.setLocation((int)(location.getX() + 20),
                            (int)(location.getY() + 20));
            this.add(content);
            content.setSize(this.getSize());
            content.add(this.buildLabels());
            content.add(this.buildTextBoxesAndRadioButtons());
            content.add(this.buildSubmitButton());
            this.pack();
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Bank name: ");
            final JLabel interestLabel = new JLabel("Bank interest: ");
            final JLabel staticInterest = new JLabel("Interest type: ");
            panel.setLocation(10, 10);
            panel.setSize(100, 80);
            nameLabel.setSize(100, 20);
            interestLabel.setLocation(0, 30);
            interestLabel.setSize(100, 20);
            staticInterest.setLocation(0, 60);
            staticInterest.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(interestLabel);
            panel.add(staticInterest);
            return panel;
        }

        private JPanel buildTextBoxesAndRadioButtons()
        {
            final JPanel panel = new JPanel(null);
            final JPanel radioPanel = this.buildRadioPannel();
            this.bankNameTextField = new JTextField();
            this.interestTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(150, 110);
            this.bankNameTextField.setSize(150, 20);
            this.interestTextField.setLocation(0, 30);
            this.interestTextField.setSize(150, 20);
            radioPanel.setLocation(0, 60);
            panel.add(this.bankNameTextField);
            panel.add(this.interestTextField);
            panel.add(radioPanel);
            return panel;
        }

        private JPanel buildRadioPannel()
        {
            final JPanel panel = new JPanel(null);
            final ButtonGroup buttonGroup = new ButtonGroup();
            this.staticInterestRadioButton = new JRadioButton("Static", true);
            this.dynamicInterestRadioButton = new JRadioButton("Dynamic");
            this.staticInterestRadioButton.setSize(100, 20);
            this.dynamicInterestRadioButton.setLocation(0, 30);
            this.dynamicInterestRadioButton.setSize(100, 20);
            panel.setSize(100, 50);
            buttonGroup.add(this.staticInterestRadioButton);
            buttonGroup.add(this.dynamicInterestRadioButton);
            panel.add(this.staticInterestRadioButton);
            panel.add(this.dynamicInterestRadioButton);
            return panel;
        }

        private JButton buildSubmitButton()
        {
            final JButton button = new JButton("Submit");
            button.setLocation(90, 130);
            button.setSize(100, 20);
            button.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent action)
                {
                    try
                    {
                        if (MainWindow.this.controller.addBank(
                                        AddBankDialog.this.bankNameTextField
                                                        .getText(),
                                        AddBankDialog.this.staticInterestRadioButton
                                                        .isSelected(),
                                        new Double(
                                                        AddBankDialog.this.interestTextField
                                                                        .getText())))
                        {
                            JOptionPane.showMessageDialog(
                                            AddBankDialog.this,
                                            "The bank has been successfully added!",
                                            "Success!",
                                            JOptionPane.INFORMATION_MESSAGE);
                            AddBankDialog.this.dispose();
                        }
                        else
                        {
                            JOptionPane.showMessageDialog(
                                            AddBankDialog.this,
                                            "The bank name already exists in the repository!",
                                            "Error!", JOptionPane.ERROR_MESSAGE);
                        }
                    }
                    catch (BankException | ValidatorException e)
                    {
                        JOptionPane.showMessageDialog(AddBankDialog.this,
                                        e.getMessage(), "Error!",
                                        JOptionPane.ERROR_MESSAGE);
                    }
                    catch (NumberFormatException e)
                    {
                        JOptionPane.showMessageDialog(AddBankDialog.this,
                                        "The bank interest must be a number!",
                                        "Error!", JOptionPane.ERROR_MESSAGE);
                    }
                }
            });
            return button;
        }

        private JTextField bankNameTextField;
        private JTextField interestTextField;
        private JRadioButton staticInterestRadioButton;
        private JRadioButton dynamicInterestRadioButton;
        private static final long serialVersionUID = 1L;
    }

    private class AddPersonDialog extends JDialog
    {
        public AddPersonDialog()
        {
            super(MainWindow.this, "Add person");
            this.buildForm();
            this.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            this.setModalityType(ModalityType.APPLICATION_MODAL);
        }

        @Override
        public void setSize(int width, int height)
        {
            this.getContentPane()
                            .setPreferredSize(new Dimension(width, height));
            this.pack();
        }

        @Override
        public void setSize(Dimension dimension)
        {
            this.getContentPane().setPreferredSize(dimension);
            this.pack();
        }

        private void buildForm()
        {
            final JPanel content = new JPanel(null);
            final Point location = MainWindow.this.getLocation();
            this.setResizable(false);
            this.setSize(280, 100);
            this.setLocation((int)(location.getX() + 20),
                            (int)(location.getY() + 20));
            this.add(content);
            content.setSize(this.getSize());
            content.add(this.buildLabels());
            content.add(this.buildTextBoxes());
            content.add(this.buildSubmitButton());
            this.pack();
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Person name: ");
            final JLabel idLabel = new JLabel("Person ID: ");
            panel.setLocation(10, 10);
            panel.setSize(100, 50);
            nameLabel.setSize(100, 20);
            idLabel.setLocation(0, 30);
            idLabel.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(idLabel);
            return panel;
        }

        private JPanel buildTextBoxes()
        {
            final JPanel panel = new JPanel(null);
            this.personNameTextField = new JTextField();
            this.personIdTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(150, 50);
            this.personNameTextField.setSize(150, 20);
            this.personIdTextField.setLocation(0, 30);
            this.personIdTextField.setSize(150, 20);
            panel.add(this.personNameTextField);
            panel.add(this.personIdTextField);
            return panel;
        }

        private JButton buildSubmitButton()
        {
            final JButton button = new JButton("Submit");
            button.setLocation(90, 70);
            button.setSize(100, 20);
            button.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent action)
                {
                    try
                    {
                        if (MainWindow.this.controller
                                        .addPerson(AddPersonDialog.this.personNameTextField
                                                        .getText(),
                                                        AddPersonDialog.this.personIdTextField
                                                                        .getText()))
                        {
                            JOptionPane.showMessageDialog(
                                            AddPersonDialog.this,
                                            "The person has been successfully added!",
                                            "Success!",
                                            JOptionPane.INFORMATION_MESSAGE);
                            AddPersonDialog.this.dispose();
                        }
                        else
                        {
                            JOptionPane.showMessageDialog(
                                            AddPersonDialog.this,
                                            "The person id already exists in the repository!",
                                            "Error!", JOptionPane.ERROR_MESSAGE);
                        }
                    }
                    catch (ValidatorException e)
                    {
                        JOptionPane.showMessageDialog(AddPersonDialog.this,
                                        e.getMessage(), "Error!",
                                        JOptionPane.ERROR_MESSAGE);
                    }
                }
            });
            return button;
        }

        private JTextField personNameTextField;
        private JTextField personIdTextField;
        private static final long serialVersionUID = 1L;
    }

    private class LogInDialog extends JDialog
    {
        public LogInDialog()
        {
            super(MainWindow.this, "Add person");
            this.buildForm();
            this.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            this.setModalityType(ModalityType.APPLICATION_MODAL);
        }

        @Override
        public void setSize(int width, int height)
        {
            this.getContentPane()
                            .setPreferredSize(new Dimension(width, height));
            this.pack();
        }

        @Override
        public void setSize(Dimension dimension)
        {
            this.getContentPane().setPreferredSize(dimension);
            this.pack();
        }

        private void buildForm()
        {
            final JPanel content = new JPanel(null);
            final Point location = MainWindow.this.getLocation();
            this.setResizable(false);
            this.setSize(280, 100);
            this.setLocation((int)(location.getX() + 20),
                            (int)(location.getY() + 20));
            this.add(content);
            content.setSize(this.getSize());
            content.add(this.buildLabels());
            content.add(this.buildTextBoxes());
            content.add(this.buildSubmitButton());
            this.pack();
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Person name: ");
            final JLabel idLabel = new JLabel("Person ID: ");
            panel.setLocation(10, 10);
            panel.setSize(100, 50);
            nameLabel.setSize(100, 20);
            idLabel.setLocation(0, 30);
            idLabel.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(idLabel);
            return panel;
        }

        private JPanel buildTextBoxes()
        {
            final JPanel panel = new JPanel(null);
            this.personNameTextField = new JTextField();
            this.personIdTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(150, 50);
            this.personNameTextField.setSize(150, 20);
            this.personIdTextField.setLocation(0, 30);
            this.personIdTextField.setSize(150, 20);
            panel.add(this.personNameTextField);
            panel.add(this.personIdTextField);
            return panel;
        }

        private JButton buildSubmitButton()
        {
            final JButton button = new JButton("Submit");
            button.setLocation(90, 70);
            button.setSize(100, 20);
            button.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent action)
                {
                    final String personName = LogInDialog.this.personNameTextField
                                    .getText();
                    if (MainWindow.this.controller.logIn(personName,
                                    LogInDialog.this.personIdTextField
                                                    .getText()))
                    {
                        JOptionPane.showMessageDialog(LogInDialog.this,
                                        "You have successfully logged in!",
                                        "Success!",
                                        JOptionPane.INFORMATION_MESSAGE);
                        MainWindow.this.logOutMenuItem.setText("Log out ("
                                        + personName + ")");
                        MainWindow.this.logInMenuItem.setVisible(false);
                        MainWindow.this.loggedInMenu.setVisible(true);
                        MainWindow.this.depositTablePanel.setVisible(true);
                        LogInDialog.this.dispose();
                    }
                    else
                        JOptionPane.showMessageDialog(
                                        LogInDialog.this,
                                        "Bad person name or id. Could not log in!",
                                        "Success!", JOptionPane.ERROR_MESSAGE);
                }
            });
            return button;
        }

        private JTextField personNameTextField;
        private JTextField personIdTextField;
        private static final long serialVersionUID = 1L;
    }

    private class AddDepositDialog extends JDialog
    {
        public AddDepositDialog()
        {
            super(MainWindow.this, "Add deposit");
            this.buildForm();
            this.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
            this.setModalityType(ModalityType.APPLICATION_MODAL);
        }

        @Override
        public void setSize(int width, int height)
        {
            this.getContentPane()
                            .setPreferredSize(new Dimension(width, height));
            this.pack();
        }

        @Override
        public void setSize(Dimension dimension)
        {
            this.getContentPane().setPreferredSize(dimension);
            this.pack();
        }

        private void buildForm()
        {
            final JPanel content = new JPanel(null);
            final Point location = MainWindow.this.getLocation();
            this.setResizable(false);
            this.setSize(280, 160);
            this.setLocation((int)(location.getX() + 20),
                            (int)(location.getY() + 20));
            this.add(content);
            content.setSize(this.getSize());
            content.add(this.buildLabels());
            content.add(this.buildTextBoxesAndRadioButtons());
            content.add(this.buildSubmitButton());
            this.pack();
        }

        private JPanel buildLabels()
        {
            final JPanel panel = new JPanel(null);
            final JLabel nameLabel = new JLabel("Bank name: ");
            final JLabel interestLabel = new JLabel("Deposit sum: ");
            final JLabel staticInterest = new JLabel("Capitalization: ");
            panel.setLocation(10, 10);
            panel.setSize(100, 80);
            nameLabel.setSize(100, 20);
            interestLabel.setLocation(0, 30);
            interestLabel.setSize(100, 20);
            staticInterest.setLocation(0, 60);
            staticInterest.setSize(100, 20);
            panel.add(nameLabel);
            panel.add(interestLabel);
            panel.add(staticInterest);
            return panel;
        }

        private JPanel buildTextBoxesAndRadioButtons()
        {
            final JPanel panel = new JPanel(null);
            final JPanel radioPanel = this.buildRadioPannel();
            this.bankNameTextField = new JTextField();
            this.depositSumTextField = new JTextField();
            panel.setLocation(120, 10);
            panel.setSize(150, 110);
            this.bankNameTextField.setSize(150, 20);
            this.depositSumTextField.setLocation(0, 30);
            this.depositSumTextField.setSize(150, 20);
            radioPanel.setLocation(0, 60);
            panel.add(this.bankNameTextField);
            panel.add(this.depositSumTextField);
            panel.add(radioPanel);
            return panel;
        }

        private JPanel buildRadioPannel()
        {
            final JPanel panel = new JPanel(null);
            final ButtonGroup buttonGroup = new ButtonGroup();
            this.autoCapitalizeRadioButton = new JRadioButton("Automatic", true);
            this.endDepoRadioButton = new JRadioButton("None");
            this.autoCapitalizeRadioButton.setSize(100, 20);
            this.endDepoRadioButton.setLocation(0, 30);
            this.endDepoRadioButton.setSize(100, 20);
            panel.setSize(100, 50);
            buttonGroup.add(this.autoCapitalizeRadioButton);
            buttonGroup.add(this.endDepoRadioButton);
            panel.add(this.autoCapitalizeRadioButton);
            panel.add(this.endDepoRadioButton);
            return panel;
        }

        private JButton buildSubmitButton()
        {
            final JButton button = new JButton("Submit");
            button.setLocation(90, 130);
            button.setSize(100, 20);
            button.addActionListener(new ActionListener()
            {
                @Override
                public void actionPerformed(ActionEvent action)
                {
                    try
                    {
                        if (MainWindow.this.controller.addDeposit(
                                        AddDepositDialog.this.bankNameTextField
                                                        .getText(),
                                        new Double(
                                                        AddDepositDialog.this.depositSumTextField
                                                                        .getText()),
                                        AddDepositDialog.this.autoCapitalizeRadioButton
                                                        .isSelected()))
                        {
                            JOptionPane.showMessageDialog(
                                            AddDepositDialog.this,
                                            "The deposit has been successfully created!",
                                            "Success!",
                                            JOptionPane.INFORMATION_MESSAGE);
                            AddDepositDialog.this.dispose();
                        }
                        else
                        {
                            JOptionPane.showMessageDialog(
                                            AddDepositDialog.this,
                                            "The deposit already exists in the repository!",
                                            "Error!", JOptionPane.ERROR_MESSAGE);
                        }
                    }
                    catch (ValidatorException e)
                    {
                        JOptionPane.showMessageDialog(AddDepositDialog.this,
                                        e.getMessage(), "Error!",
                                        JOptionPane.ERROR_MESSAGE);
                    }
                    catch (NumberFormatException e)
                    {
                        JOptionPane.showMessageDialog(AddDepositDialog.this,
                                        "The deposit sum must be a number!",
                                        "Error!", JOptionPane.ERROR_MESSAGE);
                    }
                }
            });
            return button;
        }

        private JTextField bankNameTextField;
        private JTextField depositSumTextField;
        private JRadioButton autoCapitalizeRadioButton;
        private JRadioButton endDepoRadioButton;
        private static final long serialVersionUID = 1L;
    }

    private final Controller controller;
    private JPanel depositTablePanel;
    private JMenu loggedInMenu;
    private JMenuItem logInMenuItem;
    private JMenuItem logOutMenuItem;
    private static final long serialVersionUID = 1L;
}
