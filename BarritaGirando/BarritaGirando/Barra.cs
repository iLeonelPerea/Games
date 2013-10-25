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
    public class Barra : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D imagen;
        private float velocidadGiro;
        public float velocidad;
        public Vector2 posicion, origen;
        public Rectangle pantalla, rImagen;
        private bool giroDerecha, muerte;
        public float angulo = 0.0f;
        public float escala = 1.0f;

        public bool Muerte
        {
            get { return muerte; }
            set { muerte = value; }
        }

        public float VelocidadGiro
        {
            get { return velocidadGiro; }
            set { velocidadGiro = value; }
        }

        public Vector2 Origen
        {
            get { return origen; }
            set { origen = value; }
        }

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public Texture2D Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        public bool GiroDerecha
        {
            get { return giroDerecha; }
            set { giroDerecha = value; }
        }

        public Barra(Game game, Texture2D img, float vel, int velGiro, int posX, int posY)
            : base(game)
        {
            imagen = img;
            velocidad = vel;
            velocidadGiro = velGiro;
            giroDerecha = true;
            pantalla = new Rectangle(0,0,game.Window.ClientBounds.Width,game.Window.ClientBounds.Height);
            rImagen = new Rectangle(posX, posY, img.Width, img.Height);
            origen = new Vector2(imagen.Width/2, imagen.Height/2);
            muerte = false;
            this.iniciar(posX,posY);
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
            if (!muerte)
            {
                origen = new Vector2(imagen.Width / 2, imagen.Height / 2);
                angulo += 0.05f * velocidadGiro;
            }
            base.Update(gameTime);
        }

        public void iniciar(int posX, int posY) {
            posicion.X = posX;
            posicion.Y = posY;
            rImagen.X = posX;
            rImagen.Y = posY;
        }

        public override void Draw(GameTime gameTime)
        {
            /*SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (muerte)
            {
                sBatch.Draw(imagen,rImagen,Color.White);
            }
            if (!muerte)
            {
                sBatch.Draw(imagen, posicion, null, Color.White, angulo, origen, 1.0f, SpriteEffects.None, 1);
            }*/
            base.Draw(gameTime);
        }
    }
}