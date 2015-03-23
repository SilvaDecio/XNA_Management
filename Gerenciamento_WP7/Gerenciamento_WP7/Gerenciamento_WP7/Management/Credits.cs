using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class Credits : State
    {
        Texture2D CreditsImage, InstituteImage, TopImage, BottomImage;

        Vector2 CreditsPosition , InstitutePosition , TopPosition, BottomPosition;

        float OffSet;

        Vector2 Impulse;

        public Credits(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    CreditsImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Credits");
                    InstituteImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Institute");
                    TopImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Top");
                    BottomImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Bottom");

                    break;

                case GameLanguage.Portugues:

                    CreditsImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Creditos");
                    InstituteImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Instituto");
                    TopImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Topo");
                    BottomImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Baixo");

                    break;
            }

            # endregion

            Impulse = new Vector2(4, 0);

            OffSet = 250f;

            CreditsPosition = new Vector2(-OffSet, TopImage.Height);
            InstitutePosition = new Vector2(CreditsImage.Width - OffSet, 0);

            TopPosition = new Vector2(-OffSet, 0);
            BottomPosition = new Vector2(-OffSet, Manager.Game.GraphicsDevice.Viewport.Height - BottomImage.Height);
        }

        public override void  Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToMenu();
            }

            # endregion

            if (Manager.HorizontalDrag)
            {
                Impulse.X = Manager.CurrentGesture.Delta.X;
                Impulse.X = MathHelper.Clamp(Impulse.X, -15, 15);
            }

            if (Manager.VerticalDrag)
            {
                Impulse.Y = Manager.CurrentGesture.Delta.Y;
            }
            else
            {
                Impulse.Y = 0f;

                CreditsPosition.Y -= 0.5f;
            }

            CreditsPosition += Impulse;

            InstitutePosition.X += Impulse.X;

            TopPosition.X += Impulse.X;
            BottomPosition.X += Impulse.X;

            CreditsPosition.X = MathHelper.Clamp(CreditsPosition.X, -CreditsImage.Width, 0);
            CreditsPosition.Y = MathHelper.Clamp(CreditsPosition.Y,
                (Manager.Game.GraphicsDevice.Viewport.Height - CreditsImage.Height) - BottomImage.Height,
                TopImage.Height);

            InstitutePosition.X = MathHelper.Clamp(InstitutePosition.X, 0, CreditsImage.Width);

            TopPosition.X = MathHelper.Clamp(TopPosition.X, -CreditsImage.Width, 0);
            BottomPosition.X = MathHelper.Clamp(BottomPosition.X, -CreditsImage.Width, 0);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(CreditsImage, CreditsPosition, Color.White);
            Manager.spriteBatch.Draw(InstituteImage, InstitutePosition, Color.White);

            Manager.spriteBatch.Draw(TopImage, TopPosition, Color.White);
            Manager.spriteBatch.Draw(BottomImage, BottomPosition, Color.White);

            base.Draw(gameTime);
        }
    }
}