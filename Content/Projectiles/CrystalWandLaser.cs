using Terraria;
using Terraria.ModLoader;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Projectiles
{
    internal class CrystalWandLaser : LaserBase
    {
        //hitboxtan gidilmiyo aq
        public override bool Active => true;
        public override bool friendly => true;
        public override string Texture => AssetDirectory.Projectiles + "LaserBase1";
        public override bool tileCollide => false;
        public override bool castLight => true;
        public override float MaxRange => 2000;
        public override bool hasMaxCharge => true;
        public override float MaxCharge => 120;
        public override void SafeAI()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                float point = 0;
                Projectile projectile = Main.projectile[i];
                if (!projectile.active) continue;
                if (projectile.type == ModContent.ProjectileType<KristalProjectile>() && Collision.CheckAABBvLineCollision(projectile.TopLeft, projectile.Size, Main.LocalPlayer.Center,
                    Main.LocalPlayer.Center + Projectile.velocity * Distance, 22,ref point ))
                {
                    UsefulFunctions.MultipleShot(Main.LocalPlayer, Projectile.GetSource_FromAI(), projectile.position, Projectile.velocity, ModContent.ProjectileType<CrystalWandLaser1>(), 10, 1, 6, -180, true);
                    Main.NewText("anan");
                }
            }
        }

    }
}