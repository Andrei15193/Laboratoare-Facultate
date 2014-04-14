package ro.andrei15193.bitbucketbrowser.repository;

import ro.andrei15193.bitbucketbrowser.model.Username;

public interface IUserRepository
{
	void add(String username);
	Iterable<Username> allUsers();
}