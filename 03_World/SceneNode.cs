using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfmlGameDevelopmentBook
{
    public class SceneNode : Transformable, Drawable
    {
        private List<SceneNode> children = new List<SceneNode>();
        private SceneNode parent;

        public void AttachChild(SceneNode child)
        {
            child.parent = this;
            this.children.Add(child);
        }

        public SceneNode DetachChild(SceneNode child)
        {
            if (!this.children.Contains(child))
            {
                return null;
            }

            this.children.Remove(child);
            child.parent = null;

            return child;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            // Apply transform of current node
            states.Transform.Combine(this.Transform);

            // Draw node and children with changed transform
            DrawCurrent(target, states);
            DrawChildren(target, states);
        }

        protected virtual void DrawCurrent(RenderTarget target, RenderStates states)
        {
            // Do nothing by default
        }

        protected virtual void DrawChildren(RenderTarget target, RenderStates states)
        {
            foreach (var child in this.children)
            {
                child.Draw(target, states);
            }
        }

        public void Update(Time dt)
        {
            UpdateCurrent(dt);
            UpdateChildren(dt);
        }

        protected virtual void UpdateCurrent(Time dt)
        {
            // Do nothing by default
        }

        protected virtual void UpdateChildren(Time dt)
        {
            foreach (var child in this.children)
            {
                child.Update(dt);
            }
        }

        public Transform GetWorldTransform()
        {
            Transform transform = Transform.Identity;
            for(var node = this; node != null; node = node.parent)
            {
                transform.Combine(node.Transform);
            }

            return transform;
        }

        public Vector2f GetWorldPosition()
        {
            return GetWorldTransform().TransformPoint(new Vector2f(0f, 0f));
        }
    }
}
