namespace PIAPS_Game.Observer;

public interface EventListener
{
    public void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}