using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class Entity: SceneNode
    {
        public Vector2f Velocity { get; set; }

        protected override void UpdateCurrent(Time dt)
        {
            this.Position += Velocity * dt.AsSeconds();
        }
    }
}
