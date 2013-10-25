using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace pruebaPantallas
{
    namespace Particles
    {
        public class Particle
        {
            private Vector2 posicion;
            private Vector2 velocidad;
            private Vector2 aceleracion;
            private float tiempoVida;
            private float tiempoDesdeVivo;
            private float escala;
            private float rotacion;
            private float velRotacion;

            public bool Active {
                get { return tiempoDesdeVivo < tiempoVida; }
            }

            public void Initialize(Vector2 pos, Vector2 vel, Vector2 acel,float lifeTime, float scale, float rotationSpeed) {
                posicion = pos;
                velocidad = vel;
                aceleracion = acel;
                tiempoVida = lifeTime;
                escala = scale;
                velRotacion = rotationSpeed;


                tiempoDesdeVivo = 0.0f;
                rotacion = 0.0f;

            }

            public void Update(float dt) {
                velocidad += aceleracion * dt;
                posicion += velocidad * dt;
                rotacion += velRotacion * dt;
                tiempoDesdeVivo += dt;
            }

            public float VelRotacion
            {
                get { return velRotacion; }
                set { velRotacion = value; }
            }

            public float Rotacion
            {
                get { return rotacion; }
                set { rotacion = value; }
            }

            public float Escala
            {
                get { return escala; }
                set { escala = value; }
            }

            public float TiempoDesdeVivo
            {
                get { return tiempoDesdeVivo; }
                set { tiempoDesdeVivo = value; }
            }

            public float TiempoVida
            {
                get { return tiempoVida; }
                set { tiempoVida = value; }
            }

            public Vector2 Aceleracion
            {
                get { return aceleracion; }
                set { aceleracion = value; }
            }

            public Vector2 Posicion
            {
                get { return posicion; }
                set { posicion = value; }
            }

            public Vector2 Velocidad
            {
                get { return velocidad; }
                set { velocidad = value; }
            }
        }
    }
}
