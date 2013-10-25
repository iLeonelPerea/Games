using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using BattleFieldX.Core;

namespace BattleFieldX
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaAccion : Escena
    {
        protected Texture2D fondo, tanqueR, tanqueD, tanqueL, tanqueU, balas,cursor,block;
        protected tanque jugador1;
        protected CursorX cur;
        protected enemigos enemigo1, enemigo2, enemigo3, enemigo4, enemigo5;
        protected Rectangle pantalla,ladoI,ladoAr,ladoD,ladoAb,ladoC1,ladoc2;
        protected float nAleatorio = .01f;
        protected Random generador = new Random();
        protected SpriteFont fuente;
        TimeSpan time = TimeSpan.Zero;
        TimeSpan time2 = TimeSpan.Zero;
   //     protected AudioComponent audioComponent;
        TimeSpan tiempo = TimeSpan.Zero;
        protected int score = 0, nivel = 1,negativos=0, cont = 0,vidas=3;
        float x1=7,y1=7,xE=0f,yE=0f;
        
        protected SpriteBatch spriteBatch;

        public EscenaAccion(Game game, Texture2D fondo, Texture2D balas, Texture2D tanqueR, Texture2D tanqueD, Texture2D tanqueL, Texture2D tanqueU, SpriteFont fuente,Texture2D cursor,Texture2D block)
            : base(game)
        {
            this.fondo = fondo;
            this.balas = balas;
            this.tanqueR = tanqueR;
            this.tanqueD = tanqueD;
            this.tanqueL = tanqueL;
            this.tanqueU = tanqueU;
            this.fuente = fuente;
            this.cursor = cursor;
            this.block = block;
            pantalla = new Rectangle(0, 0, Game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            jugador1 = new tanque(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
            enemigo1 = new enemigos(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
            cur = new CursorX(Game, ref cursor);
       //     audioComponent = new AudioComponent();
        //    audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
          //  audioComponent.playCue("johnphilipsousariverkwaimarch");
           // Componentes.Add(audioComponent);
            Componentes.Add(jugador1);
            Componentes.Add(enemigo1);
            Componentes.Add(cur);

            jugador1.Initialize();
            cur.Initialize();
            enemigo1.Initialize();
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            inicio();
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            

            base.Initialize();
        }

        public void inicio() {
            ladoD = new Rectangle(880,0,20,700);
            ladoAb = new Rectangle(0, 680, 900, 20);
            ladoI = new Rectangle(0, 0, 20, 700);
            ladoAr = new Rectangle(0, 0, 900, 20);
            ladoC1 = new Rectangle(180,200,560,20);
            ladoc2 = new Rectangle(180, 480,560, 20);

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            GeneraBala(gameTime);
            ChecaColisionesBalas(gameTime);
            base.Update(gameTime);
        }
        public void GeneraBala(GameTime g)
        {

            KeyboardState tr = Keyboard.GetState();
            if (tr.IsKeyDown(Keys.A))
            {
                if (cont == 40)
                {
                    velocidad();
                    Balas e = new Balas(Game, ref balas, x1, y1, jugador1.getX(), jugador1.getY());
                    Componentes.Add(e);
                    cont = 0;
                }
                cont++;
            }

            time += g.ElapsedGameTime;
                if(time>TimeSpan.FromMilliseconds(700)){
                    velocidadEn();
                    BalasEnemy ex = new BalasEnemy(Game, ref balas, xE, yE, enemigo1.getX(), enemigo1.getY());
                    Componentes.Add(ex);
                    time=TimeSpan.Zero;
                }
            

        }
        public void velocidadX() {
            if (jugador1.getX() > cur.getX()) {
                x1 = x1*(-1);
            }
            if (jugador1.getY() > cur.getY())
            {
                y1 = y1 * (-1);
            }
        }
        public void velocidadAcomoda()
        {
            if (enemigo1.getX() > jugador1.getX())
            {
                x1 = x1 * (-1);
            }
            if (enemigo1.getY() > jugador1.getY())
            {
                y1 = y1 * (-1);
            }
        }
        public void velocidadEn() {
            yE = (float)(Math.Sin(Math.Atan2(jugador1.getY()-enemigo1.getY() , jugador1.getX()-enemigo1.getX())));
            xE = (float)(Math.Cos(Math.Atan2(jugador1.getY()-enemigo1.getY() ,jugador1.getX()- enemigo1.getX())));
            xE = 4 * (xE * xE);
            yE = 4 * (yE * yE);
            xE = (float)Math.Pow(xE,.5);
            yE = (float)Math.Pow(yE,.5);
            velocidadAcomoda();
        }
        public void velocidad() {
            if (jugador1.getX() - cur.getX()<=20&&jugador1.getX() - cur.getX()>=0){
                x1 = 0f;
                if (jugador1.getY() > cur.getY())
                {
                    y1 = -2f;
                }
                else {
                    y1 = 2f;
                }
                
            }
            if (jugador1.getX() - cur.getX() >= (20*(-1)) && jugador1.getX() - cur.getX() < 0)
            {
                x1 = 0f;
                if (jugador1.getY() > cur.getY())
                {
                    y1 = -2f;
                }
                else
                {
                    y1 = 2f;
                }
            }
            if(cur.getY()-jugador1.getY()<=20&&cur.getY()-jugador1.getY()>0)
            {
                y1 = 0;
                if (jugador1.getX() > cur.getX())
                {
                    x1 = -2f;
                }
                else
                {
                    x1 = 2f;
                }
            }
            if (cur.getY() - jugador1.getY() >=(20*(-1)) && cur.getY() - jugador1.getY() <=0)
            {
                y1 = 0;
                if (jugador1.getX() > cur.getX())
                {
                    x1 = -2f;
                }
                else
                {
                    x1 = 2f;
                }
                
            }
            else{
                y1 = (float)(Math.Sin(Math.Atan2(cur.getY()-jugador1.getY(),cur.getX()- jugador1.getX())));
                x1 = (float)(Math.Cos(Math.Atan2(cur.getY()- jugador1.getY(),cur.getX()- jugador1.getX())));
                Console.WriteLine(x1 + " c " + y1 + " " + Math.Atan2(cur.getY(), jugador1.getY()));
                x1 = 4 * (x1*x1);
                y1 = 4 * (y1*y1);
                Console.WriteLine(x1 + " b " + y1 + " h " + Math.Sin(Math.Atan2(cur.getY()-jugador1.getY(),cur.getX()- jugador1.getX())));
                x1 = (float)Math.Pow(x1, .5);
                y1 = (float)Math.Pow(y1, .5);
                Console.WriteLine(x1+" a "+y1);
               
                velocidadX();
            }

        }
        public void Jugar()
        {
          //  GeneraBala();
            ChecaColisiones();
            
            VerificaCambioDeNivel();
        }
        public void Reset() {
            while (Componentes.Count > 0) {
                Componentes.RemoveAt(0);
            }
            jugador1 = new tanque(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
            enemigo1 = new enemigos(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
            cur = new CursorX(Game, ref cursor);
            Componentes.Add(jugador1);
            Componentes.Add(enemigo1);
            Componentes.Add(cur);
        }
        public void VerificaCambioDeNivel()
        {
            if (negativos >= 5)
            {
                
                Reset();
                negativos = 0;
                nivel++;
                
            }
        }
        public void ChecaColisionesBalas(GameTime g) {
            tiempo += g.ElapsedGameTime;
            time2 += g.ElapsedGameTime;
            for (int i = 0; i < Componentes.Count; i++)
            {
                Balas e = Componentes[i] as Balas;
                if (Componentes[i] is Balas)
                {
                    if(e.ChecaColision(ladoAb)||e.ChecaColision(ladoAr)){
                        e.setY();
                    }
                    else if (e.ChecaColision(ladoD) || e.ChecaColision(ladoI))
                    {
                        e.setX();
                    }
                    else if (e.ChecaColision(ladoC1) || e.ChecaColision(ladoc2)) {
                        Componentes.RemoveAt(i);
                    }
                    else if (tiempo > TimeSpan.FromMilliseconds(10000))
                    {
                        Componentes.RemoveAt(i);
                        tiempo = TimeSpan.Zero;
                    }

                }
            }
            for (int i = 0; i < Componentes.Count; i++)
            {
                BalasEnemy b = Componentes[i] as BalasEnemy;
                if (Componentes[i] is BalasEnemy)
                {
                    if (b.ChecaColision(ladoAb) || b.ChecaColision(ladoAr))
                    {
                        b.setY();
                    }
                    else if (b.ChecaColision(ladoD) || b.ChecaColision(ladoI))
                    {
                        b.setX();
                    }
                    else if (b.ChecaColision(ladoC1) || b.ChecaColision(ladoc2))
                    {
                        Componentes.RemoveAt(i);
                    }
                    else if (time2 > TimeSpan.FromMilliseconds(5000))
                    {
                        Componentes.RemoveAt(i);
                        time2 = TimeSpan.Zero;
                    }

                }
            }
        }
        public void ChecaColisiones()
        {
            
            for (int i = 0; i < Componentes.Count; i++)
            {
                Balas e = Componentes[i] as Balas;
                if (Componentes[i] is Balas)
                {
                    
                    if (e.ChecaColision(enemigo1.GetArea()))
                    {

                        for (int xx = 0; xx < Componentes.Count; xx++)
                        {
                            if (Componentes[xx] is enemigos)
                            {

                                Componentes.RemoveAt(i);
                                if(e.ChecaColision(enemigo1.GetArea())){

                                    while (Componentes.Count > 0) {
                                        Componentes.RemoveAt(0);
                                    }
                                    enemigo1 = new enemigos(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
                                    Componentes.Add(enemigo1);
                                    Componentes.Add(jugador1);
                                    Componentes.Add(cur);
                                    negativos++;
                                    score += 100;
                                    return;
                                }
                            }

                        }
                       
                            
                        //  i--;
                        //  audioComponent.PlayCue("chiflido");
                    }
                }
            }
            
            for (int j = 0; j < Componentes.Count; j++)
            {
                BalasEnemy o = Componentes[j] as BalasEnemy;
                if (Componentes[j] is BalasEnemy)
                {

                    if (o.ChecaColision(jugador1.GetArea()))
                    {

                        for (int xx = 0; xx < Componentes.Count; xx++)
                        {
                            if (Componentes[xx] is tanque)
                            {

                                //Componentes.RemoveAt(i);
                                if (o.ChecaColision(jugador1.GetArea()))
                                {

                                    while (Componentes.Count > 0)
                                    {
                                        Componentes.RemoveAt(0);
                                    }
                                    //enemigo1 = new enemigos(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
                                    jugador1 = new tanque(Game, ref tanqueR, ref tanqueD, ref tanqueL, ref tanqueU);
                                    Componentes.Add(enemigo1);
                                    Componentes.Add(jugador1);
                                    Componentes.Add(cur);
                                    vidas--;
                                   // score += 100;
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            
        }

        public int vidasA() {
            if (vidas == 0)
            {
                while (Componentes.Count > 0)
                {
                    Componentes.RemoveAt(0);
                }
            }
            return vidas; 
        }
        public int nivelX() {
            if (nivel == 7) {
                while (Componentes.Count > 0) {
                    Componentes.RemoveAt(0);
                }
            }
            return nivel;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(fondo, new Rectangle(0, 0, pantalla.Width, pantalla.Height), new Rectangle(0, 0, fondo.Width, fondo.Height), Color.White);
            spriteBatch.DrawString(fuente, "Score: " + score.ToString() + " Nivel: " + nivel.ToString()+" Vidas: "+vidas.ToString(), new Vector2(20, 20), Color.Red);
         
            spriteBatch.Draw(block, ladoAb, Color.White);
            spriteBatch.Draw(block, ladoD, Color.White);
            spriteBatch.Draw(block, ladoI, Color.White);
            spriteBatch.Draw(block, ladoAr, Color.White);
            spriteBatch.Draw(block, ladoC1, Color.Red);
            spriteBatch.Draw(block, ladoc2, Color.Red);

            base.Draw(gameTime);
        }
    }
}