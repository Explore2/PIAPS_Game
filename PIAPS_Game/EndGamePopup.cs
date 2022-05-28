using PIAPS_Game.GameLogic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PIAPS_Game;

public class EndGamePopup : Transformable, Drawable
{
    private RectangleShape background = new RectangleShape();
    private Button restartButton;
    public Text endGame = new Text();

    public EndGamePopup()
    {
        Settings.Window.MouseButtonReleased += (sender, args) => restartButtonReleased(args);
        endGame.Font = new Font($"{Settings.ResourcesPath}/Fonts/Arial.ttf");
        endGame.CharacterSize = 100;
        endGame.DisplayedString = "You lose";
        endGame.Position = new Vector2f(Settings.Window.Size.X / 2 - endGame.GetGlobalBounds().Width/2,
            Settings.Window.Size.Y / 2 - endGame.GetGlobalBounds().Height/2 - 200);
        endGame.FillColor = Color.Black;
        background = new RectangleShape((Vector2f) Settings.Window.Size);
        background.FillColor = Settings.BackgroundColor;
        restartButton = new Button(new Vector2f(600, 200), new Image(Settings.ResourcesPath+"/buttonNextTurn.png"));
        restartButton.Position = new Vector2f(Settings.Window.Size.X/2 - restartButton.Size.X / 2,
            Settings.Window.Size.Y/2 - restartButton.Size.Y/2);
    }
    

    public void restartButtonReleased(MouseButtonEventArgs e)
    {
        if (GameManager.Instance.GameState != GameStatus.Play && this.Contains(e.X, e.Y))
        {
            GameManager.Instance.StartGame();
            GameManager.Instance.GameState = GameStatus.Play;
        }
        
    }

    public bool Contains(float x, float y)
    {
        float minX = Math.Min(restartButton.Position.X, restartButton.Position.X + restartButton.Size.X);
            float maxX = Math.Max(restartButton.Position.X, restartButton.Position.X + restartButton.Size.X);
            float minY = Math.Min(restartButton.Position.Y, restartButton.Position.Y + restartButton.Size.Y);
            float maxY = Math.Max(restartButton.Position.Y, restartButton.Position.Y + restartButton.Size.Y);
            
            return ( x >= minX ) && ( x < maxX ) && ( y >= minY ) && ( y < maxY );
    }
    
    public void Draw(RenderTarget target, RenderStates states)
    {
        if (GameManager.Instance.GameState == GameStatus.Lose)
        {
            endGame.DisplayedString = "You lost";
        } else if (GameManager.Instance.GameState == GameStatus.Win)
        {
            endGame.DisplayedString = "You win!";
        }
        
        target.Draw(background);
        target.Draw(restartButton);
        target.Draw(endGame);
    }
}