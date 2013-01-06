package domain;

import java.util.Calendar;
import java.util.Date;

public class Deposit implements java.io.Serializable
{
    public static Deposit cloneDeposit(final Deposit depo,
                    final boolean automaticCapitalisation)
    {
        return new Deposit(depo, automaticCapitalisation);
    }

    public static Deposit cloneDeposit(final Deposit depo)
    {
        return new Deposit(depo, depo.isAutomaticCapitalisation());
    }

    public Deposit(final String bankName, final String personId,
                    final double sum, final boolean automaticCapitalisation)
    {
        this.bankName = bankName;
        this.personId = personId;
        this.sum = sum;
        this.automaticCapitalisation = automaticCapitalisation;
        this.lastUpdate = this.creationDate = new Date();
    }

    public final boolean addInterest(final double interest, final Date until)
    {
        int monthCount = this.getMonth(until) - this.getMonth(this.lastUpdate);
        if (monthCount > 0)
        {
            for (int i = 0; i < monthCount; i++)
                this.sum += this.sum * interest;
            this.lastUpdate = until;
            return true;
        }
        else
            return false;
    }

    @Override
    public boolean equals(Object object)
    {
        Deposit deposit;
        boolean result = false;
        if (object instanceof Deposit)
        {
            deposit = (Deposit)object;
            result = this.bankName.equals(deposit.bankName)
                            && this.personId.equals(deposit.personId)
                            && this.creationDate.equals(deposit.creationDate)
                            && this.sum == deposit.sum
                            && this.automaticCapitalisation == deposit.automaticCapitalisation;
        }
        return result;
    }

    public final String getBankName()
    {
        return this.bankName;
    }

    public final String getPersonId()
    {
        return this.personId;
    }

    public final double getSum()
    {
        return this.sum;
    }

    public final Date getCreationDate()
    {
        return this.creationDate;
    }

    public final Date getLastUpdate()
    {
        return this.lastUpdate;
    }

    public final boolean isAutomaticCapitalisation()
    {
        return this.automaticCapitalisation;
    }

    private Deposit(final Deposit depo, final boolean automaticCapitalisation)
    {
        this.bankName = depo.bankName;
        this.personId = depo.personId;
        this.creationDate = depo.creationDate;
        this.sum = depo.sum;
        this.lastUpdate = depo.lastUpdate;
        this.automaticCapitalisation = automaticCapitalisation;
    }

    private int getMonth(Date date)
    {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        return cal.get(Calendar.MONTH);
    }

    private final String bankName;
    private final String personId;
    private final Date creationDate;
    private double sum;
    private Date lastUpdate;
    private final boolean automaticCapitalisation;
    private static final long serialVersionUID = 1L;
}
