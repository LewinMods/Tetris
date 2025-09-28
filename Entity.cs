using SFML.Graphics;
using SFML.System;

namespace Platformer;

public abstract class Entity
{
    protected readonly Sprite sprite;
    private readonly string texture;

    protected Entity(string textureName)
    {
        texture = textureName;
        sprite = new Sprite();
    }

    public void Create(Scene scene)
    {
        sprite.Texture = scene.LoadTexture(texture);
    }

    public virtual Vector2f Position
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