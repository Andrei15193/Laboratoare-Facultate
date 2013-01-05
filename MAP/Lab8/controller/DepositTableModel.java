package controller;

import java.util.ArrayList;

import javax.swing.table.AbstractTableModel;

import utils.Pair;
import domain.Bank;
import domain.Deposit;

public class DepositTableModel extends AbstractTableModel
{
    public enum DisplayData
    {
        bankNameAndSum, bankNameAndInterest, allData
    }

    public DepositTableModel()
    {
        this.displayData = DisplayData.bankNameAndSum;
        this.rows = new ArrayList<Pair<Deposit, Bank>>();
    }

    public void setDisplayData(
                    final ArrayList<Pair<Deposit, Bank>> allDepositDetail)
    {
        this.rows = allDepositDetail;
        this.fireTableStructureChanged();
        this.fireTableDataChanged();
    }

    public void setDisplayData(final DisplayData displayData,
                    final ArrayList<Pair<Deposit, Bank>> allDepositDetail)
    {
        this.displayData = displayData;
        this.rows = allDepositDetail;
        this.fireTableStructureChanged();
        this.fireTableDataChanged();
    }

    @Override
    public String getColumnName(int column)
    {
        final String result;
        switch (column)
        {
        case 0:
            result = "Bank name";
            break;
        case 1:
            if (!this.displayData.equals(DisplayData.bankNameAndSum))
            {
                result = "Bank interest";
                break;
            }
        case 2:
            result = "Sum";
            break;
        case 3:
            result = "Creation date";
            break;
        default:
            result = "No header";
            break;
        }
        return result;
    }

    @Override
    public int getColumnCount()
    {
        switch (this.displayData)
        {
        case allData:
            // bankNameInterestSumCreationDate
            return 4;
        default:
            return 2;
        }
    }

    @Override
    public int getRowCount()
    {
        return this.rows.size();
    }

    @Override
    public Object getValueAt(int line, int column)
    {
        final String result;
        Pair<Deposit, Bank> pair = this.rows.get(line);
        Deposit depo = pair.first;
        Bank bank = pair.second;
        switch (column)
        {
        case 0:
            result = bank.getName();
            break;
        case 1:
            if (!this.displayData.equals(DisplayData.bankNameAndSum))
            {
                if (bank.isInterestStatic())
                    result = bank.getInterest(depo.getCreationDate()) + "";
                else
                    result = bank.getInterest() + "";
                break;
            }
        case 2:
            result = depo.getSum() + "";
            break;
        case 3:
            result = depo.getCreationDate().toString();
            break;
        default:
            result = "No data";
            break;
        }
        return result;
    }

    private ArrayList<Pair<Deposit, Bank>> rows;
    private DisplayData displayData;
    private static final long serialVersionUID = 1L;
}
