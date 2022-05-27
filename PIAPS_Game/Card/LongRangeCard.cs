using PIAPS_Game.GameLogic;
using PIAPS_Game.View;
using SFML.System;

namespace PIAPS_Game.Card;

public class LongRangeCard : AbstractCard
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

        Vector2i attackPosition = new Vector2i(MapPosition.X, MapPosition.Y + (2*moveSign));
        Console.WriteLine($"Атакую по {attackPosition}");
        


        List<AbstractCard> target = GameManager.Instance.Field.GetCardsOnPosition(MapPosition, attackPosition);
        if (IsEnemy)
        {
            target = target.OrderBy(t => t.MapPosition.Y).ToList();
        }
        else
        {
            target = target.OrderByDescending(t => t.MapPosition.Y).ToList();
        }

        foreach (var enemy in target)
        {
            if (IsEnemy != enemy.IsEnemy)
            {
                success = true;
                enemy.ReceiveDamage(Damage);
                Console.WriteLine($"Нанёс {Damage} урона");
                return success;
            } 
        }

        if (IsEnemy && attackPosition.Y < 0)
        {
            GameManager.Instance.PlayerHP -= Damage;
        }
        else if (!IsEnemy && attackPosition.Y >= GameManager.Instance.Field.Size.Y)
        {
            GameManager.Instance.EnemyHP -= Damage;
        }


        return success;
    }
}