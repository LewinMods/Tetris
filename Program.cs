
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Platformer {
    
    class Program {
        
        public static uint ScreenWidth = 500, ScreenHeight = 800;
        
        static void Main(string[] args) {
            
            using (var window = new RenderWindow(new VideoMode(ScreenWidth, ScreenHeight), "Tetris")) {
                
                window.Closed += (o, e) => window.Close();
                
                window.SetFramerateLimit(60);

                Clock clock = new Clock();
                
                Piece.InitializePieces();
                
                Scene scene = new Scene();
                
                while (window.IsOpen) {
                    
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    
                    scene.UpdateAll(scene, deltaTime);

                    window.Clear();
                    
                    scene.RenderAll(window);

                    window.Display();
                }
            }
        }
    }
}