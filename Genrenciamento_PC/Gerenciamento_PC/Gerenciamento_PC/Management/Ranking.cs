using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class Ranking : State
    {   
        Vector2 Name0Position, Name1Position, Name2Position ,
            Record0Position, Record1Position, Record2Position;
        
        PlayerManager LocalManager;

        Button BackButton;

        public Ranking(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Ranking");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Back"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;

                case GameLanguage.Portugues:
                    
                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Recordes");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Voltar"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;
            }

            # endregion

            # region Positions

            Name0Position = new Vector2(100,100);
            Record0Position = new Vector2(300,100);
            Name1Position = new Vector2(100,300);
            Record1Position = new Vector2(300,300);
            Name2Position = new Vector2(100,500);
            Record2Position = new Vector2(300,500);

            # endregion

            LocalManager = PlayerManager.GetPlayers();

            if (LocalManager.Players != null)
            {
                LocalManager.Players = LocalManager.Players.OrderByDescending(
                    x => x.Record).ToList();
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region ESC

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                Manager.GoToMenu();
            }

            # endregion

            # region BackButton

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                if (Manager.OldMouse.LeftButton == ButtonState.Released)
                {
                    if (BackButton.Rectangle.Contains(Manager.MousePointer))
                    {
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

            BackButton.Draw(Manager.spriteBatch);

            # region Records

            if (LocalManager.Players != null)
            {
                if (LocalManager.Players.Count == 1)
                {
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[0].Name,
                        Name0Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[0].Record.ToString(),
                    Record0Position, Color.White);
                }
                else if (LocalManager.Players.Count == 2)
                {
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[0].Name,
                        Name0Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font, LocalManager.Players[0].Record.ToString(),
                    Record0Position, Color.White);

                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[1].Name,
                    Name1Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[1].Record.ToString(),
                    Record1Position, Color.White);
                }
                else if (LocalManager.Players.Count >= 3)
                {
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[0].Name,
                        Name0Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[0].Record.ToString(),
                    Record0Position, Color.White);

                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[1].Name,
                    Name1Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[1].Record.ToString(),
                    Record1Position, Color.White);

                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[2].Name,
                    Name2Position, Color.White);
                    Manager.spriteBatch.DrawString(Manager.Font,
                        LocalManager.Players[2].Record.ToString(),
                    Record2Position, Color.White);
                }
            }

            # endregion

            base.Draw(gameTime);
        }
    }
}