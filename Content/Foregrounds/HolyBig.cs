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

namespace ProjectInfinity.Content.Foregrounds
{
	class HolyBig : ParticleForeground
	{
		public override bool Visible => false;
		public override bool OverUI => true;

		public Texture2D Texture;

		public override void OnLoad()
		{
			ParticleSystem = new ParticleSystem("ProjectInfinity/Assets/Foregrounds/HolyBig", UpdateAshParticles);
			Texture = ModContent.Request<Texture2D>("ProjectInfinity/Assets/Foregrounds/HolyBig").Value;
		}

		private void UpdateAshParticles(Particle particle)
		{
			particle.Timer--;

			particle.StoredPosition.Y += particle.Velocity.Y;															  //Behaviour
			particle.StoredPosition.X += (float)Math.Sin(ProjectInfinityWorld.visualTimer + particle.Velocity.X) * 0.45f; //

			particle.Position = particle.StoredPosition - Main.screenPosition;

			particle.Alpha = particle.Timer < 70 ? particle.Timer / 70f : particle.Timer > 630 ? 1 - (particle.Timer - 630) / 70f : 1; //fade effect
        }

		public override void Draw(SpriteBatch spriteBatch, float opacity)
		{
			Vector2 pos = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
			
			ParticleSystem.DrawParticles(Main.spriteBatch);

			if (Main.rand.NextBool(20))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, new Vector2(Main.rand.NextFloat(3.14f), -Main.rand.NextFloat(0.5f, 1f)), 0, Main.rand.NextFloat(0.4f, 0.8f), Color.White * Main.rand.NextFloat(0.2f, 0.4f), 400, pos + new Vector2(Main.rand.Next(-600, 600), 500), Texture.Bounds, 0.5f));

			if (Main.rand.NextBool(60))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, new Vector2(Main.rand.NextFloat(3.14f), -Main.rand.NextFloat(1.3f, 1.7f)), 0, Main.rand.NextFloat(1.0f, 1.25f), Color.White * Main.rand.NextFloat(0.4f, 0.5f), 500, pos + new Vector2(Main.rand.Next(-600, 600), 500), Texture.Bounds, 0.5f));
		}
	}
}