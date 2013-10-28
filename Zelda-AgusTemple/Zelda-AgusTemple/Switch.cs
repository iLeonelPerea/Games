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
    public class Switch : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D textura1;
        Vector2 pos, posL;
        public bool activado;
        public Switch(Game game, bool a, float x, float y)
            : base(game)
        {
            activado = a;
            textura1 = Game.Content.Load<Texture2D>("Objetos/switch");
            pos = new Vector2(x, y);
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
        public void setPosL(Vector2 V)
        {
            posL = V;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Activar();
            base.Update(gameTime);
        }
        public bool Activado()
        {
            return activado;
        }
        public void Activar()
        {
            Rectangle area = new Rectangle((int)pos.X - 5, (int)pos.Y - 5, 40, 40);
            Rectangle link = new Rectangle((int)posL.X, (int)posL.Y, 30, 30);
            if (area.Contains(link))
                activado = true;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(textura1, new Vector2(pos.X, pos.Y), Color.Bisque);
            base.Draw(gameTime);
        }
    }
}