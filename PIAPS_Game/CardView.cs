using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game;

public class CardView : Transformable, Drawable
{
    private Vector2f position = new Vector2f(0, 0);
    private Vector2f scale = new Vector2f(1, 1);

    
    private RectangleShape back;
    private RectangleShape front;
    private Text hpText;
    private Text damageText;
    private Text costText;
    private Vector2f textXOffset;
    private Vector2f textYOffset;
    private uint fontSize;
    private Font font = new Font("C:/Users/Leonid/RiderProjects/PIAPS_Game/PIAPS_Game/Resources/Fonts/arial.ttf");
 
    public CardView(Vector2f size, Image backImage, Image frontImage, int HP, int Damage, int Cost)
    {
        //Create relative sizes and offsets;
        back = new RectangleShape(size); 
        front = new RectangleShape(new Vector2f(size.X, size.X));
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
        fontSize = (uint)fontSize - fontSize / 6;
        textXOffset = new Vector2f((2 * size.X)/10, size.X - fontSize);
        textYOffset = new Vector2f(0, fontSize + fontSize/8);
    }
    public Vector2f Position
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
    
    public Vector2f Scale
    {
        get => scale;
        set
        {
           
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
    
    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(back);
        target.Draw(front);
        target.Draw(hpText);
        target.Draw(damageText);
        target.Draw(costText);
    }
}