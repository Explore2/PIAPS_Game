using System.Drawing;
using PIAPS_Game;
using PIAPS_Game.Builder;
using PIAPS_Game.Card;
using PIAPS_Game.View;
using PIAPS_Game.GameLogic;
using PIAPS_Game.Map;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

GameManager.Instance.StartGame();
//GameManager.Instance.PlaceCard();
bool endTurn = false;
var window = Settings.Window;
var button = new Button(new Vector2f(100, 100), new Image($"{Settings.ResourcesPath}/buttonNextTurn.png"))
{
    Position = (Vector2f)(window.Size - new Vector2u(10 + 100,  10 + 100)),
};
window.MouseButtonPressed += button.OnButtonPress;
window.MouseButtonReleased += button.OnButtonRelease;
window.Closed += (s,e) =>  window.Close();
window.MouseButtonReleased += (s, e) => MouseReleased(e);
GameManager.Instance.EnemyReciveCards();

while (window.IsOpen)
{
    
    window.DispatchEvents();
    DrawCall();
    if (endTurn)
    {
        endTurn = false;
        GameManager.Instance.CastleDamage();
        GameManager.Instance.PlayersTurn();
        DrawCall();
        System.Threading.Thread.Sleep(500);
        //Sleep(500);
        GameManager.Instance.PlayerReciveCards();
        GameManager.Instance.EnemyTurn();
        DrawCall();
        System.Threading.Thread.Sleep(500);
        //Sleep(500);
        GameManager.Instance.EnemyReciveCards();
        DrawCall();
        //Sleep(500);
        System.Threading.Thread.Sleep(500);
        
    }
}

void DrawCall()
    {
        window.Clear(Settings.BackgroundColor);
        if (GameManager.Instance.GameState != GameStatus.Play) 
        {
        window.Draw(GameManager.Instance.EndGamePopup);
        window.Display(); 
        }
    else
    {
        window.Draw(GameManager.Instance.Deck.View);
        window.Draw(GameManager.Instance.Field.View);
        window.Draw(GameManager.Instance.EnemyCastle);
        window.Draw(GameManager.Instance.PlayerCastle);


        foreach (var card in GameManager.Instance.Deck.Cards)
        {
            window.Draw(card.View);
        }

        foreach (var card in GameManager.Instance.Field.Cards)
        {
            window.Draw(card.View);
        }

        var selectedCard = GameManager.Instance.Deck.Cards.Find(x => x.IsSelected);
        if (!(selectedCard is null))
            window.Draw(selectedCard.View);
        window.Draw(button);
        window.Display();
        
    }
}

void MouseReleased(MouseButtonEventArgs e)
{
    if (e.Button == Mouse.Button.Left)
    {
        if (button.Contains(e.X, e.Y))
        {
            endTurn = true;
        }
    }
}