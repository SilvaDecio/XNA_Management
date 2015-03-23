using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class Won : State
    {
        bool IsRecordBroken;
        
        float PlayerScore;

        List<Player> Players;

        public Won(StateManager Father , float Score)
        {
            Manager = Father;

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Won");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Venceu");

                    break;
            }

            # endregion

            PlayerScore = Score;

            IsRecordBroken = false;

            Players = Player.Load().OrderByDescending(x => x.Record).ToList();

            # region Is Record Broken ?

            switch (Players.Count)
            {
                case 0 :

                    IsRecordBroken = true;
                    
                    break;

                case 1 :

                    IsRecordBroken = true;

                    break;

                case 2 :

                    IsRecordBroken = true;

                    break;

                default:

                    for (int i = 0; i < 3; i++)
                    {
                        if (PlayerScore > Players[i].Record)
                        {
                            IsRecordBroken = true;
                            break;
                        }
                    }

                    break;
            }

            # endregion
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

            # region Touch

            if (Manager.Touched)
            {
                if (IsRecordBroken)
                {
                    Manager.GoToSaveRecord(PlayerScore);
                }
                else
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                    }

                    Manager.GoToMenu();
                }
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage , Vector2.Zero , Color.White);
            
            base.Draw(gameTime);
        }
    }
}