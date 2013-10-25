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
using BarritaGirando.Core;


namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaSave : Escena
    {
        protected SpriteFont fuenteSmall, fuenteBig;
        protected SpriteBatch sBatch;
        private string[] nombre = { "A", "A", "A" };
        protected Texture2D imgFondo;
        private int currcar;
        private int score;
        private AudioComponent audioComponent;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string Nombre
        {
            get { return nombre[0] + nombre[1] + nombre[2]; }
        }


        public EscenaSave(Game game,SpriteFont fuenteSmall, SpriteFont fuenteBig, Texture2D fondo)
            : base(game)
        {
            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            this.fuenteSmall = fuenteSmall;
            this.fuenteBig = fuenteBig;
            imgFondo = fondo;
            audioComponent = (AudioComponent)game.Services.GetService(typeof(AudioComponent));            
        }

        public override void Show()
        {
            base.Show();
        }

        public void cambiarNombre(int cambio) {
            switch (cambio) { 
                case 1:
                    nombre[currcar] = "" + (char)(((int)(char)nombre[currcar][0]) + 1);
                    break;
                case 2:
                    nombre[currcar] = "" + (char)(((int)(char)nombre[currcar][0]) - 1);
                    break;
                case 3:
                    currcar -= 1;
                    if (currcar < 0)
                        currcar = 2;
                    break;
                case 4:
                    currcar = (currcar + 1) % 3;
                    break;
            }
            audioComponent.PlayCue("menuChange");
            
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
            sBatch.Draw(imgFondo, new Vector2(0, 0), Color.White);
            sBatch.DrawString(fuenteBig, "Fin del Juego", new Vector2(400, 50), Color.White);
            sBatch.DrawString(fuenteBig, "Fin del Juego", new Vector2(401, 51), Color.Black);
            sBatch.DrawString(fuenteBig, "Puntaje: " + score, new Vector2(100, 100), Color.Black);
            sBatch.DrawString(fuenteBig, "Puntaje: " + score, new Vector2(101, 101), Color.Yellow);
            try
            {
                float x = 400;
                float y = 250;
                if (currcar == 0)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x+1, y+1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteBig, nombre[0], new Vector2(x, y+1), Color.White);
                    sBatch.DrawString(fuenteBig, nombre[0], new Vector2(x+1, y), Color.Red);
                    x += fuenteBig.MeasureString(""+nombre[0]).X;
                    sBatch.DrawString(fuenteSmall, "-" + nombre[1] + "-" + nombre[2], new Vector2(x, y+11), Color.Black);
                    sBatch.DrawString(fuenteSmall, "-" + nombre[1] + "-" + nombre[2], new Vector2(x, y+10), Color.White);
                }
                if (currcar == 1)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: " , new Vector2(x, y+1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: " , new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-", new Vector2(x, y+11), Color.Black);
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-", new Vector2(x+1, y+10), Color.White);
                    x += fuenteSmall.MeasureString(""+nombre[0]+"-").X;
                    sBatch.DrawString(fuenteBig, nombre[1], new Vector2(x, y+1), Color.White);
                    sBatch.DrawString(fuenteBig, nombre[1], new Vector2(x+1, y), Color.Red);
                    x += fuenteBig.MeasureString("" + nombre[1]).X;
                    sBatch.DrawString(fuenteSmall,"-"+nombre[2], new Vector2(x, y+11), Color.Black);
                    sBatch.DrawString(fuenteSmall,"-"+nombre[2], new Vector2(x + 1, y+10), Color.White);
                }
                if (currcar == 2)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y+1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-" + nombre[1]+"-", new Vector2(x, y+11), Color.Black);
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-" + nombre[1] + "-", new Vector2(x + 1, y+10), Color.White);
                    x += fuenteSmall.MeasureString("" + nombre[0] + "-"+ nombre[1]+"-").X;
                    sBatch.DrawString(fuenteBig, "" + nombre[2], new Vector2(x, y+1), Color.White);
                    sBatch.DrawString(fuenteBig, "" + nombre[2], new Vector2(x + 1, y), Color.Red);
                }
            }
            catch (Exception ex) {
                nombre[0] = ""+'A';
                nombre[1] = "" + 'A';
                nombre[2] = "" + 'A';

                float x = 400;
                float y = 250;
                if (currcar == 0)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x + 1, y + 1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteBig, nombre[0], new Vector2(x, y + 1), Color.White);
                    sBatch.DrawString(fuenteBig, nombre[0], new Vector2(x + 1, y), Color.Red);
                    x += fuenteBig.MeasureString("" + nombre[0]).X;
                    sBatch.DrawString(fuenteSmall, "-" + nombre[1] + "-" + nombre[2], new Vector2(x, y + 11), Color.Black);
                    sBatch.DrawString(fuenteSmall, "-" + nombre[1] + "-" + nombre[2], new Vector2(x, y + 10), Color.White);
                }
                if (currcar == 1)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y + 1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-", new Vector2(x, y + 11), Color.Black);
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-", new Vector2(x + 1, y + 10), Color.White);
                    x += fuenteSmall.MeasureString("" + nombre[0] + "-").X;
                    sBatch.DrawString(fuenteBig, nombre[1], new Vector2(x, y + 1), Color.White);
                    sBatch.DrawString(fuenteBig, nombre[1], new Vector2(x + 1, y), Color.Red);
                    x += fuenteBig.MeasureString("" + nombre[1]).X;
                    sBatch.DrawString(fuenteSmall, "-" + nombre[2], new Vector2(x, y + 11), Color.Black);
                    sBatch.DrawString(fuenteSmall, "-" + nombre[2], new Vector2(x + 1, y + 10), Color.White);
                }
                if (currcar == 2)
                {
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y + 1), Color.Black);
                    sBatch.DrawString(fuenteBig, "Iniciales: ", new Vector2(x, y), Color.White);
                    x += fuenteBig.MeasureString("Iniciales: ").X;
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-" + nombre[1] + "-", new Vector2(x, y + 11), Color.Black);
                    sBatch.DrawString(fuenteSmall, nombre[0] + "-" + nombre[1] + "-", new Vector2(x + 1, y + 10), Color.White);
                    x += fuenteSmall.MeasureString("" + nombre[0] + "-" + nombre[1] + "-").X;
                    sBatch.DrawString(fuenteBig, "" + nombre[2], new Vector2(x, y + 1), Color.White);
                    sBatch.DrawString(fuenteBig, "" + nombre[2], new Vector2(x + 1, y), Color.Red);
                }
            }
            base.Draw(gameTime);
        }
    }
}