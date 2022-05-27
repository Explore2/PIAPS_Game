using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.View;

public class MapView: Transformable, Drawable
{
    private Vector2f position;
    private Vector2f size;
    private Vector2u length;
    private Vector2f cellSize;
    private RectangleShape background;
    private List<RectangleShape> cells = new List<RectangleShape>();
    
    
    public MapView(float size, Vector2u length, Color backgroundColor, Color foregroundColor)
    {
        this.Size = new Vector2f(size-(5 * (size/100)),  size/length.X);
        this.length = length;
        background = new RectangleShape(this.Size);
        background.FillColor = backgroundColor;
        cellSize = new Vector2f(this.Size.X / this.length.X, this.Size.Y / this.length.Y);
        for (int i = 0; i < this.length.X; i++)
        {
            for (int j = 0; j < this.length.Y; j++)
            {
                cells.Add(new RectangleShape(cellSize)
                {
                    FillColor = Color.Transparent,
                    OutlineColor = foregroundColor,
                    OutlineThickness = 2f,
                    Position = new Vector2f(cellSize.X * i, cellSize.Y * j) 
                });
            }
        }
    }
    public MapView(Vector2f size, Vector2u length, Color backgroundColor, Color foregroundColor)
    {
        this.Size = size-(5 * (size/100));
        this.length = length;
        background = new RectangleShape(this.Size);
        background.FillColor = backgroundColor;
        cellSize = new Vector2f(this.Size.X / this.length.X, this.Size.Y / this.length.Y);
        for (int i = 0; i < this.length.X; i++)
        {
            for (int j = 0; j < this.length.Y; j++)
            {
                cells.Add(new RectangleShape(cellSize)
                {
                    FillColor = Color.Transparent,
                    OutlineColor = foregroundColor,
                    OutlineThickness = 2f,
                    Position = new Vector2f(cellSize.X * i, cellSize.Y * j) 
                });
            }
        }
    }

    public new Vector2f Position
    {
        get => position;
        set
        {
            background.Position = value;
            for (int i = 0; i < length.X; i++)
            {
                for (int j = 0; j < length.Y; j++)
                {
                    cells[(int)(i * length.Y + j)].Position = value + new Vector2f(cellSize.X * i, cellSize.Y * j);
                }
            }

            position = value;
        }
    }

    public Vector2f Size
    {
        get => size;
        set => size = value;
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(background);
        foreach (var cell in cells)
        {
            target.Draw(cell);
        }
    }

    public Vector2u? CellContains(float x, float y)
    {
        for (int i = 0; i < length.X; i++)
        {
            for (int j = 0; j < length.Y; j++)
            {
                if (cells[(int)(i * length.Y + j)].GetGlobalBounds().Contains(x, y))
                {
                    return new Vector2u((uint)i, (uint)j);
                };
            }
        }
        return null;
    }

    public bool Contains(float x, float y)
    {
        float minX = Math.Min(position.X, position.X + this.size.X);
        float maxX = Math.Max(position.X, position.X + this.size.X);
        float minY = Math.Min(position.Y, position.Y + this.size.Y);
        float maxY = Math.Max(position.Y, position.Y + this.size.Y);
        
        return ( x >= minX ) && ( x < maxX ) && ( y >= minY ) && ( y < maxY );
    }

    public Vector2f GetCoords(int i, int j)
    {
        return new Vector2f(cellSize.X * i, cellSize.Y * j);
    }
}