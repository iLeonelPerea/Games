using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace BattleFieldX
{
        public class enemigos: Microsoft.Xna.Framework.DrawableGameComponent
        {
        private Texture2D texturaR,texturaD,texturaL,texturaU;
        private Vector2 posicion=new Vector2();
        private float velocidadX = 1.8f;
        private Rectangle pantalla;
        private int direc = 1;
        
        public enemigos(Game game, ref Texture2D texturaR, ref Texture2D texturaD, ref Texture2D texturaL, ref Texture2D texturaU)
            : base(game)
        {
            this.texturaR = texturaR;
            this.texturaD = texturaD;
            this.texturaL = texturaL;
            this.texturaU = texturaU;
            pantalla = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            Iniciar();
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
           // KeyboardState teclado = Keyboard.GetState();
            if (direc==2){
                posicion.X -= velocidadX;
            }else if (direc==0){
                posicion.X += velocidadX;
            }else if (direc==1){
                posicion.Y += velocidadX;             
            }else if (direc == 3){
                posicion.Y -= velocidadX;                
            }

            ChecaColision();
            base.Update(gameTime);
        }
        public int getX()
        {
            return (int)posicion.X;
        }
        public int getY()
        {
            return (int)posicion.Y;
        }
        public int checaDireccion() {
            
            if (direc == 1)
            {
                return 1;//abajo
            }
            if(direc ==3)
            {
                return 3;//arriba
            }
            
            if(direc ==0)
            {
                return 0;//derecha
            }
            else
            {
                    return 2;//izquierda
            }

 
        }
        public int getWidth(int direccion) { 
            switch(direccion){
                case 0:
                    return texturaR.Width;
                case 1:
                    return texturaD.Width;
                case 2:
                    return texturaL.Width;
                case 3:
                    return texturaU.Width;

            }
            return 0;
        }
        public int getHeight(int direccion)
        {
            switch (direccion)
            {
                case 0:
                    return texturaR.Height;
                case 1:
                    return texturaD.Height;
                case 2:
                    return texturaL.Height;
                case 3:
                    return texturaU.Height;

            }
            return 0;
        }
        public void ChecaColision() {
            //Derecha
            if (posicion.X + getWidth(checaDireccion()) >= pantalla.Width) {
                posicion.X = pantalla.Width - getWidth(checaDireccion())-20;
                direc = direccion(0);
            }
            //Izquierda
            if (posicion.X < 20)
            {
                posicion.X = 20;
                direc = direccion(2);
            }
            if (posicion.Y + getHeight(checaDireccion()) >= pantalla.Height-20)
            {
                direc = direccion(1);
                posicion.Y = pantalla.Height - getHeight(checaDireccion())-20;
            }
            //arriba
            if (posicion.Y < 20)
            {
                direc = direccion(3);
                posicion.Y = 20;
            }
            Rectangle ladoC1 = new Rectangle(180, 200, 560, 20);
            Rectangle ladoc2 = new Rectangle(180, 480, 560, 20);
            if (checaDireccion() == 0 && posicion.X + getWidth(checaDireccion()) >= 180 && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                direc=direccion(0);
                posicion.X = 160 - getWidth(checaDireccion());
            }
            if (checaDireccion() == 1 && (posicion.Y + getHeight(checaDireccion()) >= 200 || posicion.Y + getHeight(checaDireccion()) >= 480) && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                direc = direccion(1);
                if (posicion.Y < 200)
                {
                    posicion.Y = 200 - getHeight(checaDireccion());
                }
                else
                {
                    posicion.Y = 480 - getHeight(checaDireccion());
                }
                
            }
            if (checaDireccion() == 3 && posicion.X <= 740 && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                posicion.X = 740;
                direc = direccion(3);
            }
            if (checaDireccion() == 4 && (posicion.Y < 220 || posicion.Y < 500) && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                direc = direccion(4);
                if (posicion.Y <= 220)
                {
                    posicion.Y = 220;
                }
                else
                {
                    posicion.Y = 500;
                }
                
            }
            
        }
        public int direccion(int x) {
            Random ax = new Random();
            int a = ax.Next(4);
            while (a == x) {
                a = ax.Next(4);
            }
        //    int a = ax;
            Console.WriteLine(a);
            return a;        
        }
        public void Iniciar() {
            posicion.X = pantalla.Width /2  - texturaR.Width / 2;
            posicion.Y = pantalla.Height/3 + texturaR.Height;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (checaDireccion() == 0)
            {
                sBatch.Draw(texturaR, posicion, Color.Red);
            }
            if (checaDireccion() == 1)
            {
                sBatch.Draw(texturaD, posicion, Color.Red);
            }
            if (checaDireccion() == 2)
            {
                sBatch.Draw(texturaL, posicion, Color.Red);
            }
            if (checaDireccion() == 3)
            {
                sBatch.Draw(texturaU, posicion, Color.Red);
            }
            base.Draw(gameTime);
        }
        public Rectangle GetArea() {
            return new Rectangle((int)posicion.X, (int)posicion.Y, getWidth(checaDireccion()), getHeight(checaDireccion()));
        }
    }    
}
