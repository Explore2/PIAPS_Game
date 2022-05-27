using PIAPS_Game.Observer;
using EventListener = System.Diagnostics.Tracing.EventListener;
using SFML.System;

namespace PIAPS_Game.Card;

public abstract class AbstractCard : EventRaiser
{
    protected CardState State;
    protected int _hp;
    protected int _damage;
    protected int _cost;
    protected Vector2i _mapPosition;
    protected bool _isEnemy;


    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }


    public Vector2i MapPosition
    {
        get => _mapPosition;
        protected set => _mapPosition = value;
    }
    

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