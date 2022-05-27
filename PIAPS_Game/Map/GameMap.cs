using PIAPS_Game.Card;
using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.Map;

public class GameMap : AbstractMap
{
    private Vector2u _size;
    public Vector2u Size
    {
        get => _size;
        protected set => _size = value;
    }
    
    public GameMap() : this(new Vector2u(4, 4)) { }

    public GameMap(Vector2u size)
    {
        Size = size;
        View = new MapView(Settings.Window.Size.X, size, Color.Transparent, Color.Transparent);
        View.Position = new Vector2f(Settings.Window.Size.X / 2 - View.Size.X / 2, Settings.Window.Size.Y/2 - View.Size.Y/2);
    }

    



    public override void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs)
    {
        throw new NotImplementedException();
    }
}