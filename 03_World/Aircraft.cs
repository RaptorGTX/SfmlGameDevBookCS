using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class Aircraft: Entity
    {
        public enum Type
        {
            Eagle,
            Raptor
        }

        private Type type;
        private Sprite sprite;

        public Aircraft(Type type, TextureResourceHolder textures)
        {
            this.type = type;
            this.sprite = new Sprite(textures.Get(ToTextureId(type)));

            FloatRect bounds = this.sprite.GetLocalBounds();
            this.sprite.Origin = new Vector2f(bounds.Width / 2f, bounds.Height / 2f);
        }

        protected override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            target.Draw(this.sprite, states);
        }

        private static TextureId ToTextureId(Type type)
        {
            switch (type)
            {
                case Type.Eagle:
                    return TextureId.Eagle;
                case Type.Raptor:
                    return TextureId.Raptor;
            }

            return TextureId.Eagle;
        }
    }
}
