package repository;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.util.Date;

import utils.Utils;

public class BankFile extends domain.Bank
{
    public BankFile(final String bankName, final boolean isInterestStatic,
                    final double interest) throws domain.BankException
    {
        super(bankName, isInterestStatic);
        java.io.ObjectOutputStream fileWithInterests = null;
        try
        {
            fileWithInterests = new java.io.ObjectOutputStream(
                            new java.io.FileOutputStream(this.getName()));
            fileWithInterests.writeObject(new Interest(interest, new Date()));
        }
        catch (IOException e)
        {
            throw new domain.BankException(
                            "The bank file could not be created! Please make sure you have enough rights to create files and that you are not trying to create a bank with the same name as an existing folder in the working directory!");
        }
        finally
        {
            Utils.closeStream(fileWithInterests);
        }
    }

    @Override
    public boolean addInterest(final double value, final Date startDate)
    {
        boolean result = true;
        final String fileName = this.getName();
        final String tempFileName = fileName + ".temp";
        java.io.ObjectOutputStream fileWithInterests = null;
        try
        {
            Utils.renameFile(fileName, tempFileName);
            fileWithInterests = new java.io.ObjectOutputStream(
                            new java.io.FileOutputStream(fileName));
            fileWithInterests.writeObject(new Interest(value, startDate));
            BankFile.copyInterests(fileWithInterests, tempFileName);
            Utils.removeFile(tempFileName);
        }
        catch (IOException e)
        {
            result = false;
            Utils.renameFile(tempFileName, fileName);
        }
        finally
        {
            Utils.closeStream(fileWithInterests);
        }
        return result;
    }

    @Override
    public double getInterest()
    {
        return this.getInterest(new Date());
    }

    @Override
    public double getInterest(final Date date)
    {
        return this.getInterestBeforeDate(date).value;
    }

    private Interest getInterestBeforeDate(final Date date)
    {
        Interest interest = null;
        try
        {
            data.iterators.StreamIterator<Interest> it = new data.iterators.ObjectStreamIterator<Interest>(
                            new java.io.ObjectInputStream(
                                            new java.io.FileInputStream(
                                                            this.getName())));
            do
                interest = it.next();
            while (it.hasNext() && date.before(interest.startDate));
            it.close();
        }
        catch (IOException e)
        {
        }
        return interest;
    }

    private static void copyInterests(final java.io.ObjectOutputStream output,
                    final String fileName)
    {
        try
        {
            data.iterators.StreamIterator<Interest> it = new data.iterators.ObjectStreamIterator<Interest>(
                            new ObjectInputStream(new FileInputStream(fileName)));
            while (it.hasNext())
                output.writeObject(it.next());
        }
        catch (IOException e)
        {
        }
    }

    private class Interest implements java.io.Serializable
    {
        public Interest(final double value, final java.util.Date startDate)
        {
            this.value = value;
            this.startDate = startDate;
        }

        public final double value;
        public final java.util.Date startDate;
        private static final long serialVersionUID = 1L;
    }

    private static final long serialVersionUID = 1L;
}
