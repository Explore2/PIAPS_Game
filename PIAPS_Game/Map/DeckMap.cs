using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game.Map;

public class DeckMap : AbstractMap
{
    protected int _size;
    
    public int Size
    {
        get => _size;
        protected set => _size = value;
    }
    
    public DeckMap() : this(5) { }

    public DeckMap(int size)
    {
        Size = size;
        View = new MapView(Settings.Window.Size.X/1.5f, new Vector2u((uint)size, 1), Color.White, Color.Black);
        View.Position = new Vector2f(Settings.Window.Size.X / 2 - View.Size.X / 2, Settings.Window.Size.Y - View.Size.Y);
    }




    public override void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs)
    {
        throw new NotImplementedException();
    }
}