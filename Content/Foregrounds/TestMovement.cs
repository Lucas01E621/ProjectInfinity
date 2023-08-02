using ProjectInfinity.Core.Systems.ForegroundSystem;
using ProjectInfinity.Core.Systems.CameraHandler;
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
	class TestMovement : ParticleForeground
	{
		public override bool Visible => false;
		public override bool OverUI => false;
		public bool test = false;
		public Texture2D Texture;
		
		
		public float timerr = 0;

		public override void OnLoad()
		{
			ParticleSystem = new ParticleSystem("ProjectInfinity/Assets/Foregrounds/HolyBig", UpdateAshParticles);
			Texture = ModContent.Request<Texture2D>("ProjectInfinity/Assets/Foregrounds/HolyBig").Value;
			CameraSystem.AsymetricalPan(120,120,120,Main.MouseWorld);
		}

		private void UpdateAshParticles(Particle particle)
		{
			particle.Timer--;

			Vector2 pos1 = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) + new Vector2(Main.rand.Next(-600, 600), 500);
			if(timerr <= 1)
				timerr += 0.00005f;
			if(timerr > 1)
				timerr = 0;
			
			Vector2 curveVel;
			
			if(pos1.X < Main.screenPosition.X + Main.screenWidth / 2)
			{
				curveVel = BezierCurve.QuadrantBezierCurve(pos1, Main.screenPosition + new Vector2(0, Main.screenHeight / 2), pos1 - new Vector2(0, Main.screenHeight), timerr);
			}else
			{
				curveVel = BezierCurve.QuadrantBezierCurve(pos1, Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight / 2), pos1 - new Vector2(0, Main.screenHeight), timerr);
			}
			curveVel.Normalize();
			particle.StoredPosition += curveVel;

			particle.Position = particle.StoredPosition - Main.screenPosition;
			Main.NewText("mouse: " + Main.MouseWorld + "\nParticle: " + particle.Position + "\nParticle count: " + ParticleSystem.Particles.Count);
			

			particle.Alpha = particle.Timer < 70 ? particle.Timer / 70f : particle.Timer > 630 ? 1 - (particle.Timer - 630) / 70f : 1; //fade effect
        }

		public override void Draw(SpriteBatch spriteBatch, float opacity)
		{
			Vector2 pos1 = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) + new Vector2(Main.rand.Next(-600, 600), 500);

            
            float rot = 0;
            float scale1 = Main.rand.NextFloat(0.4f, 0.8f);
            float scale2 = Main.rand.NextFloat(1.0f, 1.25f);
            int timeleft1 = (int)(400 * scale1);
            int timeleft2 = (int)(400 * scale2);
			

			
			ParticleSystem.DrawParticles(Main.spriteBatch);

			
				ParticleSystem.AddParticle(new Particle(Vector2.Zero, Vector2.Zero, 0, scale1, Color.White, timeleft1, pos1, Texture.Bounds, 1, 1));
				
			

		}
	}
}