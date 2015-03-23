using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class SaveRecord : State
    {
        float PlayerRecord;

        Player NewPlayer;

        List<Player> Players;

        bool OkTouched , CancelTouched , Finished;

        string TextEntered;

        public SaveRecord(StateManager Father , float PLAYERRECORD)
        {
            Manager = Father;
            
            PlayerRecord = PLAYERRECORD;

            Players = Player.Load();

            Finished = false;
            OkTouched = false;
            CancelTouched = false;

            TextEntered = string.Empty;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/SaveRecord");

                    Guide.BeginShowKeyboardInput(PlayerIndex.One,
                        "Your Score is " + PlayerRecord.ToString(),
                        "Insert Your Name", string.Empty,
                        new AsyncCallback(OnEndShowKeyboardInput), null);

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/SalvarRecorde");

                    Guide.BeginShowKeyboardInput(PlayerIndex.One,
                        "Você fez " + PlayerRecord.ToString() + " pontos !",
                        "Insira Seu Nome", string.Empty,
                        new AsyncCallback(OnEndShowKeyboardInput), null);

                    break;
            }

            # endregion
        }

        private void OnEndShowKeyboardInput(IAsyncResult result)
        {
            if (Guide.EndShowKeyboardInput(result) == string.Empty)
            {
                Finished = false;
                OkTouched = true;
                CancelTouched = false;

                TextEntered = string.Empty;
            }
            
            else if (Guide.EndShowKeyboardInput(result) == null)
            {
                Finished = true;
                CancelTouched = true;
                OkTouched = false;

                TextEntered = null;
            }

            else if (Guide.EndShowKeyboardInput(result).Length > 0)
            {
                Finished = true;
                OkTouched = true;
                CancelTouched = false;

                TextEntered = Guide.EndShowKeyboardInput(result);
            }
        }

        private void OnEndShowMessageBox(IAsyncResult result)
        {
            OkTouched = false;
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (StateManager.HasAudioControl)
                {
                    MediaPlayer.Stop();
                }

                Manager.GoToMenu();
            }

            # endregion

            # region Save

            if (Finished)
            {
                if (OkTouched)
                {
                    NewPlayer = new Player(TextEntered, PlayerRecord);

                    Players.Add(NewPlayer);
                    
                    Player.Save(Players);

                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Manager.MenuSong);
                    }
                    
                    Manager.GoToRanking();
                }
                else if (CancelTouched)
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Manager.MenuSong);
                    }

                    Manager.GoToMenu();
                }
            }
            else
            {
                if (OkTouched)
                {
                    if (!Guide.IsVisible)
                    {
                        switch (StateManager.CurrentLanguage)
                        {
                            case GameLanguage.English:

                                Guide.BeginShowMessageBox("Alert", "You must enter a name !",
                                    new string[]
                                    {
                                        "Ok"
                                    },
                                0, MessageBoxIcon.Alert, new AsyncCallback(OnEndShowMessageBox),
                                null);

                                break;

                            case GameLanguage.Portugues:

                                Guide.BeginShowMessageBox("Alerta", "Você deve inserir um nome !",
                                    new string[]
                                    {
                                        "Ok"
                                    },
                                0, MessageBoxIcon.Alert, new AsyncCallback(OnEndShowMessageBox),
                                null);

                                break;
                        }
                    }
                }
                else
                {
                    if (!Guide.IsVisible)
                    {
                        switch (StateManager.CurrentLanguage)
                        {
                            case GameLanguage.English:

                                Guide.BeginShowKeyboardInput(PlayerIndex.One,
                                    "Your Score is " + PlayerRecord.ToString(),
                                    "Insert Your Name", string.Empty,
                                    new AsyncCallback(OnEndShowKeyboardInput), null);

                                break;

                            case GameLanguage.Portugues:

                                Guide.BeginShowKeyboardInput(PlayerIndex.One,
                                    "Você fez " + PlayerRecord.ToString() + " pontos",
                                    "Insira Seu Nome", string.Empty,
                                    new AsyncCallback(OnEndShowKeyboardInput), null);

                                break;
                        }
                    }
                }
            }

            #endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}