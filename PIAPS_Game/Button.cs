using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game;

public class Button : Transformable, Drawable
{
    // /Users/leonid/RiderProjects/PIAPS_Game/PIAPS_Game/Resources/CellTexture.png
    #region fields

    private RectangleShape button;
    private RectangleShape foreground;
    private Vector2f position;
    private Vector2f scale;
    private Vector2f size;
    private Texture buttonBackGround = new Texture($"{Settings.ResourcesPath}/buttonBackGround.png");
    private Texture buttonPressedBackGround = new Texture($"{Settings.ResourcesPath}/buttonPressedBackGround.png");

    #endregion

    #region Properties

    public new Vector2f Position
    {
        get => position;
        set
        {
            position = value;
            button.Position = value;
            foreground.Position = value + new Vector2f(button.Size.X/2 - foreground.Size.X/2, button.Size.Y/2 - foreground.Size.Y/2);
        } 
    }

    public Vector2f Scale
    {
        get => scale;
        set {
            scale = value;
            button.Scale = value;
        }
    }

    public Vector2f Size
    {
        get => size;
    }

    #endregion

    #region Methods

    public Button(Vector2f size, Image foreGround)
    {
        this.size = size;
        button = new RectangleShape()
        {
            Size = size,
            Texture = buttonBackGround
        };
        foreground = new RectangleShape(new Vector2f(size.Y-size.Y/10, size.Y-size.Y/10))
        {
            Position = new Vector2f(Size.X/2 + Size.Y/2, Size.Y/2 + Size.Y/2),
            Texture = new Texture(foreGround)
        };
    }
    
    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(button);
        target.Draw(foreground);
    }

    public bool Contains(float x, float y)
    {
        float minX = Math.Min(position.X, position.X + this.Size.X);
        float maxX = Math.Max(position.X, position.X + this.Size.X);
        float minY = Math.Min(position.Y, position.Y + this.Size.Y);
        float maxY = Math.Max(position.Y, position.Y + this.Size.Y);
        return ( x >= minX ) && ( x < maxX ) && ( y >= minY ) && ( y < maxY );
    }

    public void OnButtonPress(object? o,MouseButtonEventArgs e)
    {
        if(Contains(e.X, e.Y) && e.Button == Mouse.Button.Left)
            button.Texture = buttonPressedBackGround;
    }
    
    public void OnButtonRelease(object? o,MouseButtonEventArgs e)
    {
        if(e.Button == Mouse.Button.Left && button.Texture != buttonBackGround)
            button.Texture = buttonBackGround;
    }

    #endregion

}