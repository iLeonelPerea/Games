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


namespace BarritaGirando.Core
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Escena : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        private readonly List<GameComponent> componentes;

        public Escena(Game game)
            : base(game)
        {
            componentes = new List<GameComponent>();
            Visible = false;
            Enabled = false;
        }

        public List<GameComponent> Componentes {
            get {
                return componentes;
            }
        }

        public virtual void Hide() {
            Enabled = false;
            Visible = false;
        }

        public virtual void Show()
        {
            Enabled = true;
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
            for (int i = 0; i < componentes.Count; i++) {
                if (componentes[i].Enabled) {
                    componentes[i].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < componentes.Count; i++) {
                GameComponent gc = componentes[i];
                if ((gc is DrawableGameComponent) && ((DrawableGameComponent)gc).Visible) { 
                    ((DrawableGameComponent)gc).Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
    }
}