using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.View;

public class MapView: Transformable, Drawable
{
    #region Fields
    
    private Vector2f position;
    private Vector2f size;
    public Vector2u length;
    private Vector2f cellSize;
    private RectangleShape background;
    private List<RectangleShape> cells = new List<RectangleShape>();

    #endregion

    #region Properties

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
                    cells[(int)(i * length.Y + j)].Position = value + new Vector2f(CellSize.X * i + cellSize.X/10 * i , CellSize.Y * j + cellSize.Y/10 * j);
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

    public Vector2f CellSize
    {
        get => cellSize;
        set => cellSize = value;
    }

    #endregion

    #region Methods

    public MapView(float size, Vector2u length, Color backgroundColor, Color foregroundColor)
    {
        Size = new Vector2f(size-(5 * (size/100)),  size/length.X * length.Y);
        this.length = length;
        background = new RectangleShape(this.Size);
        background.FillColor = backgroundColor;
        CellSize = new Vector2f(this.Size.X / this.length.X, this.Size.Y / this.length.Y);
        
        for (int i = 0; i < this.length.X; i++)
        {
            for (int j = 0; j < this.length.Y; j++)
            {
                cells.Add(new RectangleShape(CellSize)
                {
                    Texture = new Texture(new Image(Settings.ResourcesPath + @"/CellTexture.png")),
                    //FillColor = Color.Transparent,
                    //OutlineColor = foregroundColor,
                    //OutlineThickness = 2f,
                    //Size = CellSize - CellSize/10,
                    Size = CellSize - CellSize / 10,
                    Position = new Vector2f(CellSize.X * i + CellSize.X/10 * i, CellSize.Y * j + cellSize.Y/10 * j) 
                });
            }
            
        }
        CellSize = CellSize - CellSize / 10;
    }
    public MapView(Vector2f size, Vector2u length, Color backgroundColor, Color foregroundColor)
    {
        this.Size = size-(5 * (size/100));
        this.length = length;
        background = new RectangleShape(this.Size);
        background.FillColor = backgroundColor;
        CellSize = new Vector2f(this.Size.X / this.length.X, this.Size.Y / this.length.Y);
        for (int i = 0; i < this.length.X; i++)
        {
            for (int j = 0; j < this.length.Y; j++)
            {
                cells.Add(new RectangleShape()
                {
                    FillColor = Color.Transparent,
                    OutlineColor = foregroundColor,
                    OutlineThickness = 2f,
                    Size = CellSize - CellSize / 10,
                    Position = new Vector2f(CellSize.X * i + CellSize.X/10 * i, CellSize.Y * j + cellSize.Y/10 * j) 
                });
            }
        }
        CellSize = CellSize - CellSize / 10;
    }

    

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(background);
        foreach (var cell in cells)
        {
            target.Draw(cell);
        }
    }

    public Vector2i? CellContains(float x, float y)
    {
        for (int i = 0; i < length.X; i++)
        {
            for (int j = 0; j < length.Y; j++)
            {
                if (cells[(int)(i * length.Y + j)].GetGlobalBounds().Contains(x, y))
                {
                    return new Vector2i(i, j);
                }
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
        if ((int)(i * length.Y + j) >= 0 && (int)(i * length.Y + j) < cells.Count)
            return cells[(int)(i * length.Y + j)].Position;
        return new Vector2f(0, 0);
        //return new Vector2f(CellSize.X * i + cellSize.X/10 * i + this.Position.X, CellSize.Y * j + CellSize.X/10 * j + this.Position.Y);
    }

    public Vector2f GetCoords(Vector2i? coords)
    {
        return GetCoords(coords.Value.X, coords.Value.Y);
    }

    #endregion
    
}