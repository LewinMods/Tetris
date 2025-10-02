using SFML.Window;

namespace Platformer;

public delegate void InputEvent(string key);

public class InputManager
{
    private Dictionary<string, bool> previousKeyStates = new();
    private List<string> keys;
    
    public event InputEvent InputHit;

    public InputManager(List<string> keys)
    {
        this.keys = keys;
        
        foreach (var key in keys)
        {
            previousKeyStates[key] = false;
        }
    }

    public void Update()
    {
        foreach (var key in keys)
        {
            if (Enum.TryParse(key, true, out Keyboard.Key keyEnum))
            {
                bool isPressed = Keyboard.IsKeyPressed(keyEnum);
                bool wasPressed = previousKeyStates[key];
                
                if (isPressed && !wasPressed)
                {
                    InputHit?.Invoke(key);
                }
                
                previousKeyStates[key] = isPressed;
            }
        }
    }
}