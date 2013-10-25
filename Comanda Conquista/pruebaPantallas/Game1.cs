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
using pruebaPantallas;
using pruebaPantallas.Particles;
using GameStateManagement;

namespace pruebaPantallas
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        //ContentManager Content;
        public SpriteBatch spriteBatch;
        Texture2D fondo, status, vida, mapa;
        Rectangle pantalla;
        GameObject cannon;
        const int maxCannonBalls = 3;
        GameObject[] balas;
        MouseState previousMouseState;

        const int maximoEnemigos = 3;
        const float alturaEnemigo = 0.1f;
        const float minenemigoHeight = 0.5f;
        const float velocidadMax = 5.0f;
        const float velocidadMin = 1.0f;
        Random random = new Random();
        GameObject[] enemigos;

        private int score = 0;
        private SpriteFont fuente;

        private int vidas = 3;

        ParticleSystem explosion;
        ParticleSystem smoke;
        ParticleSystem explosionMia;

        GameObject shield;
        int shieldpower = 100;
        Vector2 shieldpowerDrawPoint = new Vector2(0.25f, 0.9f);

        const int maxEnemyCannonBalls = 10;
        GameObject[] balasEnemigo;

        FondoScroll fondoUno;
        FondoScroll fondoDos;
        FondoScroll fondoTres;
        FondoScroll fondoCuatro;

        public bool fin = false;

        //Pausa
        private bool pausado = false;

        private WaveBank waveBank;
        private SoundBank soundBank;

        public SoundBank SoundBank
        {
            get { return soundBank; }
            set { soundBank = value; }
        }
        private AudioEngine audioEngine;

        public AudioEngine AudioEngine
        {
            get { return audioEngine; }
            set { audioEngine = value; }
        }

        private int posX = 0;

        //Para el Jefe
        GameObject jefe;
        GameObject[] helice;
        GameObject[] torreta;
        GameObject[] misiles;
        GameObject[] balasJefe;
        GameObject[] misilesJefe;

        public Game1(ContentManager Content)
        {
            graphics = new GraphicsDeviceManager(this);
            this.Content = Content;
            this.Content.RootDirectory = "Content";
            explosion = new ExplosionParticleSystem(this, 1, "Sprites\\explosion");
            smoke = new ExplosionSmokeParticleSystem(this, 2, "Sprites\\smoke");
            explosionMia = new ExplosionSmokeParticleSystem(this, 2, "Sprites\\sanddust");
            explosion.LoadContent();
            smoke.LoadContent();
            explosionMia.LoadContent();
            Components.Add(smoke);
            Components.Add(explosion);
            Components.Add(explosionMia);
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            graphics.IsFullScreen = true;
//            Mouse.WindowHandle = this.Window.Handle;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize1()
        {
            //Mouse.WindowHandle
            // TODO: Add your initialization logic here
            fondoUno = new FondoScroll();
            fondoUno.escala = 1.0f;
            fondoDos = new FondoScroll();
            fondoDos.escala = 1.0f;
            fondoTres = new FondoScroll();
            fondoTres.escala = 1.0f;
            fondoCuatro = new FondoScroll();
            fondoCuatro.escala = 1.0f;
            audioEngine = new AudioEngine("sonidos.xgs");
            waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");
            soundBank.PlayCue("mundo1");
            //this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent1(ContentManager Content, SpriteBatch sb)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            //Para el fondo en scroll
            this.Content = Content;
            this.spriteBatch = sb;
            smoke.setSpriteBatch(sb);
            explosion.setSpriteBatch(sb);
            explosionMia.setSpriteBatch(sb);
            fondoUno.LoadContent(Content, "Sprites\\blastnya_ground");
            fondoUno.posicion = new Vector2(0, 0);
            fondoDos.LoadContent(Content, "Sprites\\blastnya_bg");
            fondoDos.posicion = new Vector2(0, 0);
            fondoTres.LoadContent(Content, "Sprites\\blastnya_bg2");
            fondoTres.posicion = new Vector2(0, 0);
            fondoCuatro.LoadContent(Content, "Sprites\\blastnya_sky");
            fondoCuatro.posicion = new Vector2(0, 0);
            mapa = Content.Load<Texture2D>("Sprites\\blastnya_map");
            fondo = Content.Load<Texture2D>("Sprites\\background");
            pantalla = new Rectangle(0, 0, 640, 480);
            cannon = new GameObject(this, Content.Load<Texture2D>("Sprites\\tank"), 2);
            //            mouse = Content.Load<Texture2D>("Sprites\\cursor_pointer");
            status = Content.Load<Texture2D>("Sprites\\statusbar");
            vida = Content.Load<Texture2D>("Sprites\\tankicon");
            fuente = Content.Load<SpriteFont>("fuente");
            cannon.Posicion = new Vector2(120, pantalla.Height - 100);
            balas = new GameObject[maxCannonBalls];
            for (int i = 0; i < maxCannonBalls; i++)
            {
                balas[i] = new GameObject(this, Content.Load<Texture2D>(
                    "Sprites\\bombfrag"), 1);
            }

            balasEnemigo = new GameObject[maxEnemyCannonBalls];
            for (int i = 0; i < maxEnemyCannonBalls; i++)
            {
                balasEnemigo[i] = new GameObject(this, Content.Load<Texture2D>(
                    "Sprites\\bombfrag"), 1);
            }

            enemigos = new GameObject[maximoEnemigos];
            for (int i = 0; i < maximoEnemigos; i++)
            {
                enemigos[i] = new GameObject(this,
                    Content.Load<Texture2D>("Sprites\\smalljet"), 1);
            }
            cannon.destinationRect = new Rectangle(100, pantalla.Width / 2, cannon.spriteWidth, cannon.spriteHeight);
            shield = new GameObject(this, Content.Load<Texture2D>("Sprites\\shield"), 1);
            shield.Posicion = new Vector2(cannon.Posicion.X, cannon.posicion.Y - 100);
            shield.destinationRect = cannon.destinationRect;
            //Jefe
            jefe = new GameObject(this, Content.Load<Texture2D>("Sprites\\Jefe\\hugecopter"), 50);
            jefe.posicion = new Vector2(-500, 100);
            jefe.Vivo = true;
            helice = new GameObject[2];
            for (int i = 0; i < 2; i++)
            {
                helice[i] = new GameObject(this, Content.Load<Texture2D>("Sprites\\Jefe\\copterblades"), 10);
                helice[i].Vivo = true;
            }
            torreta = new GameObject[2];
            for (int i = 0; i < 2; i++)
            {
                torreta[i] = new GameObject(this, Content.Load<Texture2D>("Sprites\\Jefe\\turret"), 6);
                torreta[i].Vivo = true;
            }
            misiles = new GameObject[2];
            for (int i = 0; i < 2; i++)
            {
                misiles[i] = new GameObject(this, Content.Load<Texture2D>("Sprites\\Jefe\\launcher"), 10);
                misiles[i].Vivo = true;
            }
            balasJefe = new GameObject[10];
            for (int i = 0; i < 10; i++)
            {
                balasJefe[i] = new GameObject(this, Content.Load<Texture2D>(
                    "Sprites\\Jefe\\orb"), 1);
            }
            misilesJefe = new GameObject[5];
            for (int i = 0; i < misilesJefe.Length; i++)
            {
                misilesJefe[i] = new GameObject(this, Content.Load<Texture2D>(
                    "Sprites\\Jefe\\rocket"), 1);
                misilesJefe[i].spriteWidth = 30;
                misilesJefe[i].spriteHeight = 30;
            }

            // raton2 = new GameObject(this, Content.Load<Texture2D>("Sprites\\cursor_pointer"));
            // raton2.destinationRect = new Rectangle(previousMouseState.X, previousMouseState.Y, mouse.Width, mouse.Height);
            base.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent1()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update1(GameTime gameTime, SpriteBatch sb)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            cannon.actualizaSprite(gameTime);
            shield.actualizaSprite(gameTime);
            //raton2.actualizaSprite(gameTime);
            ScrollGame(gameTime);
            //escuchaMouse();
            //escuchaTeclado();
            actualizaBalas();
             if (posX < 12000)
             {
                 actualizaEnemigos();
                 UpdateEnemyCannonBalls();
                 FireEnemyCannonBalls();
             }
             else if (posX >= 12000)
             {
                 actualizaJefe(gameTime);
                 retiraBalasEnemigas();
                 retiraEnemigos();
                 actualizaBalasJefe();
                 disparaBalasJefe();
                 actualizaMisilesJefe();
                 disparaMisilesJefe();
                 for (int i = 0; i < misilesJefe.Length; i++) {
                     misilesJefe[i].actualizaSprite(gameTime);
                 }
             }
            actualizaEscudo();
            checaColision(sb);
            cannon.Rotacion = MathHelper.Clamp(cannon.Rotacion, -MathHelper.Pi, 0);

            base.Update(gameTime);
        }

        public void UpdateEnemyCannonBalls()
        {
            foreach (GameObject bala in balasEnemigo)
            {
                if (bala.Vivo)
                {
                    bala.Posicion += bala.Velocidad;
                    if (!pantalla.Contains(new Point((int)bala.Posicion.X, (int)bala.Posicion.Y)))
                    {
                        bala.Vivo = false;
                        continue;
                    }
                }
            }
        }

        public void FireEnemyCannonBalls()
        {
            foreach (GameObject enemy in enemigos)
            {
                int fireCannonBall = random.Next(0, 50);
                if (fireCannonBall == 1)
                {
                    foreach (GameObject bala in balasEnemigo)
                    {
                        if (bala.Vivo == false)
                        {
                            bala.Vivo = true;
                            bala.Posicion = enemy.Posicion;
                            bala.Velocidad = new Vector2(0, 5.05f);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw1(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!pausado)
            {
                //GraphicsDevice.Clear(Color.CornflowerBlue);
                //spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                //spriteBatch.Draw(fondo, pantalla, Color.White);
                //Para el fondo scroll
                this.spriteBatch = spriteBatch;
                fondoCuatro.Draw(spriteBatch);
                fondoTres.Draw(spriteBatch);
                fondoDos.Draw(spriteBatch);
                fondoUno.Draw(spriteBatch);

                foreach (GameObject bala in balas)
                {
                    if (bala.Vivo)
                    {
                        spriteBatch.Draw(bala.Imagen,
                            bala.Posicion, Color.White);
                    }
                }
                 if (posX < 12000)
                 {
                     foreach (GameObject bala in balasEnemigo)
                     {
                         if (bala.Vivo)
                         {
                             spriteBatch.Draw(bala.Imagen,
                                 bala.Posicion, Color.Red);
                         }
                     }
                     foreach (GameObject enemigo in enemigos)
                     {
                         if (enemigo.Vivo)
                         {
                             spriteBatch.Draw(enemigo.Imagen,
                                 enemigo.Posicion, Color.White);
                         }
                     }
                 }
                 if (posX >= 12000 && jefe.Vivo)
                 {
                     spriteBatch.Draw(jefe.Imagen, jefe.Posicion, Color.White);
                     for (int i = 0; i < helice.Length; i++)
                     {
                         spriteBatch.Draw(helice[i].Imagen, helice[i].destinationRect, helice[i].sourceRect, Color.White);
                     }
                     for (int i = 0; i < misiles.Length; i++)
                     {
                         if (misiles[i].Vivo)
                             spriteBatch.Draw(misiles[i].Imagen, misiles[i].posicion, Color.White);
                     }
                     for (int i = 0; i < torreta.Length; i++)
                     {
                         if (torreta[i].Vivo)
                             spriteBatch.Draw(torreta[i].Imagen, torreta[i].destinationRect, torreta[i].sourceRect, Color.White);
                     }
                     for (int i = 0; i < balasJefe.Length; i++)
                     {
                         if (balasJefe[i].Vivo)
                             spriteBatch.Draw(balasJefe[i].Imagen, balasJefe[i].posicion, Color.White);
                     }
                     for (int i = 0; i < misilesJefe.Length; i++)
                     {
                         if (misilesJefe[i].Vivo)
                             spriteBatch.Draw(misilesJefe[i].Imagen, misilesJefe[i].destinationRect, misilesJefe[i].sourceRect, Color.White);
                     }
                 }
                spriteBatch.Draw(cannon.Imagen, cannon.destinationRect, cannon.sourceRect, Color.White);//, cannon.Rotacion, cannon.Centro, 1.0f, SpriteEffects.None, 0);
                if (shield.Vivo)
                    spriteBatch.Draw(shield.Imagen, shield.destinationRect, shield.sourceRect, Color.White);

                //Parte superior / Barra de datos
                spriteBatch.Draw(status, new Rectangle(0, 0, status.Width, status.Height), Color.White);
                for (int i = 0; i < vidas; i++)
                {
                    spriteBatch.Draw(vida, new Rectangle(25 * i, 8, vida.Width, vida.Height), Color.White);
                }

                spriteBatch.DrawString(fuente, "" + score, new Vector2(120, 4), Color.GreenYellow);
                for (int i = 0; i < shieldpower; i++)
                {
                    if (shieldpower < 50)
                        spriteBatch.DrawString(fuente, "|", new Vector2(440 + (i / 3), 4), Color.Red);
                    if (shieldpower >= 50 && shieldpower < 100)
                        spriteBatch.DrawString(fuente, "|", new Vector2(440 + (i / 3), 4), Color.Yellow);
                    if (shieldpower >= 100 && shieldpower < 150)
                        spriteBatch.DrawString(fuente, "|", new Vector2(440 + (i / 3), 4), Color.GreenYellow);
                    if (shieldpower >= 150 && shieldpower <= 200)
                        spriteBatch.DrawString(fuente, "|", new Vector2(440 + (i / 3), 4), Color.DarkGreen);
                }
                spriteBatch.Draw(mapa, new Vector2(205, 6), Color.White);
                spriteBatch.DrawString(fuente, "|", new Vector2(200 + (posX * 0.01f), 4), Color.Red);
                for (int i = 0; i < cannon.fuerza; i++)
                {
                    spriteBatch.DrawString(fuente, "  *  ", new Vector2(360 + (i * 15), 4), Color.GreenYellow);
                }
                //spriteBatch.Draw(raton2.Imagen, raton2.destinationRect,raton2.sourceRect, Color.White);
                //spriteBatch.End();
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
        }

        public void escuchaTeclado(KeyboardState teclado)
        {
            //KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Right))
            {
                cannon.posicion.X += 5;
            }
            if (teclado.IsKeyDown(Keys.Left))
            {
                cannon.posicion.X -= 5;
            }

            if (teclado.IsKeyDown(Keys.S))
            {
                cannon.posicion.X -= 5;
            }
            if (teclado.IsKeyDown(Keys.F))
            {
                cannon.posicion.X += 5;
            }
        }

        public void escuchaMouse(MouseState raton)
        {
            //MouseState raton = Mouse.GetState();
            //cursorX = Window.ClientBounds.X + Mouse.GetState().X;
            //cursorY = Window.ClientBounds.Y + Mouse.GetState().Y;
            Console.WriteLine("Posicion del mouse en X: " + raton.X);
            Console.WriteLine("Posicion del mouse en Y: " + raton.Y);
            //float ratonX = Window.ClientBounds.X;
            //float ratonY = Window.ClientBounds.Y;
            //MathHelper.cl
            //Console.WriteLine("Nueva Posicion del mouse en X: " + ratonX);
            //Console.WriteLine("Nueva Posicion del mouse en Y: " + ratonY);
            cannon.Rotacion = (float)Math.Atan2((/*ratonY*/raton.Y - cannon.posicion.Y), (/*ratonX*/raton.X -cannon.posicion.X));
            if (raton.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                disparaBala();
            }
            if (raton.RightButton == ButtonState.Pressed)
            {
                shield.Vivo = true;
            }
            previousMouseState = raton;
            // raton2.posicion.X = raton.X-300;
            // raton2.posicion.Y = raton.Y-80;
        }

        public void actualizaBalas()
        {
            foreach (GameObject bala in balas)
            {
                if (bala.Vivo)
                {
                    bala.Posicion += bala.Velocidad;
                    if (!pantalla.Contains(new Point(
                        (int)bala.Posicion.X,
                        (int)bala.Posicion.Y)))
                    {
                        bala.Vivo = false;
                        continue;
                    }
                }
            }
        }

        public void disparaBala()
        {
            foreach (GameObject bala in balas)
            {
                if (!bala.Vivo)
                {
                    bala.Vivo = true;
                    bala.posicion.X = cannon.posicion.X + cannon.destinationRect.Width / 2;
                    bala.posicion.Y = cannon.destinationRect.Y + 10;
                    bala.Velocidad = new Vector2(
                        (float)Math.Cos(cannon.Rotacion),
                        (float)Math.Sin(cannon.Rotacion)) * 5.0f;
                    soundBank.PlayCue("flak2");
                    return;
                }
            }
        }

        public void actualizaEnemigos()
        {
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo.Vivo)
                {
                    enemigo.Posicion += enemigo.Velocidad;
                    if (!pantalla.Contains(new Point((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y)))
                    {
                        enemigo.Vivo = false;
                    }
                }
                else
                {
                    enemigo.Vivo = true;
                    double x = random.NextDouble();
                    if (x < 0.5)
                    {
                        enemigo.Posicion = new Vector2(pantalla.Right, MathHelper.Lerp((float)pantalla.Height * minenemigoHeight, (float)pantalla.Height * alturaEnemigo, (float)random.NextDouble()));
                        enemigo.Velocidad = new Vector2(MathHelper.Lerp(-velocidadMin, -velocidadMax, (float)random.NextDouble()), 0);
                        enemigo.Imagen = Content.Load<Texture2D>("Sprites\\smalljet");
                    }
                    if (x >=0.5)
                    {
                        enemigo.Posicion = new Vector2(pantalla.Left, MathHelper.Lerp((float)pantalla.Height * minenemigoHeight, (float)pantalla.Height * alturaEnemigo, (float)random.NextDouble()));
                        enemigo.Velocidad = new Vector2(MathHelper.Lerp(velocidadMin, velocidadMax, (float)random.NextDouble()), 0);
                        enemigo.Imagen = Content.Load<Texture2D>("Sprites\\smalljet2");
                    }
                }

            }
        }

        public void checaColision(SpriteBatch spriteBatch)
        {
            //Checamos si no se sale de pantalla
            if (cannon.posicion.X < 0)
                cannon.posicion.X = 0;
            if (cannon.posicion.X + cannon.destinationRect.Width > pantalla.Width)
                cannon.posicion.X = pantalla.Width - cannon.destinationRect.Width - 1;
            //Colision de balas del canon contra las naves enemigas
            foreach (GameObject bala in balas)
            {
                if (bala.Vivo)
                {
                    Rectangle rBala = new Rectangle((int)bala.Posicion.X, (int)bala.Posicion.Y, (int)bala.Imagen.Width, (int)bala.Imagen.Height);
                    foreach (GameObject enemigo in enemigos)
                    {
                        if (enemigo.Vivo)
                        {
                            Rectangle rEnemigo = new Rectangle((int)enemigo.Posicion.X, (int)enemigo.Posicion.Y, (int)enemigo.Imagen.Width, (int)enemigo.Imagen.Height);
                            if (rBala.Intersects(rEnemigo))
                            {
                                bala.Vivo = false;
                                if (enemigo.fuerza > 0)
                                {
                                    enemigo.fuerza = enemigo.fuerza - 1;
                                }
                                if (enemigo.fuerza <= 0)
                                {
                                    enemigo.Vivo = false;
                                    smoke.addParticles(enemigo.Posicion);
                                    explosion.addParticles(enemigo.Posicion);
                                    soundBank.PlayCue("boom");
                                }
                                score += 10;
                            }
                        }
                    }
                }
            }
            //Checar balas enemigas contra mi
            foreach (GameObject bala in balasEnemigo)
            {
                Rectangle rBala = new Rectangle(
                           (int)bala.Posicion.X,
                           (int)bala.Posicion.Y,
                           bala.Imagen.Width,
                           bala.Imagen.Height);

                Rectangle rCannon = new Rectangle(
                    (int)cannon.Posicion.X,
                    (int)cannon.Posicion.Y,
                    cannon.destinationRect.Width,
                    cannon.destinationRect.Height);

                Rectangle rShield = new Rectangle(
                    (int)shield.Posicion.X,
                    (int)shield.Posicion.Y,
                    shield.destinationRect.Width,
                    shield.destinationRect.Height);

                if (shield.Vivo == true && rBala.Intersects(rShield) && bala.Vivo)
                {
                    bala.Vivo = false;
                }
                else if (rBala.Intersects(rCannon) && shield.Vivo == false && bala.Vivo)
                {
                    bala.Vivo = false;
                    if (cannon.fuerza > 0)
                    {
                        shieldpower = 20;
                        shield.Imagen = Content.Load<Texture2D>("Sprites\\shieldzap");
                        shield.Vivo = true;
                        cannon.fuerza = cannon.fuerza - 1;
                        soundBank.PlayCue("shock");
                    }
                    else if (cannon.fuerza <= 0)
                    {
                        vidas -= 1;
                        explosionMia.addParticles(cannon.posicion);
                        soundBank.PlayCue("boom");
                        cannon.posicion.X = -100;
                        while (cannon.posicion.X <= 0)
                            cannon.posicion += new Vector2(5, 0);
                        cannon.fuerza = 2;
                    }
                }
            }

            //Jefe
            foreach (GameObject bala in balas)
            {
                if (bala.Vivo)
                {
                    Rectangle rBala = new Rectangle((int)bala.Posicion.X, (int)bala.Posicion.Y, (int)bala.Imagen.Width, (int)bala.Imagen.Height);
                    //Que primero ataque torretas y launchers de misiles
                    foreach (GameObject torre in torreta)
                    {
                        if (torre.Vivo)
                        {
                            Rectangle rEnemigo = new Rectangle((int)torre.Posicion.X, (int)torre.Posicion.Y, (int)torre.destinationRect.Width, (int)torre.destinationRect.Height);
                            if (rBala.Intersects(rEnemigo))
                            {
                                bala.Vivo = false;
                                if (torre.fuerza > 0)
                                    torre.fuerza = torre.fuerza - 1;
                                else if (torre.fuerza <= 0 && torre.Vivo)
                                {
                                    torre.Vivo = false;
                                    smoke.addParticles(torre.Posicion);
                                    soundBank.PlayCue("boom");
                                }
                                score += 10;
                            }
                        }
                    }
                    foreach (GameObject launcher in misiles)
                    {
                        if (launcher.Vivo)
                        {
                            Rectangle rEnemigo = new Rectangle((int)launcher.Posicion.X, (int)launcher.Posicion.Y, (int)launcher.Imagen.Width, (int)launcher.Imagen.Height);
                            if (rBala.Intersects(rEnemigo))
                            {
                                bala.Vivo = false;
                                if (launcher.fuerza > 0)
                                    launcher.fuerza = launcher.fuerza - 1;
                                else if (launcher.fuerza <= 0 && launcher.Vivo)
                                {
                                    launcher.Vivo = false;
                                    smoke.addParticles(launcher.Posicion);
                                    soundBank.PlayCue("boom");
                                }
                                score += 10;
                            }
                        }
                    }
                    if (misiles[0].Vivo == false && misiles[1].Vivo == false && torreta[0].Vivo == false && torreta[1].Vivo == false)
                    {
                        if (jefe.Vivo)
                        {
                            Rectangle rEnemigo = new Rectangle((int)jefe.Posicion.X, (int)jefe.Posicion.Y, (int)jefe.Imagen.Width, (int)jefe.Imagen.Height);
                            if (rBala.Intersects(rEnemigo))
                            {
                                bala.Vivo = false;
                                if (jefe.fuerza > 0 && jefe.Vivo)
                                    jefe.fuerza = jefe.fuerza - 1;
                                else if (jefe.fuerza <= 0 && jefe.Vivo)
                                {
                                    jefe.Vivo = false;
                                    smoke.addParticles(jefe.Posicion);
                                    explosion.addParticles(jefe.posicion);
                                    soundBank.PlayCue("boom");
                                    smoke.addParticles(jefe.Posicion+new Vector2(400,100));
                                    explosion.addParticles(jefe.posicion + new Vector2(400, 100));
                                    smoke.addParticles(jefe.Posicion + new Vector2(300, 100));
                                    explosion.addParticles(jefe.posicion + new Vector2(300, 100));
                                    fin = true;
                                }
                                score += 10;
                            }
                        }
                    }//Fin del if para checar que ya no tenga nada el jefe mas que su cuerpo

                }
            }//Fin del foreach de las balas mias

            //Balas del jefe contra mi
            foreach (GameObject bala in balasJefe)
            {
                Rectangle rBala = new Rectangle(
                           (int)bala.Posicion.X,
                           (int)bala.Posicion.Y,
                           bala.Imagen.Width,
                           bala.Imagen.Height);

                Rectangle rCannon = new Rectangle(
                    (int)cannon.Posicion.X,
                    (int)cannon.Posicion.Y,
                    cannon.destinationRect.Width,
                    cannon.destinationRect.Height);

                Rectangle rShield = new Rectangle(
                    (int)shield.Posicion.X,
                    (int)shield.Posicion.Y,
                    shield.destinationRect.Width,
                    shield.destinationRect.Height);

                if (shield.Vivo == true && rBala.Intersects(rShield) && bala.Vivo)
                {
                    bala.Vivo = false;
                }
                else if (rBala.Intersects(rCannon) && shield.Vivo == false && bala.Vivo)
                {
                    bala.Vivo = false;
                    if (cannon.fuerza > 0)
                    {
                        shieldpower = 20;
                        shield.Imagen = Content.Load<Texture2D>("Sprites\\shieldzap");
                        shield.Vivo = true;
                        cannon.fuerza = cannon.fuerza - 1;
                        soundBank.PlayCue("shock");
                    }
                    else if (cannon.fuerza <= 0)
                    {
                        vidas -= 1;
                        explosionMia.addParticles(cannon.posicion);
                        soundBank.PlayCue("boom");
                        cannon.posicion.X = -100;
                        while (cannon.posicion.X <= 0)
                            cannon.posicion += new Vector2(5, 0);
                        cannon.fuerza = 2;
                    }
                }
            }

            //Misiles del jefe contra mi
            foreach (GameObject bala in misilesJefe)
            {
                Rectangle rBala = new Rectangle(
                           (int)bala.Posicion.X,
                           (int)bala.Posicion.Y,
                           bala.destinationRect.Width,
                           bala.destinationRect.Height);

                Rectangle rCannon = new Rectangle(
                    (int)cannon.Posicion.X,
                    (int)cannon.Posicion.Y,
                    cannon.destinationRect.Width,
                    cannon.destinationRect.Height);

                Rectangle rShield = new Rectangle(
                    (int)shield.Posicion.X,
                    (int)shield.Posicion.Y,
                    shield.destinationRect.Width,
                    shield.destinationRect.Height);

                if (shield.Vivo == true && rBala.Intersects(rShield) && bala.Vivo)
                {
                    bala.Vivo = false;
                }
                else if (rBala.Intersects(rCannon) && shield.Vivo == false && bala.Vivo)
                {
                    bala.Vivo = false;
                    vidas -= 1;
                    explosionMia.addParticles(cannon.posicion);
                    soundBank.PlayCue("boom");
                    cannon.posicion.X = -100;
                    while (cannon.posicion.X <= 0)
                        cannon.posicion += new Vector2(5, 0);
                    cannon.fuerza = 2;
                    shieldpower = 100;
                    shield.Vivo = true;
                }
            }
        }

        public void actualizaEscudo()
        {
            int shieldAdjustment = 1;
            if (shield.Vivo)
            {
                shieldAdjustment = -1;

            }
            shieldpower = (int)MathHelper.Clamp(shieldpower + shieldAdjustment, 0, 200);
            if (shieldpower == 0)
            {
                shield.Vivo = false;
                shield.Imagen = Content.Load<Texture2D>("Sprites\\shield");
            }
            shield.posicion = cannon.posicion;
        }

        public void ScrollGame(GameTime gameTime)
        {
            //Reiniciar el fondo si llego a su fin
            /*
             * Fondo1 - tierra
             * Fondo2 - bg
             * Fondo3 - bg2
             * Fondo4 - cielo
             */
            if (posX < 12000)
            {
                fondoUno.posicion.Y = pantalla.Height - fondoUno.size.Height;
                fondoDos.posicion.Y = pantalla.Height - fondoDos.size.Height;
                fondoTres.posicion.Y = pantalla.Height - fondoTres.size.Height;
                fondoCuatro.posicion.Y = pantalla.Height - fondoCuatro.size.Height;

                if (fondoUno.posicion.X + fondoUno.size.Width / 2 <= 0)
                {
                    fondoUno.posicion.X = 0;
                }
                if (fondoDos.posicion.X + fondoDos.size.Width / 2 <= 0)
                {
                    fondoDos.posicion.X = 0;
                }
                if (fondoTres.posicion.X + fondoTres.size.Width / 2 <= 0)
                {
                    fondoTres.posicion.X = 0;
                }
                if (fondoCuatro.posicion.X < -fondoCuatro.size.Width)
                {
                    fondoCuatro.posicion.X = pantalla.Width;
                }
                Vector2 direccion = new Vector2(-1, 0);
                Vector2 velocidad = new Vector2(160, 0);
                //Cambiar la posicion de cada capa del fondo
                fondoUno.posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
                fondoDos.posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
                fondoTres.posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                posX+=4;
            }

        }

        public int getVidas()
        {
            return vidas;
        }

        public void actualizaJefe(GameTime gameTime)
        {
            //Movimiento de la nave
            if (jefe.posicion.X + jefe.Imagen.Width > pantalla.Width)
                jefe.Velocidad = new Vector2(-3, 0);
            if (jefe.posicion.X < 0)
                jefe.Velocidad = new Vector2(3, 0);
            jefe.posicion += jefe.Velocidad;
            //Helices
            helice[0].posicion.X = jefe.posicion.X - 61;
            helice[0].posicion.Y = jefe.posicion.Y - 32;
            helice[0].frameCount = 5;
            helice[0].spriteWidth = 200;
            helice[0].spriteHeight = 50;
            helice[1].posicion.X = jefe.posicion.X + 183;
            helice[1].posicion.Y = jefe.posicion.Y - 32;
            helice[1].frameCount = 5;
            helice[1].spriteWidth = 200;
            helice[1].spriteHeight = 50;

            helice[0].actualizaSprite(gameTime);
            helice[1].actualizaSprite(gameTime);

            //Launchers
            misiles[0].posicion.X = jefe.posicion.X + 20;
            misiles[0].posicion.Y = jefe.posicion.Y + 85;
            misiles[1].posicion.X = jefe.posicion.X + 260;
            misiles[1].posicion.Y = jefe.posicion.Y + 85;

            //Lanzabalas
            torreta[0].posicion.X = jefe.posicion.X + 60;
            torreta[0].posicion.Y = jefe.posicion.Y + 95;
            torreta[0].frameCount = 21;
            torreta[0].spriteWidth = 60;
            torreta[0].spriteHeight = 40;
            torreta[1].posicion.X = jefe.posicion.X + 200;
            torreta[1].posicion.Y = jefe.posicion.Y + 95;
            torreta[1].frameCount = 21;
            torreta[1].spriteWidth = 60;
            torreta[1].spriteHeight = 40;

            int pos = 0;
            if (cannon.posicion.X > torreta[0].posicion.X)
                pos = 1;
            else if (cannon.posicion.X < torreta[0].posicion.X)
                pos = -1;
            torreta[0].actualizaSprite2(gameTime, pos);


            if (cannon.posicion.X > torreta[1].posicion.X)
                pos = 1;
            else if (cannon.posicion.X < torreta[1].posicion.X)
                pos = -1;
            torreta[1].actualizaSprite2(gameTime, pos);
        }


        public void retiraBalasEnemigas()
        {
            foreach (GameObject bala in balasEnemigo)
            {
                bala.Vivo = false;
            }
        }

        public void retiraEnemigos() {
            foreach (GameObject enemigo in enemigos) {
                enemigo.Vivo = false;
            }
        }

        public void actualizaBalasJefe()
        {
            if (torreta[0].Vivo || torreta[1].Vivo)
            {
                foreach (GameObject bala in balasJefe)
                {
                    if (bala.Vivo)
                    {
                        bala.Posicion += bala.Velocidad;
                        if (!pantalla.Contains(new Point((int)bala.Posicion.X, (int)bala.Posicion.Y)))
                        {
                            bala.Vivo = false;
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject bala in balasJefe)
                {
                    bala.Vivo = false;
                }
            }
        }

        public void disparaBalasJefe()
        {
            if (torreta[0].Vivo || torreta[1].Vivo)
            {
                int fireCannonBall = random.Next(0, 50);
                int i = random.Next(0, 2);
                if (fireCannonBall == 1)
                {
                    foreach (GameObject bala in balasJefe)
                    {
                        if (bala.Vivo == false)
                        {
                            bala.Vivo = true;
                            //Para que te sigan las balas
                            float rotacion = (float)Math.Atan2((torreta[i].posicion.Y - cannon.posicion.Y), (torreta[i].posicion.X - cannon.posicion.X));
                            if (torreta[i].Vivo)
                            {
                                bala.Posicion = torreta[i].Posicion;
                                bala.Velocidad = new Vector2(
                                -(float)Math.Cos(rotacion),
                                -(float)Math.Sin(rotacion)) * 5.0f;
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }

        public void actualizaMisilesJefe()
        {
            if (misiles[0].Vivo || misiles[1].Vivo)
            {
                foreach (GameObject misil in misilesJefe)
                {
                    if (misil.Vivo)
                    {
                        misil.Posicion += misil.Velocidad;
                        if (!pantalla.Contains(new Point((int)misil.Posicion.X, (int)misil.Posicion.Y)))
                        {
                            misil.Vivo = false;
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject misil in misilesJefe)
                {
                    misil.Vivo = false;
                }
            }
        }

        public void disparaMisilesJefe()
        {
            if (misiles[0].Vivo || misiles[1].Vivo)
            {
                int fireCannonBall = random.Next(0, 50);
                int i = random.Next(0, 2);
                if (fireCannonBall == 1)
                {
                    foreach (GameObject misil in misilesJefe)
                    {
                        if (misil.Vivo == false)
                        {
                            misil.Vivo = true;
                            //Para que te sigan los misiles
                            float rotacion = (float)Math.Atan2((misiles[i].posicion.Y - cannon.posicion.Y), (misiles[i].posicion.X - cannon.posicion.X));
                            if (misiles[i].Vivo)
                            {
                                misil.Posicion = misiles[i].Posicion;
                                misil.Velocidad = new Vector2(
                                -(float)Math.Cos(rotacion),
                                -(float)Math.Sin(rotacion)) * 5.0f;
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }
    }
}

