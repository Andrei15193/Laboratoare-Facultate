package domain;

public abstract class Bank implements java.io.Serializable
{
    public abstract boolean addInterest(final double value,
                    final java.util.Date startDate);

    public abstract double getInterest();

    public abstract double getInterest(final java.util.Date date);

    public final String getName()
    {
        return this.name;
    }

    public final boolean isInterestStatic()
    {
        return this.staticInterest;
    }

    @Override
    public boolean equals(Object object)
    {
        boolean result = false;
        if (object instanceof Bank)
        {
            Bank bank = (Bank)object;
            result = this.name.equals(bank.name)
                            && this.staticInterest == bank.staticInterest;
        }
        return result;
    }

    @Override
    public String toString()
    {
        String staticInterest = null;
        if (this.staticInterest)
            staticInterest = "yes";
        else
            staticInterest = "no";
        return this.name + " Static interest: " + staticInterest;
    }

    protected Bank(final String name, final boolean isInterestStatic)
    {
        this.name = name;
        this.staticInterest = isInterestStatic;
    }

    private final String name;
    private final boolean staticInterest;
    private static final long serialVersionUID = 1L;
}
