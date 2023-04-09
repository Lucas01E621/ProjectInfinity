using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles
{
    internal class CrystalVortexShard : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "CrystalHiveProj";
        public override void SetDefaults()
        {
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 3 * 60;
            Projectile.friendly = true;
            Projectile.damage = 20;
        }
        public override void AI()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC target = Main.npc[i];
                if(target.active && !target.friendly)
                {
                    Hover(target, 30);
                }
            }
        }
        private void Hover(NPC target, int homingCooldown)
        {
            const int homingDelay = 15;
            float desiredFlySpeedInPixelsPerFrame = 10;
            const float amountOfFramesToLerpBy = 30;


            if (homingCooldown > homingDelay)
            {
                
                Vector2 desiredVel = Projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVel, 1f / amountOfFramesToLerpBy);
            }

        }
    }
}
