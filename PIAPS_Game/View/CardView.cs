using System.Drawing;
using PIAPS_Game.Map;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Color = SFML.Graphics.Color;

namespace PIAPS_Game.View;

public class CardView : Transformable, Drawable
{
    #region fields
    private Vector2f position = new Vector2f(0, 0);
    private Vector2f prevPosition = new Vector2f(0, 0);
    private Vector2f scale = new Vector2f(1, 1);
    private Vector2f size;
    
    private RectangleShape back;
    private RectangleShape front;
    private Text hpText;
    private Text damageText;
    private Text costText;
    private Vector2f textXOffset;
    private Vector2f textYOffset;
    private uint fontSize;
    //TODO Reference font in settings 
    private Font font = new Font(Settings.ResourcesPath + @"/Fonts/arial.ttf"); 
    public Vector2f grabOffset = new Vector2f(0, 0);
    #endregion

    #region properties
    public new Vector2f Position
    {
        get => position;
        set
        {
            back.Position = value;
            front.Position = value;
            hpText.Position = value + textXOffset;
            hpText.Position += textYOffset * 1;
            damageText.Position = value + textXOffset;
            damageText.Position += textYOffset * 2;
            costText.Position = value + textXOffset;
            costText.Position += textYOffset * 3;
            position = value;
        }
    }
    
    public new Vector2f Scale
    {
        get => scale;
        set
        {
            Size = new Vector2f(back.Size.X * value.X, back.Size.Y * value.Y);
            back.Scale = value;
            front.Scale = value;
            SolveSizesAndOffsets(new Vector2f(back.Size.X * value.X, back.Size.Y * value.Y));
            hpText.CharacterSize = fontSize;
            hpText.Position = back.Position + textXOffset + textYOffset * 1;
            damageText.CharacterSize = fontSize;
            damageText.Position = back.Position + textXOffset + textYOffset * 2;
            costText.CharacterSize = fontSize;
            costText.Position = back.Position + textXOffset + textYOffset * 3;
            scale = value;
        }
    }

    public Vector2f PrevPosition
    {
        get => prevPosition;
        set => prevPosition = value;
    }

    public new Vector2f Size
    {
        get => size;
        set => size = value;
    }
    #endregion

    #region methods
    public CardView(Vector2f size, Image backImage, Image frontImage, int HP, int Damage, int Cost)
    {
        back = new RectangleShape(size); 
        front = new RectangleShape(new Vector2f(size.X, size.X));
        position = new Vector2f(0, 0);
        Size = size;
        SolveSizesAndOffsets(size);
       

        back.Texture = new Texture(backImage);
        front.Texture = new Texture(frontImage);
        hpText = new Text(HP.ToString(), font, fontSize);
        hpText.FillColor = Color.Black;
        hpText.Position = textXOffset + (textYOffset*1);
        damageText = new Text(Damage.ToString(), font, fontSize);
        damageText.FillColor = Color.Black;
        damageText.Position = textXOffset + (textYOffset*2);
        costText = new Text(Cost.ToString(), font, fontSize);
        costText.FillColor = Color.Black;
        costText.Position = textXOffset + (textYOffset*3);
    }

    private void SolveSizesAndOffsets(Vector2f size)
    {
        fontSize = (uint)(size.Y / 15.36 + size.Y / (15.36 * 2));
        fontSize = fontSize - fontSize / 6;
        textXOffset = new Vector2f((2 * size.X)/10, size.X - fontSize);
        textYOffset = new Vector2f(0, fontSize + fontSize/8);
    }
   

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(back);
        target.Draw(front);
        target.Draw(hpText);
        target.Draw(damageText);
        target.Draw(costText);
    }

    public bool Contains(float x, float y)
    {
        float minX = Math.Min(position.X, position.X + this.Size.X);
        float maxX = Math.Max(position.X, position.X + this.Size.X);
        float minY = Math.Min(position.Y, position.Y + this.Size.Y);
        float maxY = Math.Max(position.Y, position.Y + this.Size.Y);
        return ( x >= minX ) && ( x < maxX ) && ( y >= minY ) && ( y < maxY );
    }
    #endregion
}