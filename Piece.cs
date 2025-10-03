using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Tetris;

public class Piece
{
    public Vector2f Position =  new Vector2f(150,50);
    
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

    private int dir = 0;
    private int speed = 1000;
    
    private bool placed = false;

    public BuildingBlock[,] selectedPiece;
    
    public Piece(Scene scene)
    {
        clock.Restart();
        
        int r = new Random().Next(0, 7);
        
        int[,] chosen = pieces[r];
        
        selectedPiece = new BuildingBlock[chosen.GetLength(0), chosen.GetLength(1)];
        
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

        Position -= new Vector2f(chosen.GetLength(0)*16, 0);
        
        scene.inputManager.InputHit += Move;
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

    public void Update(Scene scene)
    {
        if (placed) return;
        
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            speed = 100;
        }
        else
        {
            speed = 1000;
        }
        
        if (CheckCollishion(scene, "down"))
        {
            Placed = true;

            scene.CreatePiece();

            return;
        }

        if (clock.ElapsedTime.AsMilliseconds() >= speed)
        {
            clock.Restart();
            Position += new Vector2f(0, 16);
        }
    }


    private void Move(Scene scene, string hit)
    {
        switch (hit)
        { 
            case "A":
                if (!CheckCollishion(scene, "left"))
                {
                    dir = -16;
                }
                break;
            case "D":
                if (!CheckCollishion(scene, "right"))
                {
                    dir = 16; 
                }
                break;
            case "W":
                Rotate(scene);
                break;
        }
        
        Position += new Vector2f(dir, 0); 
        dir = 0;
    }

    private bool CheckCollishion(Scene scene, string looking)
    {
        for (int x = 0; x < selectedPiece.GetLength(0); x++)
        {
            for (int y = 0; y < selectedPiece.GetLength(1); y++)
            {
                var block = selectedPiece[x, y];
                
                if (block == null) continue;
                
                if (block.Position.X >= Program.ScreenWidth / 2 - 32 && looking == "right") return true;
                if (block.Position.X <= 16 && looking == "left") return true;

                Vector2f at = Position + new Vector2f(y * 16, x * 16);

                Vector2f direction = new Vector2f(0, 0);

                switch (looking)
                {
                    case "down":
                        direction = at +  new Vector2f(0, 16);
                        break;
                    case "left":
                        direction = at +  new Vector2f(-16, 0);
                        break;
                    case "right":
                        direction = at +  new Vector2f(16, 0);
                        break;
                }
                
                FloatRect rect = new FloatRect(direction.X, direction.Y, 1, 1);

                if (direction.Y >= 378) return true;

                var hits = scene.FindIntersects(rect);
                
                if (hits.Any(e => e.Solid && !selectedPiece.Cast<Entity>().Contains(e)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void Destroy(Scene scene)
    {
        scene.inputManager.InputHit -= Move;
    }

    private void Rotate(Scene scene)
    {
        int sizeX = selectedPiece.GetLength(0);
        int sizeY = selectedPiece.GetLength(1);
        
        BuildingBlock[,] rotated = new BuildingBlock[sizeY, sizeX];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                BuildingBlock block = selectedPiece[x, y];
                
                if (block == null) continue;
                
                rotated[y, sizeX - 1 - x] = block;
                
                block.OriginalPos = new Vector2f((sizeX - 1 - x) * 16, y * 16);
            }
        }

        selectedPiece = rotated;
    }
}