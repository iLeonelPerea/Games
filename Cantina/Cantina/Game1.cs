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

namespace Cantina
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D fondo;
        private Texture2D frente;
        private Texture2D cuerpo;
        private Texture2D mano;
        private Texture2D vaso;
        private Texture2D trago1;
        private Texture2D trago2;
        private Texture2D trago3;

        private Texture2D caida1;
        private Texture2D caida2;
        private Texture2D caida3;
        private Texture2D caida4;
        private Texture2D caida5;
        private Texture2D caida6;
        private Texture2D caida7;
        private Texture2D caida8;
        private Texture2D caida9;

        private int vidas = 4;
        private Texture2D vidas4;
        private Texture2D vidas3;
        private Texture2D vidas2;
        private Texture2D vidas1;
        private Texture2D vidas0;

        private Texture2D gameOver;

        private Texture2D[] arregloTragos = new Texture2D[9];
        private Texture2D[] arregloCaida = new Texture2D[27];
        private int indiceArregloTragos = 0;
        private int indiceEnderezado = 0;
        private int indiceArregloCaida = 0;
        private int indiceShot = 0;
        private Vector2 posCuerpo;
        private Vector2 posCuerpo2;
        private Vector2 posMano;
        private Vector2 centroRotacion;
        private Vector2 posVaso;
        private Vector2 posTrago;
        private bool esTrago;
        private bool estaBalanceado;
        private bool enderezando;
        private bool nuevoShot;
        private bool auxiliar1;
        private bool entrada;
        private bool cayo;
        private float rotacion;
        private float intervalo;
        private float intervaloShot;
        private float hebriedad = 0.01f;
        private SpriteFont fuente;
        private String direccion = "";
        private int score = 0;
        System.Random generator = new System.Random();

        private WaveBank waveBank;
        private SoundBank soundBank;
        private AudioEngine audioEngine;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 403;
            graphics.PreferredBackBufferWidth = 545;

            Content.RootDirectory = "Content";
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

            audioEngine = new AudioEngine("SonidosCantina.xgs");
            waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");

            soundBank.PlayCue("oyeCantinero");

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

            fondo = Content.Load<Texture2D>("fondo");
            frente = Content.Load<Texture2D>("frente");
            cuerpo = Content.Load<Texture2D>("cuerpo");
            mano = Content.Load<Texture2D>("mano");
            vaso = Content.Load<Texture2D>("vaso");
            trago1 = Content.Load<Texture2D>("Trago_s01");
            trago2 = Content.Load<Texture2D>("Trago_s02");
            trago3 = Content.Load<Texture2D>("Trago_s03");

            caida1 = Content.Load<Texture2D>("piernasArr_s01");
            caida2 = Content.Load<Texture2D>("piernasArr_s02");
            caida3 = Content.Load<Texture2D>("piernasArr_s03");
            caida4 = Content.Load<Texture2D>("piernasArr_s04");
            caida5 = Content.Load<Texture2D>("piernasArr_s05");
            caida6 = Content.Load<Texture2D>("piernasArr_s06");
            caida7 = Content.Load<Texture2D>("piernasArr_s07");
            caida8 = Content.Load<Texture2D>("piernasArr_s08");
            caida9 = Content.Load<Texture2D>("piernasArr_s09");

            vidas0 = Content.Load<Texture2D>("vasoVidas0");
            vidas1 = Content.Load<Texture2D>("vasoVidas1");
            vidas2 = Content.Load<Texture2D>("vasoVidas2");
            vidas3 = Content.Load<Texture2D>("vasoVidas3");
            vidas4 = Content.Load<Texture2D>("vasoVidas4");

            gameOver = Content.Load<Texture2D>("GameOver");

            arregloTragos[0] = trago1;
            arregloTragos[1] = trago1;
            arregloTragos[2] = trago1;
            arregloTragos[3] = trago2;
            arregloTragos[4] = trago2;
            arregloTragos[5] = trago2;
            arregloTragos[6] = trago3;
            arregloTragos[7] = trago3;
            arregloTragos[8] = trago3;

            arregloCaida[0] = caida1;
            arregloCaida[1] = caida1;
            arregloCaida[2] = caida1;
            arregloCaida[3] = caida2;
            arregloCaida[4] = caida2;
            arregloCaida[5] = caida2;
            arregloCaida[6] = caida3;
            arregloCaida[7] = caida3;
            arregloCaida[8] = caida3;
            arregloCaida[9] = caida4;
            arregloCaida[10] = caida4;
            arregloCaida[11] = caida4;
            arregloCaida[12] = caida5;
            arregloCaida[13] = caida5;
            arregloCaida[14] = caida5;
            arregloCaida[15] = caida6;
            arregloCaida[16] = caida6;
            arregloCaida[17] = caida6;
            arregloCaida[18] = caida7;
            arregloCaida[19] = caida7;
            arregloCaida[20] = caida7;
            arregloCaida[21] = caida8;
            arregloCaida[22] = caida8;
            arregloCaida[23] = caida8;
            arregloCaida[24] = caida9;
            arregloCaida[25] = caida9;
            arregloCaida[26] = caida9;

            posCuerpo = new Vector2(223, 160);
            posCuerpo2 = new Vector2(259, 239);
            posVaso = new Vector2(570, 271);
            posMano.X = posCuerpo.X - 4;
            posMano.Y = posCuerpo.Y + 101;
            centroRotacion.X = cuerpo.Width / 2;
            centroRotacion.Y = cuerpo.Height / 2;
            posTrago.X = posMano.X;
            posTrago.Y = 234;
            esTrago = false;
            nuevoShot = false;
            enderezando = false;
            fuente = Content.Load<SpriteFont>("SpriteFont1");


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

            desbalancea();
            escuchaTeclado();
            if (enderezando) {
                enderezarMono();
            }//fin del if
            if (nuevoShot)
            {
                enviarNuevoShot();
            }//fin del if
            actualizaPosicion();

            base.Update(gameTime);
        } //fin del update

        public void actualizaPosicion()
        {
            posMano.X = (20f * rotacion) + 259 - 35;
            //posMano.Y = posCuerpo2.Y + 21;
            posCuerpo2.Y = (Math.Abs(rotacion) * 33.6f) + 239;  //para modificar la altura hay que cambiar el 239
            posCuerpo2.X = (45f * rotacion) + 259; //para modificar la posX hay que cambiar el 259
            posTrago.X = posMano.X;
        }//fin del actualiza Posicion

        public void desbalancea() {
            if (rotacion == 0 && auxiliar1==false)
            {
                float rnd = generator.Next();
                if (rnd < 0.5)
                {
                    rotacion = -0.03f;
                }
                else {
                    rotacion = 0.03f;
                }//fin del else
            }//fin del if    
            if(rotacion > 0 && rotacion < 0.8 && cayo == false){
                rotacion += (rotacion * hebriedad);
            }//fin del if
            if (rotacion < 0 && rotacion > -0.8 && cayo == false) {
                rotacion += (rotacion * hebriedad);
            }//fin del if
            if (rotacion < -0.8 || rotacion > 0.8) {
                cayo = true;
                rotacion = 0;
                soundBank.PlayCue("vidrio");
            }//fin del if
        }//fin del desbalancea

        public void enviarNuevoShot() { 
            if(indiceShot == 0){
                intervaloShot = (posVaso.X - (posMano.X + mano.Width)) / 9;
            }//fin del if
            posVaso.X -= intervaloShot;
            indiceShot++;
            if (indiceShot > 9) {
                nuevoShot = false;
                indiceShot = 0;
                posVaso.X = 570;
            }
        }//fin de enviarNuevoShot

        public void enderezarMono()
        { //rotacion < 0 && rotacion > 0.18
            if (rotacion < -0.16 || rotacion > 0.16)
            {

                if (rotacion < 0)
                {
                    rotacion -= intervalo;
                }
                if (rotacion > 0)
                {
                    rotacion -= intervalo;
                }
            }//fin del if
            else
            {
                enderezando = false;
            }
            if (rotacion > 0.8 || rotacion < -0.8) {
                enderezando = false;

            }//fin del if
        }//fin de enderezar

        public void escuchaTeclado()
        {
            double circunferencia = MathHelper.Pi * 2;
            KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Left) && rotacion > -0.8 && !teclado.IsKeyDown(Keys.Space))
            {
                rotacion = (float)((rotacion - 0.03f) % circunferencia);
                auxiliar1 = true;

            }
            if (teclado.IsKeyDown(Keys.Right) && rotacion < 0.8 && !teclado.IsKeyDown(Keys.Space))
            {
                rotacion = (float)((rotacion + 0.03f) % circunferencia);
                auxiliar1 = true;

            }
            if (teclado.IsKeyDown(Keys.Space))
            {
                if (!esTrago && !enderezando && vidas>0)
                {
                    nuevoShot = true;
                    esTrago = true;
                    enderezando = true;
                    intervalo = rotacion / 4f;
                    score += 10;
                    hebriedad += 0.005f;
                }
                if (vidas <= 0) {
                    vidas = 4;
                    score = 0;
                    hebriedad = 0.01f;
                    rotacion = 0;
                }
            }//fin del if

            if (teclado.IsKeyUp(Keys.Left) && teclado.IsKeyUp(Keys.Right)) {
                auxiliar1 = false;
            }

        }//fin de escucha teclado

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (vidas > 0)
            {
                spriteBatch.Draw(fondo, new Vector2(0, 0), Color.White);

                if (cayo == false)
                {
                    spriteBatch.Draw(cuerpo, posCuerpo2, null, Color.White, rotacion, centroRotacion, 1.0f, SpriteEffects.None, 1);
                }//fin del if
                else
                {
                    spriteBatch.Draw(arregloCaida[indiceArregloCaida], new Vector2(posCuerpo.X - 20, posCuerpo.Y + 20), Color.White);
                    indiceArregloCaida++;
                    if (indiceArregloCaida >= arregloCaida.Length)
                    {
                        rotacion = 0f;
                        hebriedad = 0.01f;
                        cayo = false;
                        indiceArregloCaida = 0;
                        vidas--;
                    }//fin del if
                }//fin del else
                spriteBatch.Draw(frente, new Vector2(0, 0), Color.White);

                if (cayo == false)
                {
                    if (esTrago)
                    {
                        spriteBatch.Draw(arregloTragos[indiceArregloTragos], posTrago, Color.White);
                        if (indiceArregloTragos == 4) {
                            soundBank.PlayCue("gulp");
                        }
                        indiceArregloTragos++;
                        if (indiceArregloTragos >= arregloTragos.Length)
                        {

                            esTrago = false;
                            indiceArregloTragos = 0;

                        }//fin del if
                    }//fin del if
                    else
                    {
                        spriteBatch.Draw(mano, posMano, Color.White);
                    }//fin del else
                }

                spriteBatch.Draw(vaso, posVaso, Color.White);

                //spriteBatch.DrawString(fuente, "rotacion: " + rotacion, new Vector2(100, 20), Color.Blue);
                //spriteBatch.DrawString(fuente, "Pos Y: " + posCuerpo2.Y, new Vector2(100, 40), Color.White);
                //spriteBatch.DrawString(fuente, "Pos X: " + posCuerpo2.X, new Vector2(100, 60), Color.White);
                spriteBatch.DrawString(fuente, "" + score, new Vector2(24, 348), Color.Black);

                if (vidas == 4)
                {
                    spriteBatch.Draw(vidas4, new Vector2(380, 348), Color.White);
                }
                if (vidas == 3)
                {
                    spriteBatch.Draw(vidas3, new Vector2(380, 348), Color.White);
                }
                if (vidas == 2)
                {
                    spriteBatch.Draw(vidas2, new Vector2(380, 348), Color.White);
                }
                if (vidas == 1)
                {
                    spriteBatch.Draw(vidas1, new Vector2(380, 348), Color.White);
                }
                if (vidas == 0)
                {
                    spriteBatch.Draw(vidas0, new Vector2(380, 348), Color.White);
                }
            }//fin del juego (con vidas).
            else { 
                //aqui va la pantalla final (cuando se acabo el juego)
                spriteBatch.Draw(gameOver,new Vector2(0,0),Color.White);
                spriteBatch.DrawString(fuente, "Puntaje: " + score, new Vector2(35, 117), Color.White);
                spriteBatch.DrawString(fuente, "Presiona espacio para volver a jugar", new Vector2(35, 147), Color.White);
            }//fin del else
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
