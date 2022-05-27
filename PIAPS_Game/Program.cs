using PIAPS_Game;
using PIAPS_Game.Builder;
using PIAPS_Game.View;
using PIAPS_Game.GameLogic;
using PIAPS_Game.Map;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var window = Settings.Window;
window.Closed += (sender, eventArgs) => window.Close();
GameManager.Instance.StartGame();
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
    window.Display();
}