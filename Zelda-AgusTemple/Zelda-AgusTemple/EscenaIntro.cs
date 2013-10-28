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


namespace Zelda_AgusTemple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaIntro : Escena
    {
        protected Menu menu;
        protected Texture2D fondo;
        protected SpriteBatch spriteBatch;
        protected AudioComponent audioComponent;

        public EscenaIntro(Game game)
            : base(game)
        {
            menu = new Menu(game, Game.Content.Load<SpriteFont>("Arial"), Game.Content.Load<SpriteFont>("Arial"));
            fondo = Game.Content.Load<Texture2D>("Zelda_wall");
            string[] items = { "Instrucciones", "Continuar", "Salir" };
            menu.SetMenuItems(items);
            Componentes.Add(menu);
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
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
        public override void Show()
        {
            menu.Posicion = new Vector2((Game.Window.ClientBounds.Width -
                                         menu.Width) / 2, (Game.Window.ClientBounds.Height / 2) - (menu.Height / 2));
            menu.Visible = true;
            menu.Enabled = true;
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
        public int SelectedMenuIndex
        {
            get { return menu.SelectedIndex; }
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