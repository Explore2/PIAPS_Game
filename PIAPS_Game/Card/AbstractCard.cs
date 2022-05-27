using PIAPS_Game.GameLogic;
using PIAPS_Game.Map;
using PIAPS_Game.Observer;
using PIAPS_Game.View;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game.Card;

public abstract class AbstractCard : EventRaiser
{
    public CardState State; //TODO fix
    protected int _hp;
    protected int _damage;
    protected int _cost;
    protected Vector2i _mapPosition;
    protected bool _isEnemy;
    public CardView View;
    private bool isSelected = false;
    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }


    public Vector2i MapPosition
    {
        get => _mapPosition;
        set
        {
            _mapPosition = value;
            AbstractMap field;
            if (State == CardState.InMap)
            {
                field = GameManager.Instance.Field;
            }
            else
            { 
                field = GameManager.Instance.Deck;
            }
            Vector2f realCoords = field.View.GetCoords(_mapPosition);
            View.Position = realCoords + (field.View.CellSize/2 - View.Size/2);
        } 
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

    public void ReceiveDamage(int damage)
    {
        HP -= damage;
        if (HP < 0)
            Notify(State);
    }
    
    
    public void MousePressed(MouseButtonEventArgs e)
    {
        if(State == CardState.InMap) return;
        if(View.Contains(e.X, e.Y) && e.Button == Mouse.Button.Left)
        {
            isSelected = true;
            View.grabOffset = new Vector2f(e.X - View.Position.X, e.Y - View.Position.Y);
            View.PrevPosition = View.Position;
            View.Scale = new Vector2f(1.2f, 1.2f);
        }
    }
    public void MouseMoved(MouseMoveEventArgs e)
    {
        if (isSelected)
            View.Position = new Vector2f(e.X, e.Y) - View.grabOffset;
    }
    public void MouseReleased(MouseButtonEventArgs e)
    {
        if (isSelected && e.Button == Mouse.Button.Left)
        { 
            View.Scale = new Vector2f(1f, 1f);
            View.grabOffset = new Vector2f(0, 0);
            isSelected = false;
            var field = GameManager.Instance.Field;
            if(field.View.Contains(e.X, e.Y))
            {
                var coords = field.View.CellContains(e.X, e.Y);
                if (coords is { Y: 3 })
                {
                    State = CardState.InMap;
                    MapPosition = (Vector2i)coords;
                    return;
                }
               
            }
            View.Position = View.PrevPosition;
        }
    }
}
