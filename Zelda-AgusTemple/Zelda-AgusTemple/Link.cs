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
using WiimoteLib;


namespace Zelda_AgusTemple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Link : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private List<Texture2D> Estados= new List<Texture2D>();
        private Texture2D textura, espada, corazon1, corazon2, corazon3, textllave;
        private List<Texture2D> Vida = new List<Texture2D>();
        private bool[] keys= new bool[3];
        private Vector2 posicion = new Vector2();
        private Vector2 posicionEspada = new Vector2();
        private Rectangle pantalla;
        AudioComponent audio;
        TimeSpan tiempo;
        int contador, edoactual, iniEspada, vida, llaves, herido;
        double angulo;
        bool activa, bh;
        public Wiimote wm;
        public Link(Game game, int life, int px, int py)
            : base(game)
        {
            Estados.Add(Game.Content.Load<Texture2D>("Link/Camina_abajo1"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/Camina_abajo2"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaArriba1"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaArriba2"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaIzq1"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaIzq2"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaDer1"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/CaminaDer2"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/EspadaAbajo"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/EspadaArriba"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/EspadaIzq"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/EspadaDer"));
            Estados.Add(Game.Content.Load<Texture2D>("Link/Encontro"));
            Vida.Add(Game.Content.Load<Texture2D>("Link/Vida1"));
            Vida.Add(Game.Content.Load<Texture2D>("Link/Vida2"));
            Vida.Add(Game.Content.Load<Texture2D>("Link/Vida3"));
            vida = life;
            llaves=0;
            CambiarCor();
            espada = Game.Content.Load<Texture2D>("Espada/left");
            textllave = Game.Content.Load<Texture2D>("Objetos/keys");
            tiempo = new TimeSpan(0,0,0);
            textura = Estados[0];
            contador = 0;
            herido = 0;
            angulo = 3*Math.PI/2;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            posicion.X = px-(textura.Width/2);
            posicion.Y = py;
            audio = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
            activa = false;
            posicionEspada.X = posicion.X + 12;
            posicionEspada.Y = posicion.Y + 60;
            keys[0] = false;
            keys[1] = false;
            keys[2] = false;
            bh = false;
            /*wm = new Wiimote();
            wm.Connect();
            wm.WiimoteChanged += wm_WiimoteChanged;
            wm.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;
            wm.SetReportType(Wiimote.InputReport.IRAccel, true);
             */ 
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
            cambiarPosEsp();
            
            // TODO: Add your update code here
            if (!activa)
            {
                CambiarEdo(Mover());
               // CambiarEdo(MoverWM());
            }
            else
            {

                if (iniEspada == 40)
                {
                    activa = false;
                    iniEspada = 0;

                    textura = Estados[edoactual];
                }
                iniEspada++;
            }
            tiempo=gameTime.TotalGameTime;
            contador++;
            if (herido > 0 && bh)
            {
                //wm.SetRumble(true);
                herido++;
            }
            if (herido == 3)
            {
                herido = 0;
                //wm.SetRumble(false);
                bh = false;
            }
            //espada.Posicion = posicion;
            base.Update(gameTime);
        }
        public void cambiarPosEsp()
        {
            if (edoactual > 1)
            {
                if (edoactual > 3)
                {
                    if (edoactual > 5)
                    {
                        posicionEspada.X = posicion.X + 55;
                        posicionEspada.Y = posicion.Y + 27;
                    }
                    else
                    {
                        posicionEspada.X = posicion.X - 20;
                        posicionEspada.Y = posicion.Y + 13;
                    }
                }
                else
                {
                    posicionEspada.X = posicion.X + 15;
                    posicionEspada.Y = posicion.Y - 20;
                }

            }
            else
            {
                posicionEspada.X = posicion.X + 12;
                posicionEspada.Y = posicion.Y + 60;
            }
        }
        public void CambiarEdo(int actual){
            if (actual == 0)
                return;
            if (contador < 15)
            {
                return;
            }
            else
            {
                switch (actual)
                {
                    case 1: if (edoactual == 0)
                        {
                            textura = Estados[1];
                            edoactual = 1;
                        }
                        else
                        {
                            edoactual = 0;
                            textura = Estados[0];
                        }
                        angulo = 3*Math.PI / 2;
                        break;
                    case 2: if (edoactual == 2)
                            {
                                textura = Estados[3];
                                edoactual = 3;
                            }
                            else
                            {
                                edoactual = 2;
                                textura = Estados[2];
                            }
                            angulo = Math.PI / 2;
                            break;
                    case 3: if (edoactual == 4)
                            {
                                textura = Estados[5];
                                edoactual = 5;
                            }
                            else
                            {
                                edoactual = 4;
                                textura = Estados[4];
                            }
                            angulo = 0;
                            break;
                    case 4: if (edoactual == 6)
                            {
                                textura = Estados[7];
                                edoactual = 7;
                            }
                            else
                            {
                                edoactual = 6;
                                textura = Estados[6];
                            }
                            angulo = Math.PI;
                            break;
                    case 5: audio.PlayCue("bigslash");
                            switch (edoactual)
                            {
                                case 0: textura = Estados[8]; break;
                                case 1: textura = Estados[8]; break;
                                case 2: textura = Estados[9]; break;
                                case 3: textura = Estados[9]; break;
                                case 4: textura = Estados[10]; break;
                                case 5: textura = Estados[10]; break;
                                case 6: textura = Estados[11]; break;
                                case 7: textura = Estados[11]; break;
                            }
                            activa = true;
                            iniEspada = 0;
                            break;
                }
                contador = 0;
            }
        }
        public int Mover()
        {
            KeyboardState teclado = Keyboard.GetState();
            if (!teclado.IsKeyDown(Keys.Space))
            {
            if (teclado.IsKeyDown(Keys.Left))
            {
                posicion.X -= 2;
                return 3;
            }
            if (teclado.IsKeyDown(Keys.Right))
            {
                posicion.X += 2;
                return 4;
            }
            if (teclado.IsKeyDown(Keys.Up))
            {
                posicion.Y -= 2;
                return 2;
            }
            if (teclado.IsKeyDown(Keys.Down))
            {
                posicion.Y += 2;
                return 1;
            }
            
            }
            if (teclado.IsKeyDown(Keys.Space))
            {
                return 5;
            }
            return 0;
        }
        /*
        public int MoverWM()
        {
            if (!wm.WiimoteState.ButtonState.Two)
            {
                if (wm.WiimoteState.ButtonState.Up)
                {
                    posicion.X -= 2;
                    return 3;
                }
                if (wm.WiimoteState.ButtonState.Down)
                {
                    posicion.X += 2;
                    return 4;
                }
                if (wm.WiimoteState.ButtonState.Right)
                {
                    posicion.Y -= 2;
                    return 2;
                }
                if (wm.WiimoteState.ButtonState.Left)
                {
                    posicion.Y += 2;
                    return 1;
                }
            }
            if (wm.WiimoteState.ButtonState.Two)
            {
                return 5;
            }
            return 0;
        }
         */ 
        public void tomoLlave()
        {
            llaves++;

        }
        public void usoLlave()
        {
            llaves--;
        }
        public int getLlave()
        {
            return llaves;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (activa)
                sBatch.Draw(espada, posicionEspada, null, Color.White, (float)angulo, new Vector2(0,0), 1.0f, SpriteEffects.None, 0f);
            
            sBatch.Draw(textura, posicion, Color.White);
            sBatch.Draw(corazon1, new Vector2(20,10), Color.White);
            sBatch.Draw(corazon2, new Vector2(40, 10), Color.White);
            sBatch.Draw(corazon3, new Vector2(60, 10), Color.White);
            sBatch.Draw(textllave, new Vector2(80, 20), Color.White);
            sBatch.DrawString(Game.Content.Load<SpriteFont>("Arial"), "" + tiempo.Seconds, new Vector2(20, 20), Color.White);
            sBatch.DrawString(Game.Content.Load<SpriteFont>("Arial"), "  X " + llaves, new Vector2(90, 20), Color.White);
            base.Draw(gameTime);
        }
        public bool getAtaque()
        {
            return activa;
        }
        public Vector2 getPos()
        {
            return posicion;
        }
        public int getVida()
        {
            return vida;
        }
        public Rectangle getArea()
        {
            return new Rectangle((int)posicion.X, (int)posicion.Y, 32, 32);
        }
        public void setX(float x)
        {
            posicion.X=x;
        }
        public void setY(float y)
        {
            posicion.Y = y;
        }
        public Rectangle GetEspadaArea()
        {
            return new Rectangle((int)posicionEspada.X, (int)posicionEspada.Y, (int)espada.Width, (int)espada.Height);
        }
        public bool Herir(Rectangle r)
        {
            if (r.Intersects(getArea()))
            {
                herido = 1;
                bh = true;
                vida--;
                CambiarCor();
                return true;
            }
            else
                return false;
        }
        /*
        private void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
        {


            if (args.Inserted)
            {
                wm.SetReportType(Wiimote.InputReport.IRAccel, true);   
                wm.SetReportType(Wiimote.InputReport.IRExtensionAccel, true);
            }
            else
                wm.SetReportType(Wiimote.InputReport.ButtonsAccel, true);   
        }

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            WiimoteState ws = args.WiimoteState;  
            System.Diagnostics.Debug.WriteLine(ws.ButtonState.A);
        }
         * */
        public void CambiarCor()
        {
            if (edoactual > 1)
            {
                if (edoactual > 3)
                {
                    if (edoactual > 5)
                    {
                        posicion.X -= 20;
                    }
                    else
                    {
                        posicion.X += 20;
                    }
                }
                else
                {
                    posicion.Y += 20;
                }

            }
            else
            {
                posicion.Y -= 20;
            }
            switch (vida)
            {
                case 6:
                    corazon1 = Vida[0];
                    corazon2 = Vida[0];
                    corazon3 = Vida[0];
                    break;
                case 5:
                    corazon1 = Vida[0];
                    corazon2 = Vida[0];
                    corazon3 = Vida[1];
                    break;
                case 4:
                    corazon1 = Vida[0];
                    corazon2 = Vida[0];
                    corazon3 = Vida[2];
                    break;
                case 3:
                    corazon1 = Vida[0];
                    corazon2 = Vida[1];
                    corazon3 = Vida[2];
                    break;
                case 2:
                    corazon1 = Vida[0];
                    corazon2 = Vida[2];
                    corazon3 = Vida[2];
                    break;
                case 1:
                    corazon1 = Vida[1];
                    corazon2 = Vida[2];
                    corazon3 = Vida[2];
                    break;
                case 0:
                    corazon1 = Vida[2];
                    corazon2 = Vida[2];
                    corazon3 = Vida[2];
                    break;
            }
        }
    }
}