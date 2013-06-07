package proiect.repository;

import proiect.controller.EntityReader;
import proiect.domain.Firm;
import proiect.domain.FirmException;

// Represents a specialized FirmRepository that stores Firms on external memory
// using text files.
public class FirmRepositoryFile implements FirmRepository
{
    // Creates a new FirmRepositoryFile instance and uses the file, given by
    // it's
    // name, to read and write Firms.
    public FirmRepositoryFile(String fileName)
    {
        this.fileName = fileName;
        this.firmStringifier = new FirmStringifier();
    }

    @Override
    // Gets a FirmReader that can iterate through all Firms in the repository.
    public EntityReader<Firm> getFirmsReader()
    {
        return new FirmReaderFile(this.fileName);
    }

    @Override
    // Stores a Firm. Throws RepositoryException if the firm name already exists
    // in
    // the repository.
    public void storeFirm(Firm firm) throws RepositoryException
    {
        java.io.BufferedWriter file = null;
        if (this.getFirmByName(firm.getName()) == null)
            try
            {
                file = new java.io.BufferedWriter(new java.io.FileWriter(
                                this.fileName, true));
                file.append(this.firmStringifier.stringify(firm) + "\r\n");
            }
            catch (java.io.IOException e)
            {
                throw new RepositoryException(
                                "The Firms file could not be opened! ");
            }
            finally
            {
                try
                {
                    file.close();
                }
                catch (Exception e)
                {
                }
            }
        else
            throw new RepositoryException("The Firm name (" + firm.getName()
                            + ") already exists in the repository!");
    }

    @Override
    // Outputs in the file specified all Firms that have the turnover between
    // the
    // specified bounds. To ignore a bound call with null instead of an actual
    // value.
    public void filterFirmsByTurnover(String fileName, Integer lowerBound,
                    Integer higherBound) throws RepositoryException
    {
        java.io.PrintStream file = null;
        FirmReaderFile reader = new FirmReaderFile(this.fileName);
        Firm firm;
        try
        {
            file = new java.io.PrintStream(fileName);
            while (reader.next())
            {
                firm = reader.getCurrentEntity();
                if ((lowerBound == null || lowerBound.compareTo(firm
                                .getTurnover()) <= 0)
                                && (higherBound == null || firm.getTurnover()
                                                .compareTo(higherBound) < 0))
                    file.println(this.firmStringifier.stringify(reader
                                    .getCurrentEntity()));
            }
        }
        catch (java.io.IOException e)
        {
            throw new RepositoryException(
                            "The Firms file could not be created! ");
        }
        finally
        {
            try
            {
                reader.close();
                file.close();
            }
            catch (Exception e)
            {
            }
        }
    }

    @Override
    // Searches for a firm with the given name in the repository. If the firm is
    // found it is returned, otherwise null is returned.
    public Firm getFirmByName(String name)
    {
        Firm firm = null;
        EntityReader<Firm> reader = new FirmReaderFile(this.fileName);
        while (reader.next()
                        && !(firm = reader.getCurrentEntity()).getName()
                                        .equals(name))
            firm = null;
        return firm;
    }

    private class FirmReaderFile implements EntityReader<Firm>
    {
        // Creates a new FirmReaderInstance that iterates through the file of
        // firms. It automatically reads the first Firm in the file.
        public FirmReaderFile(String nameOfFile)
        {
            this.stringifier = FirmRepositoryFile.this.firmStringifier;
            try
            {
                this.file = new java.io.BufferedReader(new java.io.FileReader(
                                nameOfFile));
            }
            catch (java.io.FileNotFoundException e)
            {
                this.file = null;
            }
        }

        @Override
        // Gets the current Firm. If there is no current Firm (e.g.: end of file
        // has been reached) null is returned.
        public Firm getCurrentEntity()
        {
            return this.currentFirm;
        }

        @Override
        // Goes to the next Firm in the file. If no more Firms could be read
        // false
        // is returned and the file is automatically closed.
        public Boolean next()
        {
            try
            {
                this.currentFirm = null;
                String line = this.file.readLine();
                while (line != null && this.currentFirm == null)
                    try
                    {
                        this.currentFirm = this.stringifier.destring(line);
                    }
                    catch (FirmException e)
                    {
                        line = this.file.readLine();
                    }
                if (line == null)
                    this.close();
            }
            catch (Exception e)
            {
                this.close();
            }
            return this.currentFirm != null;
        }

        @Override
        // Closes the file.
        public void close()
        {
            try
            {
                this.file.close();
            }
            catch (Exception e)
            {
            }
            finally
            {
                this.file = null;
            }
        }

        private java.io.BufferedReader file;
        private final FirmStringifier stringifier;
        private Firm currentFirm;
    }

    private final String fileName;
    private final FirmStringifier firmStringifier;
}
