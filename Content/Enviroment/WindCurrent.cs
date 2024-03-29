using ProjectInfinity.Core.Systems.ForegroundSystem;
using ProjectInfinity.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace ProjectInfinity.Content.Foregrounds //HORIZONTAL FLOW
{
	class WindCurrent : ParticleForeground
	{
		public override bool Visible => false;
		public override bool OverUI => false;

		public Texture2D Texture;
		public int randomDir;

		public override void OnLoad()
		{
			ParticleSystem = new ParticleSystem("ProjectInfinity/Assets/Foregrounds/HolyBig", UpdateAshParticles);
			Texture = ModContent.Request<Texture2D>("ProjectInfinity/Assets/Foregrounds/HolyBig").Value;
			randomDir = Main.rand.Next(0, 1) == 0 ? -1 : 1; 
		}

		private void UpdateAshParticles(Particle particle)
		{
			particle.Timer--;

			particle.StoredPosition.X += particle.Velocity.X;															  //Behaviour
			particle.StoredPosition.Y += (float)Math.Sin(ProjectInfinityWorld.visualTimer + particle.Velocity.Y) * 0.45f; //

			particle.Position = particle.StoredPosition - Main.screenPosition;

			particle.Alpha = particle.Timer < 70 ? particle.Timer / 70f : particle.Timer > 630 ? 1 - (particle.Timer - 630) / 70f : 1; //fade effect
        }

		public override void Draw(SpriteBatch spriteBatch, float opacity)
		{
			Vector2 pos = Main.screenPosition + new Vector2(0, Main.rand.Next(0, Main.screenHeight));
            Vector2 vel1 = new Vector2(Main.rand.NextFloat(3.14f, 6.28f), -Main.rand.NextFloat(0.5f, 1f));
            Vector2 vel2 = new Vector2(Main.rand.NextFloat(3.14f, 6.28f), -Main.rand.NextFloat(1.3f, 1.7f));
            float rot = 0;
            float scale1 = Main.rand.NextFloat(0.4f, 0.8f);
            float scale2 = Main.rand.NextFloat(1.0f, 1.25f);
            int timeleft1 = (int)(400 * scale1);
            int timeleft2 = (int)(400 * scale2);

			
			ParticleSystem.DrawParticles(Main.spriteBatch);

			if (Main.rand.NextBool(8))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, vel1 , rot, scale1, Color.Yellow, timeleft1, pos, Texture.Bounds));

			if (Main.rand.NextBool(20))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, vel2, rot, scale2 , Color.Yellow, timeleft2, pos, Texture.Bounds));
		}
	}
}