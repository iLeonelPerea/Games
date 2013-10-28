using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace Zelda_AgusTemple
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class EscenaTeclado : Escena
    {
        protected Texture2D fondo,teclado,enter,backspace;
        protected SpriteFont fuente,fMensaje;
        protected SpriteBatch sBatch;
        public Link jugador;
        protected string nombre;
        protected string mensaje;
        protected int score;
        protected AudioComponent audioComponent;
        private MouseState estadoAnterior;
        private char caracter;
        public struct Tecla{
            public char c;
            public int x;
            public int y;
            public Tecla(char cc,int xx,int yy){
                c=cc;
                x=xx;
                y=yy;
            }            
        }
        public char Caracter
        {
            get { return caracter; }
        }
        Tecla[] teclas = new Tecla[27];      
        
        protected KeyboardState oldKeyboardState;
        public EscenaTeclado(Game game, Link l)
            : base(game)
        {
            audioComponent = (AudioComponent)Game.Services.GetService(typeof(AudioComponent));
            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            fondo = Game.Content.Load<Texture2D>("back");
            teclado = Game.Content.Load<Texture2D>("Keyboard");
            fuente = Game.Content.Load<SpriteFont>("Arial");
            fMensaje = Game.Content.Load<SpriteFont>("Arial");
            backspace = Game.Content.Load<Texture2D>("Enter");
            enter = Game.Content.Load<Texture2D>("Backspace");
            jugador = l;
            teclas[0] = new Tecla('Q', 100, 107);
            teclas[1] = new Tecla('W', 130, 107);
            teclas[2] = new Tecla('E', 160, 107);
            teclas[3] = new Tecla('R', 190, 107);
            teclas[4] = new Tecla('T', 220, 107);
            teclas[5] = new Tecla('Y', 250, 107);
            teclas[6] = new Tecla('U', 280, 107);
            teclas[7] = new Tecla('I', 310, 107);
            teclas[8] = new Tecla('O', 340, 107);
            teclas[9] = new Tecla('P', 370, 107);
            teclas[10] = new Tecla('A', 106, 137);
            teclas[11] = new Tecla('S', 136, 137);
            teclas[12] = new Tecla('D', 166, 137);
            teclas[13] = new Tecla('F', 196, 137);
            teclas[14] = new Tecla('G', 226, 137);
            teclas[15] = new Tecla('H', 256, 137);
            teclas[16] = new Tecla('J', 286, 137);
            teclas[17] = new Tecla('K', 316, 137);
            teclas[18] = new Tecla('L', 346, 137);
            teclas[19] = new Tecla('Ñ', 376, 137);
            teclas[20] = new Tecla('Z', 119, 167);
            teclas[21] = new Tecla('X', 149, 167);
            teclas[22] = new Tecla('C', 179, 167);
            teclas[23] = new Tecla('V', 209, 167);
            teclas[25] = new Tecla('B', 239, 167);
            teclas[25] = new Tecla('N', 269, 167);
            teclas[26] = new Tecla('M', 299, 167);
            estadoAnterior = Mouse.GetState();
        }

        public int Score {
            set { score = value; }
            get { return score; } 
        }
        
        public string Nombre {
            get { return nombre; }
            set { nombre = value; }
        }
        public override void Show()
        {
            nombre = "";
            mensaje = "";
            base.Show();
        }

        public void EstadoMensaje(bool estado) {
            if (estado==true)
            {
                mensaje = "Teclea tu nombre";
                
            }
            else {
                mensaje = "";
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
            ManejaRaton();
            base.Update(gameTime);
            
        }

        public void ManejaRaton() { 
            MouseState raton = Mouse.GetState();
            if (raton.LeftButton==ButtonState.Released && estadoAnterior.LeftButton==ButtonState.Pressed){
                char tecla = BuscarTecla(raton.X,raton.Y);
                if (tecla == '-') {
                    if (nombre.Length>0)
                        nombre = nombre.Substring(0, nombre.Length - 1);
                }else 
                   if (tecla == '+') {
                       caracter = '+';
                   }
                   else if (tecla != '*') {
                       nombre += tecla.ToString();
                       //nombre = "Coord: x="+ raton.X +" y="+ raton.Y ;
                   }
                
            }
            estadoAnterior = raton;
        }

        public char BuscarTecla(int x, int y) {
            char key='*';
            for (int i = 0; i < teclas.Length; i++) {
                if ((x >= teclas[i].x && (x < teclas[i].x + 30)) && (y >= teclas[i].y && (y < teclas[i].y + 30)))
                {
                    return teclas[i].c;
                }
            }
            //Backspace
            if (x >= 174 && x < 240 && y >= 245 && y <= 273) {
                return '-';
            }
            //Enter
            if (x >= 259&& x < 335 &&  y >= 245 && y <= 273)
            {
                return '+';
            }
            return key;
        }
        public override void Draw(GameTime gameTime)
        {
            //sBatch.Draw(teclado, new Vector2(0, 0), Color.White);
            base.Draw(gameTime);
            sBatch.Draw(fondo, new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height), Color.White);
            sBatch.DrawString(fuente,nombre,new Vector2(2,2),Color.White);
            sBatch.DrawString(fuente, "Has visto que bonito esta el templo!!!! ", new Vector2(2, 30), Color.Yellow);
            sBatch.DrawString(fuente, "Escribe su clave: ", new Vector2(2, 50), Color.Yellow);           
            sBatch.Draw(teclado, new Vector2(Game.Window.ClientBounds.Width / 2 - teclado.Width / 2, Game.Window.ClientBounds.Height / 2 - teclado.Height / 2), Color.White);
            sBatch.Draw(enter, new Vector2(Game.Window.ClientBounds.Width / 2 - enter.Width, Game.Window.ClientBounds.Height / 2 + teclado.Height), Color.White);
            sBatch.Draw(backspace, new Vector2(Game.Window.ClientBounds.Width / 2 , Game.Window.ClientBounds.Height / 2 + teclado.Height), Color.White);
        }
    }
}