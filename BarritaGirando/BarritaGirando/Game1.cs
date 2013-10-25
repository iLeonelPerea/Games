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
using System.IO;
using BarritaGirando.Core;

namespace BarritaGirando
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D mundo, plus100, barraEntera, inicio, fin, imgScore, reversa, imgCambioNivel;
        private Texture2D [] imagenes;
        private SpriteFont fuente, fuenteSeleccionada;
        private AudioComponent audioComponent;
        private BarritaGirando.EscenaJuego ej;
        private EscenaIntro ei;
        private EscenaScore eScores;
        private EscenaInstrucciones eInstr;
        private EscenaSave esa;
        private Core.Escena escenaActiva;
        private KeyboardState oldKeyboardState;
        private GamePadState oldGamePadState;
        private string escenaAnterior = "";
        private string nombreArchivo = "scores.isaac";
        public List<Score> listaScores = new List<Score>();
        private Cue cue;
        // The color data for the images; used for per pixel collision
        Color[] barraTextureData;
        Color[] inicioTextureData;
        Color[] finTextureData;
        Color[] reversaTextureData;
        List<Color[]> imagenesTextureData;
        //Particulas para choques
        private static int HEIGHT = 650;
        private static int WIDTH = 1000;



        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        public Cue Cue
        {
            get { return cue; }
            set { cue = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.PreferredBackBufferWidth = WIDTH;
            LeerArchivo();
        }

        public void LeerArchivo()
        {
            TextReader tr = new StreamReader(StorageContainer.TitleLocation +@"\scores.txt");
            string linea = tr.ReadLine();
            while (linea != null)
            {
                string[] arr = linea.Split(':');
                listaScores.Add(new Score(arr[0], int.Parse(arr[1])));
                linea = tr.ReadLine();
            }
            tr.Close();
            listaScores.Sort(new ComparaScore());
        }

        public void EscribeArchivo()
        {
            //string camino = Path.Combine(StorageContainer.TitleLocation, @"\scores.txt");
            TextWriter tw = new StreamWriter(StorageContainer.TitleLocation + @"\scores.txt");
            //TextWriter tw = new StreamWriter(camino);
            listaScores.Sort(new ComparaScore());
            for (int i = 0; i < listaScores.Count; i++)
            {
                tw.WriteLine(((Score)listaScores[i]).Nombre + ":" + ((Score)listaScores[i]).Puntos);
            }
            tw.Close();
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
            // TODO: Add your initialization logic here
            audioComponent = new AudioComponent(this);
            Services.AddService(typeof(AudioComponent), audioComponent);
            Components.Add(audioComponent);
            this.IsMouseVisible = true;
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
            mundo = Content.Load<Texture2D>("Sprites//mundo");
            barraEntera = Content.Load<Texture2D>("Sprites//barraEntera");
            inicio = Content.Load<Texture2D>("Sprites//inicio");
            fin = Content.Load<Texture2D>("Sprites//fin");
            imgScore = Content.Load<Texture2D>("Sprites//imgScore");
            reversa = Content.Load<Texture2D>("Sprites//reversa");
            plus100 = Content.Load<Texture2D>("Sprites//plus100");
            imgCambioNivel = Content.Load<Texture2D>("Sprites//nextLevel");
            Texture2D imgContinuarNivel = Content.Load<Texture2D>("Sprites//btnContinuar");
            //Imagenes para los niveles
            imagenes = new Texture2D[14];
            imagenes[0] = Content.Load<Texture2D>("Sprites//arriba");
            imagenes[1] = Content.Load<Texture2D>("Sprites//abajo");
            imagenes[2] = Content.Load<Texture2D>("Sprites//izquierda");
            imagenes[3] = Content.Load<Texture2D>("Sprites//derecha");
            imagenes[4] = Content.Load<Texture2D>("Sprites//arribaizq");
            imagenes[5] = Content.Load<Texture2D>("Sprites//arribader"); 
            imagenes[6] = Content.Load<Texture2D>("Sprites//abajoizq");
            imagenes[7] = Content.Load<Texture2D>("Sprites//abajoder");
            imagenes[8] = Content.Load<Texture2D>("Sprites//ring");
            imagenes[9] = Content.Load<Texture2D>("Sprites//bar");
            imagenes[10] = Content.Load<Texture2D>("Sprites//mundo");
            imagenes[11] = Content.Load<Texture2D>("Sprites//vacio");

            //Cargar fuentes para menus
            fuente = Content.Load<SpriteFont>("fuente");
            fuenteSeleccionada = Content.Load<SpriteFont>("seleccionado");
            Texture2D imgFondo = Content.Load<Texture2D>("Sprites//imgFondo");
            Texture2D imgCrece = Content.Load<Texture2D>("Sprites//inicioCrece");
            Texture2D imgInstrucciones = Content.Load<Texture2D>("Sprites//instrucciones");
            Texture2D imgVida = Content.Load<Texture2D>("Sprites//vidas");
            Texture2D imgBarra = Content.Load<Texture2D>("Sprites//barraSuperior");
            Texture2D imgExtraLife = Content.Load<Texture2D>("Sprites//1up");
            Texture2D imgClock = Content.Load<Texture2D>("Sprites//clock");
            Texture2D imgShrinker = Content.Load<Texture2D>("Sprites//shrinker");
            Texture2D imgPlusTime = Content.Load<Texture2D>("Sprites//plusTime");
            
            // Extraer datos de colision
            finTextureData = new Color[fin.Width * fin.Height];
            fin.GetData(finTextureData);
            inicioTextureData = new Color[inicio.Width * inicio.Height];
            inicio.GetData(inicioTextureData);
            barraTextureData = new Color[barraEntera.Width * barraEntera.Height];
            barraEntera.GetData(barraTextureData);
            reversaTextureData = new Color[reversa.Width * reversa.Height];
            reversa.GetData(reversaTextureData);
            Color[] extraLifeTextureData = new Color[imgExtraLife.Width * imgExtraLife.Height];
            imgExtraLife.GetData(extraLifeTextureData);
            Color[] clockTextureData = new Color[imgClock.Width * imgClock.Height];
            imgClock.GetData(clockTextureData);
            Color[] shrinkerTextureData = new Color[imgShrinker.Width * imgShrinker.Height];
            imgShrinker.GetData(shrinkerTextureData);
            Color[] plusTimeTextureData = new Color[imgPlusTime.Width * imgPlusTime.Height];
            imgPlusTime.GetData(plusTimeTextureData);

            imagenesTextureData = new List<Color[]>();
            Color[] cTemp = new Color[imagenes[0].Width * imagenes[0].Height];
            imagenesTextureData.Add(cTemp);
            imagenes[0].GetData(imagenesTextureData[0]);

            Color[] cTemp1 = new Color[imagenes[1].Width * imagenes[1].Height];
            imagenesTextureData.Add(cTemp1);
            imagenes[1].GetData(imagenesTextureData[1]);

            Color[] cTemp2 = new Color[imagenes[2].Width * imagenes[2].Height];
            imagenesTextureData.Add(cTemp2);
            imagenes[2].GetData(imagenesTextureData[2]);

            Color[] cTemp3 = new Color[imagenes[3].Width * imagenes[3].Height];
            imagenesTextureData.Add(cTemp3);
            imagenes[3].GetData(imagenesTextureData[3]);

            Color[] cTemp4 = new Color[imagenes[4].Width * imagenes[4].Height];
            imagenesTextureData.Add(cTemp4);
            imagenes[4].GetData(imagenesTextureData[4]);

            Color[] cTemp5 = new Color[imagenes[5].Width * imagenes[5].Height];
            imagenesTextureData.Add(cTemp5);
            imagenes[5].GetData(imagenesTextureData[5]);

            Color[] cTemp6 = new Color[imagenes[6].Width * imagenes[6].Height];
            imagenesTextureData.Add(cTemp6);
            imagenes[6].GetData(imagenesTextureData[6]);

            Color[] cTemp7 = new Color[imagenes[7].Width * imagenes[7].Height];
            imagenesTextureData.Add(cTemp7);
            imagenes[7].GetData(imagenesTextureData[7]);

            Color[] cTemp8 = new Color[imagenes[8].Width * imagenes[8].Height];
            imagenesTextureData.Add(cTemp8);
            imagenes[8].GetData(imagenesTextureData[8]);

            Color[] cTemp9 = new Color[imagenes[9].Width * imagenes[9].Height];
            imagenesTextureData.Add(cTemp9);
            imagenes[9].GetData(imagenesTextureData[9]);

            ej = new EscenaJuego(this, this, graphics, fuente,imgFondo, barraEntera,barraTextureData,
                fin,finTextureData,inicio,imagenes,imagenesTextureData, reversa, reversaTextureData,
                plus100, imgCambioNivel, imgContinuarNivel, fuenteSeleccionada, imgVida, imgBarra,
                imgExtraLife, extraLifeTextureData, imgClock,clockTextureData,imgShrinker,
                shrinkerTextureData, imgPlusTime,plusTimeTextureData);
            ei = new EscenaIntro(this, ref fuente, ref fuenteSeleccionada, ref imgFondo, ref imgCrece, ref barraEntera);
            eScores = new EscenaScore(this,ref fuente, ref imgScore);
            eInstr = new EscenaInstrucciones(this, ref imgInstrucciones);
            esa = new EscenaSave(this,fuente,fuenteSeleccionada, imgFondo);

            Components.Add(ej);
            Components.Add(ei);
            Components.Add(eInstr);
            Components.Add(eScores);
            Components.Add(esa);

            ei.Show();
            escenaActiva = ei;
            cue = audioComponent.SoundBank.GetCue("rightRound");
            cue.Play();
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
            ManejarEscenas(gameTime);
            ManejarEscenasPad(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void ManejarEscenasPad(GameTime gameTime) {
            if (escenaActiva == ej)
            {
                //IsMouseVisible = false;
                if (ChecaBtnBack())
                {
                    MostrarEscena(ei);
                }
                else
                {
                    if (ChecaBtnNext() && !ej.FinNivel)
                    {
                        ej.Pausado = !ej.Pausado;
                        cue = ej.Cue;
                        cue.Stop(AudioStopOptions.Immediate);
                        cue = audioComponent.SoundBank.GetCue("Pausa");
                        cue.Play();
                        if (!ej.Pausado)
                        {
                            ej.Cue = cue;
                            ej.Cue = audioComponent.SoundBank.GetCue("gamePlay");
                            ej.Cue.Play();
                        }
                    }
                    ej.Jugar(gameTime);
                    //ChecaPad();
                    ChecaPad2();
                    if (ej.Terminado)
                    {
                        //cue = ej.Cue;
                        //Console.Write(cue.Name);
                        //cue.Stop(AudioStopOptions.Immediate);
                        esa.Score = ej.Score;
                        MostrarEscena(esa);
                    }
                    if (escenaAnterior != "ej")
                    {
                        cue.Stop(AudioStopOptions.Immediate);
                    }
                }
                escenaAnterior = "ej";
            }//Fin de si la escena activa es EJ
            if (escenaActiva == ei)
            {
                IsMouseVisible = true;
                ManejarEscenaIntroPad();
                if (escenaAnterior == "ej")
                {
                    cue = ej.Cue;
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                if (escenaAnterior == "esa")
                {
                    cue = ej.Cue;
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                if (escenaAnterior != "ei")
                {
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                escenaAnterior = "ei";
            }
            if (escenaActiva == eInstr)
            {
                IsMouseVisible = false;
                if (ChecaBtnNext() || ChecaBtnBack())
                {
                    MostrarEscena(ei);
                }
                escenaAnterior = "eInstr";
            }
            if (escenaActiva == eScores)
            {
                eScores.ListaScores = listaScores;
                if (ChecaBtnNext() || ChecaBtnBack())
                {
                    MostrarEscena(ei);
                }
                escenaAnterior = "eScores";
            }
            if (escenaActiva == esa)
            {
                IsMouseVisible = false;
                manejaEscenaSavePad();
                escenaAnterior = "esa";
            }
            oldGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void ManejarEscenas(GameTime gameTime)
        {
            if (escenaActiva == ej)
            {
                //IsMouseVisible = false;
                if (ChecaEsc())
                {
                    MostrarEscena(ei);
                }
                else
                {
                    if (ChecaEnter() && !ej.FinNivel)
                    {
                        ej.Pausado = !ej.Pausado;
                        cue = ej.Cue;
                        cue.Stop(AudioStopOptions.Immediate);
                        cue = audioComponent.SoundBank.GetCue("Pausa");
                        cue.Play();
                        if (!ej.Pausado) {
                            ej.Cue = cue;
                            ej.Cue = audioComponent.SoundBank.GetCue("gamePlay");
                            ej.Cue.Play();
                        }
                    }
                    ej.Jugar(gameTime);
                    ChecaTeclado2();
                    if (ej.Terminado)
                    {
                        //cue = ej.Cue;
                        //Console.Write(cue.Name);
                        //cue.Stop(AudioStopOptions.Immediate);
                        esa.Score = ej.Score;
                        MostrarEscena(esa);
                    }
                    if (escenaAnterior != "ej")
                    {
                        cue.Stop(AudioStopOptions.Immediate);
                    }
                }
                escenaAnterior = "ej";
            }//Fin de si la escena activa es EJ
            if (escenaActiva == ei)
            {
                IsMouseVisible = true;
                ManejarEscenaIntro();
                if (escenaAnterior == "ej")
                {
                    cue = ej.Cue;
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                if (escenaAnterior == "esa") {
                    cue = ej.Cue;
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                if (escenaAnterior != "ei")
                {
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("rightRound");
                    cue.Play();
                }
                escenaAnterior = "ei";
            }
            if (escenaActiva == eInstr)
            {
                IsMouseVisible = false;
                if (ChecaEsc() || ChecaEnter())
                {
                    MostrarEscena(ei);
                }
                escenaAnterior = "eInstr";
            }
            if (escenaActiva == eScores)
            {
                eScores.ListaScores = listaScores;
                if (ChecaEsc() || ChecaEnter())
                {
                    MostrarEscena(ei);
                }
                escenaAnterior = "eScores";
            }
            if (escenaActiva == esa)
            {
                IsMouseVisible = false;
                manejaEscenaSave();
                escenaAnterior = "esa";
            }
            oldKeyboardState = Keyboard.GetState();
        }

        public void ManejarEscenaIntro()
        {
            if (ChecaEnter())
            {
                switch (ei.SelectedMenuIndex)
                {
                    case 0:
                        //Jugar
                        //Reiniciar el juego
                        ej.reiniciarJuego();
                        MostrarEscena(ej);
                        break;
                    case 1:
                        MostrarEscena(eInstr);
                        break;
                    case 2:
                        MostrarEscena(eScores);
                        break;
                    case 3:
                        //Salir del juego
                        Exit();
                        break;
                }
            }
            oldKeyboardState = Keyboard.GetState();
        }

        public void ManejarEscenaIntroPad()
        {
            if (ChecaBtnNext())
            {
                switch (ei.SelectedMenuIndex)
                {
                    case 0:
                        //Jugar
                        //Reiniciar el juego
                        ej.reiniciarJuego();
                        MostrarEscena(ej);
                        break;
                    case 1:
                        MostrarEscena(eInstr);
                        break;
                    case 2:
                        MostrarEscena(eScores);
                        break;
                    case 3:
                        //Salir del juego
                        Exit();
                        break;
                }
            }
            oldGamePadState = GamePad.GetState(PlayerIndex.One);
        }


        public void manejaEscenaSave()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            ChecaTeclado();
            if (ChecaEnter())
            {
                listaScores.Add(new Score(esa.Nombre, esa.Score));
                //EscribeArchivo();
                MostrarEscena(ei);
            }
            oldKeyboardState = keyboardState;
        }

        public void manejaEscenaSavePad()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            ChecaPad();
            if (ChecaBtnNext())
            {
                listaScores.Add(new Score(esa.Nombre, esa.Score));
                //EscribeArchivo();
                MostrarEscena(ei);
            }
            oldGamePadState = gamePadState;
        }

        public void ChecaTeclado()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (oldKeyboardState.IsKeyDown(Keys.Left) && (keyboardState.IsKeyUp(Keys.Left)))
            {
                esa.cambiarNombre(3);
            }
            if (oldKeyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyUp(Keys.Up)))
            {
                esa.cambiarNombre(2);
            }
            if (oldKeyboardState.IsKeyDown(Keys.Down) && (keyboardState.IsKeyUp(Keys.Down)))
            {
                esa.cambiarNombre(1);
            }
            if (oldKeyboardState.IsKeyDown(Keys.Right) && (keyboardState.IsKeyUp(Keys.Right)))
            {
                esa.cambiarNombre(4);
            }
        }

        public void ChecaPad()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (oldGamePadState.IsButtonDown(Buttons.DPadLeft) && gamePadState.IsButtonUp(Buttons.DPadLeft))
            {
                esa.cambiarNombre(3);
            }
            if (oldGamePadState.IsButtonDown(Buttons.DPadUp) && gamePadState.IsButtonUp(Buttons.DPadUp))
            {
                esa.cambiarNombre(2);
            }
            if (oldGamePadState.IsButtonDown(Buttons.DPadDown) && gamePadState.IsButtonUp(Buttons.DPadDown))
            {
                esa.cambiarNombre(1);
            }
            if (oldGamePadState.IsButtonDown(Buttons.DPadRight) && gamePadState.IsButtonUp(Buttons.DPadRight))
            {
                esa.cambiarNombre(4);
            }
        }

        public void ChecaTeclado2()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (oldKeyboardState.IsKeyDown(Keys.X) && (keyboardState.IsKeyUp(Keys.X)))
            {
                if (ej.nivel <= 14)
                {
                    ej.nivel++;
                    ej.cambiarNivel(ej.nivel, 0);
                }
            }
            if (oldKeyboardState.IsKeyDown(Keys.Z) && (keyboardState.IsKeyUp(Keys.Z)))
            {
                if (ej.nivel >= 2)
                {
                    ej.nivel--;
                    ej.cambiarNivel(ej.nivel, 0);
                }
            }
        }

        public void ChecaPad2()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (oldGamePadState.IsButtonDown(Buttons.Y) && (gamePadState.IsButtonUp(Buttons.Y)))
            {
                if (ej.nivel <= 14)
                {
                    ej.nivel++;
                    ej.cambiarNivel(ej.nivel, 0);
                }
            }
            if (oldGamePadState.IsButtonDown(Buttons.X) && (gamePadState.IsButtonUp(Buttons.X)))
            {
                if (ej.nivel >= 2)
                {
                    ej.nivel--;
                    ej.cambiarNivel(ej.nivel, 0);
                }
            }
        }

        public bool ChecaEnter()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool resultado = (oldKeyboardState.IsKeyDown(Keys.Enter) && (keyboardState.IsKeyUp(Keys.Enter)));
            return resultado;
        }

        public bool ChecaEsc()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool resultado = (oldKeyboardState.IsKeyDown(Keys.Escape) && (keyboardState.IsKeyUp(Keys.Escape)));
            return resultado;
        }

        public bool ChecaBtnBack()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            bool resultado = (oldGamePadState.IsButtonDown(Buttons.B) && (gamePadState.IsButtonUp(Buttons.B)));
            return resultado;
        }
        public bool ChecaBtnNext()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            bool resultado = (oldGamePadState.IsButtonDown(Buttons.A) && (gamePadState.IsButtonUp(Buttons.A)));
            return resultado;
        }

        protected void MostrarEscena(Escena scene)
        {
            escenaActiva.Hide();
            escenaActiva = scene;
            if (escenaActiva == eScores || escenaActiva == eInstr) {
                cue.Stop(AudioStopOptions.Immediate);
                cue = audioComponent.SoundBank.GetCue("introBarritaGirando");
                cue.Play();
            }
            scene.Show();
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
