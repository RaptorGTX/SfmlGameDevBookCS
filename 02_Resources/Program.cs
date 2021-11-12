using SFML.Graphics;
using System;

namespace SfmlGameDevelopmentBook
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(640, 480), "SFML Application");
            window.SetFramerateLimit(20);

            TextureResourceHolder textures = new TextureResourceHolder();

            // Try to load resources
            try
            {
                textures.load(TextureId.Landscape, "Media/Textures/Desert.png");
                textures.load(TextureId.Airplane, "Media/Textures/Eagle.png");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
                
                return;
            }

            // Access resources
            Sprite landscape = new Sprite(textures.get(TextureId.Landscape));
            Sprite airplane = new Sprite(textures.get(TextureId.Airplane));
            airplane.Position = new SFML.System.Vector2f(200f, 200f);

            // Use lambda expressions to replace the window.pollEvent() loop
            window.KeyPressed += (sender, args) =>
            {
                window.Close();
            };

            window.Closed += (sender, args) =>
            {
                window.Close();
            };

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                window.Draw(landscape);
                window.Draw(airplane);
                window.Display();
            }
        }
    }
}
