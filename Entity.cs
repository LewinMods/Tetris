using SFML.Graphics;
using SFML.System;

namespace Platformer;

public abstract class Entity
{
    public readonly Sprite sprite;
    private readonly string texture;

    public Entity(string textureName)
    {
        texture = textureName;
        sprite = new Sprite();
    }

    public void Create(Scene scene)
    {
        sprite.Texture = scene.LoadTexture(texture);
    }

    public Vector2f Position
    {
        get { return sprite.Position; }
        set { sprite.Position = value; }
    }

    public virtual void Update(float deltaTime)
    {
        
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}