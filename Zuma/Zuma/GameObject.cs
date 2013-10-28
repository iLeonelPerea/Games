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


namespace Zuma
{
    class GameObject
    {
        public Texture2D sprite;
        public Vector2 posicion;
        public float rotacion;
        public Vector2 centro;
        public Vector2 velocidad;
        public bool alive;
        public Color color;
        public int posVector;
        public bool nueva, cambio;


        public GameObject(ref Texture2D loadedTexture)
        {
            rotacion = 0.0f;
            posicion = Vector2.Zero;
            sprite = loadedTexture;
            centro = new Vector2(sprite.Width / 2, sprite.Height / 2);
            velocidad = Vector2.Zero;
            alive = false;
            color = Color.Blue;
            posVector = 0;
            nueva = false;
            cambio = false;
        }
        public GameObject()
        {
            rotacion = 0.0f;
            posicion = Vector2.Zero;
            sprite = null;
            centro = Vector2.Zero;
            velocidad = Vector2.Zero;
            alive = false;
            color = Color.Blue;
            posVector = 0;
            nueva = false;
            cambio = false;
        }
    }
}