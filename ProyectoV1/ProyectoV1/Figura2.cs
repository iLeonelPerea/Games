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
    public class Figura2 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Texture2D imagen;
        protected float velocidad, velTemp;
        protected Vector2 posicion;
        protected Rectangle pantalla;
        private int tipo;
        protected Rectangle[] cuadros;
        protected KeyboardState tecladoAnterior = Keyboard.GetState();
        protected int estado = 0, cambio = 0;
        protected Boolean choque = false, contar = false;
        protected Point[] puntos;
        protected float timer;
        protected Color color;




        public Figura2(Game game, Texture2D img, float vel, int tipo)
            : base(game)
        {
            imagen = img;
            velocidad = 0;
            velTemp = 0;
            pantalla = new Rectangle(275, 85, 250, 525);
            this.tipo = tipo;
            iniciar();
        }

        //propiedades""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
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

        public void iniciar()
        {
            posicion.X = 615;
            posicion.Y = 155;
            cuadros = new Rectangle[4];
            puntos = new Point[4];
            choque = false;
            velocidad = velTemp;
            contar = false;
            timer = 0f;
            hacerColor();
        }

        public void hacerColor()
        {
            switch (tipo)
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (!choque)
            {
                actualizaPosicion();
            }
            base.Update(gameTime);
        }

        public void actualizaPosicion()
        {
            posicion.Y += velocidad;
            /////////////ver tipos////////////////////////////////////////////////////////////////////////////////////////////////
            if (tipo == 1)
            {
                if (estado == 0)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                }
                if (estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 50, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                }
                if (estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y + 25, 25, 25);
                }
            }
            if (tipo == 2)
            {
                if (estado == 0)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                }
                if (estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 50, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                }
            }

            if (tipo == 3)
            {
                if (estado == 0)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                }
                if (estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                }
            }
            if (tipo == 4)
            {
                if (estado == 0 || estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1 || estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y + 25, 25, 25);
                }
            }
            if (tipo == 5)
            {
                if (estado == 0)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y + 25, 25, 25);
                }
                if (estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                }
            }
            if (tipo == 6)
            {
                if (estado == 0 || estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 75, 25, 25);
                }
                if (estado == 1 || estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 50, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 75, (int)posicion.Y, 25, 25);
                }
            }
            if (tipo == 7)
            {
                if (estado == 0 || estado == 2 || estado == 3 || estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                }
            }

            /////////////ver tipos////////////////////////////////////////////////////////////////////////////////////////////////

            for (int i = 0; i < cuadros.Length; i++)
            {
                puntos[i] = new Point(cuadros[i].X + 12, cuadros[i].Y + 12);
            }


        }

        
        public Rectangle[] getArea()
        {
            return cuadros;
        }

        public Point[] getPuntos()
        {
            return puntos;
        }

        public Boolean getChoque()
        {
            return choque;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            for (int i = 0; i < cuadros.Length; i++)
            {
                sBatch.Draw(imagen, cuadros[i], color);
            }
            base.Draw(gameTime);
        }
    }
}