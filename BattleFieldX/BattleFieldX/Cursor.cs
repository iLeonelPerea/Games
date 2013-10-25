
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BattleFieldX
{
    class CursorX : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D figura;
        private Vector2 pRectangulo;
        private Rectangle rRectangulo;
        private Rectangle pantalla;
        protected TimeSpan tiempo = TimeSpan.Zero;
        public CursorX(Game game, ref Texture2D figura)
            : base(game)
        {

            this.figura = figura;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            Initialize();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public override void Initialize()
        {

            pRectangulo = new Vector2(80, 150);
            rRectangulo = new Rectangle(0, 0, 135, 100);
            //Dimensiones de la pantalla
            //  pantalla = graphics.GraphicsDevice.Viewport;
            base.Initialize();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            ChecaColision(gameTime);
            ChecaRaton();
            base.Update(gameTime);
        }


        public void ChecaColision(GameTime g)
        {
            tiempo += g.ElapsedGameTime;
            if (tiempo > TimeSpan.FromMilliseconds(1000))
            {
                //tiempo-=g.ElapsedGameTime;
                tiempo = TimeSpan.Zero;
            }

        }

        public void ChecaRaton()
        {
            MouseState raton = Mouse.GetState();
            pRectangulo.X = raton.X;
            pRectangulo.Y = raton.Y;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(figura, pRectangulo, rRectangulo, Color.White);
            base.Draw(gameTime);
        }
    }
}
