
using PIAPS_Game.Card;

namespace PIAPS_Game.Observer;

public abstract class EventRaiser
{
    protected List<EventListener> Listeners = new List<EventListener>();
    
    public void AddListener(EventListener listener)
    {
        Listeners.Add(listener);
    }
    
    public void RemoveListener(EventListener listener)
    {
        Listeners.Remove(listener);
    }

    public void Notify(AbstractCard sender, CardState EventArgs)
    {
        foreach (var listener in Listeners)
        {
            listener.Update(sender ,EventArgs);
        }
    }
}