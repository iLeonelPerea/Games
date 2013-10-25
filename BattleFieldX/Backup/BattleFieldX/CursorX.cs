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
    public class CursorX : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D textura;
        private int velocidadX =5;
        private Rectangle pantalla;
        protected TimeSpan tiempo = TimeSpan.Zero;
        private int direc = 1;
        private Rectangle rRectangulo;
        private Vector2 pRectangulo;

        public CursorX(Game game, ref Texture2D textura)
            : base(game)
        {
            this.textura = textura;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
           // Iniciar();
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            pRectangulo = new Vector2(80, 150);
            rRectangulo = new Rectangle(0, 0, 135, 100);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            ChecaColision(gameTime);
            ChecaRaton();
            base.Update(gameTime);
        }

        public void ChecaColision(GameTime g)
        {
            tiempo += g.ElapsedGameTime;
            if (tiempo > TimeSpan.FromMilliseconds(1000))
            {
                //tiempo-=g.ElapsedGameTime;
                tiempo = TimeSpan.Zero;
            }

        }

        public void ChecaRaton()
        {
            MouseState raton = Mouse.GetState();
            pRectangulo.X = raton.X;
            pRectangulo.Y = raton.Y;
        }
        public Boolean escuchaRaton() {
            //MouseState a = Mouse.GetState();
            //if (a.LeftButton == MouseButtonState.Pressed)
            KeyboardState te = Keyboard.GetState();
            if (te.IsKeyDown(Keys.A))
            {
                return true;
            }
            else {
                return false;
            }
        }
        public double getX()
        {
            return (double)pRectangulo.X;
        }
        public double getY()
        {
            return (double)pRectangulo.Y;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sBatch.Draw(textura, pRectangulo, rRectangulo, Color.White);
            base.Draw(gameTime);
        }
        public Rectangle GetArea()
        {
            return new Rectangle((int)pRectangulo.X, (int)pRectangulo.Y, 20, 20);
        }
       
    }
}