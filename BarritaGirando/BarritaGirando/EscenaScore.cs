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
    public class EscenaScore : Escena
    {
        private SpriteBatch sBatch;
        private Texture2D imgScore;
        private Rectangle pantalla;
        private SpriteFont fuente;
        private List<Score> listaScores;

        public List<Score> ListaScores
        {
            get { return listaScores; }
            set { listaScores = value; }
        }

        public EscenaScore(Game game, ref SpriteFont fuente, ref Texture2D imgScore)
            : base(game)
        {
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            this.fuente = fuente;
            this.imgScore = imgScore;
            pantalla = new Rectangle(0, 0,1000, 650);
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
            int i = 0;
            sBatch.Draw(imgScore, pantalla, Color.White);
            while ((i <= 10) && (i < listaScores.Count)) {
                sBatch.DrawString(fuente, "" + (i + 1) + ": " + ((Score)(listaScores[i])).Puntos + " - " + ((Score)(listaScores[i])).Nombre, new Vector2(50, i * 50 + 100), Color.Black);
                i++;
            }

            base.Draw(gameTime);
        }

    }
}