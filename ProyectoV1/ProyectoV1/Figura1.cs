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
    public class Figura1 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Texture2D imagen;
        protected float velocidad, velTemp;
        protected Vector2 posicion, prevPos;
        protected Rectangle pantalla;
        private int tipo;
        protected Rectangle[] cuadros;
        protected KeyboardState tecladoAnterior = Keyboard.GetState();
        protected int estado = 0, cambio = 0;
        protected Boolean choque = false,contar=false;
        protected Point[] puntos;
        protected float timer;
        protected Color color;

        private SoundBank soundBank;




        public Figura1(Game game, Texture2D img, float vel, int tipo)
            : base(game)
        {
            imagen = img;
            velocidad = vel;
            velTemp = vel;
            pantalla = new Rectangle(275, 85, 250, 525);
            this.tipo = tipo;
            iniciar();

            soundBank = (SoundBank)game.Services.GetService(typeof(SoundBank));
        }

        //propiedades""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public void setVelocidad(float n)
        {
            velocidad = n;
            velTemp = n;
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
            posicion.X = 300;
            posicion.Y = 85;
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
                manejaTeclado();
                actualizaPosicion();
                checaColision();
            }
            if (contar)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= 0.5f)
                {
                    choque = true;
                    contar = false;
                }
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
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y+25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y+25, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y+25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X+50, (int)posicion.Y, 25, 25);
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
                    cuadros[0] = new Rectangle((int)posicion.X+25, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X+25, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 1 || estado== 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y+25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y+25, 25, 25);
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
                    cuadros[3] = new Rectangle((int)posicion.X + 50, (int)posicion.Y+25, 25, 25);
                }
                if (estado == 2)
                {
                    cuadros[0] = new Rectangle((int)posicion.X+25, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X, (int)posicion.Y + 25, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 25, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X + 25, (int)posicion.Y + 50, 25, 25);
                }
                if (estado == 3)
                {
                    cuadros[0] = new Rectangle((int)posicion.X, (int)posicion.Y, 25, 25);
                    cuadros[1] = new Rectangle((int)posicion.X + 25, (int)posicion.Y, 25, 25);
                    cuadros[2] = new Rectangle((int)posicion.X + 25, (int)posicion.Y+25, 25, 25);
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
                    cuadros[2] = new Rectangle((int)posicion.X+25, (int)posicion.Y, 25, 25);
                    cuadros[3] = new Rectangle((int)posicion.X+25, (int)posicion.Y + 25, 25, 25);
                }
            }

            /////////////ver tipos////////////////////////////////////////////////////////////////////////////////////////////////

            for (int i = 0; i < cuadros.Length; i++)
            {
                puntos[i] = new Point(cuadros[i].X + 12, cuadros[i].Y + 12);
            }


        }

        public void manejaTeclado()
        {
            KeyboardState teclado = Keyboard.GetState();
            prevPos = posicion;
            if (teclado.IsKeyDown(Keys.Left) && tecladoAnterior.IsKeyUp(Keys.Left))
            {
                soundBank.PlayCue("mover");
                if (posicion.X > pantalla.X)
                    posicion.X -= 25;
            }
            if (teclado.IsKeyDown(Keys.Right) && tecladoAnterior.IsKeyUp(Keys.Right))
            {
                soundBank.PlayCue("mover");
                if (tipo != 6)
                {
                    if (tipo == 7)
                    {
                        if (posicion.X + 50 < pantalla.Width + pantalla.X)
                            posicion.X += 25;
                    }
                    else
                    {
                        if (estado == 3 || estado == 1)
                        {
                            if (posicion.X + 75 < pantalla.Width + pantalla.X)
                                posicion.X += 25;
                        }
                        else
                        {
                            if (posicion.X + 50 < pantalla.Width + pantalla.X)
                                posicion.X += 25;
                        }
                    }
                }
                else
                {
                    if (estado == 3 || estado == 1)
                    {
                        if (posicion.X + 100 < pantalla.Width + pantalla.X)
                            posicion.X += 25;
                    }
                    else
                    {
                        if (posicion.X + 25 < pantalla.Width + pantalla.X)
                            posicion.X += 25;
                    }
                }
            }//fin del izq der


            if (teclado.IsKeyDown(Keys.A) && tecladoAnterior.IsKeyUp(Keys.A))
            {
                soundBank.PlayCue("mover2");
                cambio++;
                estado = cambio % 4;
            }
            if (teclado.IsKeyDown(Keys.S) && tecladoAnterior.IsKeyUp(Keys.S))
            {
                soundBank.PlayCue("mover2");
                cambio--;
                estado = cambio % 4;
            }
            if (teclado.IsKeyDown(Keys.Down))
            {
                if (!contar)
                {
                    velocidad = 5;
                }
            }
            if (teclado.IsKeyUp(Keys.Down) && tecladoAnterior.IsKeyDown(Keys.Down))
            {

                velocidad = velTemp;
            }

            tecladoAnterior = teclado;
            if (estado < 0)
            {
                estado *= -1;
            }

        }

        public void checaColision()
        {
            for (int i = 0; i < cuadros.Length; i++)
            {
                if (estado == 3 || estado == 1)
                {
                    if (tipo != 6)
                    {
                        if(tipo==7){
                            if (posicion.X + 50 > pantalla.Width + pantalla.X)
                            posicion.X -= 25;
                        }else{
                        if (posicion.X + 75 > pantalla.Width + pantalla.X)
                            posicion.X -= 25;
                        }
                    }
                    else
                    {
                        if (posicion.X + 100 > pantalla.Width + pantalla.X)
                            posicion.X -= 25;
                    }
                }
                else
                {
                    if (tipo != 6)
                    {
                        if (tipo == 7)
                        {
                            if (posicion.X + 50 > pantalla.Width + pantalla.X)
                                posicion.X -= 25;
                        }
                        else
                        {
                            if (posicion.X + 50 > pantalla.Width + pantalla.X)
                                posicion.X -= 25;
                        }
                    }
                    else 
                    {
                        if (posicion.X + 25 > pantalla.Width + pantalla.X)
                            posicion.X -= 25;
                    }
                }


                if (cuadros[i].Y + 25 > pantalla.Y + pantalla.Height)
                {
                    velocidad = 0;
                    //choque = true;
                    contar = true;
                }

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

        public Vector2 getPrevPos()
        {
            return prevPos;
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