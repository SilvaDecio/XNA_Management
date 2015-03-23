using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class GamePlay : State
    {
        float ElapsedTime;

        public GamePlay(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/GamePlay");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Jogando");

                    break;
            }

            # endregion

            Restart();
        }

        public override void Restart()
        {
            ElapsedTime = 0f;

            MediaPlayer.Play(Manager.GamePlayMusic);

            base.Restart();
        }

        public override void Update(GameTime gameTime)
        {
            # region ESC

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                Manager.GoToPause();
            }

            # endregion

            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            # region Won

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Enter) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Enter))
            {
                Manager.GoToWon(ElapsedTime);
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage , Vector2.Zero , Color.White);

            Manager.spriteBatch.DrawString(Manager.Font, ElapsedTime.ToString(),
                new Vector2(300, 200), Color.White);

            base.Draw(gameTime);
        }
    }
}