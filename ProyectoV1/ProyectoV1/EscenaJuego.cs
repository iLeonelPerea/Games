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
using Tetris.Core;

namespace Tetris
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaJuego : escena
    {
        private ImagenFondoTetris imgF;
        private Figura1 figura1;
        private Figura2 figura2;
        private Random generador = new Random();
        private float aleatorio = 0.1f;
        private Rectangle tetrisRect;
        private float velocidad = 1 / 2f;
        private Rectangle[,] matriz;
        private Texture2D tetris;
        private Boolean bajar = false;
        private int tipo, nextTipo;
        private int score = 0, lineas = 0,numLineas=0,nivel=1;
        private Boolean salirJuego = false;

        

        private SoundBank soundBank;


        public EscenaJuego(Game game, Texture2D imgTetris, Texture2D imgFondo, Texture2D imgFondo2, Rectangle rectan, Texture2D titulo,
            Texture2D fondoN, Texture2D[] arrImagenes)
            : base(game)
        {
            tipo = 7;
            inicializaMatris();
            imgF = new ImagenFondoTetris(game, imgFondo2, imgFondo, imgTetris, rectan, titulo, matriz, fondoN, arrImagenes);
            tetrisRect = rectan;
            figura1 = new Figura1(game, imgTetris, velocidad, tipo);
            figura2 = new Figura2(game, imgTetris, velocidad, tipo);
            Componentes.Add(imgF);
            Componentes.Add(figura2);
            //Componentes.Add(figura1);
            tetris = imgTetris;

            int x = (generador.Next(7)) + 1;
            nextTipo = x;

            soundBank = (SoundBank)game.Services.GetService(typeof(SoundBank));
        }

        public Boolean SalirJuego
        {
            get { return salirJuego; }
            set { salirJuego = value; }
        }
        

        public void inicializaMatris()
        {
            matriz = new Rectangle[10, 21];
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 21; r++)
                {
                    matriz[c, r] = new Rectangle((c * 25) + 275, (r * 25) + 85, 25, 25);
                }
            }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }



        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void parar()
        {
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is Figura1)
                {
                    Figura1 e = Componentes[i] as Figura1;
                    e.Enabled = false;
                    e.Visible = false;
                    Componentes[i] = e;
                }
            }
        }

        public void recuperar()
        {
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is Figura1)
                {
                    Figura1 e = Componentes[i] as Figura1;
                    e.Enabled = true;
                    e.Visible = true;
                    Componentes[i] = e;
                }
            }
        }


        public void jugar()
        {
            if (!bajar)
            {
                ponerPieza();
            }
            else
            {
                //verificar choque con el piso
                for (int i = 0; i < Componentes.Count; i++)
                {
                    if (Componentes[i] is Figura1)
                    {
                        Figura1 e = Componentes[i] as Figura1;
                        if (e.getChoque())
                        {
                            tomaCuadros(e);
                            Componentes.RemoveAt(i);
                            bajar = false;
                            i--;
                            //cambia score
                            //sonido
                        }
                    }
                }
                //verificar choque entre cuadros
                for (int i = 0; i < Componentes.Count; i++)
                {
                    if (Componentes[i] is Figura1)
                    {
                        Figura1 e = Componentes[i] as Figura1;
                        Rectangle[] cuadros = e.getArea();
                        for (int c = 0; c < Componentes.Count; c++)
                        {
                            Boolean cancel = false;
                            if (Componentes[c] is cuadro)
                            {
                                cuadro cuadroP = Componentes[c] as cuadro;
                                for (int r = 0; r < cuadros.Length; r++)
                                {
                                    if (cuadros[r].Intersects(cuadroP.getArea()))
                                    {
                                        //perdio_______________________________________________________________
                                        if(cuadros[r].Y<85+51){
                                            perdio();
                                        }
                                        //??????????????????????????????????????????????????
                                        if (tomaCuadros(e))
                                        {
                                            Componentes.RemoveAt(i);
                                            bajar = false;
                                            i--;
                                            cancel = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (cancel)
                            {
                                break;
                            }

                        }
                    }
                }///fin del verificar colisiones
                //////verificar lineas
                lineas = 0;
                //for (int r = 20; r >= 0; r--)
                for (int r = 0; r < 21; r++)
                {
                    int repeticiones = 0;
                    for (int c = 0; c < 10; c++)
                    {
                        for (int i = 0; i < Componentes.Count; i++)
                        {
                            if (Componentes[i] is cuadro)
                            {
                                cuadro cuadroP = Componentes[i] as cuadro;
                                Point puntoP = new Point(cuadroP.getArea().X + 12, cuadroP.getArea().Y + 12);
                                if (matriz[c, r].Contains(puntoP))
                                {
                                    repeticiones += 1;
                                }
                            }
                        }
                    }
                    if (repeticiones >= 10)
                    {
                        lineas++;
                        numLineas++;
                        //eliminarLinea(r);
                        for (int c = 0; c < 10; c++)
                        {
                            for (int i = 0; i < Componentes.Count; i++)
                            {
                                if (Componentes[i] is cuadro)
                                {
                                    cuadro cuadroP = Componentes[i] as cuadro;
                                    Point puntoP = new Point(cuadroP.getArea().X + 12, cuadroP.getArea().Y + 12);
                                    if (matriz[c, r].Contains(puntoP))
                                    {
                                        Componentes.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                        }
                        bajarTodos(r);
                        // lineas++;
                    }
                }//fin de verificar lineas
                if (lineas > 0)
                {
                    Console.WriteLine("lineas: " + lineas);
                    sumarPuntaje();
                }


            }//fin del else
            verificarNivel();
        }////fin del juagr

        public void bajarTodos(int r)
        {
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is cuadro)
                {
                    cuadro cuadroP = Componentes[i] as cuadro;
                    if (cuadroP.Posicion.Y < (r * 25) + 85)
                    {
                        cuadroP.Posicion = new Vector2(cuadroP.Posicion.X, cuadroP.Posicion.Y + 25);
                    }
                }
            }
        }

        public void sumarPuntaje()
        {
            switch (lineas)
            {
                case 1: score += 100;
                    break;
                case 2: score += 400;
                    break;
                case 3: score += 800;
                    break;
                case 4: score += 2000;
                    break;
            }
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is ImagenFondoTetris)
                {
                    ImagenFondoTetris cuadroP = Componentes[i] as ImagenFondoTetris;
                    cuadroP.setScore(score);
                    Componentes[i] = cuadroP;
                }
            }
            soundBank.PlayCue("bajar");

        }

        public void ponerPieza()
        {
            //incrementaVel();
            //Console.WriteLine("Entre a poner");
            int x = (generador.Next(7)) + 1;
            tipo = nextTipo;
            nextTipo = x;
            ponerNext();
            figura1.Tipo = tipo;
            Console.WriteLine(x);
            bajar = true;
            figura1.iniciar();
            Componentes.Add(figura1);
            //Componentes.Add(figura2);
        }

        public void incrementaVel(float numf)
        {
            velocidad = numf;
            figura1.setVelocidad(velocidad);
        }

        public void ponerNext()
        {
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is Figura2)
                {
                    Figura2 fig = Componentes[i] as Figura2;
                    fig.Tipo = nextTipo;
                    fig.iniciar();
                    Componentes[i] = fig;
                }
            }
        }

        public void ChecaColision()
        {


        }

        public Boolean tomaCuadros(Figura1 f)
        {
            Boolean tomo = true;
            Point[] puntos = f.getPuntos();
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 21; r++)
                {
                    for (int p = 0; p < puntos.Length; p++)
                    {
                        if (matriz[c, r].Contains(puntos[p]))
                        {
                            cuadro cuadrito = new cuadro(Game, ref tetris, new Vector2(matriz[c, r].X, matriz[c, r].Y), tipo);
                            //Console.WriteLine(puntos[p].X +" . "+ puntos[p].Y);
                            for (int x = 0; x < Componentes.Count; x++)
                            {
                                if (Componentes[x] is cuadro)
                                {
                                    cuadro cuadroP = Componentes[x] as cuadro;
                                    if (cuadroP.getArea().Contains(puntos[p]))
                                    {
                                        ponerPosicionAnterior();
                                        tomo = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (tomo)
            {
                tomaCuadros2(f);
            }
            return tomo;
        }//FIN DEL METODO

        public void tomaCuadros2(Figura1 f)
        {
            Point[] puntos = f.getPuntos();
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 21; r++)
                {
                    for (int p = 0; p < puntos.Length; p++)
                    {
                        if (matriz[c, r].Contains(puntos[p]))
                        {
                            cuadro cuadrito = new cuadro(Game, ref tetris, new Vector2(matriz[c, r].X, matriz[c, r].Y), tipo);
                            //Console.WriteLine(puntos[p].X +" . "+ puntos[p].Y);
                            Componentes.Add(cuadrito);
                        }

                    }
                }
            }
        }//FIN DEL METODO


        public void ponerPosicionAnterior()
        {
            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is Figura1)
                {
                    Figura1 e = Componentes[i] as Figura1;
                    e.Posicion = e.getPrevPos();
                    Componentes[i] = e;
                }
            }
        }

        public void verificarNivel()
        {
            if (numLineas >= 20*nivel)
            {
                nivel++;
                incrementaVel((float)(.5*nivel));
            }



            for (int i = 0; i < Componentes.Count; i++)
            {
                if (Componentes[i] is ImagenFondoTetris)
                {
                    ImagenFondoTetris fondo1 = Componentes[i] as ImagenFondoTetris;
                    fondo1.setNumLineas(numLineas);
                    fondo1.setNivel(nivel);
                    Componentes[i] = fondo1;
                }
            }
        }

        public void perdio()
        {
            salirJuego = true;
        }

    
    }//fin del la clase
}//fin del namespace