using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using Gerenciamento_WP7.Management;

namespace Gerenciamento_WP7
{
    public enum GameLanguage
    {
        English, Portugues
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag | GestureType.VerticalDrag;

            graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Services.AddService(typeof(SpriteBatch), spriteBatch);

            Components.Add(new StateManager(this));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static bool VerifyPixelCollision(Rectangle Rectangle1, Color[] Data1,
            Rectangle Rectangle2, Color[] Data2)
        {
            // Encontra os limites do retangulo de interseção
            int Top = Math.Max(Rectangle1.Top, Rectangle2.Top);
            int Bottom = Math.Min(Rectangle1.Bottom, Rectangle2.Bottom);
            int Left = Math.Max(Rectangle1.Left, Rectangle2.Left);
            int Right = Math.Min(Rectangle1.Right, Rectangle2.Right);

            // Verifica todos os pontos dentro do limite de intereseção
            for (int y = Top; y < Bottom; y++)
            {
                for (int x = Left; x < Right; x++)
                {
                    // Verifica a cor de ambos os pixels neste momento
                    Color Color1 = Data1[(x - Rectangle1.Left) +
                                         (y - Rectangle1.Top) * Rectangle1.Width];
                    Color Color2 = Data2[(x - Rectangle2.Left) +
                                         (y - Rectangle2.Top) * Rectangle2.Width];

                    // Se ambos os píxels não são completamente diferentes
                    if (Color1.A != 0 && Color2.A != 0)
                    {
                        // Um cruzamento de pixel foi encontrado
                        return true;
                    }
                }
            }
            // Não foi encontrado cruzamento entre os pixels
            return false;
        }
    }
}