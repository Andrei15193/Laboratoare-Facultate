package ro.andrei15193.bitbucketbrowser.model;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class ArrayListUsernameContainer implements IUsernameContainer
{
	public ArrayListUsernameContainer(int usernameCapacity)
	{
		if (usernameCapacity < 0)
			throw new IllegalArgumentException("usernameCapacity cannot be negative!");

		_usernameCapacity = usernameCapacity;
		_usernames = new ArrayList<Username>(_usernameCapacity + 1);
	}
	public ArrayListUsernameContainer()
	{
		this(16);
	}

	@Override
	public final void subscribe(IObserver observer)
	{
		if (observer != null)
			_observers.add(observer);
	}
	@Override
	public final void unsubscribe(IObserver observer)
	{
		if (observer != null)
			_observers.remove(observer);
	}

	@Override
	public final void add(String username, int count)
	{
		if (username == null)
			throw new NullPointerException();

		int indexOfUsername = _indexOf(username);
		int insertionIndex;
		Username entry;

		if (indexOfUsername == -1)
		{
			entry = new Username(username, count);
			_usernames.add(entry);
			indexOfUsername = insertionIndex = _usernames.size() - 1;
		}
		else
		{
			insertionIndex = indexOfUsername;
			entry = _usernames.get(indexOfUsername);
			entry.addCount(count);
		}

		while (insertionIndex > 0 && entry.getUsageCount() > _usernames.get(insertionIndex - 1).getUsageCount())
			insertionIndex--;

		for (int index = indexOfUsername; index > insertionIndex; index--)
			_usernames.set(index, _usernames.get(index - 1));
		_usernames.set(insertionIndex, entry);

		int usernamesSize = _usernames.size();
		if (usernamesSize > _usernameCapacity)
		{
			_usernames.set(usernamesSize - 2, _usernames.get(usernamesSize - 1));
			_usernames.remove(usernamesSize - 1);
		}

		_notifyObservers();
	}
	@Override
	public final void add(Username username)
	{
		if (username == null)
			throw new NullPointerException();

		add(username.getUsername(), username.getUsageCount());
	}
	@Override
	public final void add(String username)
	{
		add(username, 1);
	}
	@Override
	public Username get(int position)
	{
		return _usernames.get(position);
	}
	@Override
	public int size()
	{
		return _usernames.size();
	}
	@Override
	public Iterator<Username> iterator()
	{
		return _usernames.iterator();
	}

	private final void _notifyObservers()
	{
		for (IObserver observer: _observers)
			observer.onSubjectChanged();
	}
	private final int _indexOf(String username)
	{
		int searchIndex = _usernames.size() - 1;

		while (searchIndex > -1 && !_usernames.get(searchIndex).getUsername().equals(username))
			searchIndex--;

		return searchIndex;
	}

	private final int _usernameCapacity;
	private final List<Username> _usernames;
	private final List<IObserver> _observers = new ArrayList<IObserver>();
}