using SFML.Graphics;
using SFML.System;

namespace Platformer;

public class BuildingBlock : Entity
{
    private static IntRect[] colors = new IntRect[5]
    {
        new IntRect(32,0,16,16),
        new IntRect(48,0,16,16),
        new IntRect(64,0,16,16),
        new IntRect(0,16,16,16),
        new IntRect(16,16,16,16),
    };
    
    public BuildingBlock() : base("tilemap")
    {
        int random = new Random().Next(0, colors.Length);
        
        sprite.TextureRect = colors[random];
        BuildingBlock[,] lalal =  new BuildingBlock[3, 3];
    }
}