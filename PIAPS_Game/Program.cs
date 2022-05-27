using PIAPS_Game;
using PIAPS_Game.Builder;
using PIAPS_Game.GameLogic;
using SFML;
using SFML.Graphics;
using SFML.Window;


var gameController = new GameManager();

gameController.StartGame();



var window = new RenderWindow(new VideoMode(500, 500), "PIAPS_Game");
window.Closed += (sender, eventArgs) => window.Close();
var cc = new CardCreator
{
    Builder = new CloseRangeBuilder()
};
var card = cc.CreateCard();
while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Display();
}