using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class Directions : State
    {
        Button BackButton;

        public Directions(StateManager Father)
        {
            Manager = Father;

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Directions");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Back"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

                    break;

                case GameLanguage.Portugues:
                    
                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Instrucoes");

                    BackButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Voltar"),
                        new Vector2(15, Manager.Game.GraphicsDevice.Viewport.Height - 85));

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
                Manager.GoToMenu();
            }

            # endregion

            # region Back Button

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

            base.Draw(gameTime);
        }
    }
}