package repository.file;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

import repository.RepositoryException;
import controller.EntityReader;
import domain.Bank;

public class BankRepositoryFile implements repository.BankRepository
{
    public BankRepositoryFile(String fileName)
    {
        this.fileName = fileName;
    }

    @Override
    public void add(Bank bank) throws RepositoryException
    {
        ObjectOutputStream out = null;
        String bankName = bank.getName();
        if (this.find(bankName) == null)
            try
            {
                out = new ObjectOutputStream(new FileOutputStream(
                                this.fileName, true));
                out.writeObject(bank);
            }
            catch (java.io.IOException e)
            {
                throw new RepositoryException(
                                "The banks file could not be opened!");
            }
            finally
            {
                try
                {
                    out.close();
                }
                catch (Exception e)
                {
                }
            }
        else
            throw new RepositoryException("The bank name (" + bankName
                            + ") already exists in the repository!");
    }

    @Override
    public Bank find(String name) throws RepositoryException
    {
        Bank currentEntity = null;
        BankReaderFile reader = (BankReaderFile)this.getReader();
        do
        {
            reader.next();
            currentEntity = reader.getCurrentEntity();
        }
        while (currentEntity != null && !name.equals(currentEntity.getName()));
        return currentEntity;
    }

    @Override
    public EntityReader<Bank> getReader()
    {
        return new BankReaderFile(this.fileName);
    }

    /*
     * Represents a specialized Bank reader to read Banks from a file.
     */
    private class BankReaderFile implements EntityReader<Bank>
    {
        public BankReaderFile(String fileName)
        {
            this.currentEntity = null;
            try
            {
                this.input = new ObjectInputStream(
                                new FileInputStream(fileName));
            }
            catch (IOException e)
            {
                this.input = null;
            }
        }

        @Override
        public Bank getCurrentEntity()
        {
            return this.currentEntity;
        }

        @Override
        public Boolean next()
        {
            this.currentEntity = null;
            try
            {
                do
                    try
                    {
                        this.currentEntity = (Bank)this.input.readObject();
                    }
                    catch (ClassNotFoundException e)
                    {
                        this.currentEntity = null;
                    }
                while (this.currentEntity == null);
            }
            catch (IOException e)
            {
                this.close();
                return false;
            }
            catch (NullPointerException e)
            {
                return false;
            }
            return true;
        }

        @Override
        public void close()
        {
            try
            {
                this.input.close();
            }
            catch (Exception e)
            {
            }
        }

        private Bank currentEntity;
        private ObjectInputStream input;
    }

    private final String fileName;
    private static final long serialVersionUID = 1L;
}
