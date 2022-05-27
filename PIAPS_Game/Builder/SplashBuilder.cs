using PIAPS_Game.Card;
using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.Builder;

public class SplashBuilder : Builder
{
    private SplashCard _card;
    public void Reset()
    {
       _card = new SplashCard();
    }

    public void SetHP()
    {
        _card.HP = 30;
    }

    public void SetDamage()
    {
        _card.Damage = 20;
    }

    public void SetCost()
    {
        _card.Cost = 3;
    }

    public void SetTexture()
    {
        string foreImagePath = @$"{Settings.ResourcesPath}\BaseCardIcons\splash.png";
        string backImagePath = @$"{Settings.ResourcesPath}\backcard.png";
        _card.View = new CardView(new Vector2f(100, 150),  new Image(backImagePath), new Image(foreImagePath), _card.HP, _card.Damage,
            _card.Cost);
    }

    public void SetEliteHP()
    {
        _card.HP = 35;
    }

    public void SetEliteDamage()
    {
        _card.Damage = 22;
    }

    public void SetEliteCost()
    {
        _card.Cost = 2;
    }

    public void SetEliteTexture()
    {
        throw new NotImplementedException();
    }
    
    public void SetEvents()
    {
        Settings.Window.MouseButtonPressed += (sender, args) => _card.View.MousePressed(args);
        Settings.Window.MouseButtonReleased += (sender, args) => _card.View.MouseReleased(args); 
        Settings.Window.MouseMoved += (sender, args) => _card.View.MouseMoved(args);   
    }

    public AbstractCard GetCard()
    {
        return _card;
    }
}