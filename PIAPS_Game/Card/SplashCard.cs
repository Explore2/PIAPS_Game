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


        if (wantedMove.Y < 0)
        {
            wantedMove.Y = 0;
        }

        if (wantedMove.Y >= GameManager.Instance.Field.Size.Y)
        {
            wantedMove.Y = (int)(GameManager.Instance.Field.Size.Y - 1);
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
        bool success = false;
        int moveSign = (!IsEnemy ? -1 : 1);

        Vector2i attackPosition1 = new Vector2i(MapPosition.X-1, MapPosition.Y + moveSign);
        Vector2i attackPosition2 = new Vector2i(MapPosition.X + 1, MapPosition.Y + moveSign);



        List<AbstractCard> target = GameManager.Instance.Field.GetCardsOnPosition(attackPosition1, attackPosition2);
        

        foreach (var enemy in target)
        {
            if (IsEnemy != enemy.IsEnemy)
            {
                success = true;
                enemy.ReceiveDamage(Damage);
 
            }
        }

        if (!IsEnemy && attackPosition1.Y < 0)
        {
            GameManager.Instance.EnemyHP -= Damage * 3;
        }
        else if (IsEnemy && attackPosition1.Y >= GameManager.Instance.Field.Size.Y)
        {
            GameManager.Instance.PlayerHP -= Damage * 3;
        }


        return success;
    }
}