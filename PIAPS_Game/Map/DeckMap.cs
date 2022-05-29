using PIAPS_Game.Card;
using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game.Map;

public class DeckMap : AbstractMap
{
    protected Vector2i _size;
    
    public Vector2i Size
    {
        get => _size;
        protected set => _size = value;
    }
    
    public DeckMap() : this(5) { }

    public DeckMap(int size)
    {
        Size = new Vector2i(size, 1);
        View = new MapView(Settings.Window.Size.X/1.5f, new Vector2u((uint)size, 1), Color.Transparent, Color.Transparent);
        View.Position = new Vector2f(Settings.Window.Size.X / 2 - View.Size.X / 2, Settings.Window.Size.Y - View.Size.Y);
    }

    public override void Update(AbstractCard sender, CardState eventArgs)
    {
        if (eventArgs == CardState.InDeck)
        {
            Cards.Remove(sender);
        }
    }
}