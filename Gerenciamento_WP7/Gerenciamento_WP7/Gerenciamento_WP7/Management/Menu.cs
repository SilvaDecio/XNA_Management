using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.GamerServices;

using System.IO.IsolatedStorage;
using System.Xml.Serialization;

using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.Management;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class Menu : State
    {
        Button PlayButton, DirectionsButton, CreditsButton, RankingButton,
            SettingsButton , ShareButton;

        public Menu(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Menu");

                    # region Buttons

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Play"),new Vector2(0,200));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Directions"),new Vector2(100,200));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Credits"),new Vector2(200, 200));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Ranking"),new Vector2(300, 200));
                    SettingsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Settings"),new Vector2(400,200));
                    ShareButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Share"),new Vector2(500, 200));

                    # endregion

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Menu");

                    # region Botões

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Jogar"),new Vector2(0,200));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Instrucoes"), new Vector2(100, 200));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Creditos"), new Vector2(200, 200));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Recordes"), new Vector2(300, 200));
                    SettingsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Configuracoes"), new Vector2(400, 200));
                    ShareButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Compartilhar"), new Vector2(500, 200));

                    # endregion

                    break;
            }

            # endregion

            if (StateManager.HasAudioControl)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(Manager.MenuSong);
                }
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

            #region Buttons

            if (Manager.Touched)
            {
                if (PlayButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();    
                    }

                    Manager.GoToGamePlay();
                }

                else if (DirectionsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToDirections();
                }

                else if (CreditsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToCredits();
                }

                else if (RankingButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToRanking();
                }

                else if (SettingsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToSettings();
                }

                else if (ShareButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (DeviceNetworkInformation.IsNetworkAvailable)
                    {
                        ShareLinkTask Launcher = new ShareLinkTask();
                        
                        switch (StateManager.CurrentLanguage)
                        {
                            case GameLanguage.English:

                                Launcher.Title = "Gerenciamento_WP7 Official Page";
                                Launcher.Message = "I played and recommend !";

                                break;

                            case GameLanguage.Portugues:

                                Launcher.Title = "Gerenciamento_WP7 - Página Oficial";
                                Launcher.Message = "Joguei e Recomendo !";
                                
                                break;
                        }

                        Launcher.LinkUri = new Uri("http://www.facebook.com/Decio7Silva", UriKind.Absolute);
                        Launcher.Show();
                    }
                    else
                    {
                        switch (StateManager.CurrentLanguage)
                        {
                            case GameLanguage.English:

                                Guide.BeginShowMessageBox("Error", "No Internet Connection",
                                    new string[] { "Ok" }, 0, MessageBoxIcon.Error,
                                    new AsyncCallback(OnEndShowMessageBox), null);
                                
                                break;

                            case GameLanguage.Portugues:
                                
                                Guide.BeginShowMessageBox("Erro", "Sem Conexão com a Internet",
                                    new string[] { "Ok" }, 0, MessageBoxIcon.Error,
                                    new AsyncCallback(OnEndShowMessageBox), null);
                                
                                break;
                        }
                    }
                }
            }

            # endregion

            base.Update(gameTime);
        }

        private void OnEndShowMessageBox(IAsyncResult result) {}

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            PlayButton.Draw(Manager.spriteBatch);
            CreditsButton.Draw(Manager.spriteBatch);
            DirectionsButton.Draw(Manager.spriteBatch);
            RankingButton.Draw(Manager.spriteBatch);
            SettingsButton.Draw(Manager.spriteBatch);
            ShareButton.Draw(Manager.spriteBatch);

            base.Draw(gameTime);
        }
    }
}