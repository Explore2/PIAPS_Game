using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game;

public class Button : Transformable, Drawable
{
    private RectangleShape button;
    private Vector2f position;
    private Vector2f scale;
    private Vector2f size;

    public Button(Vector2f size, Color buttonColor)
    {
        this.size = size;
        button = new RectangleShape()
        {
            Size = size,
            FillColor = buttonColor
        };
    }

    public new Vector2f Position
    {
        get => position;
        set
        {
            position = value;
            button.Position = value;
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


    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(button);
    }

    public bool Contains(float x, float y)
    {
        float minX = Math.Min(position.X, position.X + this.Size.X);
        float maxX = Math.Max(position.X, position.X + this.Size.X);
        float minY = Math.Min(position.Y, position.Y + this.Size.Y);
        float maxY = Math.Max(position.Y, position.Y + this.Size.Y);
        return ( x >= minX ) && ( x < maxX ) && ( y >= minY ) && ( y < maxY );
    }

    public void OnButtonPress(MouseButtonEventArgs e)
    {
        
    }
    
    public void OnButtonRelease(MouseButtonEventArgs e)
    {
        
    }
}