using SFML.Graphics;
using SFML.System;

namespace Tetris;

public class GUI
{
    private Text scoreText;
    private Text highScoreText;
    
    private int currentScore;
    
    private int highScore;
    
    public GUI()
    {
        scoreText = new Text();
        highScoreText = new Text();
        
        currentScore = 0;
    }

    public void Create(Scene scene)
    {
        highScore = scene.saveFile.Load();
        
        scoreText.Font = scene.LoadFont("pixel-font");
        highScoreText.Font = scene.LoadFont("pixel-font");
        
        scoreText.DisplayedString = "Score";
        scoreText.Scale = new Vector2f(0.5f, 0.5f);
        
        highScoreText.DisplayedString = "HighScore";
        highScoreText.Scale = new Vector2f(0.5f, 0.5f);
        
        scene.eventManager.GainScore += OnGainScore;
    }

    public void Render(RenderTarget target)
    {
        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(240 - scoreText.GetGlobalBounds().Width, 50);
        
        highScoreText.DisplayedString = $"HighScore: {highScore}";
        highScoreText.Position = new Vector2f(240 - highScoreText.GetGlobalBounds().Width, 20);
        
        target.Draw(scoreText);
        target.Draw(highScoreText);
    }

    private void OnGainScore(Scene scene, int amount)
    {
        currentScore += amount;
    }
}