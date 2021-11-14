using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class SpriteNode: SceneNode
    {
        private Sprite sprite = new Sprite();

        public SpriteNode(Texture texture)
        {
            this.sprite.Texture = texture;
        }

        public SpriteNode(Texture texture, IntRect rect)
        {
            this.sprite.Texture = texture;
            this.sprite.TextureRect = rect;
        }

        protected override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            target.Draw(this.sprite, states);
        }
    }
}
