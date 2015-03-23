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

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

using System.Globalization;

namespace Gerenciamento_WP7.Management
{
    public class Loading : State
    {
        TimeSpan Time;

        public Loading(StateManager Father)
        {
            Manager = Father;

            # region Language

            if (CultureInfo.CurrentCulture.Name == "pt-BR")
            {
                StateManager.CurrentLanguage = GameLanguage.Portugues;
            }
            else
            {
                StateManager.CurrentLanguage = GameLanguage.English;
            }

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Loading");

                    if (MediaPlayer.GameHasControl)
                    {
                        StateManager.HasAudioControl = true;
                    }
                    else
                    {
                        Guide.BeginShowMessageBox("Warning", "Want to stop the Music ?",
                            new string[]
                        {
                            "Yes","No"
                        },
                            0, MessageBoxIcon.Warning, new AsyncCallback(OnEndShowMessageBox), null);
                    }

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Carregando");

                    if (MediaPlayer.GameHasControl)
                    {
                        StateManager.HasAudioControl = true;
                    }
                    else
                    {
                        Guide.BeginShowMessageBox("Aviso", "Deseja interromper a Música ?",
                            new string[]
                        {
                            "Sim","Não"
                        },
                            0, MessageBoxIcon.Warning, new AsyncCallback(OnEndShowMessageBox), null);
                    }

                    break;
            }

            # endregion

            Time = new TimeSpan();

            Player.Creating();

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.IsShuffled = false;
                MediaPlayer.IsMuted = false;
                MediaPlayer.IsRepeating = true;

                SoundEffect.MasterVolume = 0.5f;
            }

            StateManager.HasVibrationControl = true;
        }

        private void OnEndShowMessageBox(IAsyncResult result)
        {
            int Choice = (int)Guide.EndShowMessageBox(result);

            if (Choice == 0)
            {
                StateManager.HasAudioControl = true;

                MediaPlayer.Stop();
            }
            else if (Choice == 1)
            {
                StateManager.HasAudioControl = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.Game.Exit();
            }

            # endregion

            Time += gameTime.ElapsedGameTime;
            
            if (Time.TotalMilliseconds >= 2000)
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