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


namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Obstaculo : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private Texture2D img;
        public Vector2 posicion, posRelativa;
        public bool arriba, derecha, movHorizontal, movVertical;

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }
        private Rectangle pantalla;
        private SpriteBatch spriteBatch;

        public Texture2D Img
        {
            get { return img; }
            set { img = value; }
        }
        public Obstaculo(Game game, ref Texture2D img, int posX, int posY, bool movVertical, bool movHorizontal)
            : base(game)
        {
            this.img = img;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            this.movHorizontal = movHorizontal;
            this.movVertical = movVertical;
            posRelativa = new Vector2(0.0f, 0.0f);
            Iniciar(posX, posY);
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

        public void Iniciar(int posX, int posY) {
            posicion.Y = posX;
            posicion.X = posY;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (movVertical) {
                if (posRelativa.Y < -10)
                {
                    arriba = false;
                }
                if (posRelativa.Y > 10) {
                    arriba = true;
                }

                if (arriba) {
                    posRelativa.Y -= 0.5f;
                    posicion.Y -= 0.5f;
                }
                if (!arriba)
                {
                    posRelativa.Y += 0.5f;
                    posicion.Y += 0.5f;
                }
            }
            if(movHorizontal){
                if (posRelativa.X < -10)
                {
                    derecha = true;
                }

                if (posRelativa.X > 10)
                {
                    derecha = false;
                }
                if (derecha)
                {
                    posRelativa.X += 0.5f;
                    posicion.X += 0.5f;
                }
                if (!derecha)
                {
                    posRelativa.X -= 0.5f;
                    posicion.X -= 0.5f;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            //sBatch.Draw(img, posicion, Color.Black);
            
            base.Draw(gameTime);
        }

    }
}