using SFML.System;

namespace Platformer;

public class Piece
{
    public Vector2f Position =  new Vector2f(Program.ScreenWidth/2,100);
    
    public bool Placed = false;
    
    private static int[,] IPiece = new int[4, 4]
    {
        { 0, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
    };
    
    private static int[,] OPiece = new int[2, 2]
    {
        { 1, 1,},
        { 1, 1,}
    };
    
    private static int[,] LPiece = new int[3, 3]
    {
        { 0, 0, 1 },
        { 1, 1, 1 },
        { 0, 0, 0 }
    };
    
    private static int[,] JPiece = new int[3, 3]
    {
        { 1, 0, 0 },
        { 1, 1, 1 },
        { 0, 0, 0 }
    };
    
    private static int[,] SPiece = new int[3, 3]
    {
        { 0, 1, 1 },
        { 1, 1, 0 },
        { 0, 0, 0 }
    };
    
    private static int[,] ZPiece = new int[3, 3]
    {
        { 1, 1, 0 },
        { 0, 1, 1 },
        { 0, 0, 0 }
    };
    
    private static int[,] TPiece = new int[3, 3]
    {
        { 0, 0, 0 },
        { 1, 1, 1 },
        { 0, 1, 0 }
    };
    
    private static int[][,] pieces;

    private static Clock clock;
    
    public Piece(Scene scene)
    {
        clock.Restart();
        
        int r = new Random().Next(0, 7);
        
        int[,] chosen = pieces[r];
        
        BuildingBlock[,] selectedPiece = new BuildingBlock[chosen.GetLength(0), chosen.GetLength(1)];
        
        int random = new Random().Next(0, BuildingBlock.Colors.Length);
        
        for (int x = 0; x < chosen.GetLength(0); x++)
        {
            for (int y = 0; y < chosen.GetLength(1); y++)
            {
                if (chosen[x, y] == 0) continue;
                selectedPiece[x, y] = new BuildingBlock(
                    BuildingBlock.Colors[random], this) {
                    OriginalPos = new Vector2f(y * 16, x * 16)
                };
                scene.Spawn(selectedPiece[x, y]);
            }
        }

        Position -= new Vector2f(chosen.GetLength(0)*8, 0);
    }
    
    public static void InitializePieces()
    {
        clock = new Clock();
        
        pieces = new int[][,]
        {
            IPiece,
            OPiece,
            LPiece,
            JPiece,
            SPiece,
            ZPiece,
            TPiece
        };
    }

    public void Update()
    {
        if (clock.ElapsedTime.AsMilliseconds() >= 1000 && Placed == false)
        {
            clock.Restart();
            
            Position += new Vector2f(0, 16);
        }
    }
}