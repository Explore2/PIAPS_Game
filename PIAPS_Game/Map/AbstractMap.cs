using PIAPS_Game.Card;
using PIAPS_Game.Observer;
using PIAPS_Game.View;
using SFML.System;

namespace PIAPS_Game.Map;

public abstract class AbstractMap : EventListener
{
    public List<AbstractCard> Cards = new List<AbstractCard>();
    public MapView View;
    public abstract void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);

    public List<AbstractCard> GetCardsOnPosition(Vector2i currentPosition, Vector2i wantedPosition)
    {
        bool horizontal = currentPosition.X != wantedPosition.X;
        List<AbstractCard> cards;

        List<Vector2i> pos = new List<Vector2i>();
        pos.Add(currentPosition);
        pos.Add(wantedPosition);
        

        if (horizontal)
            cards = Cards
                .Where(c =>
                    c.MapPosition.X > pos.Min(c => c.X) &&
                    c.MapPosition.X <= pos.Max(c => c.X) &&
                    c.MapPosition.Y == currentPosition.Y)
                .ToList();
        else
            cards = Cards
                .Where(c =>
                c.MapPosition.Y < pos.Max(c => c.Y) &&
                c.MapPosition.Y >= pos.Min(c => c.Y) &&
                c.MapPosition.X == currentPosition.X)
                .ToList();

        return cards;
    }

    public AbstractCard GetCardOnPosition(Vector2i position)
    {
        AbstractCard card = Cards.Find(Card => Card.MapPosition == position);
        return card;
    }

}