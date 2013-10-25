using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using BattleFieldX.Core;

namespace BattleFieldX
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //GraphicsDeviceManager graphics;
        GraphicsDeviceManager graphics;
        ContentManager content;
        protected SpriteBatch spriteBatch;
        private SpriteFont arialNormal, arialSeleccionada;
        private Texture2D tanqueR,tanqueD,tanqueL,tanqueU, fondo, balas, fondo2,cursor,block,intruc,muere,gana;
        private SpriteFont fuente;
        EscenaAccion escenaAccion;
        private KeyboardState oldKeyboardState, old;
        private bool pausado = false;
        private EscenaIntro escenaIntro;
        private Escena escenaActiva;
        private Pausa pausa;
        private Murio murio;
        private Gano gano;
        private Instrucciones intr;
        public Game1()
        {
            //graphics = new GraphicsDeviceManager(this);
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);
            
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
          //  audioComponent = new AudioComponent(this);
        //    Components.Add(audioComponent);
         //   Services.AddService(typeof(AudioComponent), audioComponent);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            muere = Content.Load<Texture2D>("gameover");
            gana = Content.Load<Texture2D>("Gana");

            tanqueR = Content.Load<Texture2D>("tanqueR");
            tanqueD = Content.Load<Texture2D>("tanqueD");
            tanqueL = Content.Load<Texture2D>("tanqueL");
            tanqueU = Content.Load<Texture2D>("tanqueU");
            cursor = Content.Load<Texture2D>("Cursor");
            fondo = Content.Load<Texture2D>("fondoJuego");
            fondo2 = Content.Load<Texture2D>("fondog");
            balas = Content.Load<Texture2D>("Balas");
            fuente = Content.Load<SpriteFont>("arial");
            block = Content.Load<Texture2D>("Block");
            intruc = Content.Load<Texture2D>("castle1");
            arialNormal = Content.Load<SpriteFont>("ArialNormal");
            arialSeleccionada = Content.Load<SpriteFont>("ArialSeleccionada");
            escenaIntro = new EscenaIntro(this,arialNormal,arialSeleccionada,fondo2);
            escenaAccion = new EscenaAccion(this, fondo, balas, tanqueR, tanqueD, tanqueL, tanqueU, fuente,cursor,block);
            pausa = new Pausa(this, arialNormal, arialSeleccionada, fondo);
            intr = new Instrucciones(this, arialNormal, arialSeleccionada, intruc);
            gano = new Gano(this, arialNormal, arialSeleccionada, gana);
            murio = new Murio(this, arialNormal, arialSeleccionada, muere);
            muere = Content.Load<Texture2D>("gameover");
            gana = Content.Load<Texture2D>("Gana");
            Components.Add(escenaIntro);
            Components.Add(escenaAccion);
            Components.Add(pausa);
            Components.Add(intr);
            escenaIntro.Show();
            escenaActiva = escenaIntro;
          
            
            
            
        }
        public void Resetear() {
            while (Components.Count > 0)
            {
                Components.RemoveAt(0);
            }
            tanqueR = Content.Load<Texture2D>("tanqueR");
            tanqueD = Content.Load<Texture2D>("tanqueD");
            tanqueL = Content.Load<Texture2D>("tanqueL");
            tanqueU = Content.Load<Texture2D>("tanqueU");
            cursor = Content.Load<Texture2D>("Cursor");
            fondo = Content.Load<Texture2D>("fondoJuego");
            fondo2 = Content.Load<Texture2D>("fondog");
            balas = Content.Load<Texture2D>("Balas");
            fuente = Content.Load<SpriteFont>("arial");
            block = Content.Load<Texture2D>("Block");
            intruc = Content.Load<Texture2D>("castle1");
            arialNormal = Content.Load<SpriteFont>("ArialNormal");
            arialSeleccionada = Content.Load<SpriteFont>("ArialSeleccionada");
            escenaIntro = new EscenaIntro(this, arialNormal, arialSeleccionada, fondo2);
            escenaAccion = new EscenaAccion(this, fondo, balas, tanqueR, tanqueD, tanqueL, tanqueU, fuente, cursor, block);
            pausa = new Pausa(this, arialNormal, arialSeleccionada, fondo);
            intr = new Instrucciones(this, arialNormal, arialSeleccionada, intruc);
            Components.Add(escenaIntro);
            Components.Add(escenaAccion);
            Components.Add(pausa);
            Components.Add(intr);
            escenaIntro.Show();
            escenaActiva = escenaIntro;
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

            //Jugar();
            ManejarEscenas();
            base.Update(gameTime);
        }

        public void ManejarEscenas() {
            if(escenaAccion.vidasA()==0){
                MostrarEscena(murio);
                Maneja();
            }
            if (escenaAccion.nivelX() == 7)
            {
                MostrarEscena(gano);
                Maneja();
            }
            if (escenaActiva == escenaAccion) {
               
                if (ChecaEsc())
                {
                    MostrarEscena(escenaIntro);
                    Resetear();
                }
                if (ChecaP())
                {
                   // Console.WriteLine("checap");
                    pausado = !pausado;
                    MostrarEscena(pausa);
                }
                if (!pausado)
                {                  
                     escenaAccion.Jugar();
                 }
                 
                
            }
            if (escenaActiva == escenaIntro) {
                ManejaEscenaIntro();
            } if (escenaActiva == pausa) {
                ManejaPausa();
            } if (escenaActiva == intr)
            {
                ManejaInt();
            }
            if (escenaActiva == murio || escenaActiva == gano) {
                Maneja();
            }
        }
        public void Maneja() { 
             if (ChecaEsc())
            {
                MostrarEscena(escenaIntro);
            }
        
        
        }
        public void ManejaInt() {
            if (ChecaEsc())
            {
                MostrarEscena(escenaIntro);
            }
        }
        public void ManejaPausa()
        {
            if (ChecaP())
            {
                MostrarEscena(escenaAccion);
            }
            if (ChecaEsc()) {
                MostrarEscena(escenaIntro);
            }
        }
        public void ManejaEscenaIntro() {
            if (ChecaEnter()) {
                switch (escenaIntro.SelectedMenuIndex)
                {
                    case 0:
                        MostrarEscena(intr);
                        break;
                    case 1:
                        //Jugar.
                        MostrarEscena(escenaAccion);
                        break;

                    case 2:
                        Exit();
                        break;
                }
            }
            
        }

        public bool ChecaEnter() {
            KeyboardState keyboardState = Keyboard.GetState();
            bool resultado = (oldKeyboardState.IsKeyDown(Keys.Enter) && (keyboardState.IsKeyUp(Keys.Enter)));
            oldKeyboardState = keyboardState;
            return resultado;
        }

        public bool ChecaEsc()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool resultado = (oldKeyboardState.IsKeyDown(Keys.Escape) && (keyboardState.IsKeyUp(Keys.Escape)));
            oldKeyboardState = keyboardState;
            return resultado;
        }
        public bool ChecaP()
        {
            
            KeyboardState keyboardState = Keyboard.GetState();
            bool resultado = (old.IsKeyDown(Keys.P) && (keyboardState.IsKeyUp(Keys.P)));
            old = keyboardState;
            return resultado;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
                base.Draw(gameTime);
            spriteBatch.End();
        }
        protected void MostrarEscena(Escena scene)
        {
            escenaActiva.Hide();
            escenaActiva = scene;
            scene.Show();
        }
    }
}
