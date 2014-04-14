package ro.andrei15193.bitbucketbrowser.model;

public interface IObservable
{
	void subscribe(IObserver observer);
	void unsubscribe(IObserver observer);
}