using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;


namespace ProjectInfinity.Content.Projectiles.CrystalDesert
{
    internal class TestProjectile : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "NightmareShriekProj"; 
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 600;
        }
        int fails = 0;
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 60)
            {
                Calc(fails);
            }
        }
        void Calc(int fails)
        {
            Vector2 line1 = Projectile.Center - Main.MouseWorld;
            line1.Normalize();
            float random = Main.rand.NextFloat(MathF.PI * 9, -MathF.PI * 9);
            int maxTries = 100;
            for (int i = 0; i < maxTries + fails; i++)
            {
                int index = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position.X + random + Main.rand.Next(-i, i), Projectile.position.Y + random + Main.rand.Next(-i,i), 0, 0, ModContent.ProjectileType<TestProjectile1>(), 0, 0, Main.myPlayer);
                Projectile proj = Main.projectile[index];
                Vector2 projLine = Projectile.Center - proj.Center;
                projLine.Normalize();
                if(Vector2.Dot(line1,projLine) < 0)
                {
                    proj.Kill();
                    fails++;
                    Projectile.timeLeft++;
                }
            }
            Main.NewText(fails);
        }
    }
    internal class TestProjectile1 : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "CrystalHiveProj";
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 600;
        }
    }
}
