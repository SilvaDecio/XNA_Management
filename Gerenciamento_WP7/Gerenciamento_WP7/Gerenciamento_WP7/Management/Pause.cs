using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    class Pause : State
    {
        Button ResumeButton, RestartButton, MenuButton , VibrationButton;

        Slider SongSlider, SoundEffectSlider;

        public Pause(StateManager Father)
        {
            Manager = Father;

            # region Sliders

            SongSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                MediaPlayer.Volume * 10, new Vector2(), new Vector2(200, 350), 200, string.Empty);

            SongSlider.Position = new Vector2(200 + (SongSlider.Value * 10), 285);
            SongSlider.Rectangle = new Rectangle((int)SongSlider.Position.X,
                (int)SongSlider.Position.Y, SongSlider.Image.Width,
                SongSlider.Image.Height);

            SoundEffectSlider = new Slider(Manager.Game.Content.Load<Texture2D>("Slider"), Manager.Game.Content.Load<Texture2D>("SliderBar"),
                SoundEffect.MasterVolume * 10, new Vector2(), new Vector2(400, 350), 400, string.Empty);

            SoundEffectSlider.Position = new Vector2(400 + (SoundEffectSlider.Value * 10), 285);
            SoundEffectSlider.Rectangle = new Rectangle((int)SoundEffectSlider.Position.X,
                (int)SoundEffectSlider.Position.Y, SoundEffectSlider.Image.Width,
                SoundEffectSlider.Image.Height);

            # endregion

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Pause");

                    # region Buttons

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Resume"), new Vector2(0, 100));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Restart"), new Vector2(100, 100));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/Menu"), new Vector2(200, 100));

                    if (StateManager.HasVibrationControl)
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/VibrationOn"), new Vector2(300, 100));
                    }
                    else
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Pause/VibrationOff"), new Vector2(300, 100));
                    }

                    # endregion

                    SongSlider.Name = "Musics";

                    SoundEffectSlider.Name = "Effects";

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Pausa");

                    # region Botões

                    ResumeButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Continuar"), new Vector2(0, 100));
                    RestartButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Reiniciar"), new Vector2(100, 100));
                    MenuButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/Menu"), new Vector2(200, 100));

                    if (StateManager.HasVibrationControl)
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/ComVibracao"), new Vector2(300, 100));
                    }
                    else
                    {
                        VibrationButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Pausa/SemVibracao"), new Vector2(300, 100));
                    }

                    # endregion

                    SongSlider.Name = "Musicas";

                    SoundEffectSlider.Name = "Efeitos";

                    break;
            }

            # endregion

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Pause();
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (StateManager.HasAudioControl)
                {
                    MediaPlayer.Resume();
                }

                Manager.ResumeGamePlay();
            }

            # endregion

            # region Buttons

            if (Manager.Touched)
            {
                if (ResumeButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Resume();
                    }

                    Manager.ResumeGamePlay();
                }

                else if (RestartButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.RestartGamePlay();
                }

                else if (MenuButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();
                    }

                    Manager.GoToMenu();
                }

                else if (VibrationButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    switch (StateManager.CurrentLanguage)
                    {
                        case GameLanguage.English:

                            if (StateManager.HasVibrationControl)
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("English/Buttons/Pause/VibrationOff");

                                StateManager.HasVibrationControl = false;
                            }
                            else
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("English/Buttons/Pause/VibrationOn");

                                StateManager.HasVibrationControl = true;

                                Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 400));
                            }

                            break;

                        case GameLanguage.Portugues:

                            if (StateManager.HasVibrationControl)
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("Portugues/Botoes/Pausa/SemVibracao");

                                StateManager.HasVibrationControl = false;
                            }
                            else
                            {
                                VibrationButton.Image = Manager.Game.Content.Load<Texture2D>
                                    ("Portugues/Botoes/Pausa/ComVibracao");

                                StateManager.HasVibrationControl = true;

                                Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 400));
                            }

                            break;
                    }
                }
            }

            # endregion

            # region Sliders

            if (Manager.HorizontalDrag)
            {
                Rectangle A = new Rectangle(SongSlider.Rectangle.X - SongSlider.Rectangle.Width / 2,
                    SongSlider.Rectangle.Y, SongSlider.Rectangle.Width * 2, SongSlider.Rectangle.Height);

                Rectangle B = new Rectangle(SoundEffectSlider.Rectangle.X - SoundEffectSlider.Rectangle.Width / 2,
                    SoundEffectSlider.Rectangle.Y, SoundEffectSlider.Rectangle.Width * 2, SoundEffectSlider.Rectangle.Height);

                if (A.Contains(Manager.TouchedPlace))
                {
                    SongSlider.Position.X += Manager.CurrentGesture.Delta.X;

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

                    StateManager.SongVolume = SongSlider.Value;
                }
                else if (B.Contains(Manager.TouchedPlace))
                {
                    SoundEffectSlider.Position.X += Manager.CurrentGesture.Delta.X;

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

                    StateManager.EffectVolume = SoundEffectSlider.Value;
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
            VibrationButton.Draw(Manager.spriteBatch);

            SongSlider.Draw(Manager.spriteBatch, Manager.Font);
            SoundEffectSlider.Draw(Manager.spriteBatch, Manager.Font);

            base.Draw(gameTime);
        }
    }
}