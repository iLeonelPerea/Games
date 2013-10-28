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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Tetris.Core
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ImagenFondo : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D img;
        private Rectangle pantalla;
        private SpriteBatch sBatch;

        public ImagenFondo(Game game, Texture2D imagen)
            : base(game)
        {
            this.img = imagen;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            Visible = true;

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                sBatch.Draw(img, new Rectangle(0, 0, img.Width, img.Height), pantalla, Color.White);
            }
            base.Draw(gameTime);
        }
    }
}