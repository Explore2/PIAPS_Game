using SFML.Graphics;
using SFML.Window;

namespace PIAPS_Game;

public static class Settings
{
    public static RenderWindow Window = new RenderWindow(new VideoMode(600, 900), "PIAPS_game", Styles.Close);
    public static string ResourcesPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Resources"; 
    
}