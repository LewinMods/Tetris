using SFML.Graphics;
using SFML.System;

namespace Tetris;

public class BuildingBlock : Entity
{
    public static IntRect[] Colors = new IntRect[5]
    {
        new IntRect(32,0,16,16),
        new IntRect(48,0,16,16),
        new IntRect(64,0,16,16),
        new IntRect(0,16,16,16),
        new IntRect(16,16,16,16),
    };
    
    public Vector2f OriginalPos;
    private Piece piece;
    
    public BuildingBlock(IntRect random, Piece piece) : base("tilemap")
    {
        sprite.TextureRect = random;
        this.piece = piece;
    }

    public override void Update(float deltaTime)
    {
        if (!piece.Placed)
        {
            Position = OriginalPos + piece.Position;
        }
    }
}