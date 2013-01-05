package controller;

import java.util.ArrayList;
import java.util.Iterator;

import javax.swing.AbstractListModel;

import domain.Bank;

public class BankListModel extends AbstractListModel<String>
{
    public BankListModel(Iterator<Bank> banks)
    {
        this.bankNames = new ArrayList<String>();
        while (banks.hasNext())
            this.bankNames.add(banks.next().getName());
    }

    public void addBank(Bank bank)
    {
        this.bankNames.add(bank.getName());
    }

    @Override
    public String getElementAt(int index)
    {
        return this.bankNames.get(index);
    }

    @Override
    public int getSize()
    {
        return this.bankNames.size();
    }

    private final ArrayList<String> bankNames;
    private static final long serialVersionUID = 1L;
}
