using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class Lost : State
    {
        public Lost(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Lost");

                    break;
                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Perdeu");

                    break;
            }

            # endregion
        }

        public override void Update(GameTime gameTime)
        {
            # region Esc

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                MediaPlayer.Stop();
                Manager.GoToMenu();
            }

            # endregion

            # region Enter

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Enter) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Enter))
            {
                MediaPlayer.Stop();
                Manager.GoToMenu();
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}