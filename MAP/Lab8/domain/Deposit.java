package domain;

import java.io.Serializable;
import java.util.Date;

public class Deposit implements Serializable
{
    // Enumerates the end options available. An end option is a procedure
    // to be made for the deposit such as automatic capitalization (add the
    // obtained sum to the deposit and renew the deposit).
    public enum EndOption
    {
        // The obtained sum is added to the deposit and then it is renewed.
        // This option requires manual deposit closing.
        automaticCapitalization,
        // The deposit is closed when it reaches it's end date.
        close
    }

    public Deposit(String bankName, String personId, Double sum,
                    EndOption endOption)
    {
        this.personId = personId;
        this.bankName = bankName;
        this.sum = sum;
        this.endOption = endOption;
        this.creationDate = this.lastUpdate = new Date();
    }

    public final void setPersonId(String personId)
    {
        this.personId = personId;
    }

    public final String getPersonId()
    {
        return this.personId;
    }

    public final void setBankName(String bankName)
    {
        this.bankName = bankName;
    }

    public final String getBankName()
    {
        return this.bankName;
    }

    public final Double getSum()
    {
        return this.sum;
    }

    public final void updateSum(Double interest)
    {
        this.sum += this.sum * interest;
        this.lastUpdate = new Date();
    }

    public final Date getCreationDate()
    {
        return this.creationDate;
    }

    public final Date getLastUpdateDate()
    {
        return this.lastUpdate;
    }

    public final EndOption getEndOption()
    {
        return this.endOption;
    }

    private String bankName;
    private String personId;
    private Double sum;
    private Date lastUpdate;
    private final Date creationDate;
    private final EndOption endOption;
    private static final long serialVersionUID = 1L;
}
