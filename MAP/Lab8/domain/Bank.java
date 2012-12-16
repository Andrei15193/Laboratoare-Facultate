package domain;

import java.io.Serializable;
import java.util.Date;
import repository.RepositoryException;

public abstract class Bank implements Serializable
{
    public Bank(String name, boolean staticInterest)
    {
        this.name = name;
        this.staticInterest = staticInterest;
    }

    public final void setName(String name)
    {
        this.name = name;
    }

    public final String getName()
    {
        return this.name;
    }

    public final boolean getStaticInterest()
    {
        return this.staticInterest;
    }

    public final boolean isStaticInterest()
    {
        return this.staticInterest;
    }

    // Adds a new interest to the repository having the given starting date.
    public abstract void addInterest(double value, Date startingDate)
                    throws RepositoryException;

    // Returns the current interest value.
    public abstract double getInterest() throws repository.RepositoryException;

    // Returns the interest value for the given date.
    public abstract double getInterest(Date date)
                    throws repository.RepositoryException;

    protected class Interest implements Serializable
    {
        public Interest(final double value, final Date startingDate,
                        final String bankName)
        {
            this.value = value;
            this.startingDate = startingDate;
            this.bankName = bankName;
        }

        public final double getValue()
        {
            return this.value;
        }

        public final Date getStartingDate()
        {
            return this.startingDate;
        }

        public final String getBankName()
        {
            return this.bankName;
        }

        private final double value;
        private final Date startingDate;
        private final String bankName;
        private static final long serialVersionUID = 1L;
    }

    private String name;
    // If set to true then the bank interest is the same as in the day
    // the deposit was created. If set to false means that the bank interest
    // can change over time and affect the deposit.
    private final Boolean staticInterest;
    private static final long serialVersionUID = 1L;
}
