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


namespace Tetris
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class cuadro : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D img;
        private Vector2 posicion;
        private Rectangle pantalla;
        private int velocidadY=1;
        private SpriteBatch sBatch;
        private Random r = new Random();
        private Color color;

        
        public cuadro(Game game,ref Texture2D img,Vector2 pos,int col)
            : base(game)
        {
            this.img = img;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            posicion = pos;
            switch (col)
            {
                case 1: color = Color.Red;
                    break;
                case 2: color = Color.Blue;
                    break;
                case 3: color = Color.Green;
                    break;
                case 4: color = Color.Orange;
                    break;
                case 5: color = Color.Purple;
                    break;
                case 6: color = Color.Cyan;
                    break;
                case 7: color = Color.Yellow;
                    break;
            }
        }

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
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

        public bool checaColision(Rectangle r)
        {
            Rectangle rDuff = new Rectangle((int)posicion.X, (int)posicion.Y, img.Width, img.Height);
            return rDuff.Intersects(r);
        }

        public bool colisionaSuelo()
        {
            if (posicion.Y + img.Height >= pantalla.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Rectangle getArea()
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
        }

        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(img, posicion, color);
            base.Draw(gameTime);
        }

    }
}