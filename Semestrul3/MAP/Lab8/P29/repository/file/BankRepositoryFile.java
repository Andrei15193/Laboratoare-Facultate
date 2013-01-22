package repository.file;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

import repository.BankRepository;
import repository.RepositoryException;
import utils.AppendingObjectOutputStream;
import utils.Pair;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Bank;
import domain.Deposit;

public class BankRepositoryFile implements BankRepository
{
    public BankRepositoryFile(final String fileName) throws RepositoryException
    {
        this.fileName = fileName;
        this.count = 0;
        for (@SuppressWarnings("unused")
        Bank bank: this)
            this.count++;
    }

    @Override
    public boolean add(Bank bank)
    {
        if (this.find(bank.getName()) == null)
            return this.storeBank(bank);
        else
            return false;
    }

    @Override
    public void clear()
    {
        this.count = 0;
        try
        {
            Utils.removeFile(this.fileName);
        }
        catch (Exception e)
        {
        }
    }

    @Override
    public boolean contains(Bank bank)
    {
        try
        {
            boolean found = false;
            StreamIterator<Bank> it = new ObjectStreamIterator<Bank>(
                            new ObjectInputStream(new FileInputStream(
                                            this.fileName)));
            while (it.hasNext() && !found)
                found = bank.equals(it.next());
            it.close();
            return found;
        }
        catch (IOException e)
        {
            return false;
        }
    }

    @Override
    public Bank find(String bankName)
    {
        Bank found = null;
        StreamIterator<Bank> it = null;
        try
        {
            it = new ObjectStreamIterator<Bank>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
            while (it.hasNext() && found == null)
            {
                found = it.next();
                if (!bankName.equals(found.getName()))
                    found = null;
            }
            it.close();
        }
        catch (IOException e)
        {
        }
        return found;
    }

    @Override
    public ArrayList<Pair<Deposit, Bank>> getBanksForDeposits(
                    Iterable<Deposit> deposits)
    {
        Bank found;
        ArrayList<Pair<Deposit, Bank>> result = new ArrayList<Pair<Deposit, Bank>>();
        for (Deposit depo: deposits)
        {
            found = this.find(depo.getBankName());
            if (found != null)
                result.add(new Pair<Deposit, Bank>(depo, found));
        }
        return result;
    }

    @Override
    public boolean isEmpty()
    {
        return this.count == 0;
    }

    @Override
    public StreamIterator<Bank> iterator()
    {
        StreamIterator<Bank> iterator = null;
        try
        {
            iterator = new ObjectStreamIterator<Bank>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
        }
        catch (IOException e)
        {
            iterator = Utils.<Bank>buildStreamIteratorOverEmptyCollection();
        }
        return iterator;
    }

    @Override
    public Bank remove(String bankName)
    {
        Bank removed = null;
        ObjectOutputStream output = null;
        final String tempFileName = this.fileName + ".temp";
        try
        {
            output = new ObjectOutputStream(new FileOutputStream(tempFileName));
            for (Bank bank: this)
                if (!bankName.equals(bank.getName()))
                    output.writeObject(bank);
                else
                    removed = bank;
            output.close();
            if (removed != null)
                this.count--;
            Utils.removeFile(this.fileName);
            Utils.renameFile(tempFileName, this.fileName);
        }
        catch (IOException e)
        {
            Utils.renameFile(tempFileName, this.fileName);
        }
        finally
        {
            Utils.closeStream(output);
        }
        return removed;
    }

    @Override
    public int size()
    {
        return this.count;
    }

    private boolean storeBank(Bank bank)
    {
        boolean result = true;
        ObjectOutputStream output = null;
        try
        {
            if (this.count == 0)
                output = new ObjectOutputStream(new FileOutputStream(
                                this.fileName));
            else
                output = new AppendingObjectOutputStream(new FileOutputStream(
                                this.fileName, true));
            output.writeObject(bank);
            this.count++;
        }
        catch (Exception e)
        {
            result = false;
        }
        finally
        {
            Utils.closeStream(output);
        }
        return result;
    }

    private final String fileName;
    private int count;
}
