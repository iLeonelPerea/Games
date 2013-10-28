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

namespace Tetris
{


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tetris;
        Texture2D fondo;
        Texture2D fondoT,titulo,fondoNext,imgSiguiente,imgPuntaje,fondoMenu;
        Vector2 pos;
        int cuboAncho = 25;
        int cuboAlto = 25;
        Rectangle tetrisRect;
        private EscenaJuego ej;
        private Menu menu;
        private SpriteFont fuente;
        protected KeyboardState tecladoAnterior = Keyboard.GetState();
        private Boolean pausa=false;
        private Texture2D[] arrImg;
        private Texture2D[] arrImg2;

        Song mysong,mysong2;

        private WaveBank waveBank;
        private SoundBank soundBank;
        private AudioEngine audioEngine;
        private Boolean jugarJuego =false,acabarJuego=false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 630;
            graphics.PreferredBackBufferWidth = 800;
            //graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            audioEngine = new AudioEngine("tetrisAudio.xgs");
            waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");

            Services.AddService(typeof(SoundBank), soundBank);

            pos = new Vector2(300, 0);

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

            tetris = Content.Load<Texture2D>("tetris");
            fondo = Content.Load<Texture2D>("forest");
            fondoT = Content.Load<Texture2D>("fondoT");
            titulo = Content.Load<Texture2D>("Titulo");
            fondoNext = Content.Load<Texture2D>("fondoNext");
            imgSiguiente = Content.Load<Texture2D>("siguiente");
            imgPuntaje = Content.Load<Texture2D>("puntaje");
            fuente = Content.Load<SpriteFont>("fuente");
            fondoMenu = Content.Load<Texture2D>("fondoMenu");

            Services.AddService(typeof(SpriteFont), fuente);
            
            mysong = Content.Load<Song>("mystery");
            mysong2 = Content.Load<Song>("Intro");
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = .2f;
            //MediaPlayer.Play(mysong);


            tetrisRect = new Rectangle((fondo.Width / 2) - (fondoT.Width / 2), 75, 100, 210);
            arrImg = new Texture2D[5];
            arrImg[0] = imgSiguiente;
            arrImg[1] = imgPuntaje;
            arrImg[2] = Content.Load<Texture2D>("fondoScore");
            arrImg[3] = Content.Load<Texture2D>("Lineas");
            arrImg[4] = Content.Load<Texture2D>("nivel");

            ej = new EscenaJuego(this,tetris , fondoT,fondo,tetrisRect,titulo,fondoNext,arrImg);
            Components.Add(ej);

            arrImg2 = new Texture2D[4];
            arrImg2[0] = Content.Load<Texture2D>("menuItem1");
            arrImg2[1] = Content.Load<Texture2D>("menuItem2");
            arrImg2[2] = Content.Load<Texture2D>("menuItem3");
            arrImg2[3] = Content.Load<Texture2D>("menuItem4");

            menu = new Menu(this,fondoMenu,arrImg2);
            Components.Add(menu);
            menu.show();
            //ej.Show();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1.0f;
            MediaPlayer.Play(mysong2);

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

            //pos.Y += (float)1/2;
            manejaEscena();
            if (jugarJuego)
            {
                KeyboardState teclado = Keyboard.GetState();
                if (teclado.IsKeyDown(Keys.P) && tecladoAnterior.IsKeyUp(Keys.P))
                {
                    if (pausa)
                    {
                        pausa = false;
                        MediaPlayer.Resume();
                    }
                    else
                    {
                        pausa = true;
                        MediaPlayer.Pause();
                    }
                }

                if (!pausa)
                {
                    ej.recuperar();
                    ej.jugar();
                }
                else
                {
                    ej.parar();
                }

                if (ej.SalirJuego)
                {
                    acabarJuego = true;
                }
                tecladoAnterior = teclado;
            }
            base.Update(gameTime);
        }

        public void manejaEscena()
        {
            if (!jugarJuego)
            {
                if (menu.Iniciar)
                {
                    jugarJuego = true;
                    ej.Show();
                    menu.hide();
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = .2f;
                    MediaPlayer.Play(mysong);
                }
                if (menu.Salir)
                {
                    this.Exit();
                }
            }
            if (acabarJuego)
            {
                menu.Comenzar();
                menu.show();
                Components.Remove(ej);
                ej = new EscenaJuego(this, tetris, fondoT, fondo, tetrisRect, titulo, fondoNext, arrImg);
                ej.Hide();
                Components.Add(ej);
                jugarJuego = false;
                acabarJuego = false;
                //MediaPlayer.Stop();
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 1.0f;
                MediaPlayer.Play(mysong2);
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
