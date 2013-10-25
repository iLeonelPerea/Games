using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using BattleFieldX.Core;

namespace BattleFieldX
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Murio : Escena
    {
        protected Menu menu;
        protected Texture2D fondo;
        protected SpriteBatch spriteBatch;
        protected SpriteFont fuenteNormal;
        //     protected AudioComponent audioComponent;

        public Murio(Game game, SpriteFont fuenteNormal, SpriteFont fuenteSeleccionada, Texture2D fondo)
            : base(game)
        {
            menu = new Menu(game, fuenteNormal, fuenteSeleccionada);
            // string[] items = { "Instructions", "play", "Exit" };
            //   menu.SetMenuItems(items);
            //   Componentes.Add(menu);
            this.fondo = fondo;
            this.fuenteNormal = fuenteNormal;
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            //   audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
        }

        public override void Show()
        {
            menu.Posicion = new Vector2((Game.Window.ClientBounds.Width - menu.Width) / 2, (Game.Window.ClientBounds.Height / 2) - (menu.Height / 2));
            menu.Visible = true;
            menu.Enabled = true;
            base.Show();
        }

        public override void Hide()
        {
            //backMusic.Stop(AudioStopOptions.Immediate);
            base.Hide();
        }

        public int SelectedMenuIndex
        {
            get { return menu.SelectedIndex; }
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

            spriteBatch.Draw(fondo, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);

            base.Draw(gameTime);
        }
    }
}