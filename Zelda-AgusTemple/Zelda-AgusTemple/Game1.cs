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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AudioComponent audioComponent;
        Escena escenaActiva;
        EscenaIntro intro;
        EscenaTeclado esct;
        Nivel1 niv1;
        Nivel2 niv2;
        Nivel3 niv3;
        Nivel4 niv4;
        Nivel5 niv5;
        Nivel6 niv6;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 300;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            audioComponent = new AudioComponent(this);
            Components.Add(audioComponent);
            Services.AddService(typeof(AudioComponent), audioComponent);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            intro = new EscenaIntro(this);
            niv1 = new Nivel1(this, new Link(this, 6, this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2), this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2);
            
            Components.Add(intro);
            Components.Add(niv1);
            audioComponent.PlayCue("zelda");
            niv1.Show();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            SecuenciaNiveles();
           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        public void SecuenciaNiveles()
        {
            //Puertas del nivel 1
            if (niv1 != null)
            {
                if (niv1.Enabled)
                {
                    //Puerta arriba
                    if (niv1.EntroPuerta() != 0)
                    {
                        niv2 = new Nivel2(this, niv1.jugador, this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height-40);
                        niv1.Enabled = false;
                        niv1.Hide();
                        Components.Remove(niv1);
                        Components.Add(niv2);
                        niv2.Show();
                    }
                }
            }
            //Puertas niv 2
            if (niv2 != null)
            {
                if (niv2.Enabled)
                {
                    //Puerta abajo
                    if (niv2.EntroPuerta() == 2)
                    {
                        niv1 = new Nivel1(this, niv2.jugador, this.Window.ClientBounds.Width / 2, 50);
                        niv2.Enabled = false;
                        niv2.Hide();
                        Components.Remove(niv2);
                        Components.Add(niv1);
                        niv1.Show();
                    }
                    //Puerta izquierda
                    if (niv2.EntroPuerta() == 4)
                    {
                        niv3 = new Nivel3(this, niv2.jugador, this.Window.ClientBounds.Width - 70, this.Window.ClientBounds.Height/2);
                        niv2.Enabled = false;
                        niv2.Hide();
                        Components.Remove(niv2);
                        Components.Add(niv3);
                        niv3.Show();
                    }
                    //Puerta Arriba
                    if (niv2.EntroPuerta() == 1)
                    {
                        niv5 = new Nivel5(this, niv2.jugador, this.Window.ClientBounds.Width/2, this.Window.ClientBounds.Height -70);
                        niv2.Enabled = false;
                        niv2.Hide();
                        Components.Remove(niv2);
                        Components.Add(niv5);
                        niv5.Show();
                    }
                    //Puerta Derecha
                    if (niv2.EntroPuerta() == 3)
                    {
                        niv4 = new Nivel4(this, niv2.jugador, 50, this.Window.ClientBounds.Height / 2);
                        niv2.Enabled = false;
                        niv2.Hide();
                        Components.Remove(niv2);
                        Components.Add(niv4);
                        niv4.Show();
                    }
                }
            }
            //Puertas nivel 3
            if (niv3 != null)
            {
                if (niv3.Enabled)
                {
                    //Puerta abajo
                    if (niv3.EntroPuerta() == 3)
                    {
                        niv2 = new Nivel2(this, niv3.jugador, 50, this.Window.ClientBounds.Height/2);
                        niv3.Enabled = false;
                        niv3.Hide();
                        Components.Remove(niv3);
                        Components.Add(niv2);
                        niv2.Show();
                    }
                }
            }
            //Puertas nivel 4
            if (niv4 != null)
            {
                if (niv4.Enabled)
                {
                    //Puerta izq
                    if (niv4.EntroPuerta() == 4)
                    {
                        niv2 = new Nivel2(this, niv4.jugador, this.Window.ClientBounds.Width - 70, this.Window.ClientBounds.Height / 2);
                        niv4.Enabled = false;
                        niv4.Hide();
                        Components.Remove(niv4);
                        Components.Add(niv2);
                        niv2.Show();
                    }
                }
            }
            //Puertas nivel 5
            if (niv5 != null)
            {
                if (niv5.Enabled)
                {
                    //Puerta Abajo
                    if (niv5.EntroPuerta() == 2)
                    {
                        niv2 = new Nivel2(this, niv5.jugador, this.Window.ClientBounds.Width /2-20, this.Window.ClientBounds.Height -220);
                        niv5.Enabled = false;
                        niv5.Hide();
                        Components.Remove(niv5);
                        Components.Add(niv2);
                        niv2.Show();
                    }
                    //Puerta Arriba
                    if (niv5.EntroPuerta() == 1)
                    {
                        IsMouseVisible = true;
                        esct = new EscenaTeclado(this, niv5.jugador);
                        niv5.Enabled = false;
                        niv5.Hide();
                        Components.Remove(niv5);
                        Components.Add(esct);
                        esct.Show();
                    
                    }
                }
            }
            //Escena Teclado
            if (esct != null)
            {
                if (esct.Enabled)
                {
                    if (esct.Caracter == '+')
                    {
                        if (esct.Nombre == "SHEIK")
                        {
                            IsMouseVisible = false;
                            niv6 = new Nivel6(this, esct.jugador, this.Window.ClientBounds.Width / 2 - 20, this.Window.ClientBounds.Height - 70);
                            esct.Enabled = false;
                            Components.Remove(esct);
                            Components.Add(niv6);
                            niv6.Show();
                        }
                        else
                        {
                            IsMouseVisible = false;
                            niv5 = new Nivel5(this, esct.jugador, this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height - 70);
                            esct.Enabled = false;
                            Components.Remove(esct);
                            Components.Add(niv5);
                            niv5.Show();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
