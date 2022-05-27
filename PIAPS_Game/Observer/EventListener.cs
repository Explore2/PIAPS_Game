using PIAPS_Game.Card;

namespace PIAPS_Game.Observer;

public interface EventListener
{
    public void Update(AbstractCard sender, CardState eventArgs);
}