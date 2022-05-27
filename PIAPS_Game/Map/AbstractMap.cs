using PIAPS_Game.Card;
using PIAPS_Game.Observer;

namespace PIAPS_Game.Map;

public abstract class AbstractMap : EventListener
{
    public List<AbstractCard> Cards = new List<AbstractCard>();
    public abstract void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}