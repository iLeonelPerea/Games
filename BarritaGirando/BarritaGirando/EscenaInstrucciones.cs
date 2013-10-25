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
using BarritaGirando.Core;


namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaInstrucciones : Escena
    {
        private SpriteBatch sBatch;
        private Texture2D imgInstrucciones;
        private Rectangle pantalla;

        public EscenaInstrucciones(Game game, ref Texture2D instrucciones)
            : base(game)
        {
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            this.imgInstrucciones = instrucciones;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
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
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(imgInstrucciones, new Vector2(0, 0), Color.White);
            base.Draw(gameTime);
        }

    }
}