using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public enum TextureId
    {
        Landscape,
        Airplane
    }

    public class TextureResourceHolder : ResourceHolder<Texture, TextureId>
    {
        public override void load(TextureId identifier, string fileName)
        {
            Texture texture = new Texture(fileName);
            this.resourceMap.Add(identifier, texture);
        }
    }
}
