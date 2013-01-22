package userInterface;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.*;

// Represents the main window that will be displayed first for the User.
public class MainWindow extends JFrame {
    // Creates a new MainWindow instance which uses the specified controller.
    public MainWindow(controller.ApplicationController controller){
        super("Firms management");
        this.controller = controller;
        this.setResizable(false);
        this.setLayout(new GridLayout(4, 1));
        this.add(new JLabel("Firms list:"));
        this.add(createListViewPanel());
        this.add(createInputPanel());
        this.add(createButtonPanel());
        setDefaultCloseOperation(EXIT_ON_CLOSE);
    }

    // Creates a panel containing a scrollable list of all Firms memorized.
    private JPanel createListViewPanel(){
        JPanel myPanel = new JPanel();
        myPanel.setLayout(new GridLayout(1, 1));
        myPanel.add(new JScrollPane(new JList(this.controller.getModel()), JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_NEVER));
        return myPanel;
    }
    
    // Creates a panel containing labels and text fields for user input;
    private JPanel createInputPanel(){
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
    private JPanel createButtonPanel(){
        JPanel myPanel = new JPanel();
        JButton addFirmButton = new JButton("Add firm");
        JButton clasiffyFirmsButton = new JButton("Classify firms");
        addFirmButton.addActionListener(new AddFirmActionListener());
        clasiffyFirmsButton.addActionListener(new ClasiffyFirmsActionListener());
        myPanel.add(addFirmButton);
        myPanel.add(clasiffyFirmsButton);
        return myPanel;
    }
    
    // Displays an error text box with the specified message.
    private void showError(String message){
        JOptionPane.showMessageDialog(this, message, "Error!", JOptionPane.ERROR_MESSAGE);
    }
    
    // Displays a text box with the given message.
    private void showMessage(String message){
        JOptionPane.showMessageDialog(this, message, "Operation completed!", JOptionPane.INFORMATION_MESSAGE);
    }
    
    // Represents the action listener for the Classify Firms button.
    private class ClasiffyFirmsActionListener implements ActionListener{
        @Override
        // Runs when the user clicks Classify Firms button
        public void actionPerformed(ActionEvent e) {
            try{
                MainWindow.this.controller.filterFirms("FirmsTurnover");
            }
            catch (java.io.IOException exception){
                showError(exception.getMessage());
            }
        }
    }
    
    // Represents the action listener for the Add Firm button.
    private class AddFirmActionListener implements ActionListener{
        @Override
        // Runs when the user click the Add Firm button.
        public void actionPerformed(ActionEvent e){
            try{
                String firmName = MainWindow.this.firmNameTextBox.getText();
                Integer firmTurnover = null;
                try{
                    firmTurnover = new Integer(MainWindow.this.firmTurnoverTextBox.getText());
                    MainWindow.this.controller.addFirm(firmName, firmTurnover);
                    showMessage("The firm " + firmName + "and turnover " + firmTurnover + " has been added!");
                    MainWindow.this.firmNameTextBox.setText("");
                    MainWindow.this.firmTurnoverTextBox.setText("");
                }
                catch (NumberFormatException exception){
                    showError("Invalid turnover! The turnover must be a number!");
                }
            }
            catch (domain.FirmException exception){
                showError(exception.getMessage());
            }
        }
    }

    private JTextField firmNameTextBox;
    private JTextField firmTurnoverTextBox;
    private controller.ApplicationController controller;
    private static final long serialVersionUID = 1L;
}
