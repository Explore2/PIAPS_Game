using PIAPS_Game.GameLogic;
using SFML.System;

namespace PIAPS_Game.Card;

public class SplashCard : AbstractCard
{
    //Some kind of view here
    protected override bool Move()
    {
        bool success = false;
        int moveSign = !IsEnemy ? -1 : 1;


        Console.WriteLine($"Начинаю ходить с {MapPosition.ToString()}");
        Vector2i wantedMove = new Vector2i(MapPosition.X, MapPosition.Y + (moveSign));


        List<AbstractCard> obstacles = GameManager.Instance.Field.GetCardsOnPosition(MapPosition, wantedMove);
        obstacles.Remove(this);
        if (obstacles.Count > 0)
        {
            if (IsEnemy)
                wantedMove.Y = obstacles.Min(obstacle => obstacle.MapPosition.Y) - moveSign;
            else
                wantedMove.Y = obstacles.Max(obstacle => obstacle.MapPosition.Y) - moveSign;
        }



        if (wantedMove != MapPosition)
        {
            success = true;
            MapPosition = wantedMove;
        }

        Console.WriteLine($"Пришёл в {MapPosition.ToString()}");

        return success;
    }


    protected override bool Attack()
    {
        throw new NotImplementedException();
    }
}