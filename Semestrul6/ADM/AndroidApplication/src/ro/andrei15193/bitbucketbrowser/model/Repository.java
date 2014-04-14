package ro.andrei15193.bitbucketbrowser.model;

public class Repository
{
	public Repository(String name, String language, String owner, String description)
	{
		_name = name;
		_language = language;
		_owner = owner;
		_description = description;
	}

	public String getName()
	{
		return _name;
	}
	public String getLanguage()
	{
		return _language;
	}
	public String getOwner()
	{
		return _owner;
	}
	public String getDescription()
	{
		return _description;
	}

	private final String _name;
	private final String _language;
	private final String _owner;
	private final String _description;
}