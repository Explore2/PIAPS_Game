﻿using PIAPS_Game.Builder;
using PIAPS_Game.Card;

namespace PIAPS_Game;

public class CardCreator
{
    public Builder.Builder Builder;

    public AbstractCard CreateCard()
    {
        Builder.Reset();
        Builder.SetHP();
        Builder.SetDamage();
        Builder.SetCost();
        return Builder.GetCard();
    }
    public AbstractCard CreateEliteCard()
    {
        Builder.Reset();
        Builder.SetEliteHP();
        Builder.SetEliteDamage();
        Builder.SetEliteCost();
        return Builder.GetCard();
    }
}