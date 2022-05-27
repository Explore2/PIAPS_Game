using PIAPS_Game.Observer;
using PIAPS_Game.View;
using EventListener = System.Diagnostics.Tracing.EventListener;

namespace PIAPS_Game.Card;

public abstract class AbstractCard : EventRaiser
{
    protected CardState State;
    protected int _hp;
    protected int _damage;
    protected int _cost;
    public CardView View;

    public int HP
    {
        get => _hp;
        set => _hp = value;
    }

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

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