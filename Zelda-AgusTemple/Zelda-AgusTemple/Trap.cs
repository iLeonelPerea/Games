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
    public class Trap : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Texture2D textura;
        Rectangle[] vision;
        Vector2 pos,avance,posL;
        bool activo;

        public Trap(Game game, int x, int y)
            : base(game)
        {
           
            textura = Game.Content.Load<Texture2D>("Enemigos/Trap/Trap");
            pos = new Vector2(x, y);
            avance = new Vector2(0, 0);
            vision = new Rectangle[2];
            activo = false;
            vision[0] = new Rectangle(0, (int)pos.Y, (int)Game.Window.ClientBounds.Width,(int)textura.Height );
            vision[1] = new Rectangle((int)pos.X, 0, (int)textura.Width, (int)Game.Window.ClientBounds.Height);
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
            
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(textura, new Vector2(pos.X+avance.X, pos.Y+avance.Y), Color.White);
            base.Draw(gameTime);
        }
        public Rectangle getArea()
        {
            return new Rectangle((int)(pos.X + avance.X), (int)(pos.Y + avance.Y), 32, 32);
        }
        public bool Destruido(Rectangle r)
        {
            if (r.Intersects(getArea()))
                return true;
            else
                return false;
        }
        public bool Visto(Rectangle r)
        {
            if (r.Intersects(vision[0])&& !activo)
            {
                if(posL.X>=pos.X)
                    avance.X += 2;
                else
                    avance.X -= 2;
                return true;
            }
            else
            {
                if (avance.X != 0)
                {
                    activo = true;

                    if (posL.X >= pos.X)
                        avance.X -= 1;
                    else
                        avance.X += 1;
                }
                else
                {
                    activo = false;
                }
            }
            if (r.Intersects(vision[1]) && !activo)
            {
                if (posL.Y >= pos.Y)
                    avance.Y += 2;
                else
                    avance.Y -= 2;
                return true;
            }
            else
            {
                if (avance.Y != 0)
                {
                    activo = true;

                    if (posL.Y >= pos.Y)
                        avance.Y -= 1;
                    else
                        avance.Y += 1;
                }
                else
                {
                    activo = false;
                }
            }
            return false;
        }
        public void setPosL(Vector2 V)
        {
            posL = V;
        }
    }
}