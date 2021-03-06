using PIAPS_Game.Card;
using PIAPS_Game.GameLogic;
using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.Builder;

public class LongRangeBuilder : Builder
{
    private LongRangeCard _card;
    public void Reset()
    {
        _card = new LongRangeCard();
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
        string foreImagePath = @$"{Settings.ResourcesPath}\BaseCardIcons\longRange.png";
        #region 
        if (GameManager.isCreatingEnemy)
        {
            _card.IsEnemy = true;
            foreImagePath = @$"{Settings.ResourcesPath}\BaseCardIcons\longRangeEnemy.png";
        }
        #endregion
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
    

    public AbstractCard GetCard()
    {
        return _card;
    }
}