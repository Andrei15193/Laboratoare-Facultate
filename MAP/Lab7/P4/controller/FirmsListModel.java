package controller;

import javax.swing.AbstractListModel;

// Represents a specialized List model for displaying the Firms list.
public class FirmsListModel extends AbstractListModel{
    // Creates a new FirmsListModel instance that contains no firms.
    public FirmsListModel(){
        this.firms = new java.util.ArrayList<String>();
    }

    // Creates a new FirmsListModel instance that contains all Firms that can be
    // returned by the given EntityReader.
    public FirmsListModel(controller.EntityReader<domain.Firm> firms){
        this();
        while (firms.next())
            this.firms.add(firms.getCurrentEntity().toString());
    }
    
    // Tells the model to add the specified firm to display.
    public void addFirm(domain.Firm firm){
        int size = this.firms.size();
        this.firms.add(this.firms.size(), firm.toString());
        this.fireContentsChanged(this, size - 1, size);
    }

    @Override
    // Returns the firm found at the specified index in the list.
    public Object getElementAt(int index){
        return this.firms.get(index);
    }

    @Override
    // Returns the size of the list.
    public int getSize(){
        return firms.size();
    }
    
    private java.util.ArrayList<String> firms;
    private static final long serialVersionUID = 1L;
}
