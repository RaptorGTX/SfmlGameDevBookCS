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
        private static readonly float PlayerSpeed = 100.0f;
        private static readonly Time TimePerFrame = Time.FromSeconds(1.0f / 60.0f);

        private RenderWindow window;
        private Texture texture;
        private Sprite player = new Sprite();

        private Font font;
        private Text statisticsText = new Text();
        private Time statisticsUpdateTime = Time.Zero;
        private int statisticsNumFrames = 0;

        private bool isMovingUp = false;
        private bool isMovingDown = false;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;

        public Game()
        {
            this.window = new RenderWindow(new SFML.Window.VideoMode(640, 480), "SFML Application");

            this.texture = new Texture("Media/Textures/Eagle.png");

            this.player.Texture = this.texture;
            this.player.Position = new Vector2f(100.0f, 100.0f);

            this.font = new Font("Media/Sansation.ttf");

            this.statisticsText.Font = this.font;
            this.statisticsText.Position = new Vector2f(5.0f, 5.0f);
            this.statisticsText.CharacterSize = 10;

            // These 2 event handlers replace the handlePlayerInput method from the book's C++ code
            this.window.KeyPressed += HandlePlayerKeyDown;
            this.window.KeyReleased += HandlePlayerKeyUp;
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
            if (args.Code == Keyboard.Key.W)
                this.isMovingUp = true;
            else if (args.Code == Keyboard.Key.S)
                this.isMovingDown = true;
            else if (args.Code == Keyboard.Key.A)
                this.isMovingLeft = true;
            else if (args.Code == Keyboard.Key.D)
                this.isMovingRight = true;
        }

        private void HandlePlayerKeyUp(object sender, KeyEventArgs args)
        {
            if (args.Code == Keyboard.Key.W)
                this.isMovingUp = false;
            else if (args.Code == Keyboard.Key.S)
                this.isMovingDown = false;
            else if (args.Code == Keyboard.Key.A)
                this.isMovingLeft = false;
            else if (args.Code == Keyboard.Key.D)
                this.isMovingRight = false;
        }

        private void Update(Time deltaTime)
        {
            Vector2f movement = new Vector2f(0.0f, 0.0f);

            if (this.isMovingUp)
                movement += new Vector2f(0.0f, -PlayerSpeed);

            if (this.isMovingDown)
                movement += new Vector2f(0.0f, PlayerSpeed);

            if (this.isMovingLeft)
                movement += new Vector2f(-PlayerSpeed, 0.0f);

            if (this.isMovingRight)
                movement += new Vector2f(PlayerSpeed, 0.0f);

            this.player.Position += movement * deltaTime.AsSeconds();
        }

        private void Render()
        {
            this.window.Clear();
            this.window.Draw(this.player);
            this.window.Draw(this.statisticsText);
            this.window.Display();
        }
    }
}
