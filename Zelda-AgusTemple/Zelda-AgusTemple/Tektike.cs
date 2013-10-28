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
    public class Tektike : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D textura1, textura2, actual;
        Vector2 pos, posL;
        int contador, contador1;
        public Tektike(Game game, int x, int y)
            : base(game)
        {
            textura1 = Game.Content.Load<Texture2D>("Enemigos/Tektike/Tektike1");
            textura2 = Game.Content.Load<Texture2D>("Enemigos/Tektike/Tektike2");
            actual = textura1;
            contador = 0;
            contador1 = 0;
            pos = new Vector2(x, y);
            posL = new Vector2(0, 0);
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (contador == 30)
            {
                if (actual == textura1)
                    actual = textura2;
                else
                    actual = textura1;
                contador = 0;
            }
            if (contador1 == 100)
            {
                Mover();
                contador1 = 0;
            }
            contador++;
            contador1++;
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(actual, new Vector2(pos.X , pos.Y ), Color.White);
            base.Draw(gameTime);
        }
        public Rectangle getArea()
        {
            return new Rectangle((int)(pos.X ), (int)(pos.Y ), 32, 32);
        }
        public void setPosL(Vector2 V)
        {
            posL = V;
        }
        public bool Destruido(Rectangle r)
        {
            if (r.Intersects(getArea()))
                return true;
            else
                return false;
        }
        public void Mover()
        {
            float xactual = pos.X;
            float yactual = pos.Y;
            Random r = new Random();
            float limitex = posL.X - pos.X;
            limitex = .5f + (limitex * .002f);
            float limitey = posL.Y - pos.Y;
            limitey = .5f + (limitey * .002f);
            int i = 0;
            while (i<10)
            {
                if (r.NextDouble()<limitex)
                {
                    if (r.NextDouble() < limitey)
                    {
                        pos.X += (float)r.NextDouble() * 7;
                        pos.Y += (float)r.NextDouble() * 7;
                    }
                    else
                    {
                        pos.X += (float)r.NextDouble() * 7;
                        pos.Y -= (float)r.NextDouble() * 7;
                    }
                }
                else
                {
                    if (r.NextDouble() > limitey)
                    {
                        pos.X -= (float)r.NextDouble() * 7;
                        pos.Y -= (float)r.NextDouble() * 7;
                    }
                    else
                    {
                        pos.X -= (float)r.NextDouble() * 7;
                        pos.Y += (float)r.NextDouble() * 7;
                    }
                }
                i++;
            }
        }
    }
}