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
var button = new Button(new Vector2f(50, 50), Color.Green)
{
    Position = (Vector2f)(window.Size - new Vector2u(60, 60)),
};
window.Closed +=  (s,e) =>  window.Close();
window.MouseButtonReleased += (s, e) => MouseReleased(e);
window.SetFramerateLimit(30);

while (window.IsOpen)
{
    
    window.DispatchEvents();
    window.Clear(Settings.BackgroundColor);

    
    
    window.Draw(GameManager.Instance.Deck.View);
    window.Draw(GameManager.Instance.Field.View);


    foreach (var card in GameManager.Instance.Deck.Cards)
    {
        window.Draw(card.View);
    }
    foreach (var card in GameManager.Instance.Field.Cards)
    {
        window.Draw(card.View);
    }

    var selectedCard = GameManager.Instance.Deck.Cards.Find(x => x.IsSelected);
    if(!(selectedCard is null))
        window.Draw(selectedCard.View);
    window.Draw(button);
    window.Display();
    if (endTurn)
    {
        endTurn = false;
        GameManager.Instance.PlayersTurn();
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