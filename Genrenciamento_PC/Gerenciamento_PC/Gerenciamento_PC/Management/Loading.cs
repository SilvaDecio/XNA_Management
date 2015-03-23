using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

using System.Globalization;

namespace Gerenciamento_PC.Management
{
    public class Loading : State
    {
        TimeSpan ElapsedTime;

        public Loading(StateManager Father)
        {
            Manager = Father;

            # region Language

            if (CultureInfo.CurrentCulture.IetfLanguageTag == "pt-BR")
            {
                Preferences.CurrentLanguage = GameLanguage.Portugues;
            }
            else
            {
                Preferences.CurrentLanguage = GameLanguage.English;
            }

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Loading");
                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Carregando");

                    break;
            }

            # endregion

            ElapsedTime = new TimeSpan();
        }

        public override void Update(GameTime gameTime)
        {
            # region ESC

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                Manager.Game.Exit();
            }

            # endregion

            ElapsedTime += gameTime.ElapsedGameTime;

            if (ElapsedTime.TotalMilliseconds > 2000)
            {
                Manager.GoToMenu();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}