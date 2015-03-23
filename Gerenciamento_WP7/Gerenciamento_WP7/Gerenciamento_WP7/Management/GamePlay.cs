using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class GamePlay : State
    {
        float ElapsedTime;

        public GamePlay(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
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

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Play(Manager.GamePlaySong);
            }

            base.Restart();
        }

        public override void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToPause();
            }

            # endregion
            
            # region Won

            if (Manager.Touched)
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