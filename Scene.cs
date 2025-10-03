using SFML.Graphics;
using SFML.System;

namespace Tetris;

public class Scene
{
    private List<Entity> entities;
    private List<Piece> pieces;
    private Dictionary<string, Texture> textures;
    private Dictionary<string, Font> fonts;
    
    public InputManager inputManager;
    public SaveFile saveFile;
    public EventManager eventManager;

    private GUI gui;

    public int Score = 0;
    
    public Scene()
    {
        entities = new List<Entity>();
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
        pieces = new List<Piece>();
        
        inputManager = new InputManager(new List<string>(){"A", "D", "W"});
        saveFile = new SaveFile("SaveFile");
        eventManager = new EventManager();
        
        gui = new GUI();
        
        CreatePiece();
        
        gui.Create(this);
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
    
    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entities.RemoveAt(i); 
            entity.Destroy(this);
        }
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(Scene scene, float deltaTime)
    {
        inputManager.Update(scene);
        eventManager.Update(scene, deltaTime);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(deltaTime);
        }
        
        for (int i = pieces.Count - 1; i >= 0; i--)
        {
            pieces[i].Update(scene);
        }
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];

            if (entity.Dead)
            {
                entities.RemoveAt(i);
                entity.Destroy(scene);
            }
        }
        
        for (int i = pieces.Count - 1; i >= 0; i--)
        {
            Piece piece = pieces[i];

            if (piece.Placed)
            {
                pieces.RemoveAt(i);
                piece.Destroy(scene);
                
                foreach (var block in piece.selectedPiece)
                {
                    if (block == null) continue;

                    if (block.Position.Y <= 50 && block.Position.Y >= 20)
                    {
                        Clear();
                        
                        return;
                    }
                }
                
                CheckRows();
            }
        }
    }

    public void RenderAll(RenderTarget target)
    {
        gui.Render(target);
        
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

    private void CheckRows()
    {
        Dictionary<float, List<BuildingBlock>> rows = new Dictionary<float, List<BuildingBlock>>();

        foreach (var entity in entities)
        {
            if (entity is not BuildingBlock block || !block.Solid) continue;

            float rowYlevel = (float)Math.Floor(block.Position.Y / 16f) * 16f;

            if (!rows.ContainsKey(rowYlevel))
            {
                rows[rowYlevel] = new List<BuildingBlock>();
            }

            rows[rowYlevel].Add(block);
        }
        
        foreach (var pair in rows)
        {
            float y = pair.Key;
            var blocks = pair.Value;

            if (blocks.Count == 15)
            {
                eventManager.PublishGainScore(100);
                
                foreach (var block in blocks)
                {
                    block.Dead = true;
                }
                
                for (int i = entities.Count - 1; i >= 0; i--)
                {
                    if (entities[i].Position.Y < y)
                    {
                       entities[i].Position += new Vector2f(0, 16); 
                    }
                }
            }
        }
    }
    
    public Font LoadFont(string name)
    {
        if (fonts.TryGetValue(name, out Font foundFont))
        {
            return foundFont;
        }
     
        Font font = new Font($"assets/{name}.ttf");
        fonts[name] = font;
        return font;
    }
}