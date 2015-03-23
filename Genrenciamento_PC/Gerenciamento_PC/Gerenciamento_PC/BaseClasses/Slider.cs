using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gerenciamento_PC.BaseClasses
{
    class Slider
    {
        public Texture2D Image;

        Texture2D BarImage;

        public Vector2 Position , NamePosition;

        public Rectangle Rectangle;

        public float MinimumPosition, MaximumPosition, Value;

        public string Name;

        public Slider(Texture2D image, Texture2D barImage, float value, Vector2 position, Vector2 namePosition, float minimumPosition,
            string name)
        {
            Image = image;
            BarImage = barImage;

            Value = value;

            Name = name;

            Position = position;
            NamePosition = namePosition;

            MinimumPosition = minimumPosition;
            MaximumPosition = MinimumPosition + 100;

            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
        }

        public void Update(GameTime gameTime)
        {
            Position.X = MathHelper.Clamp(Position.X , MinimumPosition , MaximumPosition);

            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch , SpriteFont font)
        {
            spriteBatch.DrawString(font , Name , NamePosition, Color.White);

            spriteBatch.Draw(BarImage, new Vector2(MinimumPosition, Position.Y + (Image.Height / 3)), Color.White);

            spriteBatch.Draw(Image, Position, Color.White);
            
            spriteBatch.DrawString(font, ((int)Value).ToString(), new Vector2(NamePosition.X,
                NamePosition.Y + 60),Color.White);
        }
    }
}