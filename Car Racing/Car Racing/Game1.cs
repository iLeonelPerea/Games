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

namespace Car_Racing_Deluxe
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue audio;
        Cue motor;
        Cue motor2;
        Cue choque;
        Cue choque2;

        Texture2D mTrack;
        Texture2D menuPrincipal;
        Texture2D pistas;
        Texture2D presEnter;
        Texture2D presEnter2;
        Texture2D presEspacio;
        Texture2D presEspacio2;
        Texture2D mCar;
        Texture2D mCar2;
        Texture2D player;
        Texture2D player2;
        Texture2D mTrackOverlay;
        Texture2D meta;
        Texture2D menuPause;
        Texture2D menuMini;
        Texture2D menuSeleccion;
        Texture2D regresar;
        Texture2D restaurar;
        Texture2D controlesMando;
        Texture2D jugadores;
        Texture2D ganador;
        Texture2D [] lapsos;
        Texture2D porcentaje;

        RenderTarget2D mTrackRender;
        RenderTarget2D mTrackRenderRotated;
        RenderTarget2D m2TrackRender;
        RenderTarget2D m2TrackRenderRotated;

        Vector2 mCarPosition = new Vector2(758, 655);
        Vector2 mCar2Position = new Vector2(758, 705);
        Vector2 metaPosition = new Vector2(790 + 20, 607);

        int mCarHeight;
        int mCar2Height;
        int mCarWidth;
        int mCar2Width;
        int temp = 0;
        int lapso = 6;
        int sPista = 1;
        int mCarLapso = 6;
        int mCar2Lapso = 6;
        int mCarVida = 100;
        int mCar2Vida = 100;
        int tamaño;
        
        String s;
        
        char[] texto;
        
        float mCarRotation = 0;
        float mCar2Rotation = 0;

        double mCarVelocity = .2;
        double mCarVelocity2 = .2;
        double mCarScale = .3;
        double mCar2Velocity = .2;
        double mCar2Velocity2 = .2;
        double mCar2Scale = .3;

        Boolean bMenuPrincipal = true;
        Boolean bPistas = false;
        Boolean bMenuPause = false;
        Boolean bControles = false;
        Boolean bGanador = false;
        Boolean bCarMeta = false;
        Boolean bCar2Meta = false;
        Boolean bCarChoque = false;
        Boolean bCar2Choque = false;

        KeyboardState mPreviousKeyboard;

        Rectangle mCarArea;
        Rectangle mCar2Area;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // Cambiar la resolución a 800x600
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            audioEngine = new AudioEngine("audio.xgs");
            //Para cambiar estos nombres se debe cambiar la compilación XACT
            waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");    //ejecuta el Audio
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Crear el SpriteBatch utilizados para la elaboración de la Sprites.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Cargar las imágenes desde el ordenador en los objetos texture2D
            mTrack = Content.Load<Texture2D>("Track");
            mTrackOverlay = Content.Load<Texture2D>("TrackOverlay");
            mCar = Content.Load<Texture2D>("Car");
            mCar2 = Content.Load<Texture2D>("Car2");
            meta = Content.Load<Texture2D>("Meta");
            menuPrincipal = Content.Load<Texture2D>("menuPrincipal");
            pistas = Content.Load<Texture2D>("pistas");
            presEnter = Content.Load<Texture2D>("precioneEnter");
            presEnter2 = Content.Load<Texture2D>("precioneEnter2");
            presEspacio = Content.Load<Texture2D>("precioneEspacio");
            presEspacio2 = Content.Load<Texture2D>("precioneEspacio2");
            player = Content.Load<Texture2D>("player1G");
            player2 = Content.Load<Texture2D>("player2G");
            menuPause = Content.Load<Texture2D>("menuPause");
            menuMini = Content.Load<Texture2D>("menu");
            menuSeleccion = Content.Load<Texture2D>("menuSeleccion");
            controlesMando = Content.Load<Texture2D>("controlesMando");
            regresar = Content.Load<Texture2D>("regresar");
            restaurar = Content.Load<Texture2D>("restaurar");
            ganador = Content.Load<Texture2D>("ganador");
            jugadores = Content.Load<Texture2D>("jugadores");
            lapsos = new Texture2D[10];
            lapsos[0] = Content.Load<Texture2D>("cero_mini");
            lapsos[1] = Content.Load<Texture2D>("uno_mini");
            lapsos[2] = Content.Load<Texture2D>("dos_mini");
            lapsos[3] = Content.Load<Texture2D>("tres_mini");
            lapsos[4] = Content.Load<Texture2D>("cuatro_mini");
            lapsos[5] = Content.Load<Texture2D>("cinco_mini");
            lapsos[6] = Content.Load<Texture2D>("seis_mini");
            lapsos[7] = Content.Load<Texture2D>("siete_mini");
            lapsos[8] = Content.Load<Texture2D>("ocho_mini");
            lapsos[9] = Content.Load<Texture2D>("nueve_mini");
            porcentaje = Content.Load<Texture2D>("porcentaje");
            //Escala de la altura y la anchura del vehículo debidamente
            mCarWidth = (int)(mCar.Width * mCarScale);
            mCarHeight = (int)(mCar.Height * mCarScale);
            mCar2Width = (int)(mCar2.Width * mCar2Scale);
            mCar2Height = (int)(mCar2.Height * mCar2Scale);

            //Configuración de las metas de hacer que se utilizará para determinar si el coche está en la pista 
            mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, 1, SurfaceFormat.Color);
            mTrackRenderRotated = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, 1, SurfaceFormat.Color);
            m2TrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCar2Width + 100,
                mCar2Height + 100, 1, SurfaceFormat.Color);
            m2TrackRenderRotated = new RenderTarget2D(graphics.GraphicsDevice, mCar2Width + 100,
                mCar2Height + 100, 1, SurfaceFormat.Color);
            motor = soundBank.GetCue("carro");
            motor2 = soundBank.GetCue("carro2");
            choque = soundBank.GetCue("pared");
            choque2 = soundBank.GetCue("pared");
            audio = soundBank.GetCue("MenuPr");
            audio.Play();
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
            // TODO: Add your update logic here
            KeyboardState aKeyboard = Keyboard.GetState();
            
            //Comprueba si el juego se sale
            if (aKeyboard.IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }
            if (!bMenuPrincipal)
            {
                if (!bPistas)
                {
                    if (!bGanador)
                    {
                        //Menu Pause
                        if (aKeyboard.IsKeyDown(Keys.M) == true)
                        {
                            soundBank.PlayCue("click");
                            bMenuPause = true;
                        }
                        if (!bMenuPause)
                        {

                            //Rotar el sprite de coche con la flechas arriba y derecha 
                            //Car1
                            if (aKeyboard.IsKeyDown(Keys.Left) == true)
                            {
                                mCarRotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            else if (aKeyboard.IsKeyDown(Keys.Right) == true)
                            {
                                mCarRotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            //Car2
                            if (aKeyboard.IsKeyDown(Keys.X) == true)
                            {
                                mCar2Rotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                            }
                            else if (aKeyboard.IsKeyDown(Keys.V) == true)
                            {
                                mCar2Rotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
                            }

                            //Configuración del incremento del Movimiento. 
                            int aMove = (int)(200 * gameTime.ElapsedGameTime.TotalSeconds);

                            //Entradas para ver si se produjo una colisión.  Si una colisión no se produjo, a continuación, mueva el sprite
                            //Car1
                            if (CollisionOccurred(aMove, mCarRotation) == false)
                            {
                                if (aKeyboard.IsKeyDown(Keys.Up) == true)
                                {
                                    if (!motor.IsPlaying)
                                    {
                                        motor = soundBank.GetCue("carro");
                                        motor.Play();
                                    }
                                    if (bCarChoque)
                                    {
                                        mCarVida -= 5;
                                        bCarChoque = false;
                                    }
                                    mCarPosition.X += (float)(aMove * Math.Cos(mCarRotation) * mCarVelocity);
                                    mCarPosition.Y += (float)(aMove * Math.Sin(mCarRotation) * mCarVelocity);
                                    if (mCarVelocity < (double)2)
                                    {
                                        mCarVelocity += .02;
                                    }
                                }
                                else if (aKeyboard.IsKeyDown(Keys.Down) == true)
                                {
                                    if (!motor.IsPlaying)
                                    {
                                        motor = soundBank.GetCue("carro");
                                        motor.Play();
                                    }
                                    if (bCarChoque)
                                    {
                                        mCarVida -= 5;
                                        bCarChoque = false;
                                    }
                                    mCarPosition.X -= (float)(aMove * Math.Cos(mCarRotation) * mCarVelocity2);
                                    mCarPosition.Y -= (float)(aMove * Math.Sin(mCarRotation) * mCarVelocity2);
                                    if (mCarVelocity2 < (double)1)
                                    {
                                        mCarVelocity2 += .02;
                                    }
                                }
                                if (aKeyboard.IsKeyDown(Keys.Down) == false && aKeyboard.IsKeyDown(Keys.Up) == false)
                                {
                                    motor.Stop(AudioStopOptions.AsAuthored);
                                }
                                if (mCarLapso == 0 || mCar2Lapso == 0 || mCarVida == 0 || mCar2Vida == 0)
                                {
                                    if (mCarVida == 0)
                                        mCar2Lapso = 0;
                                    if (mCar2Vida == 0)
                                        mCarLapso = 0;
                                    bGanador = true;
                                    motor.Stop(AudioStopOptions.AsAuthored);
                                    motor2.Stop(AudioStopOptions.AsAuthored);
                                    audio.Stop(AudioStopOptions.AsAuthored);
                                    audio = soundBank.GetCue("victoria");
                                    audio.Play();
                                }
                                mCarArea.X = (int)mCarPosition.X;
                                mCarArea.Y = (int)mCarPosition.Y;
                                if (mCarArea.Intersects(new Rectangle((int)metaPosition.X + 20, (int)metaPosition.Y, meta.Width, meta.Height)) && !aKeyboard.IsKeyDown(Keys.Down))
                                {
                                    if (!bCarMeta)
                                    {
                                        if (mCarLapso > 0)
                                            mCarLapso--;
                                        bCarMeta = true;
                                    }
                                }
                                else
                                {
                                    bCarMeta = false;
                                }
                                if (aKeyboard.IsKeyDown(Keys.Up) == false)
                                {
                                    mCarVelocity = .2;
                                }
                                if (aKeyboard.IsKeyDown(Keys.Down) == false)
                                {
                                    mCarVelocity2 = .2;
                                }
                            }
                            else
                            {
                                motor.Stop(AudioStopOptions.AsAuthored);
                                if (!choque.IsPlaying)
                                {
                                    choque = soundBank.GetCue("pared");
                                    choque.Play();
                                }
                                mCarVelocity = .2;
                                mCarVelocity2 = .2;
                                bCarChoque = true;
                            }
                            mPreviousKeyboard = aKeyboard;
                            //Entradas para ver si se produjo una colisión.  Si una colisión no se produjo, a continuación, mueva el sprite
                            //Car2
                            if (CollisionOccurred2(aMove, mCar2Rotation) == false)
                            {
                                if (aKeyboard.IsKeyDown(Keys.D) == true)
                                {
                                    if (!motor2.IsPlaying)
                                    {
                                        motor2 = soundBank.GetCue("carro2");
                                        motor2.Play();
                                    }
                                    if (bCar2Choque)
                                    {
                                        mCar2Vida -= 5;
                                        bCar2Choque = false;
                                    }
                                    mCar2Position.X += (float)(aMove * Math.Cos(mCar2Rotation) * mCar2Velocity);
                                    mCar2Position.Y += (float)(aMove * Math.Sin(mCar2Rotation) * mCar2Velocity);
                                    if (mCar2Velocity < (double)2)
                                    {
                                        mCar2Velocity += .02;
                                    }
                                }
                                else if (aKeyboard.IsKeyDown(Keys.C) == true)
                                {
                                    if (!motor2.IsPlaying)
                                    {
                                        motor2 = soundBank.GetCue("carro2");
                                        motor2.Play();
                                    }
                                    if (bCar2Choque)
                                    {
                                        mCar2Vida -= 5;
                                        bCar2Choque = false;
                                    }
                                    mCar2Position.X -= (float)(aMove * Math.Cos(mCar2Rotation) * mCar2Velocity2);
                                    mCar2Position.Y -= (float)(aMove * Math.Sin(mCar2Rotation) * mCar2Velocity2);
                                    if (mCar2Velocity2 < (double)1)
                                    {
                                        mCar2Velocity2 += .02;
                                    }
                                }
                                if (aKeyboard.IsKeyDown(Keys.C) == false && aKeyboard.IsKeyDown(Keys.D) == false)
                                {
                                    motor2.Stop(AudioStopOptions.AsAuthored);
                                }
                                mCar2Area.X = (int)mCar2Position.X;
                                mCar2Area.Y = (int)mCar2Position.Y;
                                if (mCar2Area.Intersects(new Rectangle((int)metaPosition.X,(int)metaPosition.Y, meta.Width, meta.Height)) && !aKeyboard.IsKeyDown(Keys.C))
                                {
                                    if (!bCar2Meta)
                                    {
                                        if (mCar2Lapso > 0)
                                            mCar2Lapso--;
                                        bCar2Meta = true;
                                    }
                                }
                                else
                                {
                                    bCar2Meta = false;
                                }
                                if (aKeyboard.IsKeyDown(Keys.D) == false)
                                {
                                    mCar2Velocity = .2;
                                }
                                if (aKeyboard.IsKeyDown(Keys.C) == false)
                                {
                                    mCar2Velocity2 = .2;
                                }
                            }
                            else
                            {
                                if (!choque2.IsPlaying)
                                {
                                    choque2 = soundBank.GetCue("pared");
                                    choque2.Play();
                                }
                                motor2.Stop(AudioStopOptions.AsAuthored);
                                mCar2Velocity = .2;
                                mCar2Velocity2 = .2;
                                bCar2Choque = true;
                            }
                        }
                        else
                        {
                            if (aKeyboard.IsKeyDown(Keys.P) == true)
                            {
                                audio.Stop(AudioStopOptions.AsAuthored);
                                audio = soundBank.GetCue("MenuPr");
                                audio.Play();
                                soundBank.PlayCue("click");
                                bPistas = true;
                                bMenuPause = false;
                                resetear();
                            }
                            if (aKeyboard.IsKeyDown(Keys.R) == true)
                            {
                                audio.Stop(AudioStopOptions.AsAuthored);
                                audio = soundBank.GetCue("Game");
                                audio.Play();
                                soundBank.PlayCue("click");
                                bMenuPause = false;
                                resetear();
                            }
                            if (aKeyboard.IsKeyDown(Keys.O) == true)
                            {
                                soundBank.PlayCue("click");
                                bControles = true;
                            }
                            if (aKeyboard.IsKeyDown(Keys.E) == true && bControles)
                            {
                                soundBank.PlayCue("click");
                                bControles = false;
                            }
                            //Menu Pause
                            if (aKeyboard.IsKeyDown(Keys.T) == true && !bControles)
                            {
                                soundBank.PlayCue("click");
                                bMenuPause = false;
                            }
                        }
                    }
                    else
                    {
                        if (aKeyboard.IsKeyDown(Keys.Space) == true)
                        {
                            audio.Stop(AudioStopOptions.AsAuthored);
                            audio = soundBank.GetCue("MenuPr");
                            audio.Play();
                            soundBank.PlayCue("click");
                            bGanador = false;
                            bMenuPrincipal = true;
                            resetear();
                        }
                    }
                }
                else
                {
                    if (aKeyboard.IsKeyDown(Keys.A) == true)
                    {
                        soundBank.PlayCue("click");
                        sPista = 1;
                        bPistas = false;
                        audio.Stop(AudioStopOptions.AsAuthored);
                        audio = soundBank.GetCue("Game");
                        audio.Play();
                        resetear();
                    }
                    if (aKeyboard.IsKeyDown(Keys.I) == true)
                    {
                        soundBank.PlayCue("click");
                        sPista = 2;
                        bPistas = false;
                        audio.Stop(AudioStopOptions.AsAuthored);
                        audio = soundBank.GetCue("Game");
                        audio.Play();
                        resetear();
                    }
                }
            }
            //Menu principal
            if (aKeyboard.IsKeyDown(Keys.Enter) == true)
            {
                soundBank.PlayCue("click");
                bMenuPrincipal = false;
                bPistas = true;
            }
            mPreviousKeyboard = aKeyboard;

            base.Update(gameTime);
        }
        private void resetear()
        {
            mCarLapso = lapso;
            mCar2Lapso = lapso;
            mCarVida = 100;
            mCar2Vida = 100;
            if (sPista == 2)
            {
                mCarPosition = new Vector2(758, 655);
                mCar2Position = new Vector2(758, 705);
                metaPosition = new Vector2(790, 607);
                mTrack = Content.Load<Texture2D>("Track");
                mTrackOverlay = Content.Load<Texture2D>("TrackOverlay");
                lapso = 4;
            }
            if (sPista == 1)
            {
                mCarPosition = new Vector2(768, 525);
                mCar2Position = new Vector2(768, 575);
                metaPosition = new Vector2(810, 460);
                mTrack = Content.Load<Texture2D>("Track2");
                mTrackOverlay = Content.Load<Texture2D>("TrackOverlay2");
                lapso = 6;
            }
        }
        // Este método de control para ver si el Sprite se va a mover en un área que se 
        // no contienen todos los píxeles gris.  Si la cantidad de movimiento que causa un movimiento en un no-gris 
        // pixel, a continuación, una colisión se ha producido. 
        private bool CollisionOccurred(int aMove, float aCarRotation)
        {
            //Calcular la posición del coche y crear la textura de colisión.  Esta textura contendrá 
            // todos los píxeles que están directamente debajo del sprite en la actualidad en la imagen de pista.
            float aXPosition = (float)((-mCarWidth / 2) + (mCarPosition.X + (aMove * Math.Cos(aCarRotation))));
            float aYPosition = (float)((-mCarHeight / 2) + (mCarPosition.Y + (aMove * Math.Sin(aCarRotation))));
            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation);

            //Uso GetData para llenar una matriz con todos los colores de los píxeles en el área de la textura de colisión 
            int aPixels = (mCarWidth - 10) * (mCarHeight);
            Color[] myColors = new Color[aPixels];
            mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mCarWidth / 2 + 10, aCollisionCheck.Height / 2 - mCarHeight / 2, mCarWidth - 10, mCarHeight);
            aCollisionCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);

            //Ciclo a través de todos los colores de la matriz y ver si alguno de ellos 
            // no son gris.  Si uno de ellos no es gris, entonces el coche es la partida de la carretera 
            // y una colisión se ha producido
            bool aCollision = false;
            foreach (Color aColor in myColors)
            {
                //Si uno de los píxeles en esa zona no es gris, entonces el sprite se mueve 
                // fuera de la zona de movimiento permitido 
                if (aColor != Color.Gray && aColor != Color.Blue && aColor != new Color(102, 102, 102, 255))
                {
                    aCollision = true;
                    break;
                }
            }
            return aCollision;
        }
        // Este método de control para ver si el Sprite se va a mover en un área que se 
        // no contienen todos los píxeles gris.  Si la cantidad de movimiento que causa un movimiento en un no-gris 
        // pixel, a continuación, una colisión se ha producido. 
        private bool CollisionOccurred2(int aMove, float aCarRotation)
        {
            //Calcular la posición del coche y crear la textura de colisión.  Esta textura contendrá 
            // todos los píxeles que están directamente debajo del sprite en la actualidad en la imagen de pista.
            float aXPosition = (float)((-mCar2Width / 2) + (mCar2Position.X + (aMove * Math.Cos(aCarRotation))));
            float aYPosition = (float)((-mCar2Height / 2) + (mCar2Position.Y + (aMove * Math.Sin(aCarRotation))));
            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation);

            //Uso GetData para llenar una matriz con todos los colores de los píxeles en el área de la textura de colisión 
            int aPixels = (mCar2Width - 10) * (mCar2Height);
            Color[] myColors = new Color[aPixels];
            mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mCar2Width / 2 + 10, aCollisionCheck.Height / 2 - mCar2Height / 2, mCar2Width - 10, mCar2Height);
            aCollisionCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);

            //Ciclo a través de todos los colores de la matriz y ver si alguno de ellos 
            // no son gris.  Si uno de ellos no es gris, entonces el coche es la partida de la carretera 
            // y una colisión se ha producido
            bool aCollision = false;
            foreach (Color aColor in myColors)
            {
                //Si uno de los píxeles en esa zona no es gris, entonces el sprite se mueve 
                // fuera de la zona de movimiento permitido 
                if (aColor != Color.Gray && aColor != Color.Blue && aColor != new Color(102, 102, 102, 255))
                {
                    aCollision = true;
                    break;
                }
            }
            return aCollision;
        }

        //Crear la textura de la colisión que contiene la imagen girada Para la determinación de vías para la 
        // los píxeles bajo la srite de coches.
        //Car1
        private Texture2D CreateCollisionTexture(float theXPosition, float theYPosition, float aCarRotation)
        {
            //Se obtiene un cuadrado de la imagen de pista, que es alrededor del coche 
            graphics.GraphicsDevice.SetRenderTarget(0, m2TrackRender);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCar2Width + 100, mCar2Height + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCar2Width + 100, mCar2Height + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(0, null);

            Texture2D aPicture = m2TrackRender.GetTexture();

            //Rotar la instantánea de la zona que rodea el sprite de coches y de retorno que 
            graphics.GraphicsDevice.SetRenderTarget(0, m2TrackRenderRotated);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(0, null);

            return m2TrackRenderRotated.GetTexture();
        }
        //Crear la textura de la colisión que contiene la imagen girada Para la determinación de vías para la 
        // los píxeles bajo la srite de coches.
        //Car2
        private Texture2D CreateCollisionTexture2(float theXPosition, float theYPosition, float aCarRotation)
        {
            //Se obtiene un cuadrado de la imagen de pista, que es alrededor del coche 
            graphics.GraphicsDevice.SetRenderTarget(0, mTrackRender);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(0, null);

            Texture2D aPicture = mTrackRender.GetTexture();

            //Rotar la instantánea de la zona que rodea el sprite de coches y de retorno que 
            graphics.GraphicsDevice.SetRenderTarget(0, mTrackRenderRotated);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(0, null);

            return mTrackRenderRotated.GetTexture();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (!bMenuPrincipal)
            {
                if (!bPistas)
                {
                    if (!bGanador)
                    {
                        spriteBatch.Draw(mTrackOverlay, new Rectangle(0, 0, mTrackOverlay.Width, mTrackOverlay.Height), Color.White);
                        spriteBatch.Draw(menuMini, new Rectangle(1194, 0, menuMini.Width, menuMini.Height), Color.White);
                        spriteBatch.Draw(jugadores, new Rectangle(10, 580, jugadores.Width, jugadores.Height), Color.White);
                        spriteBatch.Draw(meta, new Rectangle((int)metaPosition.X, (int)metaPosition.Y, meta.Width, meta.Height), Color.White);
                        spriteBatch.Draw(mCar2, new Rectangle((int)mCar2Position.X, (int)mCar2Position.Y, mCar2Width, mCar2Height),
                            new Rectangle(0, 0, mCar2.Width, mCar2.Height), Color.White, mCar2Rotation,
                            new Vector2(mCar2.Width / 2, mCar2.Height / 2), SpriteEffects.None, 0);
                        spriteBatch.Draw(mCar, new Rectangle((int)mCarPosition.X, (int)mCarPosition.Y, mCarWidth, mCarHeight),
                            new Rectangle(0, 0, mCar.Width, mCar.Height), Color.White, mCarRotation,
                            new Vector2(mCar.Width / 2, mCar.Height / 2), SpriteEffects.None, 0);
                        s = "" + mCarLapso;
                        texto = s.ToCharArray();
                        tamaño = texto.Length;
                        for (int x = 0; x < tamaño; x++)
                            spriteBatch.Draw(lapsos[texto[x] - 48], new Rectangle(130, 617, lapsos[texto[x] - 48].Width, lapsos[texto[x] - 48].Height), Color.White);
                        s = "" + mCar2Lapso;
                        texto = s.ToCharArray();
                        tamaño = texto.Length;
                        for (int x = 0; x < tamaño; x++)
                            spriteBatch.Draw(lapsos[texto[x] - 48], new Rectangle(130, 701, lapsos[texto[x] - 48].Width, lapsos[texto[x] - 48].Height), Color.White);
                        s = "" + mCarVida;
                        texto = s.ToCharArray();
                        tamaño = texto.Length;
                        for (int x = 0; x < tamaño; x++)
                            spriteBatch.Draw(lapsos[texto[x] - 48], new Rectangle(130 + (x * 15), 637, lapsos[texto[x] - 48].Width, lapsos[texto[x] - 48].Height), Color.White);
                        spriteBatch.Draw(porcentaje, new Rectangle(130 + (tamaño * 15), 637, porcentaje.Width, porcentaje.Height), Color.White);
                        s = "" + mCar2Vida;
                        texto = s.ToCharArray();
                        tamaño = texto.Length;
                        for (int x = 0; x < tamaño; x++)
                            spriteBatch.Draw(lapsos[texto[x] - 48], new Rectangle(130 + (x * 15), 721, lapsos[texto[x] - 48].Width, lapsos[texto[x] - 48].Height), Color.White);
                        spriteBatch.Draw(porcentaje, new Rectangle(130 + (tamaño * 15), 721, porcentaje.Width, porcentaje.Height), Color.White);
                        if (!bMenuPause)
                        {
                            spriteBatch.Draw(menuMini, new Rectangle(1194, 0, menuMini.Width, menuMini.Height), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(menuPause, new Rectangle(420, 130, menuPause.Width, menuPause.Height), Color.White);
                            if (!bControles)
                            {
                                spriteBatch.Draw(menuSeleccion, new Rectangle(523, 275, menuSeleccion.Width, menuSeleccion.Height), Color.White);
                                spriteBatch.Draw(restaurar, new Rectangle(530, 535, restaurar.Width, restaurar.Height), Color.White);
                            }
                            else
                            {
                                spriteBatch.Draw(controlesMando, new Rectangle(510, 290, controlesMando.Width, controlesMando.Height), Color.White);
                                spriteBatch.Draw(regresar, new Rectangle(530, 535, regresar.Width, regresar.Height), Color.White);
                            }
                        }
                    }
                    else
                    {
                        motor.Stop(AudioStopOptions.AsAuthored);
                        motor2.Stop(AudioStopOptions.AsAuthored);
                        spriteBatch.Draw(ganador, new Rectangle(0, 0, menuPrincipal.Width, menuPrincipal.Height), Color.White);
                        if (mCar2Lapso == 0)
                            spriteBatch.Draw(player2, new Rectangle(770, 240, player2.Width, player2.Height), Color.White);
                        else
                            spriteBatch.Draw(player, new Rectangle(770, 240, player.Width, player.Height), Color.White);
                        if (temp <= 20)
                        {
                            spriteBatch.Draw(presEspacio, new Rectangle(380, 620, presEnter.Width, presEnter.Height), Color.White);
                        }
                        else if (temp <= 40)
                        {
                            spriteBatch.Draw(presEspacio2, new Rectangle(380, 620, presEnter2.Width, presEnter2.Height), Color.White);
                            temp = 0;
                        }
                        temp++;
                    }
                }
                else
                {
                    spriteBatch.Draw(pistas, new Rectangle(0, 0, pistas.Width, pistas.Height), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(menuPrincipal, new Rectangle(0, 0, menuPrincipal.Width, menuPrincipal.Height), Color.White);
                if (temp <= 20)
                {
                    spriteBatch.Draw(presEnter, new Rectangle(390, 620, presEnter.Width, presEnter.Height), Color.White);
                }
                else if(temp <=40)
                {
                    spriteBatch.Draw(presEnter2, new Rectangle(390, 620, presEnter2.Width, presEnter2.Height), Color.White);
                    temp = 0;
                }
                temp++;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}