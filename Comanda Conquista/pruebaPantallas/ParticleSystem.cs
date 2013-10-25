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
        public abstract class ParticleSystem:DrawableGameComponent
        {
            private Game1 game;
            private Texture2D textura;
            private Vector2 origen;
            private static Random random = new Random();
            private SpriteBatch spriteBatch;

            private int howManyEffects;
            Particle[] particles;
            Queue<Particle> freeParticles;

            public const int AlphaBlendDrawOrder = 100;
            public const int AdditiveDrawOrder = 200;
            protected SpriteBlendMode spriteBlendMode;
            protected int minNumParticles;
            protected int maxNumParticles;
            protected float minLifetime;
            protected float maxLifetime;
            protected string textureFileName;
            protected float minInitialSpeed;
            protected float maxInitialSpeed;
            protected float minAcceleration;
            protected float maxAcceleration;
            protected float minRotationSpeed;
            protected float maxRotationSpeed;
            protected float minScale;
            protected float maxScale;


            public Queue<Particle> FreeParticles
            {
                get { return freeParticles; }
                set { freeParticles = value; }
            }
            
            public int Random
            {
                get { return freeParticles.Count; }
            }

            protected ParticleSystem(Game1 game, int howManyEffects, string textureFileName) : base(game) {
                this.game = game;
                this.howManyEffects = howManyEffects;
                this.textureFileName = textureFileName;
            }

            public override void Initialize()
            {
                InitializeConstants();
                particles = new Particle[howManyEffects * maxNumParticles];
                freeParticles = new Queue<Particle>(howManyEffects * maxNumParticles);
                for (int i = 0; i < particles.Length; i++) {
                    particles[i] = new Particle();
                    freeParticles.Enqueue(particles[i]);
                }
                base.Initialize();

            }

            protected abstract void InitializeConstants();

            public void LoadContent()
            {
                textura = game.Content.Load<Texture2D>(textureFileName);
                origen.X = textura.Width / 2;
                origen.Y = textura.Height / 2;
                base.LoadContent();
            }

            public void addParticles(Vector2 where) {
                int numParticles = random.Next(minNumParticles, maxNumParticles);
                for (int i = 0; i < numParticles && freeParticles.Count > 0; i++) {
                    Particle p = freeParticles.Dequeue();
                    InitializeParticle(p, where);
                }
            }

            protected virtual Vector2 pickRandomDirection() {
                float angle = randomBetween(0, MathHelper.TwoPi);
                return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }

            public static float randomBetween(float min, float max) {
                return min + (float)random.NextDouble() * (max - min);
            }

            protected virtual void InitializeParticle(Particle p, Vector2 where)
            { 
                Vector2 direction = pickRandomDirection();

                float velocity = randomBetween(minInitialSpeed, maxInitialSpeed);
                float acceleration = randomBetween(minAcceleration, maxAcceleration);
                float lifetime = randomBetween(minLifetime, maxLifetime);
                float scale = randomBetween(minScale, maxScale);
                float rotationSpeed = randomBetween(minRotationSpeed, maxRotationSpeed);

                p.Initialize(where, velocity * direction, acceleration * direction,
                    lifetime, scale, rotationSpeed);
            }

            public override void Update(GameTime gameTime)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (Particle p in particles)
                {
                    if (p.Active)
                    {
                        p.Update(dt);
                        if (!p.Active)
                        {
                            freeParticles.Enqueue(p);
                        }
                    }
                }

                base.Update(gameTime);
            }

            public void setSpriteBatch(SpriteBatch sb) {
                this.spriteBatch = sb;
            }

            public override void Draw(GameTime gameTime)
            {
                spriteBatch.End();
                spriteBatch.Begin(spriteBlendMode);
                foreach (Particle p in particles)
                {
                    
                    if (!p.Active)
                        continue;
                    float normalizedLifetime = p.TiempoDesdeVivo/ p.TiempoVida;
                    float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                    float scale = p.Escala* (.75f + .25f * normalizedLifetime);
                    Color color = new Color(new Vector4(1, 1, 1, alpha));
                    //textura = game.Content.Load<Texture2D>("Sprites\\smoke");
                    spriteBatch.Draw(textura, p.Posicion, null, color,
                        p.Rotacion, origen, scale, SpriteEffects.None, 0.0f);
                }
               // spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}


