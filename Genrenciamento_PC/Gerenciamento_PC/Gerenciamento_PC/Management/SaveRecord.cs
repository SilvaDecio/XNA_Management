using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Nuclex.Input;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class SaveRecord : State
    {
        float PlayerRecord;

        Player NewPlayer;
        
        PlayerManager LocalPlayerManager;
        
        Vector2 NamePosition;

        Button BackButton;

        public SaveRecord(StateManager Father , float PLAYERRECORD)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/SaveRecord");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Back"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;

                case GameLanguage.Portugues:
                    
                     BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/SalvarRecorde");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Voltar"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;
            }

            # endregion

            PlayerRecord = PLAYERRECORD;

            NamePosition = new Vector2(350, 300);

            Manager.TextEntered = string.Empty;

            LocalPlayerManager = PlayerManager.GetPlayers();
            
            if (LocalPlayerManager.Players == null)
            {
                LocalPlayerManager.Players = new List<Player>();
            }
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
                if (Manager.TextEntered.Length > 1)
                {
                    NewPlayer = new Player(Manager.TextEntered, PlayerRecord);
                    LocalPlayerManager.Players.Add(NewPlayer);
                    PlayerManager.SavePlayers(LocalPlayerManager);
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Manager.MenuMusic);
                    Manager.GoToRanking();
                }
                else
                {
                    Manager.TextEntered = Manager.TextEntered.Substring(0, Manager.TextEntered.Length - 1);
                }
            }

            # endregion

            # region Back Button

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                if (Manager.OldMouse.LeftButton == ButtonState.Released)
                {
                    if (BackButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        MediaPlayer.Stop();
                        Manager.GoToMenu();
                    }
                }
            }
            else
            {
                BackButton.Update(Manager.MousePointer);
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            Manager.spriteBatch.DrawString(Manager.Font, Manager.TextEntered, NamePosition,
                Color.White);

            BackButton.Draw(Manager.spriteBatch);

            base.Draw(gameTime);
        }
    }
}