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


namespace Tetris.Core
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ImagenFondoTetris : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D img1,img2,tetris,titulo,fondoNext;
        private Texture2D[] arrImagenes;
        private Rectangle pantalla;
        private SpriteBatch sBatch;
        private Rectangle tetrisRect;
        private Rectangle[,] matriz;
        private int score = 0, numLineas=0,nivel=1;
        private SpriteFont fuente;


        public ImagenFondoTetris(Game game, Texture2D imagen, Texture2D imagen2, Texture2D imagen3,Rectangle rectan,Texture2D tit,
            Rectangle[,] mat,Texture2D fondoN,Texture2D[] arrImg)
            : base(game)
        {
            this.img1 = imagen;
            img2 = imagen2;
            tetris = imagen3;
            tetrisRect = rectan;
            titulo = tit;
            matriz = mat;
            fondoNext = fondoN;
            arrImagenes = arrImg;

            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            fuente = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            Visible = true;

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

        public void setScore(int s)
        {
            score = s;
        }

        public void setNumLineas(int s)
        {
            numLineas = s;
        }

        public void setNivel(int s)
        {
            nivel = s;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                sBatch.Draw(img1, new Rectangle(0, 0, img1.Width, img1.Height), pantalla, Color.White);
                sBatch.Draw(img2, new Vector2(tetrisRect.X, tetrisRect.Y), Color.White);
                sBatch.Draw(titulo, new Vector2(pantalla.Width/2-titulo.Width/2,0), Color.White);
                //for (int i = 0; i < 210; i++)
                //{
                //    sBatch.Draw(tetris, new Vector2((i % 10) * 25 + 275, 85 + (i / 10) * 25), new Color(255, 255, 255, .2f));
                //}
                
                for (int c = 0; c < 10; c++)
                {
                    for (int r = 0; r < 21; r++)
                    {
                        //sBatch.Draw(tetris, new Vector2(matriz[c, r].X, matriz[c, r].Y), Color.Tomato);
                        sBatch.Draw(tetris, new Vector2(matriz[c, r].X, matriz[c, r].Y), new Color(255, 255, 255, .2f));
                    }
                }
                sBatch.Draw(fondoNext, new Vector2(600,145), Color.White);
                sBatch.Draw(arrImagenes[0], new Vector2(570, 85), Color.White);
                sBatch.Draw(arrImagenes[1], new Vector2(30, 85), Color.White);
                sBatch.Draw(arrImagenes[2], new Vector2(5, 140), Color.White);
                sBatch.DrawString(fuente, "" + score, new Vector2(30, 155), Color.DarkGreen);

                sBatch.Draw(arrImagenes[3], new Vector2(30, 210), Color.White);
                sBatch.Draw(arrImagenes[2], new Vector2(5, 280), Color.White);
                sBatch.DrawString(fuente, "" + numLineas, new Vector2(30, 295), Color.DarkGreen);
                
                sBatch.Draw(arrImagenes[4], new Vector2(30, 340), Color.White);
                sBatch.Draw(arrImagenes[2], new Vector2(5, 410), Color.White);
                sBatch.DrawString(fuente, "" + nivel, new Vector2(30, 425), Color.DarkGreen);

                
            }
            base.Draw(gameTime);
        }
    }
}