using SFML.Graphics;
using SFML.System;

namespace Platformer;

public class Scene
{
    private List<Entity> entities;
    private Dictionary<string, Texture> textures;

    public Scene()
    {
        entities = new List<Entity>();
        textures = new Dictionary<string, Texture>();

        for (int i = 0; i < 40; i++)
        {
            int r = new Random().Next(0, 500);
            int r1 = new Random().Next(0, 800);
            
            Spawn(new BuildingBlock() {Position = new Vector2f(r,r1)});
        }
    }

    public Texture LoadTexture(string name)
    {
        if (textures.ContainsKey(name))
        {
            return textures[name];
        }
        
        Texture texture = new Texture($"assets/{name}.png");
        textures.Add(name, texture);
        
        return texture;
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(float deltaTime)
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(deltaTime);
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }
}