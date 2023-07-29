using System;
using ProjectInfinity.Core;
using ProjectInfinity.Core.Systems.ForegroundSystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Menus
{
    public class SpaceMenu : ModMenu
    {
        public override string DisplayName => "Space Menu";

        public ParticleSystem Meteors;

        private int particleCount = 0;
        private const int maxParticleCount = 15;
        
        public override void SetStaticDefaults()
        {
            Meteors = new ParticleSystem("ProjectInfinity/Assets/Foregrounds/meteor", updateMeteor);
        }

        private void updateMeteor(Particle particle)
		{
			particle.Position += particle.Velocity;
			

            if(particle.Position.X < 0 || particle.Position.X > Main.screenHeight)
            {
                particle.Position.X = -particle.Position.X * Main.screenHeight;
            }
            if(particle.Position.Y < 0 || particle.Position.Y > Main.screenWidth)
            {
                particle.Position.Y = -particle.Position.Y * Main.screenWidth;
            }
            
            
			particle.Timer--;
		}

        public override void Update(bool isOnTitleScreen)
        {
            

            
        }

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            Main.dayTime = false;
            if(particleCount < 15)
            {
                var meteorPos = new Vector2(Main.rand.Next(20, Main.screenWidth - 20), Main.rand.Next(20, Main.screenHeight - 20));
                float scale = Main.rand.NextFloat(1.5f, 2.5f);
                

                if(Main.rand.NextBool(50))
                    Meteors.AddParticle(new Particle(meteorPos, new Vector2(Main.rand.NextFloat(1f, 1f), Main.rand.NextFloat(-1f, 1f)), Main.rand.NextFloat(1f,-1f), scale, Color.White * Main.rand.NextFloat(0.2f, 0.4f), 700, Vector2.Zero, new Rectangle(0, 0, 14, 14)));
            }
            Meteors.DrawParticles(Main.spriteBatch);
            
            return true;
        }

        public override void Unload()
        {
            Meteors = null;
        }
    }
}

//Make it have playable particles or just create random asteroids that move randomly