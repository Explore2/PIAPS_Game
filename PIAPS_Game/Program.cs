using PIAPS_Game;
using SFML;
using SFML.Graphics;
using SFML.Window;

var window = new RenderWindow(new VideoMode(500, 500), "PIAPS_Game");
window.Closed += (sender, eventArgs) => window.Close();

while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear(Color.Black);
    window.Display();
}