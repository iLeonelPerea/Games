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
        public class ExplosionSmokeParticleSystem : ParticleSystem
        {

            public ExplosionSmokeParticleSystem(Game1 game, int howManyEffects, string textureFileName)
                : base(game, howManyEffects, textureFileName)
            {
            }

            protected override void InitializeConstants()
            {

                // less initial speed than the explosion itself
                minInitialSpeed = 15;
                maxInitialSpeed = 120;

                // acceleration is negative, so particles will accelerate away from the
                // initial velocity.  this will make them slow down, as if from wind
                // resistance. we want the smoke to linger a bit and feel wispy, though,
                // so we don't stop them completely like we do ExplosionParticleSystem
                // particles.
                minAcceleration = -10;
                maxAcceleration = -50;

                // explosion smoke lasts for longer than the explosion itself, but not
                // as long as the plumes do.
                minLifetime = 1.0f;
                maxLifetime = 2.5f;

                minScale = 1.0f;
                maxScale = 2.0f;

                minNumParticles = 10;
                maxNumParticles = 20;

                minRotationSpeed = -MathHelper.PiOver4;
                maxRotationSpeed = MathHelper.PiOver4;

                spriteBlendMode = SpriteBlendMode.AlphaBlend;

                DrawOrder = AlphaBlendDrawOrder;
            }
        }
    }
}
