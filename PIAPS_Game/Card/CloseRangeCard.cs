using PIAPS_Game.GameLogic;
using SFML.System;

namespace PIAPS_Game.Card;

public class CloseRangeCard : AbstractCard
{

    protected override bool Move()
    {
        bool success = false;
        int moveSign = !IsEnemy ? -1 : 1;



        Vector2i wantedMove = new Vector2i(MapPosition.X, MapPosition.Y + (2 * moveSign) );


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


        return success;
    }

    protected override bool Attack()
    {
        bool success = false;
        int moveSign = (!IsEnemy ? -1 : 1);
        
        Vector2i attackPosition = new Vector2i(MapPosition.X,MapPosition.Y + (moveSign) );

        if (!IsEnemy && attackPosition.Y < 0)
        {
            GameManager.Instance.EnemyHP -= Damage;
            return true;
        }
        else if (IsEnemy && attackPosition.Y >= GameManager.Instance.Field.Size.Y)
        {
            GameManager.Instance.PlayerHP -= Damage;
            return true;
        }



        AbstractCard target = GameManager.Instance.Field.GetCardOnPosition(attackPosition);

        if (target != null && IsEnemy != target.IsEnemy)
        {
            success = true;
            target.ReceiveDamage(Damage);
        }

        return success;
    }
}