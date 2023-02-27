
using System.Collections.Generic;

public class Subject
{
    private List<IObserver> observers = new List<IObserver>();

    public void AttachObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void DetachObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify();
        }
    }
        
}
