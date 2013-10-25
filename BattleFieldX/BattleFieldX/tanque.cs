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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class tanque : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D texturaR,texturaD,texturaL,texturaU;
        private Vector2 posicion=new Vector2();
        private int velocidadX = 2;
        private Rectangle pantalla;
        
        private int direc = 1;

        public tanque(Game game, ref Texture2D texturaR, ref Texture2D texturaD, ref Texture2D texturaL, ref Texture2D texturaU)
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
            KeyboardState teclado = Keyboard.GetState();
            if (teclado.IsKeyDown(Keys.Left)){
                direc = 3;
                posicion.X -= velocidadX;
            }else if (teclado.IsKeyDown(Keys.Right)){
                direc = 1;
                posicion.X += velocidadX;
            }else if (teclado.IsKeyDown(Keys.Down)){
                direc = 2;
                posicion.Y += velocidadX;             
            }else if (teclado.IsKeyDown(Keys.Up)){
                direc = 4;
                posicion.Y -= velocidadX;                
            }

            ChecaColision();
            base.Update(gameTime);
        }
        public int checaDireccion() {
            
            if (direc == 2)
            {
                return 2;//abajo
            }
            if(direc ==4)
            {
                return 4;//arriba
            }
            
            if(direc ==1)
            {
                return 1;//derecha
            }
            else
            {
                    return 3;//izquierda
            }

 
        }
        public int getWidth(int direccion) { 
            switch(direccion){
                case 1:
                    return texturaR.Width;
                case 2:
                    return texturaD.Width;
                case 3:
                    return texturaL.Width;
                case 4:
                    return texturaU.Width;

            }
            return 0;
        }
        public int getHeight(int direccion)
        {
            switch (direccion)
            {
                case 1:
                    return texturaR.Height;
                case 2:
                    return texturaD.Height;
                case 3:
                    return texturaL.Height;
                case 4:
                    return texturaU.Height;

            }
            return 0;
        }
        public void ChecaColision() {
            //Derecha
            if (posicion.X + getWidth(checaDireccion()) >= pantalla.Width) {
                posicion.X = pantalla.Width - getWidth(checaDireccion())-20;
            }
            //Izquierda
            if (posicion.X < 20)
            {
                posicion.X = 20;
            }//abajo
            if (posicion.Y + getHeight(checaDireccion()) >= pantalla.Height-20)
            {
                posicion.Y = pantalla.Height - getHeight(checaDireccion())-20;
            }
            //Izquierda
            if (posicion.Y < 20)
            {
                posicion.Y = 20;
            }
            Rectangle ladoC1 = new Rectangle(180,200,560,20);
            Rectangle ladoc2 = new Rectangle(180, 480,560, 20);
            if (checaDireccion() == 1 && posicion.X + getWidth(checaDireccion()) >= 180 && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                posicion.X=160-getWidth(checaDireccion());   
            }
            if (checaDireccion() == 2 && (posicion.Y + getHeight(checaDireccion()) >= 200||posicion.Y + getHeight(checaDireccion())>=480) && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                if(posicion.Y<200){
                    posicion.Y = 200 - getHeight(checaDireccion());
                }else{
                    posicion.Y = 480 - getHeight(checaDireccion());
                }
             }
            if (checaDireccion() == 3 && posicion.X  <= 740 && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                posicion.X = 740;
            }
            if (checaDireccion() == 4 && (posicion.Y<220||posicion.Y<500) && (ladoC1.Intersects(GetArea()) || ladoc2.Intersects(GetArea())))
            {
                if(posicion.Y<=220){
                    posicion.Y=220;
                }else{
                    posicion.Y=500;
                }
            }
        }
        public int getX() { 
            return (int)posicion.X;
        }
        public int getY() { 
            return (int)posicion.Y;
        }
        public void Iniciar() {
            posicion.X = pantalla.Width / 2 - texturaR.Width / 2;
            posicion.Y = pantalla.Height - texturaR.Height;
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (checaDireccion() == 1)
            {
                sBatch.Draw(texturaR, posicion, Color.White);
            }
            if (checaDireccion() == 2)
            {
                sBatch.Draw(texturaD, posicion, Color.White);
            }
            if (checaDireccion() == 3)
            {
                sBatch.Draw(texturaL, posicion, Color.White);
            }
            if (checaDireccion() == 4)
            {
                sBatch.Draw(texturaU, posicion, Color.White);
            }
            base.Draw(gameTime);
        }
        
        public Rectangle GetArea() {
            return new Rectangle((int)posicion.X, (int)posicion.Y, getWidth(checaDireccion()), getHeight(checaDireccion()));
        }
    }
}