using SFML.Graphics;
using SFML.System;

namespace Platformer;

public class Scene
{
    private List<Entity> entities;
    private List<Piece> pieces;
    private Dictionary<string, Texture> textures;
    
    public InputManager inputManager;

    public Scene()
    {
        entities = new List<Entity>();
        textures = new Dictionary<string, Texture>();
        pieces = new List<Piece>();
        
        inputManager = new InputManager(new List<string>(){"A", "D"});
        
        CreatePiece();
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

    public void CreatePiece()
    {
        pieces.Add(new Piece(this));
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(Scene scene, float deltaTime)
    {
        inputManager.Update();
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(deltaTime);
        }
        
        for (int i = pieces.Count - 1; i >= 0; i--)
        {
            pieces[i].Update(scene);
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }
    
    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }
}