using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gerenciamento_PC.BaseClasses
{
    class Button
    {
        public Texture2D Image;
        
        public Rectangle Rectangle;
        
        float Scale;

        public Button(Texture2D image , Vector2 Position)
        {
            Image = image;

            Rectangle = new Rectangle((int)Position.X, (int)Position.Y,
                Image.Width, Image.Height);

            Decrease();
        }

        public void Update(Point mouse)
        {
            if (Rectangle.Contains(mouse))
            {
                if (Scale == 0.5f)
                {
                    Increase();
                }
            }
            else
	        {
                if (Scale == 2f)
                {
                    Decrease();
                }
	        }            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Rectangle,null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

        void Increase()
        {
            Scale = 2f;

            int amountHoriz, amountVert;
            amountHoriz = (int)((Rectangle.Width * Scale) - Rectangle.Width) / 2;
            amountVert = (int)((Rectangle.Height * Scale) - Rectangle.Height) / 2;
            
            Rectangle.Inflate(amountHoriz, amountVert);
        }

        void Decrease()
        {
            Scale = 0.5f;

            int amountHoriz, amountVert;
            amountHoriz = (int)((Rectangle.Width * Scale) - Rectangle.Width) / 2;
            amountVert = (int)((Rectangle.Height * Scale) - Rectangle.Height) / 2;

            Rectangle.Inflate(amountHoriz, amountVert);
        }
    }
}