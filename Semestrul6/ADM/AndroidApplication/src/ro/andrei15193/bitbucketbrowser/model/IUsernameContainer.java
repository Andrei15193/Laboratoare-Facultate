package ro.andrei15193.bitbucketbrowser.model;

public interface IUsernameContainer extends IObservable, Iterable<Username>
{
	void add(Username username);
	void add(String username, int count);
	void add(String username);
	Username get(int position);
	int size();
}