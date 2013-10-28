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
    public class Game2 : Microsoft.Xna.Framework.Game
    {
        //Atributos
        GraphicsDeviceManager graphics; //Entorno grafico
        SpriteBatch spriteBatch;        //Diguja en el distpositivo grafico
        int espera;
        int numBolas;
        int vidas;
        int incX, incY;
        float rotacion;
        SpriteFont fuente;
        Texture2D bola;
        Texture2D camino;
        Texture2D nivel1;
        Texture2D rana;
        Texture2D rana2;
        Texture2D salida;
        // The color data for the images; used for per pixel collision
        Color[] bolaTextureData;
        Color[] caminoTextureData;
        //posiciones 
        List<Vector2> posBolas;
        //Keyboard
        KeyboardState previousKeyboardState = Keyboard.GetState();
        //Pantalla
        private Viewport pantalla;
        private Rectangle rectPantalla;
        ///Audio
        private WaveBank waveBank;
        private SoundBank soundBank;
        private AudioEngine audioEngine;

        //CONSTRUCTOR
        public Game2()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 603;
            graphics.PreferredBackBufferWidth = 802;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            posBolas=new List<Vector2>();
            Vector2 posicion = new Vector2(51, 22);
            posBolas.Insert(0, posicion);
            incX = 0;
            incY = 1;
            espera = 40;
            numBolas = 1;
            vidas = 3;
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
            fuente = Content.Load<SpriteFont>("FuenteArial");
            bola = Content.Load<Texture2D>("bola");
            camino = Content.Load<Texture2D>("camino");
            nivel1 = Content.Load<Texture2D>("nivel1");
            rana = Content.Load<Texture2D>("rana");
            rana2 = Content.Load<Texture2D>("rana2");
            salida = Content.Load<Texture2D>("salida");
            pantalla = graphics.GraphicsDevice.Viewport;
            rectPantalla = new Rectangle(0,0,camino.Width,camino.Height);
            // Extract collision data
            bolaTextureData =
                new Color[bola.Width * bola.Height];
            bola.GetData(bolaTextureData);
            caminoTextureData =
                new Color[camino.Width * camino.Height];
            camino.GetData(caminoTextureData);
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

            //escuchaTeclado();
            //agregarEsferas();
            //actualizaEsferas();
            //actualizaBalas();
            actualizaPosicion();
            //checaColision();
            if (vidas < 0)
            {
                this.Exit();
            }

            base.Update(gameTime);
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
                rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                {
                    //Cambiar a derecha
                    posicionSig = new Vector2(posicion.X +1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                    if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        incX = 1;
                    }
                    //preguntar si se puede seguir con direccion en Y
                    posicionSig = new Vector2(posicion.X, posicion.Y + 1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
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
                rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                {
                    //Cambiar a izquierda
                    posicionSig = new Vector2(posicion.X - 1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                    if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        incX = -1;
                    }
                    //preguntar si se puede seguir con direccion en Y
                    posicionSig = new Vector2(posicion.X, posicion.Y - 1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
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
                rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                {
                    //Cambiar a arriba
                    posicionSig = new Vector2(posicion.X, posicion.Y -1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                    if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        incY = -1;
                    }
                    //preguntar si se puede seguir con direccion en X
                    posicionSig = new Vector2(posicion.X + 1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
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
                rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                {
                    //Cambiar a abajo
                    posicionSig = new Vector2(posicion.X, posicion.Y + 1);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                    if ((IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        incY = +1;
                    }
                    //preguntar si se puede seguir con direccion en X
                    posicionSig = new Vector2(posicion.X - 1, posicion.Y);
                    rectBola = new Rectangle((int)posicionSig.X, (int)posicionSig.Y, bola.Width, bola.Height);
                    if (!(IntersectPixels(rectBola, bolaTextureData, rectPantalla, caminoTextureData)))
                    {
                        incX = 0;
                    }
                }
            }
            
            
            


            //agregar la posicion en el vector
            posicionSig = new Vector2(posicion.X + incX, posicion.Y + incY);
            posBolas.Insert(0, posicionSig);
            numBolas++;
            if (posBolas.Count > numBolas * espera)
            {
                posBolas.RemoveAt(posBolas.Count-1);
            }
        }

        public void checaColision()
        {
            //Derecha e Izquierda
            Vector2 posicion = posBolas.ElementAt(0);
            if (posicion.X + bola.Width/2 >= graphics.PreferredBackBufferWidth || posicion.X <=0)
            {
                incX *= -1;
            }
            //Arriba y Abajo
            if (posicion.Y + bola.Height/2>=graphics.PreferredBackBufferHeight || posicion.Y <=0)
            {
                incY *= -1;
            }
            for (int i = 1; i < numBolas; i++)
            {
                if (i * espera < posBolas.Count)
                {
                    Vector2 pos = posBolas.ElementAt(i*espera);
                    if (pos.X + bola.Width / 2 >= graphics.PreferredBackBufferWidth || pos.X <= 0)
                    {
                    }
                    //Arriba y Abajo
                    if (pos.Y + bola.Height / 2 >= graphics.PreferredBackBufferHeight || pos.Y <= 0)
                    {
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
            spriteBatch.Draw(camino, new Vector2(0, 0), Color.Green);
            Vector2 posRana = new Vector2(pantalla.Width/2-rana.Width/2, pantalla.Height/2-rana.Height/2);
            spriteBatch.Draw(rana, posRana, Color.White);
            spriteBatch.Draw(rana2, posRana, Color.White);
            
            spriteBatch.Draw(bola, new Vector2(50, 50), Color.White);
            spriteBatch.Draw(bola, new Vector2(50, 80), Color.Red);
            spriteBatch.Draw(bola, new Vector2(50, 110), Color.Blue);
            spriteBatch.Draw(bola, new Vector2(50, 140), Color.Green);
            spriteBatch.Draw(bola, new Vector2(50, 170), Color.Yellow);
            
            for (int i = 0; i < numBolas; i++)
            {
                //Vector4 vecColor = new Vector4(255, 255, 255, 1-i*.2f);
                if (i * espera < posBolas.Count)
                {
                    Vector2 posicion = posBolas.ElementAt(i * espera);
                    //spriteBatch.Draw(bolas[i], posicion, new Color(vecColor));
                    spriteBatch.Draw(bola, 
                        //new Rectangle((int)posicion.X, (int)posicion.Y, bola.Width/2, bola.Height/2),
                        posicion,
                        Color.White);
                        //new Color(vecColor));
                }
            }
            spriteBatch.Draw(salida, new Vector2(0, 0), Color.White);
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
