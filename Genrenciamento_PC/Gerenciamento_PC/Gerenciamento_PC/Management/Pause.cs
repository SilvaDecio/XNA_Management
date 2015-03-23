using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using Gerenciamento_PC.BaseClasses;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.Management
{
    class Pause : State
    {
        Button ResumeButton, RestartButton, MenuButton;

        Slider SongSlider, SoundEffectSlider;

        bool Drag;

        float XImpulse;

        Preferences LocalPreferences;

        public Pause(StateManager Father)
        {
            Manager = Father;

            #region Sliders

            SongSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                MediaPlayer.Volume * 10, new Vector2(), new Vector2(200, 435), 200, string.Empty);

            SongSlider.Position = new Vector2(200 + (SongSlider.Value * 10), 385);
            SongSlider.Rectangle = new Rectangle((int)SongSlider.Position.X,
                (int)SongSlider.Position.Y, SongSlider.Image.Width,
                SongSlider.Image.Height);


            SoundEffectSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                SoundEffect.MasterVolume * 10, new Vector2(), new Vector2(400, 435), 400, string.Empty);

            SoundEffectSlider.Position = new Vector2(400 + (SoundEffectSlider.Value * 10), 385);
            SoundEffectSlider.Rectangle = new Rectangle((int)SoundEffectSlider.Position.X,
                (int)SoundEffectSlider.Position.Y, SoundEffectSlider.Image.Width,
                SoundEffectSlider.Image.Height);

            # endregion

            # region Language

            switch (Preferences.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Pause");
            
                    # region Buttons

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Resume"), new Vector2(100, 200));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Restart"), new Vector2(200, 200));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Menu"), new Vector2(300, 200));

                    # endregion

                    SongSlider.Name = "Musics";
                    SoundEffectSlider.Name = "Effects";

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Pausa");
            
                    # region Botões

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Continuar"), new Vector2(100, 200));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Reiniciar"), new Vector2(200, 200));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Menu"), new Vector2(300, 200));

                    # endregion

                    SongSlider.Name = "Musicas";
                    SoundEffectSlider.Name = "Efeitos";

                    break;
            }

            # endregion

            Drag = false;

            XImpulse = 0f;

            LocalPreferences = Preferences.GetPreferences();

            MediaPlayer.Pause();
        }

        public override void Update(GameTime gameTime)
        {
            # region ESC

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape)) 
            {
                Preferences.SavePreferences(LocalPreferences);

                MediaPlayer.Resume();
                Manager.ResumeGamePlay();
            }

            # endregion

            # region Buttons

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                if (Manager.OldMouse.LeftButton == ButtonState.Released)
                {
                    if (ResumeButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        MediaPlayer.Resume();
                        Manager.ResumeGamePlay();
                    }

                    else if (RestartButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        Manager.RestartGamePlay();
                    }

                    else if (MenuButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        MediaPlayer.Stop();
                        Manager.GoToMenu();
                    }
                }
            }
            else
            {
                ResumeButton.Update(Manager.MousePointer);
                RestartButton.Update(Manager.MousePointer);
                MenuButton.Update(Manager.MousePointer);
            }

            # endregion         

            # region Sliders

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                Drag = true;
                XImpulse = Manager.CurrentMouse.X - Manager.OldMouse.X;
            }
            else
            {
                Drag = false;
                XImpulse = 0f;
            }

            if (Drag)
            {
                if (SongSlider.Rectangle.Contains(Manager.MousePointer))
                {
                    SongSlider.Position.X += XImpulse;

                    # region SongSlider Value

                    if (SongSlider.Position.X <= SongSlider.MinimumPosition)
                    {
                        SongSlider.Value = 0.0f;
                    }
                    else if (SongSlider.Position.X >= SongSlider.MaximumPosition)
                    {
                        SongSlider.Value = 10.0f;
                    }
                    else
                    {
                        SongSlider.Value = ((SongSlider.Position.X - SongSlider.MinimumPosition) / 10);
                    }

                    # endregion

                    MediaPlayer.Volume = (SongSlider.Value / 10);

                    LocalPreferences.SongVolume = SongSlider.Value;
                }
                else if (SoundEffectSlider.Rectangle.Contains(Manager.MousePointer))
                {
                    SoundEffectSlider.Position.X += XImpulse;

                    # region SoundEffectSlider Value

                    if (SoundEffectSlider.Position.X <= SoundEffectSlider.MinimumPosition)
                    {
                        SoundEffectSlider.Value = 0.0f;
                    }
                    else if (SoundEffectSlider.Position.X >= SoundEffectSlider.MaximumPosition)
                    {
                        SoundEffectSlider.Value = 10.0f;
                    }
                    else
                    {
                        SoundEffectSlider.Value = ((SoundEffectSlider.Position.X - SoundEffectSlider.MinimumPosition) / 10);
                    }

                    # endregion

                    SoundEffect.MasterVolume = (SoundEffectSlider.Value / 10);

                    LocalPreferences.EffectVolume = SoundEffectSlider.Value;
                }

                SongSlider.Update(gameTime);
                SoundEffectSlider.Update(gameTime);
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero ,Color.White);

            ResumeButton.Draw(Manager.spriteBatch);
            RestartButton.Draw(Manager.spriteBatch);
            MenuButton.Draw(Manager.spriteBatch);

            SongSlider.Draw(Manager.spriteBatch, Manager.Font);
            SoundEffectSlider.Draw(Manager.spriteBatch, Manager.Font);

            base.Draw(gameTime);
        }
    }
}