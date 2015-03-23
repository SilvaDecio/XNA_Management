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
    class Menu : State
    {
        Button PlayButton , DirectionsButton , CreditsButton , RankingButton,
            ShareButton , ExitButton;

        Slider SongSlider, SoundEffectSlider;

        bool Drag;

        float XImpulse;

        Preferences LocalPreferences;

        public Menu(StateManager Father)
        {
            Manager = Father;

            LocalPreferences = Preferences.GetPreferences();

            # region Audio

            MediaPlayer.Volume = 0f;

            MediaPlayer.Volume = LocalPreferences.SongVolume / 10;

            SoundEffect.MasterVolume = LocalPreferences.EffectVolume / 10;

            # endregion

            # region Sliders

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
                        ("English/BackGroundImages/Menu");

                    # region Buttons

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Play"),new Vector2(100,200));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Directions"), new Vector2(200, 200));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Credits"), new Vector2(300, 200));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Ranking"), new Vector2(400, 200));
                    ShareButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Share"), new Vector2(600, 200));
                    ExitButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("English/Buttons/Menu/Exit"), new Vector2(700, 200));
                    
                    # endregion    

                    SongSlider.Name = "Musics";
                    SoundEffectSlider.Name = "Effects";

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Menu");

                    # region Botões

                    PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Jogar"), new Vector2(100, 200));
                    DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Instrucoes"), new Vector2(200, 200));
                    CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Creditos"), new Vector2(300, 200));
                    RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Recordes"), new Vector2(400, 200));
                    ShareButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Compartilhar"), new Vector2(600, 200));
                    ExitButton = new Button(Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Botoes/Menu/Sair"), new Vector2(700, 200));

                    # endregion

                    SongSlider.Name = "Musicas";
                    SoundEffectSlider.Name = "Efeitos";

                    break;
            }

            # endregion

            Drag = false;

            XImpulse = 0f;

            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(Manager.MenuMusic);
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region ESC

            if (Manager.CurrentKeyBoard.IsKeyDown(Keys.Escape) &&
                Manager.OldKeyboard.IsKeyUp(Keys.Escape))
            {
                Preferences.SavePreferences(LocalPreferences);

                Manager.Game.Exit();
            }

            # endregion

            # region Buttons

            if (Manager.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                if (Manager.OldMouse.LeftButton == ButtonState.Released)
                {
                    if (PlayButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        MediaPlayer.Stop();
                        Manager.GoToGamePlay();
                    }
                    
                    else if (DirectionsButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        Manager.GoToDirections();
                    }
                    
                    else if (CreditsButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        Manager.GoToCredits();
                    }

                    else if (RankingButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        Manager.GoToRanking();
                    }

                    //else if (ShareButton.Rectangle.Contains(Manager.MousePointer))
                    //{
                        
                    //}

                    else if (ExitButton.Rectangle.Contains(Manager.MousePointer))
                    {
                        Preferences.SavePreferences(LocalPreferences);

                        Manager.Game.Exit();
                    }
                }
            }
            else
            {
                PlayButton.Update(Manager.MousePointer);
                DirectionsButton.Update(Manager.MousePointer);
                CreditsButton.Update(Manager.MousePointer);
                RankingButton.Update(Manager.MousePointer);
                //ShareButton.Update(Manager.MousePointer);
                ExitButton.Update(Manager.MousePointer);
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
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            PlayButton.Draw(Manager.spriteBatch);
            CreditsButton.Draw(Manager.spriteBatch);
            DirectionsButton.Draw(Manager.spriteBatch);
            RankingButton.Draw(Manager.spriteBatch);
            //ShareButton.Draw(Manager.spriteBatch);
            ExitButton.Draw(Manager.spriteBatch);

            SongSlider.Draw(Manager.spriteBatch, Manager.Font);
            SoundEffectSlider.Draw(Manager.spriteBatch, Manager.Font);

            base.Draw(gameTime);
        }
    }
}