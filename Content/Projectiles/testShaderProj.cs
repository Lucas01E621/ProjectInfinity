using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Projectiles
{
    internal class testShaderProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Assets + "StarTexture";

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.damage = 15;
            Projectile.timeLeft = 10 * 60;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
        }
        public override void AI()
        {
            Projectile.position = Main.MouseWorld;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            
            return false;
        }
        
    }
}
