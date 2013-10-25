using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace BattleFieldX
    {
     /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Balas : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D textura;
        private Vector2 posicion=new Vector2();
        private int velocidadY = 2;
        private Rectangle pantalla;
        private Random r = new Random();
        private float x, y;
        private int px, py;
        public Balas(Game game,ref Texture2D textura,float x,float y,int px,int py)
            : base(game)
        {
            this.textura = textura;
            this.x = x;
            this.y = y;
            this.px = px;
            this.py = py;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            Iniciar(px,py);
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

        public void Iniciar(int px,int py) {
            posicion.Y = py;
            posicion.X = px;
        }
        public void setX() {
            x=x*(-1);
        }
        public void setY() {
            y = y * (-1);
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            posicion.Y += y;
            posicion.X += x;
            
            base.Update(gameTime);
        }
        public bool ColisionaSuelo() {
            if (posicion.Y + textura.Height >= pantalla.Height)
            {
                return true;
            }
            else {
                return false;
            }

        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(textura, posicion, Color.Green);
            base.Draw(gameTime);
        }
        public bool ChecaColision(Rectangle r) {
            Rectangle area = new Rectangle((int)posicion.X, (int)posicion.Y, (int)textura.Width, (int)textura.Height);
            return area.Intersects(r);
        }
        public Rectangle GetArea()
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height);
        }
    }
}
