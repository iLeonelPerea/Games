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


namespace Zelda_AgusTemple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Key : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D textura1;
        Vector2 pos, posL;
        public bool activado;

        public Key(Game game, float x, float y)
            : base(game)
        {
            pos.X = x;
            pos.Y = y;
            textura1 = Game.Content.Load<Texture2D>("Objetos/key");
            // TODO: Construct any child components here
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
        public bool Tomar()
        {
            Rectangle area = new Rectangle((int)pos.X , (int)pos.Y , 16, 32);
            Rectangle link = new Rectangle((int)posL.X, (int)posL.Y, 32, 32);
            if (area.Intersects(link))
                return true;
            return false;
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
        public void setPosL(Vector2 V)
        {
            posL = V;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(textura1, new Vector2(pos.X, pos.Y), Color.White);
            base.Draw(gameTime);
        }
    }
}