using PIAPS_Game.Card;
using PIAPS_Game.Observer;
using PIAPS_Game.View;

namespace PIAPS_Game.Map;

public abstract class AbstractMap : EventListener
{
    public List<AbstractCard> Cards = new List<AbstractCard>();
    public MapView View;
    public abstract void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}