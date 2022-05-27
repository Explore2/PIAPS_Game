using PIAPS_Game.Card;

namespace PIAPS_Game.Builder;

public class LongRangeBuilder : Builder
{
    private LongRangeCard _card = new LongRangeCard();
    public void Reset()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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