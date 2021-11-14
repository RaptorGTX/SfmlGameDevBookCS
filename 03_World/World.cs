using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class World
    {
        private enum Layer
        {
            Background,
            Air,
            LayerCount
        }

        private RenderWindow window;
        private View worldView;
        private TextureResourceHolder textures = new TextureResourceHolder();
        private SceneNode sceneGraph = new SceneNode();
        private SceneNode[] sceneLayers = new SceneNode[(int)Layer.LayerCount];

        private FloatRect worldBounds;
        private Vector2f spawnPosition;
        private float scrollSpeed = -50f;
        private Aircraft playerAircraft;

        public World(RenderWindow window)
        {
            this.window = window;
            this.worldView = new View(window.DefaultView.Center, window.DefaultView.Size);
            this.worldBounds = new FloatRect(0f, 0f, worldView.Size.X, 2000f);
            this.spawnPosition = new Vector2f(worldView.Size.X / 2f, worldBounds.Height - worldView.Size.Y / 2f);

            LoadTextures();
            BuildScene();

            // Prepare the view
            this.worldView.Center = this.spawnPosition;
        }

        public void Update(Time dt)
        {
            // Scroll the world
            this.worldView.Move(new Vector2f(0f, scrollSpeed * dt.AsSeconds()));

            // Move the player sidewards (plane scouts follow the main aircraft)
            Vector2f position = playerAircraft.Position;
            Vector2f velocity = playerAircraft.Velocity;

            // If player touches borders, flip its X velocity
            if (position.X <= worldBounds.Left + 150
                || position.X >= worldBounds.Left + worldBounds.Width - 150)
            {
                velocity.X = -velocity.X;
                this.playerAircraft.Velocity = velocity;
            }

            // Apply movements
            this.sceneGraph.Update(dt);
        }

        public void Draw()
        {
            this.window.SetView(this.worldView);
            this.window.Draw(sceneGraph);
        }

        private void LoadTextures()
        {
            this.textures.Load(TextureId.Eagle, "Media/Textures/Eagle.png");
            this.textures.Load(TextureId.Raptor, "Media/Textures/Raptor.png");
            this.textures.Load(TextureId.Desert, "Media/Textures/Desert.png");
        }

        private void BuildScene()
        {
            // Initialize the different layers
            for (int i = 0; i < (int)Layer.LayerCount; i++)
            {
                SceneNode layer = new SceneNode();
                this.sceneLayers[i] = layer;

                this.sceneGraph.AttachChild(layer);
            }

            // Prepare the tiled background
            Texture texture = textures.Get(TextureId.Desert);
            IntRect textureRect = new IntRect((int)worldBounds.Left, (int)worldBounds.Top, (int)worldBounds.Width, (int)worldBounds.Height);
            texture.Repeated = true;

            // Add the background sprite to the scene
            SpriteNode backgroundSprite = new SpriteNode(texture, textureRect);
            backgroundSprite.Position = new Vector2f(worldBounds.Left, worldBounds.Top);
            sceneLayers[(int)Layer.Background].AttachChild(backgroundSprite);

            // Add player's aircraft
            this.playerAircraft = new Aircraft(Aircraft.Type.Eagle, textures);
            this.playerAircraft.Position = this.spawnPosition;
            this.playerAircraft.Velocity = new Vector2f(40f, scrollSpeed);
            this.sceneLayers[(int)Layer.Air].AttachChild(playerAircraft);

            // Add the escorting aircrafts, placed relatively to the main plane
            Aircraft leftEscort = new Aircraft(Aircraft.Type.Raptor, textures);
            leftEscort.Position = new Vector2f(-80f, 50f);
            this.playerAircraft.AttachChild(leftEscort);

            Aircraft rightEscort = new Aircraft(Aircraft.Type.Raptor, textures);
            rightEscort.Position = new Vector2f(80f, 50f);
            this.playerAircraft.AttachChild(rightEscort);
        }
    }
}
