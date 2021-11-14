using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class Game
    {
        private static readonly Time TimePerFrame = Time.FromSeconds(1.0f / 60.0f);

        private RenderWindow window;
        private World world;

        private Font font;
        private Text statisticsText = new Text();
        private Time statisticsUpdateTime = Time.Zero;
        private int statisticsNumFrames = 0;

        public Game()
        {
            this.window = new RenderWindow(new SFML.Window.VideoMode(640, 480), "SFML Application");
            this.world = new World(this.window);

            this.font = new Font("Media/Sansation.ttf");

            this.statisticsText.Font = this.font;
            this.statisticsText.Position = new Vector2f(5.0f, 5.0f);
            this.statisticsText.CharacterSize = 10;

            // These 3 event handlers replace the processEvents method from the book's C++ code
            this.window.KeyPressed += HandlePlayerKeyDown;
            this.window.KeyReleased += HandlePlayerKeyUp;
            this.window.Closed += (sender, args) => window.Close();
        }

        public void Run()
        {
            Clock clock = new Clock();
            Time timeSinceLastUpdate = Time.Zero;

            while (window.IsOpen)
            {
                Time elapsedTime = clock.Restart();
                timeSinceLastUpdate += elapsedTime;

                while (timeSinceLastUpdate.AsMicroseconds() > TimePerFrame.AsMicroseconds())
                {
                    timeSinceLastUpdate -= TimePerFrame;
                    ProcessEvents();
                    Update(TimePerFrame);
                }

                UpdateStatistics(elapsedTime);
                Render();
            }
        }

        private void ProcessEvents()
        {
            this.window.DispatchEvents();
        }

        private void UpdateStatistics(Time elapsedTime)
        {
            this.statisticsUpdateTime += elapsedTime;
            this.statisticsNumFrames += 1;

            if (this.statisticsUpdateTime.AsSeconds() >= Time.FromSeconds(1.0f).AsSeconds())
            {
                this.statisticsText.DisplayedString = $"Frames / Second = {this.statisticsNumFrames} \nTime / Update = {this.statisticsUpdateTime.AsMicroseconds() / this.statisticsNumFrames}";

                this.statisticsUpdateTime -= Time.FromSeconds(1.0f);
                statisticsNumFrames = 0;
            }
        }

        private void HandlePlayerKeyDown(object sender, KeyEventArgs args)
        {
            HandlePlayerInput(args.Code, true);
        }

        private void HandlePlayerKeyUp(object sender, KeyEventArgs args)
        {
            HandlePlayerInput(args.Code, false);
        }

        private void Update(Time deltaTime)
        {
            this.world.Update(deltaTime);
        }

        private void Render()
        {
            this.window.Clear();
            this.world.Draw();

            this.window.SetView(this.window.DefaultView);
            this.window.Draw(this.statisticsText);
            this.window.Display();
        }

        private void HandlePlayerInput(Keyboard.Key key, bool isPressed)
        {
        }
    }
}
