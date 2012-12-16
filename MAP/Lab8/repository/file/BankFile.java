package repository.file;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.Date;

import repository.RepositoryException;

public class BankFile extends domain.Bank
{
    // The file name used to store bank interests is the same as the bank name.
    public BankFile(String name, boolean staticInterest, double interest)
                    throws RepositoryException
    {
        super(name, staticInterest);
        try
        {
            ObjectOutputStream out = new ObjectOutputStream(
                            new FileOutputStream(this.getName()));
            out.writeObject(new Interest(interest, new Date(), name));
            out.writeObject(null);
            out.close();
        }
        catch (Exception e)
        {
            throw new repository.RepositoryException(
                            "The bank file could not be found!.");
        }
    }

    @Override
    public void addInterest(double interest, Date date)
                    throws RepositoryException
    {
        String tempFileName = this.getName() + ".temp";
        File f = new File(this.getName());
        File temp = new File(tempFileName);
        f.renameTo(temp);
        this.stackInterest(
                        new Interest(interest, date, BankFile.this.getName()),
                        tempFileName);
        temp.delete();
    }

    @Override
    public double getInterest() throws repository.RepositoryException
    {
        return this.getInterest(new Date());
    }

    @Override
    public double getInterest(Date date) throws repository.RepositoryException
    {
        Interest interest = null, currentInterest;
        ObjectInputStream in = null;
        try
        {
            in = new ObjectInputStream(new FileInputStream(this.getName()));
            do
                try
                {
                    if ((currentInterest = (Interest)in.readObject())
                                    .getStartingDate().before(date))
                        interest = currentInterest;
                }
                catch (NullPointerException e)
                {
                    in.close();
                    throw new repository.RepositoryException(
                                    "No interest value can be given for the asked date! The bank didn't exist before the given date!");
                }
                catch (Exception e)
                {
                    interest = null;
                }
            while (interest == null);
        }
        catch (IOException e)
        {
            throw new repository.RepositoryException(
                            "The bank file could not be found!.");
        }
        finally
        {
            try
            {
                in.close();
            }
            catch (Exception e)
            {
            }
        }
        return interest.getValue();
    }

    private void stackInterest(Interest interest, String tempFileName)
                    throws RepositoryException
    {
        Interest currentInterest = null;
        ObjectInputStream in = null;
        ObjectOutputStream out = null;
        try
        {
            out = new ObjectOutputStream(new FileOutputStream(this.getName()));
            in = new ObjectInputStream(new FileInputStream(tempFileName));
            out.writeObject(interest);
            try
            {
                do
                    out.writeObject(currentInterest = (Interest)in.readObject());
                while (currentInterest != null);
            }
            catch (Exception e)
            {
            }
        }
        catch (IOException e)
        {
            new repository.RepositoryException(
                            "A bank could not be created! You do not have sufficient rights to create a new file.");
        }
        finally
        {
            try
            {
                in.close();
            }
            catch (Exception e)
            {
            }
            try
            {
                out.close();
            }
            catch (Exception e)
            {
            }
        }
    }

    private static final long serialVersionUID = 1L;
}
