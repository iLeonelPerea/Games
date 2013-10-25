using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class FondoScroll
    {
        public Vector2 posicion = new Vector2(0,0);
        private Texture2D textura;
        public Rectangle size;
        public float escala = 1.0f;


        //Load the texture for the sprite using the Content Pipeline
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            textura = theContentManager.Load<Texture2D>(theAssetName);
            size = new Rectangle(0, 0, (int)(textura.Width * escala), (int)(textura.Height * escala));
        }

        //Draw the sprite to the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura,posicion,new Rectangle(0, 0, textura.Width, textura.Height),Color.White,0.0f, Vector2.Zero, escala, SpriteEffects.None, 0);

        }

    }
}