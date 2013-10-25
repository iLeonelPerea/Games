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
using BarritaGirando.Core.Particles;
using BarritaGirando.Core;
using System.IO;

namespace BarritaGirando
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaJuego : Escena
    {
        //private ImagenFondo imgF;
        private Barra barraEntera;
        private Obstaculo circuloFin, circuloInicio, reversa, extraLife, slow, shrinker, plusTime;
        private Obstaculo[] paredesObst;
        private Texture2D imgF, imgInicio, imgFin, imgReversa,plus100, imgCambioNivel, imgContinuar;
        private Texture2D imgVida, imgBarraSuperior, imgExtraLife, imgClock, imgShrinker, imgBarraEntera, imgPlusTime;
        private Texture2D [] imagenes;
        private AudioComponent audioComponent;
        private SpriteFont fuenteSmall, fuenteBig;
        private Rectangle rImagen, rMouse, rPad;
        private Cue cue;
        private bool pausado, terminado, finNivel, chocado, isReversa, isDrawingPlus100, inicioNivel;
        private bool aumentoBoton, isSlow, isExtraLife, isShrinker, isPlusTime;
        private int score, puntosExtras, vidas, maxNiveles = 15, rows = 15, cols = 15;
        public int nivel = 1;
        private long tiempo = 5000;
        // The sub-rectangle of the drawable area which should be visible on all TVs
        Rectangle safeBounds, pantalla, btnCambioPantalla;
        // Percentage of the screen on every side is the safe area
        const float SafeAreaPortion = 0.05f;
        GraphicsDeviceManager graphics;
        // The color data for the images; used for per pixel collision
        Color[] barraTextureData;
        Color[] finTextureData;
        Color[] reversaTextureData;
        Color[] slowTextureData;
        Color[] extraLifeTextureData;
        Color[] shrinkerTextureData, plusTimeTextureData;
        List<Color[]> imagenesTextureData, obstTextureData;
        private int[,] matriz;
        private string nombreArchivo;
        private MouseState mouse, mouseAnterior;
        private GamePadState pad, padAnterior;
        private ParticleSystem explosionRojo;
        private ParticleSystem explosionCentro;
        private ParticleSystem explosionAzul;
        private ParticleSystem smoke;
        private static int HEIGHT = 650;
        private static int WIDTH = 1000;


        public bool Terminado
        {
            get { return terminado; }
            set { terminado = value; }
        }

        public bool FinNivel
        {
            get { return finNivel; }
            set { finNivel = value; }
        }

        
        public Cue Cue
        {
            get { return cue; }
            set { cue = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        private SpriteBatch sBatch;

        public bool Pausado
        {
            get { return pausado; }
            set { 
                pausado = value;
                if (pausado)
                {
                    barraEntera.Enabled = false;
                    circuloFin.Enabled = false;
                    if (reversa != null)
                    {
                        reversa.Enabled = false;
                    }
                    if (extraLife != null)
                    {
                        extraLife.Enabled = false;
                    }
                    if (slow != null)
                    {
                        slow.Enabled = false;
                    }
                    if (shrinker != null)
                    {
                        shrinker.Enabled = false;
                    }
                    if (plusTime != null)
                    {
                        plusTime.Enabled = false;
                    }
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].Enabled = false;
                        }
                    }
                }
                else
                {
                    barraEntera.Enabled = true;
                    circuloFin.Enabled = true;
                    if (reversa != null) {
                        reversa.Enabled = true;
                    }
                    if (extraLife != null)
                    {
                        extraLife.Enabled = true;
                    }
                    if (slow != null)
                    {
                        slow.Enabled = true;
                    }
                    if (shrinker != null)
                    {
                        shrinker.Enabled = true;
                    }
                    if (plusTime != null)
                    {
                        plusTime.Enabled = true;
                    }
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].Enabled = true;
                        }
                    }
                }
            }
        }

        public EscenaJuego(Game game, Game1 game1, GraphicsDeviceManager graphics, SpriteFont fuenteSmall, 
            Texture2D imgFondo, Texture2D imgBarraEntera, Color[] barraTextureData,  
            Texture2D imgFin, Color[] finTextureData, Texture2D imgInicio,  
            Texture2D[] imagenes, List<Color[]> imagenesTextureData, Texture2D imgReversa,
            Color[] reversaTextureData, Texture2D plus100, Texture2D imgLevelChange,
            Texture2D imgContinuarNivel, SpriteFont fuenteBig, Texture2D imgVida, 
            Texture2D imgBarraSuperior, Texture2D imgExtraLife, Color[] extraLifeTextureData,
            Texture2D imgClock, Color[] clockTextureData, Texture2D imgShrinker,
            Color[] shrinkerTextureData, Texture2D imgPlusTime, Color[] plusTimeTextureData)
            : base(game)
        {
            sBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            audioComponent = (AudioComponent)game.Services.GetService(typeof(AudioComponent));
            pantalla = new Rectangle(0, 0, WIDTH, HEIGHT);
            
            isReversa = false;
            isDrawingPlus100 = false;
            chocado = false;
            inicioNivel = false;
            aumentoBoton = false;

            this.imgBarraEntera = imgBarraEntera;
            barraEntera = new Barra(game, imgBarraEntera, 1.5f, 1, 20, 20);
            this.barraTextureData = barraTextureData;
            this.imgFin = imgFin;
            this.finTextureData = finTextureData;
            this.imgReversa = imgReversa;
            this.reversaTextureData = reversaTextureData;
            this.imgInicio = imgInicio;
            this.imagenes = imagenes;
            this.imagenesTextureData = imagenesTextureData;
            this.plus100 = plus100;
            this.imgCambioNivel = imgLevelChange;
            this.imgContinuar = imgContinuarNivel;
            this.imgVida = imgVida;
            this.imgBarraSuperior = imgBarraSuperior;
            this.imgExtraLife = imgExtraLife;
            this.extraLifeTextureData = extraLifeTextureData;
            this.slowTextureData = clockTextureData;
            this.imgClock = imgClock;
            this.imgShrinker = imgShrinker;
            this.shrinkerTextureData = shrinkerTextureData;
            this.imgPlusTime = imgPlusTime;
            this.plusTimeTextureData = plusTimeTextureData;
            obstTextureData = new List<Color[]>();

            imgF = imgFondo;
            rImagen = new Rectangle(0, 0, plus100.Width, plus100.Height);
            btnCambioPantalla = new Rectangle(430, 650, 160, 50);
            this.fuenteSmall = fuenteSmall;
            this.fuenteBig = fuenteBig;
            this.graphics = graphics;

            matriz = new int[cols, rows];
            paredesObst = new Obstaculo[cols * rows];
            
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    matriz[c, r] = 0;
                }
            }
            leerArchivo(nivel);

            explosionRojo = new ExplosionParticleSystem(game1, 1, "Sprites//rojo");//explosion 
            explosionRojo.Initialize();
            Componentes.Add(explosionRojo);
            explosionCentro = new ExplosionParticleSystem(game1, 1, "Sprites//esfera");//explosion 
            explosionCentro.Initialize();
            Componentes.Add(explosionCentro);
            explosionAzul = new ExplosionParticleSystem(game1, 1, "Sprites//azul");//explosion 
            explosionAzul.Initialize();
            Componentes.Add(explosionAzul);

            smoke = new ExplosionSmokeParticleSystem(game1, 2, "Sprites//smoke");//smoke
            smoke.Initialize();
            Componentes.Add(smoke);

            Componentes.Add(barraEntera);
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.PreferredBackBufferWidth = WIDTH;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            safeBounds = new Rectangle(
                (int)(viewport.Width * SafeAreaPortion),
                (int)(viewport.Height * SafeAreaPortion),
                (int)(viewport.Width * (1 - 2 * SafeAreaPortion)),
                (int)(viewport.Height * (1 - 2 * SafeAreaPortion)));
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!inicioNivel)
            {
                if (!pausado)
                {
                    if (tiempo > 0)
                    {
                        if (tiempo == 1000) {
                            audioComponent.PlayCue("hurry");
                        }
                        if (!barraEntera.Muerte)
                        {
                            tiempo--;
                        }
                        if (barraEntera.Muerte)
                        {
                            incrementaImagen(ref rImagen, 4, 2);
                            if (rImagen.Width >= barraEntera.Imagen.Width * 2)
                            {
                                barraEntera.Muerte = false;
                                cambiarNivel(nivel, 0);
                                //reiniciarNivel(nivel);
                            }
                        }
                        //Si va con reversa, entonces animar +100
                        if ((isReversa||isSlow||isShrinker||isExtraLife||isPlusTime) && isDrawingPlus100)
                        {
                            incrementaImagen(ref rImagen, 4, 2);
                            if (rImagen.Width >= plus100.Width)
                            {
                                isDrawingPlus100 = false;
                            }
                        }
                    }
                    else if (tiempo <= 0)
                    {
                        cue.Stop(AudioStopOptions.Immediate);
                        vidas--;
                        //reiniciarNivel(nivel);
                        cambiarNivel(nivel, 0);
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Show()
        {
            vidas = 5;
            terminado = false;
            score = 0;
            puntosExtras = 0;
            base.Show();
        }

        public void EscuchaPad() {
            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            if (gamepad.DPad.Up == ButtonState.Pressed) {
                barraEntera.posicion.Y -= barraEntera.velocidad;
            }
            if (gamepad.DPad.Down == ButtonState.Pressed)
            {
                barraEntera.posicion.Y += barraEntera.velocidad;
            }
            if (gamepad.DPad.Left == ButtonState.Pressed)
            {
                barraEntera.posicion.X -= barraEntera.velocidad;
            }
            if (gamepad.DPad.Right == ButtonState.Pressed)
            {
                barraEntera.posicion.X += barraEntera.velocidad;
            }
            ScrollPantalla();
        }
        public void EscuchaTeclado()
        {
            KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Left))
            {
                barraEntera.posicion.X -= barraEntera.velocidad;
            }
            if (teclado.IsKeyDown(Keys.Right))
            {
                barraEntera.posicion.X += barraEntera.velocidad;
            }
            if (teclado.IsKeyDown(Keys.Up))
            {
                barraEntera.posicion.Y -= barraEntera.velocidad;
            }
            if (teclado.IsKeyDown(Keys.Down))
            {
                barraEntera.posicion.Y += barraEntera.velocidad;
            }
            ScrollPantalla();
        }

        public void incrementaImagen(ref Rectangle rectangulo, int incX, int incY)
        {
            rectangulo.Width = rectangulo.Width + incX;
            rectangulo.Height = rectangulo.Height + incY;
            rectangulo.X -= incX/2;
            rectangulo.Y -= incY/2;
        }

        //Cambia de nivel, suma el puntaje y reinicia el tiempo.
        public void cambiarNivel(int num, int time) {
            //Cancion
            if (cue != null)
            {
                cue.Stop(AudioStopOptions.Immediate);
            }
            cue = audioComponent.SoundBank.GetCue("gamePlay");
            cue.Play();
            

            inicioNivel = false;
            barraEntera.Muerte = false;
            barraEntera.Imagen = imgBarraEntera;
            isReversa = false;
            isDrawingPlus100 = false;
            isSlow = false;
            isExtraLife = false;
            isShrinker = false;
            isPlusTime = false;
            chocado = false;
            barraEntera.VelocidadGiro = 1;
            score += time;
            puntosExtras = 0;
            tiempo = 5000;
            #region Reestablecer Obstaculos
            for (int i = 0; i < paredesObst.Length; i++) {
                if (paredesObst[i] != null)
                {
                    paredesObst[i].Visible = false;
                    paredesObst[i].Enabled = false;
                }
            }
            paredesObst = null;
            paredesObst = new Obstaculo[cols*rows];
            graphics.GraphicsDevice.Clear(Color.White);
            circuloFin.Visible = false;
            circuloFin.Enabled = false;
            circuloFin = null;
            circuloInicio.Visible = false;
            circuloInicio.Enabled = false;
            circuloInicio = null;
            #endregion
            #region Reestablecer power ups
            if (reversa != null)
            {
                reversa.Visible = false;
                reversa.Enabled = false;
                reversa = null;
            }
            if (slow != null)
            {
                slow.Visible = false;
                slow.Enabled = false;
                slow = null;
            }
            if (extraLife != null)
            {
                extraLife.Visible = false;
                extraLife.Enabled = false;
                extraLife = null;
            }
            if (shrinker != null)
            {
                shrinker.Visible = false;
                shrinker.Enabled = false;
                shrinker = null;
            }
            if (plusTime!= null)
            {
                plusTime.Visible = false;
                plusTime.Enabled = false;
                plusTime = null;
            }
            #endregion
            barraEntera.posicion.X = 0;
            barraEntera.posicion.Y = 0;
            for (int i = 0; i < obstTextureData.Count; i++) {
                obstTextureData.RemoveAt(i);
            }
            leerArchivo(num);
        }

        public void reiniciarJuego() {
            isReversa = false;
            isDrawingPlus100 = false;
            chocado = false;
            barraEntera.VelocidadGiro = 1;
            barraEntera.Muerte = false;
            score = 0;
            puntosExtras = 0;
            vidas = 5;
            terminado = false;
            nivel = 1;
            cambiarNivel(nivel, 0);
        }
        
        //Te reinicia el nivel con el puntaje y el tiempo que ya tenias
        public void reiniciarNivel(int num)
        {
            inicioNivel = false;
            barraEntera.Muerte = false;
            barraEntera.Imagen = imgBarraEntera;
            isReversa = false;
            isDrawingPlus100 = false;
            chocado = false;
            barraEntera.VelocidadGiro = 1;
            tiempo = 5000;
            #region Reestablecer Obstaculos
            for (int i = 0; i < paredesObst.Length; i++)
            {
                if (paredesObst[i] != null)
                {
                    paredesObst[i].Visible = false;
                    paredesObst[i].Enabled = false;
                }
            }
            paredesObst = null;
            paredesObst = new Obstaculo[cols * rows];
            graphics.GraphicsDevice.Clear(Color.White);
            circuloFin.Visible = false;
            circuloFin.Enabled = false;
            circuloFin = null;
            circuloInicio.Visible = false;
            circuloInicio.Enabled = false;
            circuloInicio = null;
            #endregion
            #region Reestablecer power ups
            if (reversa != null)
            {
                reversa.Visible = false;
                reversa.Enabled = false;
                reversa = null;
            }
            if (slow != null)
            {
                slow.Visible = false;
                slow.Enabled = false;
                slow = null;
            }
            if (extraLife != null)
            {
                extraLife.Visible = false;
                extraLife.Enabled = false;
                extraLife = null;
            }
            if (shrinker != null)
            {
                shrinker.Visible = false;
                shrinker.Enabled = false;
                shrinker = null;
            }
            if (plusTime != null)
            {
                plusTime.Visible = false;
                plusTime.Enabled = false;
                plusTime = null;
            }
            #endregion
            barraEntera.posicion.X = 0;
            barraEntera.posicion.Y = 0;
            for (int i = 0; i < obstTextureData.Count; i++)
            {
                obstTextureData.RemoveAt(i);
            }
            leerArchivo(num);

        }

        public void leerArchivo(int num)
        {
            //nombreArchivo = "mapas/screen" + num + ".txt";

            nombreArchivo = @"\screen" + num + ".txt";
            TextReader tr = new StreamReader(StorageContainer.TitleLocation + nombreArchivo);
            string linea = tr.ReadLine();
            rows = 0;
            cols = 0;
            cols = linea.Length;
            String mapa = "";
            while (linea != null)
            {
                if (linea.Length > cols)
                {
                    cols = linea.Length;
                }
                mapa = mapa + linea; // se le pone x para indicar cambio de linea
                //Console.WriteLine(linea.Length);
                //Console.WriteLine(linea);
                linea = tr.ReadLine();
                rows++;
            }
            paredesObst = new Obstaculo[cols * rows];
            tr.Close();
            char[] cadenaMapa = mapa.ToCharArray();
            int contador = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    #region Espacio
                    if (cadenaMapa[contador] == ' ')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c*100,false,false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Barra Giratoria
                    else if (cadenaMapa[contador] == '*')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false,false);
                        barraEntera.iniciar((c * 100) + 50, (r * 100) + 50);
                        circuloInicio = new Obstaculo(Game, ref imgInicio, r*100 , c*100,false,false);
                        Componentes.Add(paredesObst[contador]);
                        Componentes.Add(circuloInicio);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Reversa
                    else if (cadenaMapa[contador] == 'R')
                    {
                        //Imagen transparente
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                        //Reversa
                        reversa = new Obstaculo(Game, ref imgReversa, r * 100, c * 100, true,false);
                        Componentes.Add(reversa);
                    }
                    #endregion
                    #region Arriba
                    else if (cadenaMapa[contador] == '-')
                    {
                        //matriz[c, r] = ARRIBA;
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[0], r * 100, c * 100, false,false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[0].Width * imagenes[0].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[0].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Abajo
                    else if (cadenaMapa[contador] == '_')
                    {
                        
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[1], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[1].Width * imagenes[1].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[1].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Izquierda
                    else if (cadenaMapa[contador] == '[')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[2], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[2].Width * imagenes[2].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[2].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Derecha
                    else if (cadenaMapa[contador] == ']')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[3], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[3].Width * imagenes[3].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[3].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Arriba Izquierda
                    else if (cadenaMapa[contador] == '<')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[4], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[4].Width * imagenes[4].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[4].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Arriba Derecha
                    else if (cadenaMapa[contador] == '>')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[5], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[5].Width * imagenes[5].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[5].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Abajo Izquierda
                    else if (cadenaMapa[contador] == 'L')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[6], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[6].Width * imagenes[6].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[6].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Abajo Derecha
                    else if (cadenaMapa[contador] == '=')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[7], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[7].Width * imagenes[7].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[7].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Dona
                    else if (cadenaMapa[contador] == '@')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[8], r * 100, c * 100,true, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[8].Width * imagenes[8].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[8].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Final
                    else if (cadenaMapa[contador] == 'F')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        circuloFin = new Obstaculo(Game, ref imgFin, r * 100, c * 100,false, false);
                        //paredesObst[contador] = new Obstaculo(Game, ref imgFin, r * 100, c * 100);
                        Componentes.Add(circuloFin);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Vacío
                    else if (cadenaMapa[contador] == '/')
                    {
                        
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[10], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[10].Width * imagenes[10].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[10].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region Barra Enemiga
                    else if (cadenaMapa[contador] == ',')
                    {
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[9], r * 100, c * 100, false, true);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[9].Width * imagenes[9].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[9].GetData(obstTextureData[contador]);
                    }
                    #endregion
                    #region 1 up
                    else if (cadenaMapa[contador] == '1')
                    {
                        //Imagen transparente
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                        //1 up
                        extraLife = new Obstaculo(Game, ref imgExtraLife, r * 100, c * 100, true, true);
                        Componentes.Add(extraLife);
                    }
                    #endregion
                    #region Slow Rotation
                    else if (cadenaMapa[contador] == 's')
                    {
                        //Imagen transparente
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                        //1 up
                        slow = new Obstaculo(Game, ref imgClock, r * 100, c * 100, true, true);
                        Componentes.Add(slow);
                    }
                    #endregion
                    #region Shrinker
                    else if (cadenaMapa[contador] == 'p')
                    {
                        //Imagen transparente
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                        //Shrinker
                        shrinker = new Obstaculo(Game, ref imgShrinker, r * 100, c * 100, false, true);
                        Componentes.Add(shrinker);
                    }
                    #endregion
                    #region PlusTime
                    else if (cadenaMapa[contador] == 't')
                    {
                        //Imagen transparente
                        paredesObst[contador] = new Obstaculo(Game, ref imagenes[11], r * 100, c * 100, false, false);
                        Componentes.Add(paredesObst[contador]);
                        Color[] cTemp = new Color[imagenes[11].Width * imagenes[11].Height];
                        obstTextureData.Add(cTemp);
                        imagenes[11].GetData(obstTextureData[contador]);
                        //PlusTime
                        plusTime = new Obstaculo(Game, ref imgPlusTime, r * 100, c * 100, false, true);
                        Componentes.Add(plusTime);
                    }
                    #endregion
                    contador++;
                }
            }
        }

        public void Jugar(GameTime gameTime)
        {
            if (!pausado)//Si no esta pausado
            {
                if (vidas > 0)//Si aun tiene vidas
                {
                    if (nivel <= maxNiveles)//Si sigue en niveles validos
                    {
                        //Si esta en escena de cambio de nivel, no escuchar teclado ni colision
                        if (!inicioNivel)
                        {
                            ChecaColision(gameTime);
                            EscuchaTeclado();
                            EscuchaPad();
                        }
                        if (inicioNivel) {
                            ManejaMouse();
                            ManejaPad();
                        }
                    }
                    else if (nivel > maxNiveles) {//Si ya no esta en niveles validos
                        //cue.Stop(AudioStopOptions.Immediate); 
                        terminado = true;
                    }
                }
                else if (vidas <= 0) {//Si ya no tiene vidas
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = audioComponent.SoundBank.GetCue("gameOver");
                    cue.Play();
                    terminado = true;
                }
            }
            //Si si esta pausado, que hacer
        }

        public void ScrollPantalla()
        {
            if (!inicioNivel)
            {
                #region Hacia la derecha
                if (barraEntera.posicion.X + 200 > pantalla.Width)
                {
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].posicion.X -= barraEntera.velocidad;
                        }
                    }
                    pantalla.X -= (int)barraEntera.velocidad;
                    barraEntera.posicion.X -= barraEntera.velocidad;
                    if (circuloFin != null && circuloInicio != null)
                    {
                        circuloFin.posicion.X -= barraEntera.velocidad;
                        circuloInicio.posicion.X -= barraEntera.velocidad;
                    }
                    if (reversa != null)
                    {
                        reversa.posicion.X -= barraEntera.velocidad;
                    }
                    if (shrinker != null)
                    {
                        shrinker.posicion.X -= barraEntera.velocidad;
                    }
                    if (extraLife != null)
                    {
                        extraLife.posicion.X -= barraEntera.velocidad;
                    }
                    if (slow != null)
                    {
                        slow.posicion.X -= barraEntera.velocidad;
                    }
                    if (plusTime!= null)
                    {
                        plusTime.posicion.X -= barraEntera.velocidad;
                    }
                }
                #endregion
                #region Hacia la izquierda
                if (barraEntera.posicion.X - 200 < 0)
                {
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].posicion.X += barraEntera.velocidad;
                        }
                    }
                    pantalla.X += (int)barraEntera.velocidad;
                    barraEntera.posicion.X += barraEntera.velocidad;
                    if (circuloFin != null && circuloInicio != null)
                    {
                        circuloFin.posicion.X += barraEntera.velocidad;
                        circuloInicio.posicion.X += barraEntera.velocidad;
                    }
                    if (reversa != null)
                    {
                        reversa.posicion.X += barraEntera.velocidad;
                    }
                    if (shrinker != null)
                    {
                        shrinker.posicion.X += barraEntera.velocidad;
                    }
                    if (extraLife != null)
                    {
                        extraLife.posicion.X += barraEntera.velocidad;
                    }
                    if (slow != null)
                    {
                        slow.posicion.X += barraEntera.velocidad;
                    }
                    if (plusTime != null)
                    {
                        plusTime.posicion.X += barraEntera.velocidad;
                    }
                }
                #endregion
                #region Hacia arriba
                if (barraEntera.posicion.Y - 200 < 0)
                {
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].posicion.Y += barraEntera.velocidad;
                        }
                    }
                    pantalla.Y += (int)barraEntera.velocidad;
                    barraEntera.posicion.Y += barraEntera.velocidad;
                    if (circuloFin != null && circuloInicio != null)
                    {
                        circuloFin.posicion.Y += barraEntera.velocidad;
                        circuloInicio.posicion.Y += barraEntera.velocidad;
                    }
                    if (reversa != null)
                    {
                        reversa.posicion.Y += barraEntera.velocidad;
                    }
                    if (shrinker != null)
                    {
                        shrinker.posicion.Y += barraEntera.velocidad;
                    }
                    if (extraLife != null)
                    {
                        extraLife.posicion.Y += barraEntera.velocidad;
                    }
                    if (slow != null)
                    {
                        slow.posicion.Y += barraEntera.velocidad;
                    }
                    if (plusTime != null)
                    {
                        plusTime.posicion.Y += barraEntera.velocidad;
                    }
                }
                #endregion
                #region Hacia abajo
                if (barraEntera.posicion.Y + 200 > pantalla.Height)
                {
                    for (int i = 0; i < paredesObst.Length; i++)
                    {
                        if (paredesObst[i] != null)
                        {
                            paredesObst[i].posicion.Y -= barraEntera.velocidad;
                        }
                    }
                    pantalla.Y -= (int)barraEntera.velocidad;
                    barraEntera.posicion.Y -= barraEntera.velocidad;
                    if (circuloFin != null && circuloInicio != null)
                    {
                        circuloFin.posicion.Y -= barraEntera.velocidad;
                        circuloInicio.posicion.Y -= barraEntera.velocidad;
                    }
                    if (reversa != null)
                    {
                        reversa.posicion.Y -= barraEntera.velocidad;
                    }
                    if (shrinker != null)
                    {
                        shrinker.posicion.Y -= barraEntera.velocidad;
                    }
                    if (extraLife != null)
                    {
                        extraLife.posicion.Y -= barraEntera.velocidad;
                    }
                    if (slow != null)
                    {
                        slow.posicion.Y -= barraEntera.velocidad;
                    }
                    if (plusTime != null)
                    {
                        plusTime.posicion.Y -= barraEntera.velocidad;
                    }
                }
                #endregion
            }
        }

        public void ManejaPad() {
            pad = GamePad.GetState(PlayerIndex.One);
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) &&
                    padAnterior.IsButtonUp(Buttons.A))
                {
                    inicioNivel = false;
                    //cambiarNivel(nivel, (int)tiempo);
                    cambiarNivel(nivel, 0);
                }
                padAnterior= pad;
        }

        public void ManejaMouse() {
            mouse = Mouse.GetState();
            Rectangle rectRaton = new Rectangle(mouse.X - 1, mouse.Y - 1, 80, 80);
            rMouse = new Rectangle(mouse.X-1,mouse.Y-1,2,2);
            if (rMouse.Intersects(btnCambioPantalla))
            {
                if (btnCambioPantalla.Width < 160)
                {
                    aumentoBoton = true;
                }
                else if (btnCambioPantalla.Width > 200) {
                    aumentoBoton = false;
                }
                if (aumentoBoton) {
                    btnCambioPantalla.Width += 3;
                    btnCambioPantalla.Height += 1;
                    btnCambioPantalla.X -= 1;
                }
                if (!aumentoBoton) {
                    btnCambioPantalla.Width -= 3;
                    btnCambioPantalla.Height -= 1;
                    btnCambioPantalla.X += 1;
                }

                if (mouse.LeftButton == ButtonState.Pressed &&
                    mouseAnterior.LeftButton == ButtonState.Released)
                {
                    inicioNivel = false;
                    //cambiarNivel(nivel, (int)tiempo);
                    cambiarNivel(nivel, 0);
                }
                mouseAnterior = mouse;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            #region Color del mundo
            Color colorMundo;
            switch (nivel) { 
                case 1:
                    colorMundo = Color.White;
                    break;
                case 2:
                    colorMundo =  Color.BlueViolet;
                    break;
                case 3:
                    colorMundo = Color.LightPink;
                    break;
                case 4:
                    colorMundo = Color.LightGoldenrodYellow;
                    break;
                case 5:
                    colorMundo = Color.Orange;
                    break;
                case 6:
                    colorMundo = Color.Violet;
                    break;
                case 7:
                    colorMundo = Color.Turquoise;
                    break;
                case 8:
                    colorMundo = Color.SteelBlue;
                    break;
                case 9:
                    colorMundo = Color.Gray;
                    break;
                case 10:
                    colorMundo = Color.Tomato;
                    break;
                case 11:
                    colorMundo = Color.Snow;
                    break;
                case 12:
                    colorMundo = Color.SandyBrown;
                    break;
                case 13:
                    colorMundo = Color.PowderBlue;
                    break;
                case 14:
                    colorMundo = Color.DarkOrange;
                    break;
                case 15:
                    colorMundo = Color.Black;
                    break;
                default:
                    colorMundo = Color.White;
                    break;
            }
            #endregion
            //SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            #region Escena de Cambio de Nivel
            if (inicioNivel) {
                sBatch.Draw(imgCambioNivel, new Rectangle(0,0,WIDTH,HEIGHT), Color.White);
                sBatch.Draw(imgContinuar, btnCambioPantalla, Color.White);
                sBatch.DrawString(fuenteBig, "Resumen del Nivel " + (nivel - 1), new Vector2(pantalla.Width / 2 - 140, pantalla.Height / 2 - 232), Color.Black);
                sBatch.DrawString(fuenteBig, "Resumen del Nivel " + (nivel - 1), new Vector2(pantalla.Width / 2 - 138, pantalla.Height / 2 - 230), Color.White);
                sBatch.DrawString(fuenteBig, "Bonificacion de Vidas: " + (vidas*150), new Vector2(pantalla.Width / 2 - 168, pantalla.Height / 2 - 132), Color.Black);
                sBatch.DrawString(fuenteBig, "Bonificacion de Vidas: " + (vidas*150), new Vector2(pantalla.Width / 2 - 170, pantalla.Height / 2 - 130), Color.LightGreen);
                sBatch.DrawString(fuenteBig, "Bonificacion de Tiempo: " + (tiempo), new Vector2(pantalla.Width / 2 - 178, pantalla.Height / 2 - 32), Color.Black);
                sBatch.DrawString(fuenteBig, "Bonificacion de Tiempo: " + (tiempo), new Vector2(pantalla.Width / 2 - 180, pantalla.Height / 2 - 30), Color.YellowGreen);
                if (isReversa || isSlow || isExtraLife || isShrinker || isExtraLife)
                {
                    sBatch.DrawString(fuenteBig, "Bonificacion de Extras: " + puntosExtras, new Vector2(pantalla.Width / 2 - 173, pantalla.Height / 2 + 48), Color.Black);
                    sBatch.DrawString(fuenteBig, "Bonificacion de Extras: " + puntosExtras, new Vector2(pantalla.Width / 2 - 175, pantalla.Height / 2 + 50), Color.Yellow);
                }
                else 
                {
                    sBatch.DrawString(fuenteBig, "Bonificacion de Extras: " + 0, new Vector2(pantalla.Width / 2 - 168, pantalla.Height / 2 + 48), Color.Black);
                    sBatch.DrawString(fuenteBig, "Bonificacion de Extras: " + 0, new Vector2(pantalla.Width / 2 - 170, pantalla.Height / 2 + 50), Color.Yellow);
                }
                sBatch.DrawString(fuenteBig, "Puntaje Total: " + (score), new Vector2(pantalla.Width / 2 - 125, pantalla.Height / 2 + 132), Color.Black);
                sBatch.DrawString(fuenteBig, "Puntaje Total: " + (score), new Vector2(pantalla.Width / 2 - 127, pantalla.Height / 2 + 130), Color.Orange);
            }
            #endregion
            #region Escena de Juego
            if (!inicioNivel)
            {
                #region Pausa
                if (pausado)
                {
                    #region Obstaculos, circulos y power ups
                    sBatch.Draw(imgF, new Vector2(0, 0), Color.Red);
                    foreach (Obstaculo obst in paredesObst)
                    {
                        sBatch.Draw(obst.Img, new Rectangle((int)obst.posicion.X, (int)obst.posicion.Y, obst.Img.Width, obst.Img.Height), Color.Red);
                    }
                    sBatch.Draw(circuloFin.Img, new Rectangle((int)circuloFin.posicion.X, (int)circuloFin.posicion.Y, circuloFin.Img.Width, circuloFin.Img.Height), Color.Red);
                    sBatch.Draw(circuloInicio.Img, new Rectangle((int)circuloInicio.posicion.X, (int)circuloInicio.posicion.Y, circuloInicio.Img.Width, circuloInicio.Img.Height), Color.Red);
                    if (reversa != null && reversa.Enabled && reversa.Visible)
                    {
                        sBatch.Draw(reversa.Img, new Rectangle((int)reversa.posicion.X, (int)reversa.posicion.Y, reversa.Img.Width, reversa.Img.Height), Color.Red);
                    }
                    if (slow != null && slow.Enabled && slow.Visible)
                    {
                        sBatch.Draw(slow.Img, new Rectangle((int)slow.posicion.X, (int)slow.posicion.Y, slow.Img.Width, slow.Img.Height), Color.Red);
                    }
                    if (extraLife != null && extraLife.Enabled && extraLife.Visible)
                    {
                        sBatch.Draw(extraLife.Img, new Rectangle((int)extraLife.posicion.X, (int)extraLife.posicion.Y, extraLife.Img.Width, extraLife.Img.Height), Color.Red);
                    }
                    if (shrinker != null && shrinker.Enabled && shrinker.Visible)
                    {
                        sBatch.Draw(shrinker.Img, new Rectangle((int)shrinker.posicion.X, (int)shrinker.posicion.Y, shrinker.Img.Width, shrinker.Img.Height), Color.Red);
                    }
                    if (plusTime != null && plusTime.Enabled && plusTime.Visible)
                    {
                        sBatch.Draw(plusTime.Img, new Rectangle((int)plusTime.posicion.X, (int)plusTime.posicion.Y, plusTime.Img.Width, plusTime.Img.Height), Color.Red);
                    }
                    sBatch.DrawString(fuenteBig,"P A U S A", new Vector2(500,350),Color.White);
                    #endregion
                    #region Barra Superior
                    sBatch.Draw(imgBarraSuperior, new Rectangle(10, 40, imgBarraSuperior.Width, imgBarraSuperior.Height), Color.Red);
                    sBatch.DrawString(fuenteSmall, "" + score, new Vector2(170, 50), Color.Red);
                    sBatch.DrawString(fuenteSmall, "" + tiempo, new Vector2(440, 50), Color.Red);
                    sBatch.DrawString(fuenteSmall, "" + nivel, new Vector2(670, 50), Color.Red);
                    for (int i = 0; i < vidas; i++)
                    {
                        sBatch.Draw(imgVida, new Rectangle(950 - (i * 45), 50, imgVida.Width, imgVida.Height), Color.Red);
                    }
                    #endregion
                }
                #endregion
                #region Juego normal
                else
                {
                    #region Obstaculos, circulos y power ups
                    sBatch.Draw(imgF, new Vector2(0, 0), Color.White);
                    foreach (Obstaculo obst in paredesObst) {
                        sBatch.Draw(obst.Img, new Rectangle((int)obst.posicion.X, (int)obst.posicion.Y, obst.Img.Width, obst.Img.Height), colorMundo);
                    }
                    sBatch.Draw(circuloFin.Img, new Rectangle((int)circuloFin.posicion.X, (int)circuloFin.posicion.Y, circuloFin.Img.Width, circuloFin.Img.Height), Color.White);
                    sBatch.Draw(circuloInicio.Img, new Rectangle((int)circuloInicio.posicion.X, (int)circuloInicio.posicion.Y, circuloInicio.Img.Width, circuloInicio.Img.Height), Color.White);
                    if (reversa != null && reversa.Enabled && reversa.Visible) {
                        sBatch.Draw(reversa.Img, new Rectangle((int)reversa.posicion.X, (int)reversa.posicion.Y, reversa.Img.Width, reversa.Img.Height), Color.White);
                    }
                    if (slow != null && slow.Enabled && slow.Visible)
                    {
                        sBatch.Draw(slow.Img, new Rectangle((int)slow.posicion.X, (int)slow.posicion.Y, slow.Img.Width, slow.Img.Height), Color.White);
                    }
                    if (extraLife != null && extraLife.Enabled && extraLife.Visible)
                    {
                        sBatch.Draw(extraLife.Img, new Rectangle((int)extraLife.posicion.X, (int)extraLife.posicion.Y, extraLife.Img.Width, extraLife.Img.Height), Color.White);
                    }
                    if (shrinker != null && shrinker.Enabled && shrinker.Visible)
                    {
                        sBatch.Draw(shrinker.Img, new Rectangle((int)shrinker.posicion.X, (int)shrinker.posicion.Y, shrinker.Img.Width, shrinker.Img.Height), Color.White);
                    }
                    if (plusTime != null && plusTime.Enabled && plusTime.Visible)
                    {
                        sBatch.Draw(plusTime.Img, new Rectangle((int)plusTime.posicion.X, (int)plusTime.posicion.Y, plusTime.Img.Width, plusTime.Img.Height), Color.White);
                    }
                    #endregion
                    #region Barra Superior
                    sBatch.Draw(imgBarraSuperior, new Rectangle(10, 40, imgBarraSuperior.Width, imgBarraSuperior.Height), Color.White);
                    sBatch.DrawString(fuenteSmall, "" + score, new Vector2(170, 50), Color.White);
                    sBatch.DrawString(fuenteSmall, "" + tiempo, new Vector2(440, 50), Color.White);
                    sBatch.DrawString(fuenteSmall, "" + nivel, new Vector2(670, 50), Color.White);
                    for (int i = 0; i < vidas; i++)
                    {
                        sBatch.Draw(imgVida, new Rectangle(950 - (i * 45), 50, imgVida.Width, imgVida.Height), Color.White);
                    }
                    #endregion
                }
                #endregion
                #region Barra Entera
                if (barraEntera.Muerte)
                {
                    sBatch.Draw(barraEntera.Imagen, rImagen, null, Color.White, barraEntera.angulo, barraEntera.origen, SpriteEffects.None, 1);
                }
                if (!barraEntera.Muerte)
                {
                    if (!pausado)
                    {
                        sBatch.Draw(barraEntera.Imagen, barraEntera.posicion, null, Color.White, barraEntera.angulo, barraEntera.origen, barraEntera.escala, SpriteEffects.None, 1);
                    }
                    if (pausado)
                    {
                        sBatch.Draw(barraEntera.Imagen, barraEntera.posicion, null, Color.Red, barraEntera.angulo, barraEntera.origen, barraEntera.escala, SpriteEffects.None, 1);
                    }
                }
                #endregion
                #region Aumento en Score
                if (isDrawingPlus100)
                {
                    sBatch.Draw(plus100, rImagen, Color.White);
                }
                #endregion
                base.Draw(gameTime);
            }
            #endregion
        }

        public void ChecaColision(GameTime gameTime)
        {
            #region Matriz y rectangulo de la barra
            // Actualizar el Transform de la barra
            Matrix barraTransform = Matrix.CreateTranslation(new Vector3(-barraEntera.origen, 0.0f)) *
                // Matrix.CreateScale(block.Scale) *  would go here
                    Matrix.CreateRotationZ(barraEntera.angulo) *
                    Matrix.CreateTranslation(new Vector3(barraEntera.posicion, 0.0f));
            // Obtener el rectangulo de colisiones de la barra
            Rectangle barraRectangle = CalculateBoundingRectangle(
                     new Rectangle(0, 0, barraEntera.Imagen.Width, barraEntera.Imagen.Height),
                     barraTransform);
            #endregion
            #region Colision PAREDES Y DONAS
            for (int i = 0; i < paredesObst.Length; i++) {
                if (paredesObst[i] != null)
                {
                    // Actualizar el transform de los obstaculos
                    Matrix obstTransform = Matrix.CreateTranslation(new Vector3(paredesObst[i].Posicion, 0.0f));
                    // Calcular el rectangulo de colisiones en el mundo
                    Rectangle obstRectangle = new Rectangle((int)paredesObst[i].Posicion.X, (int)paredesObst[i].Posicion.Y,
                        paredesObst[i].Img.Width, paredesObst[i].Img.Height);

                    if (barraRectangle.Intersects(obstRectangle) && !chocado)
                    {
                        // Checar colision 
                        if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                            barraEntera.Imagen.Height, barraTextureData,
                                            obstTransform, paredesObst[i].Img.Width,
                                            paredesObst[i].Img.Height, obstTextureData[i]))
                        {
                            //Si si choco, aqui va la rutina
                            /*
                             * Rutina de choque con PARED O DONAS
                             * Se reduce una vida
                             * Se inicializa el rectangulo de imagen
                             * Se habilita la bandera chocado y de barraEntera.Muerte
                             * Reproducir el sonido de choque
                             * El cambio de nivel se manda llamar en el metodo Update
                             * */
                            vidas--;

                            rImagen.X = (int)barraEntera.posicion.X;
                            rImagen.Y = (int)barraEntera.posicion.Y;
                            rImagen.Width = barraEntera.Imagen.Width;
                            rImagen.Height = barraEntera.Imagen.Height;

                            chocado = true;
                            barraEntera.Muerte = true;
                            explosionRojo.AddParticles(barraEntera.posicion);
                            explosionAzul.AddParticles(barraEntera.posicion);
                            explosionCentro.AddParticles(barraEntera.posicion);
                            smoke.AddParticles(barraEntera.posicion);

                            audioComponent.PlayCue("lifeDown");
                            
                        }
                        
                    }
                    else
                    {
                        
                    }
                }
            }
            #endregion 
            #region Colision REVERSA
            if (reversa != null)
            {
                // Actualizar el transform de los obstaculos
                Matrix reversaTransform = Matrix.CreateTranslation(new Vector3(reversa.Posicion, 0.0f));
                // Calcular el rectangulo de colisiones en el mundo
                Rectangle reversaRectangle = new Rectangle((int)reversa.Posicion.X, (int)reversa.Posicion.Y,
                    reversa.Img.Width, reversa.Img.Height);
                /*
                 * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
                 * */
                if (barraRectangle.Intersects(reversaRectangle) && !isReversa)
                {
                    // Checar colision 
                    if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                        barraEntera.Imagen.Height, barraTextureData,
                                        reversaTransform, reversa.Img.Width,
                                        reversa.Img.Height, reversaTextureData))
                    {
                        /*
                         * Rutina de choque con REVERSA
                         * Se invierte el giro, se inicializa el rectangulo de imagen
                         * Se deshabilita el obstaculo reversa
                         * Se habilitan las banderas isReversa e isDrawingPlus100
                         * Se aumentan 100 puntos al score
                         * Se reproduce el sonido de reversa
                         * */
                        barraEntera.VelocidadGiro *= -1;
                        
                        rImagen.X = (int)reversa.posicion.X;
                        rImagen.Y = (int)reversa.posicion.Y;
                        rImagen.Width = plus100.Width / 4;
                        rImagen.Height = plus100.Height / 4;

                        reversa.Enabled = false;
                        reversa.Visible = false;
                        isReversa = true;
                        isDrawingPlus100 = true;

                        puntosExtras += 100;
                        score += 100;

                        audioComponent.PlayCue("reversa");
                    }
                }
                //Si no intersecta ni con el rectangulo
                else
                {

                }
            }
            #endregion
            #region Colision SLOW
            if (slow != null)
            {
                // Actualizar el transform de los obstaculos
                Matrix slowTransform = Matrix.CreateTranslation(new Vector3(slow.Posicion, 0.0f));
                // Calcular el rectangulo de colisiones en el mundo
                Rectangle slowRectangle = new Rectangle((int)slow.Posicion.X, (int)slow.Posicion.Y,
                    slow.Img.Width, slow.Img.Height);
                /*
                 * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
                 * */
                if (barraRectangle.Intersects(slowRectangle) && !isSlow)
                {
                    // Checar colision 
                    if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                        barraEntera.Imagen.Height, barraTextureData,
                                        slowTransform, slow.Img.Width,
                                        slow.Img.Height, slowTextureData))
                    {
                        /*
                         * Rutina de choque con SLOW
                         * Se ralentiza el giro, se inicializa el rectangulo de imagen
                         * Se deshabilita el obstaculo slow
                         * Se habilitan las banderas isSlow e isDrawingPlus100
                         * Se aumentan 100 puntos al score
                         * Se reproduce el sonido de slow
                         * */
                        barraEntera.VelocidadGiro *= 0.3f;

                        rImagen.X = (int)slow.posicion.X;
                        rImagen.Y = (int)slow.posicion.Y;
                        rImagen.Width = plus100.Width / 4;
                        rImagen.Height = plus100.Height / 4;

                        slow.Enabled = false;
                        slow.Visible = false;
                        isSlow = true;
                        isDrawingPlus100 = true;

                        puntosExtras += 100;
                        score += 100;

                        audioComponent.PlayCue("Slow");
                        
                    }
                }
                //Si no intersecta ni con el rectangulo
                else
                {

                }
            }
            #endregion
            #region Colision 1 UP
            if (extraLife != null)
            {
                // Actualizar el transform de los obstaculos
                Matrix extraLifeTransform = Matrix.CreateTranslation(new Vector3(extraLife.Posicion, 0.0f));
                // Calcular el rectangulo de colisiones en el mundo
                Rectangle extraLifeRectangle = new Rectangle((int)extraLife.Posicion.X, (int)extraLife.Posicion.Y,
                    extraLife.Img.Width, extraLife.Img.Height);
                /*
                 * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
                 * */
                if (barraRectangle.Intersects(extraLifeRectangle) && !isExtraLife)
                {
                    // Checar colision 
                    if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                        barraEntera.Imagen.Height, barraTextureData,
                                        extraLifeTransform, extraLife.Img.Width,
                                        extraLife.Img.Height, extraLifeTextureData))
                    {
                        /*
                         * Rutina de choque con 1 UP
                         * Se le aumenta una vida
                         * Se deshabilita el obstaculo extraLife
                         * Se habilitan las banderas isExtraLife
                         * Se aumentan 1000 puntos al score
                         * Se reproduce el sonido de 1 UP
                         * */
                        vidas += 1;

                        rImagen.X = (int)extraLife.posicion.X;
                        rImagen.Y = (int)extraLife.posicion.Y;
                        rImagen.Width = plus100.Width / 4;
                        rImagen.Height = plus100.Height / 4;

                        extraLife.Enabled = false;
                        extraLife.Visible = false;
                        isExtraLife = true;
                        isDrawingPlus100 = true;

                        puntosExtras += 100;
                        score += 100;

                        //cue.Stop(AudioStopOptions.AsAuthored);
                        audioComponent.PlayCue("lifeUp");
                    }
                }
                //Si no intersecta ni con el rectangulo
                else
                {

                }
            }
            #endregion
            #region Colision SHRINKER
            if (shrinker != null)
            {
                // Actualizar el transform de los obstaculos
                Matrix shrinkerTransform = Matrix.CreateTranslation(new Vector3(shrinker.Posicion, 0.0f));
                // Calcular el rectangulo de colisiones en el mundo
                Rectangle shrinkerRectangle = new Rectangle((int)shrinker.Posicion.X, (int)shrinker.Posicion.Y,
                    shrinker.Img.Width, shrinker.Img.Height);
                /*
                 * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
                 * */
                if (barraRectangle.Intersects(shrinkerRectangle) && !isShrinker)
                {
                    // Checar colision 
                    if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                        barraEntera.Imagen.Height, barraTextureData,
                                        shrinkerTransform, shrinker.Img.Width,
                                        shrinker.Img.Height, shrinkerTextureData))
                    {
                        /*
                         * Rutina de choque con SHRINKER
                         * Se cambia la imagen de la barra por una mas pequena
                         * Se deshabilita el obstaculo shrinker
                         * Se habilitan las banderas isShrinker
                         * Se aumentan 100 puntos al score
                         * Se reproduce el sonido de SHRINKER
                         * */
                        barraEntera.Imagen = imgVida;

                        rImagen.X = (int)shrinker.posicion.X;
                        rImagen.Y = (int)shrinker.posicion.Y;
                        rImagen.Width = plus100.Width / 4;
                        rImagen.Height = plus100.Height / 4;

                        shrinker.Enabled = false;
                        shrinker.Visible = false;
                        isShrinker = true;
                        isDrawingPlus100 = true;

                        puntosExtras += 100;
                        score += 100;

                        audioComponent.PlayCue("Shrink");
                    }
                }
                //Si no intersecta ni con el rectangulo
                else
                {

                }
            }
            #endregion
            #region Colision PLUS TIME
            if (plusTime != null)
            {
                // Actualizar el transform de los obstaculos
                Matrix plusTimeTransform = Matrix.CreateTranslation(new Vector3(plusTime.Posicion, 0.0f));
                // Calcular el rectangulo de colisiones en el mundo
                Rectangle plusTimeRectangle = new Rectangle((int)plusTime.Posicion.X, (int)plusTime.Posicion.Y,
                    plusTime.Img.Width, plusTime.Img.Height);
                /*
                 * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
                 * */
                if (barraRectangle.Intersects(plusTimeRectangle) && !isPlusTime)
                {
                    // Checar colision 
                    if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                        barraEntera.Imagen.Height, barraTextureData,
                                        plusTimeTransform, plusTime.Img.Width,
                                        plusTime.Img.Height, plusTimeTextureData))
                    {
                        /*
                         * Rutina de choque con PLUS TIME
                         * Se aumenta el tiempo en 5000
                         * Se deshabilita el obstaculo plusTime
                         * Se habilitan las banderas isPlusTime
                         * Se aumentan 100 puntos al score
                         * Se reproduce el sonido de PlusTime
                         * */
                        tiempo += 5000;

                        rImagen.X = (int)plusTime.posicion.X;
                        rImagen.Y = (int)plusTime.posicion.Y;
                        rImagen.Width = plus100.Width / 4;
                        rImagen.Height = plus100.Height / 4;

                        plusTime.Enabled = false;
                        plusTime.Visible = false;
                        isPlusTime = true;
                        isDrawingPlus100 = true;

                        puntosExtras += 100;
                        score += 100;

                        audioComponent.PlayCue("plusTime");
                    }
                }
                //Si no intersecta ni con el rectangulo
                else
                {

                }
            }
            #endregion
            #region Colision FINAL
            // Actualizar el transform de los obstaculos
            Matrix finTransform = Matrix.CreateTranslation(new Vector3(circuloFin.Posicion, 0.0f));
            // Calcular el rectangulo de colisiones en el mundo
            Rectangle finRectangle = new Rectangle((int)circuloFin.Posicion.X, (int)circuloFin.Posicion.Y,
                circuloFin.Img.Width, circuloFin.Img.Height);
            /*
             * Como el chequeo de colisiones pixel x pixel es muy costoso, se revisa primero con rectangulos
             * */
            if (barraRectangle.Intersects(finRectangle))
            {
                // Checar colision 
                if (IntersectPixels(barraTransform, barraEntera.Imagen.Width,
                                    barraEntera.Imagen.Height, barraTextureData,
                                    finTransform, circuloFin.Img.Width,
                                    circuloFin.Img.Height, finTextureData))
                {
                    /*
                     * Rutina de colision con FIN
                     * Se habilita la bandera del final
                     * Se aumenta el nivel del juego en uno.
                     * Reproducir el sonido de fin de nivel.
                     * SI el nivel del juego esta dentro de los limites, cambiar el valor de inicioNivel a true.
                     * El cambio de nivel se hace en la pantalla de Nivel Pasado
                     * SI el nivel del juego esta fuera de los limites, sumar el score y terminar el juego.
                     * */
                    if (finNivel == false)
                    {
                        finNivel = true;
                        nivel++;
                        if (nivel <= maxNiveles)
                        {
                            cue.Stop(AudioStopOptions.Immediate);
                            cue = audioComponent.SoundBank.GetCue("nextLevel");
                            cue.Play();
                            score += (int)tiempo + (vidas * 150);
                            inicioNivel = true;
                        }
                        else if(nivel > maxNiveles)
                        {
                            score += (int)tiempo + (vidas * 150);
                            cue.Stop(AudioStopOptions.Immediate);
                            audioComponent.PlayCue("winGame");
                            terminado = true;                            
                        }
                    }
                    //Console.WriteLine("Nivel " + nivel);
                }
            }
            else
            {
                finNivel = false;
            }
            #endregion

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
        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
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


        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels between two
        /// sprites.
        /// </summary>
        /// <param name="transformA">World transform of the first sprite.</param>
        /// <param name="widthA">Width of the first sprite's texture.</param>
        /// <param name="heightA">Height of the first sprite's texture.</param>
        /// <param name="dataA">Pixel color data of the first sprite.</param>
        /// <param name="transformB">World transform of the second sprite.</param>
        /// <param name="widthB">Width of the second sprite's texture.</param>
        /// <param name="heightB">Height of the second sprite's texture.</param>
        /// <param name="dataB">Pixel color data of the second sprite.</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        public static bool IntersectPixels(
                            Matrix transformA, int widthA, int heightA, Color[] dataA,
                            Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }


        /// <summary>
        /// Calculates an axis aligned rectangle which fully contains an arbitrarily
        /// transformed axis aligned rectangle.
        /// </summary>
        /// <param name="rectangle">Original bounding rectangle.</param>
        /// <param name="transform">World transform of the rectangle.</param>
        /// <returns>A new rectangle which contains the trasnformed rectangle.</returns>
        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}