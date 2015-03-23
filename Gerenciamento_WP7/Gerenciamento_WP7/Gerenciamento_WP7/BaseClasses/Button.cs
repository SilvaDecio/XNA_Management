using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gerenciamento_WP7.BaseClasses
{
    class Button
    {
        public Texture2D Image;

        public Vector2 Position;
        
        public Rectangle Rectangle;

        public Button(Texture2D image, Vector2 position)
        {
            Image = image;
            
            Position = position;
            
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y,
                Image.Width, Image.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}