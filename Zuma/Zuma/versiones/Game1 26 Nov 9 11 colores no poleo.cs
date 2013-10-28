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
    public class Game4 : Microsoft.Xna.Framework.Game
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
        //Elementos de juego
        private GameObject rana;
        private List<Vector2> posBolas;
        private List<GameObject> listaCamino;
        private List<GameObject> listaBalas = new List<GameObject>();
        private int espera;
        private int numBolas;
        private int vidas;
        private int incX, incY;
        private Random random = new Random();
        private Color[] colores;
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

        //CONSTRUCTOR
        public Game4()
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
            //audioEngine = new AudioEngine("AudioDiaMuertos.xgs");
            //waveBank = new WaveBank(audioEngine, "Wave Bank.xwb");
            //soundBank = new SoundBank(audioEngine, "Sound Bank.xsb");
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
            spriteBola = Content.Load<Texture2D>("bola");
            camino = Content.Load<Texture2D>("camino");
            nivel1 = Content.Load<Texture2D>("nivel1");
            spriteRana = Content.Load<Texture2D>("rana");
            spriteRanaB = Content.Load<Texture2D>("rana2");
            salida = Content.Load<Texture2D>("salida");
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
            vidas = 3;
            colores = new Color[4];
            colores[0] = Color.Blue;
            colores[1] = Color.Green;
            colores[2] = Color.Red;
            colores[3] = Color.Yellow;
            //Agregar primera Bola
            posBolas = new List<Vector2>();
            listaCamino = new List<GameObject>();
            GameObject bola1 = new GameObject(ref spriteBola);
            Vector2 posicion = new Vector2(51 - spriteBola.Width / 2, 22);
            posBolas.Insert(0, posicion);
            bola1.posicion = posicion;
            listaCamino.Insert(0, bola1);
            int c = (int)(random.NextDouble() * (4));
            bola1.color = colores[c];
            //Elementos de juego
            rana = new GameObject(ref spriteRana);
            rana.posicion = new Vector2(pantalla.Width / 2, pantalla.Height / 2); //Vector2 posRana = new Vector2(pantalla.Width / 2 - spriteRana.Width / 2, pantalla.Height / 2 - spriteRana.Height / 2);
            GameObject bola = new GameObject(ref spriteBola);
            bola.posicion = new Vector2(pantalla.Width / 2 - spriteBola.Width / 2, pantalla.Height / 2 - spriteBola.Height / 2);
            listaBalas.Add(bola);
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
            ChecaPosicionMouse();
            actualizaPosicion();
            ActualizarBalas();

            //agregarEsferas();
            //actualizaEsferas();
            ChecaColision();

            if (vidas < 0)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        public void EscuchaTeclado()
        {
            KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Space))
            {
                //agrego bola a lista de balas
                if (previousKeyboardState.IsKeyUp(Keys.Space))
                {
                    AgregarBalas();
                }
            }
            previousKeyboardState = teclado;
        }

        public void ActualizarBalas()
        {
            foreach (GameObject bala in listaBalas)
            {
                bala.posicion += bala.velocidad;
            }
        }

        public void ChecaPosicionMouse()
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
            //disparar
            if (mouse.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                AgregarBalas();
            }
            mouseStatePrevious = mouse;
        }

        public void AgregarBalas()
        {
            GameObject bola = listaBalas[listaBalas.Count - 1];
            bola.alive = true;
            bola.velocidad = new Vector2(
                (float)Math.Cos(rana.rotacion),
                (float)Math.Sin(rana.rotacion)) * 5.0f;
            GameObject bola2 = new GameObject(ref spriteBola);
            bola2.posicion = new Vector2(pantalla.Width / 2 - spriteBola.Width / 2, pantalla.Height / 2 - spriteBola.Height / 2);
            listaBalas.Add(bola2);
            int c = (int)(random.NextDouble() * (4));
            bola2.color = colores[c];
        }

        public void actualizaPosicion()
        {
            Vector2 posicion = posBolas.ElementAt(0);
            //Vector2 posicionSig = new Vector2(posicion.X + incX, posicion.Y + incY);
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
                    posicionSig = new Vector2(posicion.X +1, posicion.Y);
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
            else if (incY < 0 )
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
            else if (incX > 0 )
            {
                posicionSig = new Vector2(posicion.X + 1, posicion.Y);
                rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, spriteBola.Width, spriteBola.Height);
                if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                {
                    //Cambiar a arriba
                    posicionSig = new Vector2(posicion.X, posicion.Y -1);
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
            else if (incX < 0 )
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
            
            
            


            //agregar la bola en el vector del camino
            posicionSig = new Vector2(posicion.X + incX, posicion.Y + incY);
            posBolas.Insert(0, posicionSig);
            
            if (listaCamino.Count > numBolas * espera)
            {
                listaCamino.RemoveAt(listaCamino.Count-1);
            }
            int i = 0;
            while (i < posBolas.Count)
            {
                if (i / espera >= numBolas)
                {
                    numBolas++;
                    GameObject nueva = new GameObject();
                    listaCamino.Add(nueva);
                    int c = (int)(random.NextDouble() * (4));
                    nueva.color = colores[c];
                    //seguir = false;
                }
                
                for (int c = 0; c < listaCamino.Count; c++)
                {
                    GameObject bola = listaCamino.ElementAt(c);
                    bola.posicion = posBolas.ElementAt(c * espera);
                }
                i++;
            }
        }

        public void ChecaColision()
        {
            //BALAS con pantalla
            for (int i = 0; i < listaBalas.Count; i++)
            {
                GameObject go = listaBalas[i];
                if ((go.posicion.X <= 0) || (go.posicion.X >= pantalla.Width-go.sprite.Width) || 
                    (go.posicion.Y <= 0) || (go.posicion.Y >= pantalla.Height-go.sprite.Height))
                {
                    listaBalas.RemoveAt(i);
                    i--;
                    continue;
                }
                listaBalas[i] = go;
            }
            ///Bolas
            for (int i = 1; i < posBolas.Count; i++)
            {
                if (i * espera < listaCamino.Count)
                {
                    Vector2 pos = listaCamino.ElementAt(i*espera).posicion;
                    if (pos.X + spriteBola.Width / 2 >= graphics.PreferredBackBufferWidth || pos.X <= 0)
                    {
                    }
                    //Arriba y Abajo
                    if (pos.Y + spriteBola.Height / 2 >= graphics.PreferredBackBufferHeight || pos.Y <= 0)
                    {
                    }
                    //Colision bolas con BALAS
                    Rectangle bolaRect = new Rectangle((int)pos.X, (int)pos.Y, spriteBola.Width, spriteBola.Height);
                    foreach (GameObject bala in listaBalas)
                    {
                        Rectangle balaRect = new Rectangle((int)bala.posicion.X, (int)bala.posicion.Y, bala.sprite.Width, bala.sprite.Height);
                        if (balaRect.Intersects(bolaRect))
                        {

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
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //spriteBatch.DrawString(fuente, "Raul", posicion, Color.Red);
            spriteBatch.Draw(nivel1, new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(camino, new Vector2(0, 0), Color.Green);

            //spriteBatch.Draw(spriteRana, posRana, Color.White);
            //spriteBatch.Draw(spriteRanaB, posRana, Color.White);
            spriteBatch.Draw(rana.sprite,
                rana.posicion,
                null,
                Color.White,
                rana.rotacion,
                rana.centro, 1.0f,
                SpriteEffects.None, 0);

            foreach (GameObject bala in listaBalas)
            {
                spriteBatch.Draw(bala.sprite, bala.posicion, bala.color);
            }

            spriteBatch.Draw(spriteRanaB,
                rana.posicion,
                null,
                Color.White,
                rana.rotacion,
                rana.centro, 1.0f,
                SpriteEffects.None, 0);
            
            
            /*
            for (int i = 0; i < numBolas; i++)
            {
                //Vector4 vecColor = new Vector4(255, 255, 255, 1-i*.2f);
                if (i * espera < listaCamino.Count)
                {
                    GameObject bola = listaCamino.ElementAt(i * espera);
                    //spriteBatch.Draw(bolas[i], posicion, new Color(vecColor));
                    spriteBatch.Draw(spriteBola, 
                        //new Rectangle((int)posicion.X, (int)posicion.Y, bola.Width/2, bola.Height/2),
                        bola.posicion,
                        bola.color);
                        //new Color(vecColor));
                }
            }*/
            foreach (GameObject bola in listaCamino)
            {
                spriteBatch.Draw(spriteBola,
                    //new Rectangle((int)posicion.X, (int)posicion.Y, bola.Width/2, bola.Height/2),
                        bola.posicion,
                        bola.color);
                //new Color(vecColor));
            }
            spriteBatch.DrawString(fuente, "    Numero: "+numBolas, new Vector2(10,10), Color.Red);
            spriteBatch.DrawString(fuente, "Posiciones: " + posBolas.Count, new Vector2(10, 50), Color.Red);
            

            spriteBatch.Draw(salida, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(this.cursor, this.posicionMouse, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }



        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
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
    }
}
