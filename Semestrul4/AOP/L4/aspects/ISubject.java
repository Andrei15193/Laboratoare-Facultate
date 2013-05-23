package aspects;

public interface ISubject
{
    void addObserver(IObserver observer);

    void removeObserver(IObserver observer);

    public void notifyObservers();
}
