package repository.file;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.LinkedList;

import repository.DepositRepository;
import repository.RepositoryException;
import utils.AppendingObjectOutputStream;
import utils.Pair;
import utils.Utils;
import data.iterators.ObjectStreamIterator;
import data.iterators.StreamIterator;
import domain.Deposit;

public class DepositRepositoryFile implements DepositRepository
{
    public DepositRepositoryFile(String fileName) throws RepositoryException
    {
        this.fileName = fileName;
        this.count = 0;
        for (@SuppressWarnings("unused")
        Deposit depo: this)
            this.count++;
    }

    @Override
    public boolean add(Deposit deposit)
    {
        if (this.find(new Pair<String, String>(deposit.getBankName(), deposit
                        .getPersonId())) == null)
            return this.storeDeposit(deposit);
        else
            return false;
    }

    @Override
    public void clear()
    {
        this.count = 0;
        Utils.removeFile(this.fileName);
    }

    @Override
    public boolean contains(Deposit deposit)
    {
        boolean found = false;
        StreamIterator<Deposit> it = this.iterator();
        while (it.hasNext() && !found)
            if (deposit.equals(it.next()))
                found = true;
        it.close();
        return found;
    }

    @Override
    public Deposit find(Pair<String, String> bankNameAndPersonId)
    {
        Deposit found = null;
        StreamIterator<Deposit> it = this.iterator();
        while (it.hasNext() && found == null)
        {
            found = it.next();
            if (!bankNameAndPersonId.equals(new Pair<String, String>(found
                            .getBankName(), found.getPersonId())))
                found = null;
        }
        it.close();
        return found;
    }

    @Override
    public Iterable<Deposit> getDepositsForPerson(String personId)
    {
        LinkedList<Deposit> result = new LinkedList<Deposit>();
        for (Deposit depo: this)
        {
            if (personId.equals(depo.getPersonId()))
                result.add(depo);
        }
        return result;
    }

    @Override
    public boolean isEmpty()
    {
        return this.count == 0;
    }

    @Override
    public StreamIterator<Deposit> iterator()
    {
        StreamIterator<Deposit> iterator;
        try
        {
            iterator = new ObjectStreamIterator<Deposit>(new ObjectInputStream(
                            new FileInputStream(this.fileName)));
        }
        catch (IOException e)
        {
            iterator = Utils.<Deposit>buildStreamIteratorOverEmptyCollection();
        }
        return iterator;
    }

    @Override
    public Deposit remove(Pair<String, String> bankNameAndPersonId)
    {
        Deposit removed = null;
        ObjectOutputStream output = null;
        final String tempFileName = this.fileName + ".temp";
        try
        {
            output = new ObjectOutputStream(new FileOutputStream(tempFileName));
            for (Deposit deposit: this)
                if (!bankNameAndPersonId.equals(new Pair<String, String>(
                                deposit.getBankName(), deposit.getPersonId())))
                    output.writeObject(deposit);
                else
                    removed = deposit;
            output.close();
            if (removed != null)
                this.count--;
            Utils.removeFile(this.fileName);
            Utils.renameFile(tempFileName, this.fileName);
        }
        catch (IOException e)
        {
            Utils.removeFile(tempFileName);
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

    private boolean storeDeposit(Deposit deposit)
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
            output.writeObject(deposit);
            this.count++;
        }
        catch (IOException e)
        {
            result = false;
        }
        finally
        {
            Utils.closeStream(output);
        }
        return result;
    }

    private int count;
    private final String fileName;
}
