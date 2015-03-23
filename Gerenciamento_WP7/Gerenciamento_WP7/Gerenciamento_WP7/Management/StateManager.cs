using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using Microsoft.Devices;

using Gerenciamento_WP7.BaseClasses;
using Gerenciamento_WP7.DataBase;

namespace Gerenciamento_WP7.Management
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StateManager : DrawableGameComponent
    {
        GameStates CurrentState;

        GameStates NextState;
        
        GameStates SecondaryState;
        
        Dictionary<GameStates, State> States;
        
        bool IsOnTransition;
        
        float AlphaChannel;
        
        Texture2D TransitionImage;
        
        public SpriteBatch spriteBatch;

        public SpriteFont Font;

        public Song MenuSong , GamePlaySong;

        public bool Touched , VerticalDrag , HorizontalDrag;

        public GestureSample CurrentGesture;
        
        public Point TouchedPlace;

        public VibrateController Vibrate;



        public static bool HasAudioControl , HasVibrationControl;

        public static string PathForRecords;

        public static GameLanguage CurrentLanguage;

        public static float SongVolume, EffectVolume;

        public StateManager(Game game) : base(game)
        {
            // TODO: Construct any child components here

            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            
            PathForRecords = "Records.xml";

            SongVolume = 5f;
            EffectVolume = 5;

            Initialize();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            CurrentState = GameStates.None;
            
            NextState = GameStates.None;
            
            SecondaryState = GameStates.None;
            
            IsOnTransition = false;
            
            States = new Dictionary<GameStates, State>();
            
            AlphaChannel = 1.0f;

            TransitionImage = Game.Content.Load<Texture2D>("TransitionImage");

            Font = Game.Content.Load<SpriteFont>("GameFont");

            MenuSong = Game.Content.Load<Song>("Audio/Songs/Menu");
            GamePlaySong = Game.Content.Load<Song>("Audio/Songs/GamePlay");
            
            Touched = false;
            HorizontalDrag = false;
            VerticalDrag = false;

            Vibrate = VibrateController.Default;

            GoToLoad();

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            while (TouchPanel.IsGestureAvailable)
            {
                CurrentGesture = TouchPanel.ReadGesture();

                if (CurrentGesture.GestureType == GestureType.Tap)
                {
                    Touched = true;
                }
                else if (CurrentGesture.GestureType == GestureType.HorizontalDrag)
                {
                    HorizontalDrag = true;
                }
                else if (CurrentGesture.GestureType == GestureType.VerticalDrag)
	            {
                    VerticalDrag = true;
	            }

                TouchedPlace = new Point((int)CurrentGesture.Position.X,
                    (int)CurrentGesture.Position.Y);
            }

            // Caso não tenha transição 
            if (!IsOnTransition)
            {
                // Atualiza a cena atual
                States[CurrentState].Update(gameTime);
            }
            else
            {
                // Se tem transição e a cena atual é diferente de vazia
                if (CurrentState != GameStates.None)
                {
                    // Aumenta o alpha conforme o tempo, ou seja,
                    // aumenta o preto na tela (fade-out)
                    // a imagem começa a desaparecer
                    AlphaChannel += (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.003f;
                    if (AlphaChannel >= 1.0f)
                    {
                        // Mantem o alpha entre 0 e 1
                        MathHelper.Clamp(AlphaChannel, 0, 1);
                        // Remove a cena anterior (atual até o momento)
                        if (SecondaryState == GameStates.None)
                        {
                            States.Remove(CurrentState);
                        }
                        // Cena atual recebe estado vazio
                        CurrentState = GameStates.None;
                    }
                }
                else
                {
                    if (AlphaChannel > 0.0f)
                    {
                        // Diminui o alpha conforme o tempo, ou seja,
                        // diminui o preto na tela (fade-in)
                        // a imagem começa a aparecer
                        AlphaChannel -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.003f;
                    }
                    else
                    {
                        // Mantem o alpha entre 0 e 1
                        MathHelper.Clamp(AlphaChannel, 0, 1);
                        // Cena atual recebe o próximo estado
                        CurrentState = NextState;
                        // Acaba a transição
                        IsOnTransition = false;
                    }
                }
            }

            Touched = false;
            HorizontalDrag = false;
            VerticalDrag = false;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Se tem um estado secundário desenhe ele por baixo do atual
            if (SecondaryState != GameStates.None)
            {
                States[SecondaryState].Draw(gameTime);
            }

            // Se cena atual for diferente de vazio
            // desenha a cena atual
            // senão desenha a próxima cena
            if (CurrentState != GameStates.None)
            {
                States[CurrentState].Draw(gameTime);
            }
            else
            {
                States[NextState].Draw(gameTime);
            }

            // Se existir transição aplica o efeito de fade-in e fade-out
            if (IsOnTransition)
            {
                TransitionEffect();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        
        private void TransitionEffect()
        {
            // Desenha uma imagem preta com o alpha definido
            spriteBatch.Draw(TransitionImage,
            new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width,
            Game.GraphicsDevice.Viewport.Height),new Color(0, 0, 0, AlphaChannel));
        }

        # region StateChanges

        public void GoToLoad()
        {
            NextState = GameStates.Loading;
            IsOnTransition = true;
            States.Add(GameStates.Loading, new Loading(this));
        }

        public void GoToMenu()
        {
            NextState = GameStates.Menu;
            IsOnTransition = true;
            SecondaryState = GameStates.None;
            States.Add(GameStates.Menu, new Menu(this));
        }

        public void GoToGamePlay()
        {
            NextState = GameStates.GamePlay;
            IsOnTransition = true;
            SecondaryState = GameStates.None;
            States.Remove(GameStates.GamePlay);
            States.Add(GameStates.GamePlay, new GamePlay(this));
        }

        public void RestartGamePlay()
        {
            NextState = GameStates.GamePlay;
            IsOnTransition = true;
            SecondaryState = GameStates.None;
            States[GameStates.GamePlay].Restart();
        }

        public void ResumeGamePlay()
        {
            NextState = GameStates.GamePlay;
            IsOnTransition = true;
            SecondaryState = GameStates.None;
        }

        public void GoToDirections()
        {
            NextState = GameStates.Directions;
            IsOnTransition = true;
            States.Add(GameStates.Directions, new Directions(this));
        }

        public void GoToCredits()
        {
            NextState = GameStates.Credits;
            IsOnTransition = true;
            States.Add(GameStates.Credits, new Credits(this));
        }

        public void GoToRanking()
        {
            NextState = GameStates.Ranking;
            IsOnTransition = true;
            States.Add(GameStates.Ranking, new Ranking(this));
        }

        public void GoToSettings()
        {
            NextState = GameStates.Settings;
            IsOnTransition = true;
            States.Add(GameStates.Settings, new Settings(this));
        }

        public void GoToWon(float Score)
        {
            NextState = GameStates.Won;
            IsOnTransition = true;
            States.Add(GameStates.Won, new Won(this, Score));
        }

        public void GoToLost(float Score)
        {
            NextState = GameStates.Lost;
            IsOnTransition = true;
            States.Add(GameStates.Lost, new Lost(this));
        }

        public void GoToSaveRecord(float PlayerRecord)
        {
            NextState = GameStates.SaveRecord;
            IsOnTransition = true;
            States.Add(GameStates.SaveRecord, new SaveRecord(this, PlayerRecord));
        }

        public void GoToPause()
        {
            NextState = GameStates.Pause;
            IsOnTransition = true;
            SecondaryState = GameStates.GamePlay;
            States.Add(GameStates.Pause, new Pause(this));
        }

        # endregion
    }
}