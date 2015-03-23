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
    class Won : State
    {
        bool IsRecordBroken;

        float PlayerPontuation;
        
        PlayerManager LocalPlayerManager;

        Button BackButton;

        public Won(StateManager Father , float PLAYERPONTUATION)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Won");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Back"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Venceu");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Voltar"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;
            }

            # endregion

            PlayerPontuation = PLAYERPONTUATION;

            IsRecordBroken = false;

            LocalPlayerManager = PlayerManager.GetPlayers();

            # region Is Record Broken ?

            if (LocalPlayerManager.Players == null)
            {
                IsRecordBroken = true;
            }
            else
            {
                switch (LocalPlayerManager.Players.Count)
                {
                    case 0:

                        IsRecordBroken = true;

                        break;

                    case 1:

                        IsRecordBroken = true;

                        break;

                    case 2:

                        IsRecordBroken = true;

                        break;

                    default:

                        for (int i = LocalPlayerManager.Players.Count - 1; i > -1; i--)
                        {
                            if (PlayerPontuation > LocalPlayerManager.Players[i].Record &&
                                i < 2)
                            {
                                IsRecordBroken = true;
                                break;
                            }
                        }

                        break;
                }
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
                if (IsRecordBroken)
                {
                    Manager.GoToSaveRecord(PlayerPontuation);
                }
                else
                {
                    MediaPlayer.Stop();
                    Manager.GoToMenu();
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
            Manager.spriteBatch.Draw(BackGroundImage , Vector2.Zero , Color.White);

            BackButton.Draw(Manager.spriteBatch);

            base.Draw(gameTime);
        }
    }
}