using PIAPS_Game.Card;

namespace PIAPS_Game.Builder;
public interface Builder
{
   public void Reset();
   public void SetHP();
   public void SetDamage();
   public void SetCost();
   public void SetTexture();
   public void SetEliteHP();
   public void SetEliteDamage();
   public void SetEliteCost();
   public void SetEliteTexture();

   public AbstractCard GetCard();
}