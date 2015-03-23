using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Gerenciamento_WP7.Management;

namespace Gerenciamento_WP7.BaseClasses
{
    public abstract class State
    {
        protected Texture2D BackGroundImage;
        protected StateManager Manager;

        public virtual void Restart() { }

        public virtual void Update(GameTime gameTime) {}

        public virtual void Draw(GameTime gameTime) {}
    }
}