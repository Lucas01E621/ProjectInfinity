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
	class RainForeground : ParticleForeground
	{
		public override bool Visible => true;
		public override bool OverUI => false;

		public Texture2D Texture;

		public override void OnLoad()
		{
			ParticleSystem = new ParticleSystem("ProjectInfinity/Assets/Foregrounds/HolyBig", UpdaterRain);
			Texture = ModContent.Request<Texture2D>("ProjectInfinity/Assets/Foregrounds/HolyBig").Value;
		}

		private void UpdaterRain(Particle particle)
		{
			particle.Timer--;

			particle.StoredPosition.Y += particle.Velocity.Y;															  //Behaviour

			particle.Position = particle.StoredPosition + Main.screenPosition;

			particle.Alpha = particle.Timer < 70 ? particle.Timer / 70f : particle.Timer > 630 ? 1 - (particle.Timer - 630) / 70f : 1; //fade effect
        }

		public override void Draw(SpriteBatch spriteBatch, float opacity)
		{
			Vector2 pos = Main.screenPosition - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
			
			ParticleSystem.DrawParticles(Main.spriteBatch);

			if (Main.rand.NextBool(20))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, new Vector2(0, Main.rand.NextFloat(8f, 11f)), 0, Main.rand.NextFloat(0.4f, 0.8f), Color.White, 400, pos + new Vector2(Main.rand.Next(-600, 600), 500), new Rectangle(5, 0, 4, 14), 0.5f));

			if (Main.rand.NextBool(60))
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, new Vector2(0, Main.rand.NextFloat(12f, 15f)), 0, Main.rand.NextFloat(1.0f, 1.25f), Color.White, 500, pos + new Vector2(Main.rand.Next(-600, 600), 500), new Rectangle(4, 0, 6, 14), 0.5f));
		}
	}
}