using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public abstract class ResourceHolder<Resource, Identifier>
    {
        protected Dictionary<Identifier, Resource> resourceMap = new Dictionary<Identifier, Resource>();

        public abstract void Load(Identifier identifier, string fileName);

        public Resource Get(Identifier id)
        {
            return resourceMap[id];
        }
    }
}
