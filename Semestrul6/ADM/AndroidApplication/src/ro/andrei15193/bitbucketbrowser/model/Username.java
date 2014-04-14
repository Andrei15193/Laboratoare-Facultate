package ro.andrei15193.bitbucketbrowser.model;

public class Username
{
	public Username(String username, Integer usageCount)
	{
		_username = username;
		_usageCount = usageCount;
	}

	public final String getUsername()
	{
		return _username;
	}
	public final Integer getUsageCount()
	{
		return _usageCount;
	}
	public final void addCount(int usageCount)
	{
		_usageCount += usageCount;
	}

	private final String _username;
	private Integer _usageCount;
}