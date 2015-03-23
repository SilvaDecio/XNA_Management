using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class Credits : State
    {
        Texture2D Institute;

        Vector2 PositionCredits, PositionInstitute;

        float XImpulse , OffSet;

        public Credits(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    Institute = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Institute");
                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Credits/Credits");

                    break;

                case GameLanguage.Portugues:

                    Institute = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Instituto");
                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Creditos/Creditos");

                    break;
            }

            # endregion

            OffSet = 250f;

            PositionCredits = new Vector2(-OffSet , 0);
            PositionInstitute = new Vector2(BackGroundImage.Width - OffSet, 0);
            
            XImpulse = 4f;
        }

        public override void Update(GameTime gameTime)
        {
            # region Esc

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                Manager.GoToMenu();
            }

            # endregion

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                XImpulse = Manager.CurrentMouse.X - Manager.OldMouse.X;
                XImpulse = MathHelper.Clamp(XImpulse, -15, 15);
            }

            PositionCredits.X += XImpulse;
            PositionInstitute.X += XImpulse;

            PositionCredits.X = MathHelper.Clamp(PositionCredits.X, -BackGroundImage.Width, 0);
            PositionInstitute.X = MathHelper.Clamp(PositionInstitute.X, 0, BackGroundImage.Width);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, PositionCredits, Color.White);
            Manager.spriteBatch.Draw(Institute, PositionInstitute, Color.White);

            base.Draw(gameTime);
        }
    }
}