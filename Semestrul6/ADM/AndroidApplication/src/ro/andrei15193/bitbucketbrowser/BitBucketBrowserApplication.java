package ro.andrei15193.bitbucketbrowser;

import ro.andrei15193.bitbucketbrowser.model.ArrayListUsernameContainer;
import ro.andrei15193.bitbucketbrowser.model.IUsernameContainer;
import ro.andrei15193.bitbucketbrowser.model.Repository;
import ro.andrei15193.bitbucketbrowser.model.Username;
import ro.andrei15193.bitbucketbrowser.repository.IUserRepository;
import ro.andrei15193.bitbucketbrowser.repository.SqlLiteUserRepository;
import android.app.Application;

public class BitBucketBrowserApplication extends Application
{
	public BitBucketBrowserApplication()
	{
		_userRepository = new SqlLiteUserRepository(this);
	}

	@Override
	public void onCreate()
	{
		super.onCreate();
		for (Username username: _userRepository.allUsers())
			_usernameContainer.add(username);
	}
	public IUsernameContainer getUsernameContainer()
	{
		return _usernameContainer;
	}
	public IUserRepository getUserRepository()
	{
		return _userRepository;
	}
	public void setUsername(String username)
	{
		_username = username;
	}
	public String getUsername()
	{
		return _username;
	}
	public void setRepository(Repository repository)
	{
		_repository = repository;
	}
	public Repository getRepository()
	{
		return _repository;
	}

	private Repository _repository;
	private String _username;
	private final IUserRepository _userRepository;
	private final IUsernameContainer _usernameContainer = new ArrayListUsernameContainer();
}