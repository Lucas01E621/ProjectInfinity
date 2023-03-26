using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles.BaseProjectiles
{
    public class KristalProjectile : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PrismProj";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kristal");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.97f;
        }


    }
}