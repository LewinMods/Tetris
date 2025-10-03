namespace Tetris;

public delegate void ValueChangedEvent(Scene scene, int value);

public class EventManager
{
    public event ValueChangedEvent GainScore;
    
    private int scoreGained;
    
    public EventManager()
    {
        
    }

    public void Update(Scene scene, float deltaTime)
    {
        if (scoreGained != 0)
        {
            GainScore?.Invoke(scene, scoreGained);
            scoreGained = 0;
        }
    }
    
    public void PublishGainScore(int amount) => scoreGained += amount;
}