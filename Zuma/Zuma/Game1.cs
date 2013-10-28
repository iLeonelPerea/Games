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

namespace Zuma
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Atributos
        GraphicsDeviceManager graphics; //Entorno grafico
        SpriteBatch spriteBatch;        //Diguja en el distpositivo grafico
        //Fuente
        private SpriteFont fuente;
        //Texturas
        private Texture2D spriteBola;
        private Texture2D camino;
        private Texture2D nivel1;
        private Texture2D spriteRana;
        private Texture2D spriteRanaB;
        private Texture2D salida;
        private Texture2D spritePausa;
        private Texture2D spriteGanaste;
        private Texture2D spriteGameOver;
        private Texture2D barraZuma;
        //Elementos de juego
        private GameObject rana;
        private Vector2 posRefRana;
        private List<Vector2> posBolas;
        private List<GameObject> listaCamino;
        private List<GameObject> listaBalas = new List<GameObject>();
        private int espera;
        private int numBolas;
        private int incX, incY;
        private int score=0, barra=0;
        private bool barraCompleta = false;
        private bool inicio = true, inicio2 = true, terminar =false, terminar2 = true;
        private bool ganar = false, gameOver = false;
        private bool pausa = false;
        private Random random = new Random();
        private Color[] colores;
        //Colisiones
        private Rectangle rectFin;
        // The color data for the images; used for per pixel collision
        Color[] bolaTextureData;
        Color[] caminoTextureData;
        //Keyboard
        private KeyboardState previousKeyboardState = Keyboard.GetState();
        //Mouse
        private MouseState mouse, mouseStatePrevious = Mouse.GetState();
        private Texture2D cursor;
        private Vector2 posicionMouse;
        //Pantalla
        private Viewport pantalla;
        private Rectangle rectPantalla;
        ///Audio
        private WaveBank waveBank;
        private SoundBank soundBank;
        private AudioEngine audioEngine;
        Song mysong;

        //CONSTRUCTOR
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 603;
            graphics.PreferredBackBufferWidth = 802;
            Content.RootDirectory = "Content";
            //this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Audio
            audioEngine = new AudioEngine("AudioZuma.xgs");
            waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");
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
            //fuente
            fuente = Content.Load<SpriteFont>("FuenteArial");
            //texturas
            spriteBola = Content.Load<Texture2D>("bolaVerde");
            camino = Content.Load<Texture2D>("camino");
            nivel1 = Content.Load<Texture2D>("nivel1");
            spriteRana = Content.Load<Texture2D>("rana");
            spriteRanaB = Content.Load<Texture2D>("rana2");
            salida = Content.Load<Texture2D>("salida");
            spritePausa = Content.Load<Texture2D>("pausa");
            spriteGanaste = Content.Load<Texture2D>("ganaste");
            spriteGameOver = Content.Load<Texture2D>("gameOver");
            barraZuma = Content.Load<Texture2D>("barraZuma");
            //Audio
            mysong = Content.Load<Song>("song");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = .1f;
            MediaPlayer.Play(mysong);
            //pantalla
            pantalla = graphics.GraphicsDevice.Viewport;
            rectPantalla = new Rectangle(0,0,camino.Width,camino.Height);
            // Extract collision data
            bolaTextureData =
                new Color[spriteBola.Width * spriteBola.Height];
            spriteBola.GetData(bolaTextureData);
            caminoTextureData =
                new Color[camino.Width * camino.Height];
            camino.GetData(caminoTextureData);
            //Mouse
            cursor = Content.Load<Texture2D>("cursor1");
            //Inicializar variables
            incX = 0;
            incY = 1;
            espera = 40;
            numBolas = 1;
            colores = new Color[5];
            colores[0] = Color.Blue;
            colores[1] = Color.Green;
            colores[2] = Color.Red;
            colores[3] = Color.Yellow;
            colores[4] = Color.DarkSlateGray;
            //Agregar primera Bola en el Camino
            posBolas = new List<Vector2>();
            listaCamino = new List<GameObject>();
            GameObject bola1 = new GameObject(ref spriteBola);
            bola1.posVector = 0;
            listaCamino.Insert(0, bola1);
            bola1.color = colores[4];
            //Rana
            rana = new GameObject(ref spriteRana);
            rana.posicion = new Vector2(pantalla.Width / 2, pantalla.Height / 2); 
            posRefRana = new Vector2(pantalla.Width / 2 - spriteRana.Width / 2, pantalla.Height / 2 - spriteRana.Height / 2);
            GameObject bola = new GameObject(ref spriteBola);
            GameObject bola2 = new GameObject(ref spriteBola);
            bola.posicion = new Vector2(pantalla.Width / 2 + 33, pantalla.Height / 2);
            bola2.posicion = new Vector2(pantalla.Width / 2 - 32, pantalla.Height / 2);
            listaBalas.Add(bola);
            listaBalas.Add(bola2);
            //Colisiones
            rectFin = new Rectangle(175,350,40,40);
            //Obtener vector de posiciones del camino
            Vector2 posicion = new Vector2(51 - spriteBola.Width / 2, 32);
            posBolas.Insert(0, posicion);
            SeguirCamino();
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
            EscuchaTeclado();
            if (!pausa && !ganar && !gameOver)
            {
                EmpezarJuego();
                EscuchaMouse();
                ChecarBarra();
                ActualizarBolas();
                AgregarBola();
                //agregarEsferas();
                //actualizaEsferas();
                ChecaColision();
                TerminarJuego();
            }

            base.Update(gameTime);
        }

        public void EmpezarJuego(){
            if (inicio)
            {
                if (inicio2)
                {
                    soundBank.PlayCue("inicio2");
                    inicio2 = false;
                }
                for (int i = 0; i < listaCamino.Count; i++)
                {
                    GameObject bola = listaCamino[i];
                    bola.posVector += 10;
                }
                if (listaCamino.Count > 50) 
                {
                    inicio = false;
                }
            }
        }
        public void TerminarJuego()
        {
            if (terminar)
            {
                if (terminar2)
                {
                    soundBank.PlayCue("inicio2");
                    terminar2 = false;
                }
                for (int i = 0; i < listaCamino.Count; i++)
                {
                    GameObject bola = listaCamino[i];
                    bola.posVector += 20;
                }
                if (listaCamino[listaCamino.Count - 1].posVector > posBolas.Count - 1)
                {
                    //this.Exit();
                    gameOver = true;
                }
            }
            
        }

        public void ActualizarBolas()
        {
            //Actualizar Balas
            foreach (GameObject bala in listaBalas)
            {
                bala.posicion += bala.velocidad;
            }
            //Actualizar Bolas
            for (int i = 0; i < listaCamino.Count; i++)
            {
                double circunferencia = MathHelper.Pi * 2;
                GameObject bola = listaCamino[i];
                if (i + 1 < listaCamino.Count)
                {
                    if (!(listaCamino[i + 1].posVector < bola.posVector - 40))//si la siguiente bola no esta enseguida (40 posiciones despues)
                    {
                        bola.posVector += 1;
                        bola.rotacion = (float)((bola.posVector * -.04f) % circunferencia);//bola.rotacion - .05f
                    }
                    else
                    {
                        /*if (listaCamino[i + 1].color == bola.color)
                        {
                            bola.posVector -= 3;
                            if (bola.posVector - 5 >= listaCamino[i + 1].posVector)
                            {
                                DestruirBola(bola, i);
                            }
                        }*/
                    }
                }
                else 
                { 
                    bola.posVector += 1;
                    bola.rotacion = (float)((bola.posVector * -.04f) % circunferencia);
                }
                
                if (bola.nueva)//bola.nueva
                {
                    AcomodarBola(bola,i);
                }
                EstabilizarBola(bola);
            }
        }

        private void AcomodarBola(GameObject bola, int index)
        {
            Vector2 posV;
            if (bola.posVector < 0)
                posV = posBolas[0];
            else if (bola.posVector >= posBolas.Count)
                posV = posBolas[posBolas.Count - 1];
            else
                posV = posBolas[bola.posVector];
            int i = 0;
            while (i < 5)
            {
                Vector2 pos = bola.posicion - posV;
                if (pos != Vector2.Zero)
                {
                    if (pos.X > 0)
                        bola.posicion.X -= 1;
                    else if (pos.X < 0)
                        bola.posicion.X += 1;
                    if (pos.Y > 0)
                        bola.posicion.Y -= 1;
                    else if (pos.Y < 0)
                        bola.posicion.Y += 1;
                }
                pos = bola.posicion - posV;
                if ((int)bola.posicion.Y == (int)posV.Y && (int)bola.posicion.X == (int)posV.X)
                {
                    //DESTRUIR
                    DestruirBola(bola, index);
                    bola.nueva = false;
                }
                i++;
            }
        }
        private void EstabilizarBola(GameObject bola)
        {
            Vector2 posV;
            if (bola.posVector < 0)
                posV = posBolas[0];
            else if (bola.posVector >= posBolas.Count)
                posV = posBolas[posBolas.Count - 1];
            else
                posV = posBolas[bola.posVector];
            int i = 0;
            while (i < 10)
            {
                Vector2 pos = bola.posicion - posV;
                if (pos != Vector2.Zero)
                {
                    if (pos.X > 0)
                        bola.posicion.X -= 1;
                    else if (pos.X < 0)
                        bola.posicion.X += 1;
                    if (pos.Y > 0)
                        bola.posicion.Y -= 1;
                    else if (pos.Y < 0)
                        bola.posicion.Y += 1;
                }
                pos = bola.posicion - posV;
                if ((int)bola.posicion.Y == (int)posV.Y && (int)bola.posicion.X == (int)posV.X)
                {
                    bola.cambio = false;
                }
                i++;
            }
        }
        private void DestruirBola(GameObject bola, int index)
        {
            Color color = bola.color;
            //Color color = listaCamino[index].color;
            //recorrido hacia adelante
            bool seguir = true;
            int i=0, num=1;
            while (seguir && index + (i+1) < listaCamino.Count)
            {
                i++;
                if (color == listaCamino[index + i].color)
                {
                    num++;
                }
                else
                {
                    seguir = false;
                    Console.WriteLine("Repeticines hacia adelante "+ num);
                }
                
            }
            //recorrido hacia atras
            seguir = true;
            int a = 0;
            while (seguir && (index - (a+1) > 0))
            {
                a++;
                //Para evitar ERROR
                if (index - a < listaCamino.Count)
                {
                    if (color == listaCamino[index - a].color)
                    {
                        num++;
                    }
                    else
                    {
                        seguir = false;
                        Console.WriteLine("Repeticines hacia atras " + num);
                    }
                }
            }
            //Destruir si hay colores consecutivos iguales
            if (num >= 3)
            {
                Console.WriteLine("Se elimina de: " + (index-a)+" a "+(index-a+num));
                /*for (int c = index - a; (c < (index-a+num)); c++)
                {
                    listaCamino.RemoveAt(c);
                    Console.WriteLine("Eliminado: " + c);
                    c--;
                    num--;
                    //continue;
                }*/
                for (int c = index + i; (c > (index + i - num)); c--)
                {
                    listaCamino.RemoveAt(c-1);
                    Console.WriteLine("Eliminado: " + (c-1));
                }
                int temp = num - 3;
                score += 100 + temp * 50;
                soundBank.PlayCue("destruir");
            }
        }

        public void ChecarBarra()
        {
            if (score/10 < barraZuma.Width)
                barra = score/10;
            else
            {
                barraCompleta = true;
                barra = barraZuma.Width;
            }
            if (barraCompleta && listaCamino.Count <= 1)
            {
                ganar = true;
            }
        }
        
        public void AgregarBola(){
            GameObject bola = listaCamino[listaCamino.Count-1];
            if (bola.posVector > 40 && !terminar && !barraCompleta)
            {
                numBolas++;
                GameObject nueva = new GameObject(ref spriteBola);//GameObject()
                nueva.posVector = 0;
                int c = (int)(random.NextDouble() * (4));
                nueva.color = colores[c];
                listaCamino.Add(nueva);
            }
        }

        public void ChecaColision()
        {
            //BALAS con pantalla
            for (int i = 0; i < listaBalas.Count; i++)
            {
                GameObject go = listaBalas[i];
                if ((go.posicion.X - go.sprite.Width / 2 <= 0) || (go.posicion.X + go.sprite.Width / 2 >= pantalla.Width ) || 
                    (go.posicion.Y - go.sprite.Height / 2 <= 0) || (go.posicion.Y + go.sprite.Height / 2 >= pantalla.Height))
                {
                    listaBalas.RemoveAt(i);
                    i--;
                    continue;
                }
                listaBalas[i] = go;
            }
            //Colision bolas con BALAS
            for (int i = 0; i < listaCamino.Count; i++)
            {
                GameObject bola = listaCamino[i];
                Vector2 pos;
                if (bola.posVector < 0)
                    pos = posBolas[0];
                else if (bola.posVector >= posBolas.Count)
                    pos = posBolas[posBolas.Count - 1];
                else
                    pos = posBolas[bola.posVector];
                Rectangle bolaRect = new Rectangle((int)pos.X-spriteBola.Width/2, (int)pos.Y-spriteBola.Height/2, spriteBola.Width, spriteBola.Height);
                for(int c=0; c<listaBalas.Count; c++) 
                {
                    GameObject bala = listaBalas[c];
                    Rectangle balaRect = new Rectangle((int)bala.posicion.X - bala.sprite.Width / 2, 
                        (int)bala.posicion.Y - bala.sprite.Height / 2, 
                        bala.sprite.Width, bala.sprite.Height);
                    if (balaRect.Intersects(bolaRect))
                    {
                        soundBank.PlayCue("choque");
                        listaBalas.RemoveAt(c);
                        c--;
                        bala.nueva = true;
                        if (false)//balaRect.X >= bolaRect.X
                        {
                            //Agrega despues
                            bala.posVector = bola.posVector + 40;
                            listaCamino.Insert(i, bala);
                            numBolas++;
                            for (int r = i - 1; r >= 0; r--)
                            {
                                GameObject b = listaCamino[r];
                                b.posVector += 40;
                                b.cambio = true;
                            }
                        }
                        else
                        {
                            //Agrega antes
                            bala.posVector = bola.posVector;
                            listaCamino.Insert(i + 1, bala);
                            numBolas++;
                            for (int r = i + 1; r < listaCamino.Count; r++)
                            {
                                GameObject b = listaCamino[r];
                                b.posVector -= 40;
                                b.cambio = true;
                            }
                        }
                    }
                }
                
            }
            //Fin de juego
            GameObject primera = listaCamino[0];
            Vector2 posicion;
            if (primera.posVector >= posBolas.Count)
                posicion = posBolas[posBolas.Count-1];
            else
                posicion = posBolas[primera.posVector];
            Rectangle bolaRect1 = new Rectangle((int)posicion.X, (int)posicion.Y, spriteBola.Width, spriteBola.Height);
            if (bolaRect1.Intersects(rectFin))
            {
                terminar = true;
            }
        }
        public void EscuchaMouse()
        {
            mouse = Mouse.GetState();
            posicionMouse.X = mouse.X - cursor.Width / 2;
            posicionMouse.Y = mouse.Y - cursor.Height / 2;
            // Restringe la posicion del mouse para que permanezca dentro de la pantalla
            //this.Window.ClientBounds.Height
            if (posicionMouse.X < 0)
                posicionMouse.X = 0;
            if (posicionMouse.X > pantalla.Width - cursor.Width)
                posicionMouse.X = pantalla.Width - cursor.Width;
            if (posicionMouse.Y < 0)
                posicionMouse.Y = 0;
            if (posicionMouse.Y > pantalla.Height - cursor.Height)
                posicionMouse.Y = pantalla.Height - cursor.Height;
            //Rotar la rana de acuerdo a la posicion del mouse
            rana.rotacion = (float)Math.Atan2((mouse.Y - rana.posicion.Y), (mouse.X - rana.posicion.X));
            //Mover bala de boca
            if (listaBalas.Count > 1)
            {
                //Bola boca
                GameObject bala = listaBalas[listaBalas.Count - 2];
                bala.posicion = new Vector2(
                (float)Math.Cos(rana.rotacion),
                (float)Math.Sin(rana.rotacion)) * (33f);
                bala.posicion += rana.posicion;
                //Bola sig
                GameObject bala2 = listaBalas[listaBalas.Count - 1];
                bala2.posicion = new Vector2(
                (float)Math.Cos(rana.rotacion),
                (float)Math.Sin(rana.rotacion)) * (-32f);
                bala2.posicion += rana.posicion;
            }
            //disparar
            if (mouse.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                AgregarBalas();
            }
            //cambiar Bala
            if (mouse.RightButton == ButtonState.Pressed && mouseStatePrevious.RightButton == ButtonState.Released)
            {
                GameObject b2 =listaBalas[listaBalas.Count - 2];
                GameObject b1 = listaBalas[listaBalas.Count - 1];
                listaBalas[listaBalas.Count - 2] = b1;
                listaBalas[listaBalas.Count - 1] = b2;
            }
            mouseStatePrevious = mouse;
        }

        public void AgregarBalas()
        {
            //Sonido
            soundBank.PlayCue("disparo");
            //Modificar el valor de velocidad de ultima bola
            GameObject bola = listaBalas[listaBalas.Count - 2];
            bola.alive = true;
            bola.velocidad = new Vector2(
                (float)Math.Cos(rana.rotacion),
                (float)Math.Sin(rana.rotacion)) * 10.0f;
            //Crear bola nueva y agregarla al final
            GameObject bola2 = new GameObject(ref spriteBola);
            int c = (int)(random.NextDouble() * (4));
            bola2.color = colores[c];
            bola2.posicion = new Vector2(
                (float)Math.Cos(rana.rotacion),
                (float)Math.Sin(rana.rotacion)) * (33f);
            bola2.posicion += rana.posicion;
            listaBalas.Add(bola2);

        }

        public void EscuchaTeclado()
        {
            KeyboardState teclado = Keyboard.GetState();
            if (!pausa)
            {
                if (teclado.IsKeyDown(Keys.Space))
                {
                    //agrego bola a lista de balas
                    if (previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        AgregarBalas();
                    }
                }
            }
            if (teclado.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
            {
                if (pausa)
                    pausa = false;
                else
                    pausa = true;
            }
            previousKeyboardState = teclado;
        }        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //********************************************DRAW*****************************************
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(nivel1, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(camino, new Vector2(0, 0), Color.Green);
            //spriteBatch.Draw(spriteRana, posRefRana, Color.White);
            //spriteBatch.Draw(spriteRanaB, posRefRana, Color.White);

            //Dibujar BarraZuma
            spriteBatch.Draw(barraZuma, new Vector2(266, 13),new Rectangle(0,0,barra,22),Color.White);

            //Dibujar Rana
            spriteBatch.Draw(rana.sprite,
                rana.posicion,
                null,
                Color.White,
                rana.rotacion,
                rana.centro, 1.0f,
                SpriteEffects.None, 0);

            foreach (GameObject bala in listaBalas)
            {
                //spriteBatch.Draw(bala.sprite, bala.posicion, bala.color);
                spriteBatch.Draw(bala.sprite,bala.posicion,null,bala.color,0,bala.centro,1.0f,SpriteEffects.None,0);
            }

            spriteBatch.Draw(spriteRanaB,
                rana.posicion,
                null,
                Color.White,
                rana.rotacion,
                rana.centro, 1.0f,
                SpriteEffects.None, 0);
            
            
            //foreach (GameObject bola in listaCamino)
            //{
            for (int i = 0; i < listaCamino.Count; i++)
            {
                GameObject bola = listaCamino[i];
                Vector2 pos;
                if (bola.posVector < 0)
                    pos = posBolas[0];
                else if(bola.posVector >= posBolas.Count)
                    pos = posBolas[posBolas.Count-1];
                else
                    pos = posBolas[bola.posVector];
                if (bola.nueva || bola.cambio)
                {
                    pos = bola.posicion;
                }
                pos.X += spriteBola.Width / 2;
                pos.Y += spriteBola.Height/ 2;
                //spriteBatch.Draw(spriteBola,pos,bola.color);
                spriteBatch.Draw(spriteBola, pos, null, bola.color, bola.rotacion, bola.centro, 1.0f, SpriteEffects.None, 0);
                //spriteBatch.DrawString(fuente, "" + i, pos, Color.White);
            }

            spriteBatch.Draw(salida, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(fuente, "" + score, new Vector2(14, 4), Color.Black);
            if (pausa)
            {
                spriteBatch.Draw(spritePausa,
                        new Vector2(pantalla.Width/2-spritePausa.Width/2, pantalla.Height/2-spritePausa.Height/2),
                        Color.White);
            }
            if (ganar)
            {
                spriteBatch.Draw(spriteGanaste,
                        new Vector2(pantalla.Width / 2 - spriteGanaste.Width / 2, pantalla.Height / 2 - spriteGanaste.Height / 2),
                        Color.White);
            }
            if (gameOver)
            {
                spriteBatch.Draw(spriteGameOver,
                        new Vector2(pantalla.Width / 2 - spriteGameOver.Width / 2, pantalla.Height / 2 - spriteGameOver.Height / 2),
                        Color.White);
            }

            spriteBatch.Draw(this.cursor, this.posicionMouse, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        //********************************************termina*****************************************


        /// <summary>
        /// Determina si hay un empalme de pixeles no transparentes entre dos sprites 
        /// </summary>
        /// <param name="rectangleA">Rectangulo de borde del primer sprite</param>
        /// <param name="dataA">Vector de pixeles del primer sprite</param>
        /// <param name="rectangleB">Rectangulo de borde del segundo sprite</param>
        /// <param name="dataB">Vector de pixeles del segundo sprite</param>
        /// <returns>Verdadero si hay empalme entre pixeles no tranparentes; falso de otra forma</returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

        //************************ Seguir Camino*************************************
        public void SeguirCamino()
        {
            bool seguir = true;
            while (seguir)
            {
                Vector2 posicion = posBolas[posBolas.Count - 1];
                Vector2 posicionSig;
                Rectangle rectBola;
                //Verificar que siga el camino
                //Direccion abajo
                if (incY > 0)
                {
                    posicionSig = new Vector2(posicion.X, posicion.Y + 1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                    if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        //Cambiar a derecha
                        posicionSig = new Vector2(posicion.X + 1, posicion.Y);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incX = 1;
                        }
                        //preguntar si se puede seguir con direccion en Y
                        posicionSig = new Vector2(posicion.X, posicion.Y + 1);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incY = 0;
                        }
                    }
                }
                //Direccion arriba
                else if (incY < 0)
                {
                    posicionSig = new Vector2(posicion.X, posicion.Y - 1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                    if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        //Cambiar a izquierda
                        posicionSig = new Vector2(posicion.X - 1, posicion.Y);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incX = -1;
                        }
                        //preguntar si se puede seguir con direccion en Y
                        posicionSig = new Vector2(posicion.X, posicion.Y - 1);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incY = 0;
                        }
                    }
                }
                //Direccion derecha
                else if (incX > 0)
                {
                    posicionSig = new Vector2(posicion.X + 1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                    if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        //Cambiar a arriba
                        posicionSig = new Vector2(posicion.X, posicion.Y - 1);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incY = -1;
                        }
                        //preguntar si se puede seguir con direccion en X
                        posicionSig = new Vector2(posicion.X + 1, posicion.Y);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incX = 0;
                        }
                    }
                }
                //Direccion izquierda
                else if (incX < 0)
                {
                    posicionSig = new Vector2(posicion.X - 1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                    if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        //Cambiar a abajo
                        posicionSig = new Vector2(posicion.X, posicion.Y + 1);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incY = +1;
                        }
                        //preguntar si se puede seguir con direccion en X
                        posicionSig = new Vector2(posicion.X - 1, posicion.Y);
                        rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                        if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                        {
                            incX = 0;
                        }
                    }
                }
                //Agregar posicion en el vector
                posicionSig = new Vector2(posicion.X + incX, posicion.Y + incY);
                posBolas.Add(posicionSig);
                //Dterminar fin del camino
                GameObject primera = new GameObject(ref spriteBola);
                Rectangle bolaRect1 = new Rectangle((int)posBolas[posBolas.Count - 1].X, (int)posBolas[posBolas.Count - 1].Y, spriteBola.Width/2, spriteBola.Height/2);
                if (bolaRect1.Intersects(rectFin))
                {
                    seguir = false;
                }
            }
        }
        //************************Termina Seguir Camino*************************************
    }
}
