package proiect.userInterface;

import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.ScrollPaneConstants;

import proiect.controller.ApplicationController;
import proiect.domain.FirmException;

// Represents the main window that will be displayed first for the User.
public class MainWindow extends JFrame
{
    // Creates a new MainWindow instance which uses the specified controller.
    public MainWindow(ApplicationController controller)
    {
        super("Firms management");
        this.controller = controller;
        this.setResizable(false);
        this.setLayout(new GridLayout(4, 1));
        this.add(new JLabel("Firms list:"));
        this.add(this.createListViewPanel());
        this.add(this.createInputPanel());
        this.add(this.createButtonPanel());
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    // Creates a panel containing a scrollable list of all Firms memorized.
    private JPanel createListViewPanel()
    {
        JPanel myPanel = new JPanel();
        myPanel.setLayout(new GridLayout(1, 1));
        myPanel.add(new JScrollPane(new JList<String>(this.controller
                        .getModel()),
                        ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS,
                        ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER));
        return myPanel;
    }

    // Creates a panel containing labels and text fields for user input;
    private JPanel createInputPanel()
    {
        JPanel myPanel = new JPanel(), firmNamePanel = new JPanel(), firmTurnoverPanel = new JPanel();
        myPanel.setLayout(new GridLayout(2, 2));
        myPanel.add(new JLabel("Firm name: "));
        firmNamePanel.add(this.firmNameTextBox = new JTextField(10));
        myPanel.add(firmNamePanel);
        myPanel.add(new JLabel("Firm turnover: "));
        firmTurnoverPanel.add(this.firmTurnoverTextBox = new JTextField(10));
        myPanel.add(firmTurnoverPanel);
        return myPanel;
    }

    // Creates a new panel containing only buttons.
    private JPanel createButtonPanel()
    {
        JPanel myPanel = new JPanel();
        JButton addFirmButton = new JButton("Add firm");
        JButton clasiffyFirmsButton = new JButton("Classify firms");
        addFirmButton.addActionListener(new AddFirmActionListener());
        clasiffyFirmsButton
                        .addActionListener(new ClasiffyFirmsActionListener());
        myPanel.add(addFirmButton);
        myPanel.add(clasiffyFirmsButton);
        return myPanel;
    }

    // Displays an error text box with the specified message.
    private void showError(String message)
    {
        JOptionPane.showMessageDialog(this, message, "Error!",
                        JOptionPane.ERROR_MESSAGE);
    }

    // Displays a text box with the given message.
    private void showMessage(String message)
    {
        JOptionPane.showMessageDialog(this, message, "Operation completed!",
                        JOptionPane.INFORMATION_MESSAGE);
    }

    // Represents the action listener for the Classify Firms button.
    private class ClasiffyFirmsActionListener implements ActionListener
    {
        @Override
        // Runs when the user clicks Classify Firms button
        public void actionPerformed(ActionEvent e)
        {
            try
            {
                MainWindow.this.controller.filterFirms("FirmsTurnover");
            }
            catch (java.io.IOException exception)
            {
                MainWindow.this.showError(exception.getMessage());
            }
        }
    }

    // Represents the action listener for the Add Firm button.
    private class AddFirmActionListener implements ActionListener
    {
        @Override
        // Runs when the user click the Add Firm button.
        public void actionPerformed(ActionEvent e)
        {
            try
            {
                String firmName = MainWindow.this.firmNameTextBox.getText();
                Integer firmTurnover = null;
                try
                {
                    firmTurnover = new Integer(
                                    MainWindow.this.firmTurnoverTextBox
                                                    .getText());
                    MainWindow.this.controller.addFirm(firmName, firmTurnover);
                    MainWindow.this.showMessage("The firm " + firmName
                                    + "and turnover " + firmTurnover
                                    + " has been added!");
                    MainWindow.this.firmNameTextBox.setText("");
                    MainWindow.this.firmTurnoverTextBox.setText("");
                }
                catch (NumberFormatException exception)
                {
                    MainWindow.this.showError("Invalid turnover! The turnover must be a number!");
                }
            }
            catch (FirmException exception)
            {
                MainWindow.this.showError(exception.getMessage());
            }
        }
    }

    private JTextField firmNameTextBox;
    private JTextField firmTurnoverTextBox;
    private final ApplicationController controller;
    private static final long serialVersionUID = 1L;
}
