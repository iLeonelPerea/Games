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


namespace pruebaPantallas
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameObject : Microsoft.Xna.Framework.GameComponent
    {
        private Texture2D imagen;
        public Vector2 posicion;
        private float rotacion;
        private Vector2 centro;
        private Vector2 velocidad;
        private bool vivo;
        public float timer = 0f;
        public float interval = 1000f / 25f;
        public int frameCount = 10;
        public int currentFrame = 0;
        public int spriteWidth = 80;
        public int spriteHeight = 55;
        public Rectangle sourceRect;
        public Rectangle destinationRect;
        public int fuerza;

        public Vector2 Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        public bool Vivo
        {
            get { return vivo; }
            set { vivo = value; }
        }

        public Texture2D Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }
        }

        public float Rotacion
        {
            get { return rotacion; }
            set { rotacion = value; }
        }
        
        public Vector2 Centro
        {
            get { return centro; }
            set { centro = value; }
        }


        public GameObject(Game game, Texture2D img, int fuerza)
            : base(game)
        {
            rotacion = 0.0f;
            posicion = Vector2.Zero;
            imagen = img;
            centro = new Vector2(imagen.Width / 2, imagen.Height / 2);
            this.fuerza = fuerza;
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

        public void actualizaSprite(GameTime gameTime) { 
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount-1)
                {
                    currentFrame = 0;
                }
            timer = 0f;
            }
        sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
        destinationRect = new Rectangle((int)posicion.X, (int)posicion.Y, spriteWidth, spriteHeight);
        }

        public void actualizaSprite2(GameTime gameTime,int pos) {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval) {
                if(pos<0)
                    currentFrame--;
                if (pos > 0)
                    currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = frameCount-1;
                }
                if (currentFrame <= 0)
                    currentFrame = 1;
                timer = 0f;
            }
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            destinationRect = new Rectangle((int)posicion.X, (int)posicion.Y, spriteWidth, spriteHeight);
        }

        
    }
}