using PIAPS_Game.Observer;
using EventListener = System.Diagnostics.Tracing.EventListener;

namespace PIAPS_Game.Card;

public abstract class AbstractCard : EventRaiser
{
    protected CardState State;

    public void Go()
    {
        if (!Move())
        {
            Attack();
        }
    }

    protected abstract bool Move();

    protected abstract bool Attack();
}