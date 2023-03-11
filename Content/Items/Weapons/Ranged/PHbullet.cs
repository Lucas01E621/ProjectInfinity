using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Weapons.Ranged
{
    internal class PHbullet : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PHbullet";

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 28;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        public override void AI()
        {
            Vector2 initialCenter = Projectile.Center;
            Vector2 initialVelocity = Projectile.velocity;
            Projectile.ai[0]++;
            int wavesPerSecond = 2;
            float sine = (float)Math.Sin(MathHelper.ToRadians(Projectile.ai[0] * 6f * wavesPerSecond));
            Vector2 offset = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2);
            float amplitude = 18f;  // 1.5 tiles
            offset *= sine * amplitude;
            initialCenter += Projectile.velocity;
            Projectile.Center = initialCenter + offset;
            //

            Dust.NewDust(Projectile.position,1,1,DustID.InfernoFork,0,0,0,Color.AntiqueWhite);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        
    }
    
}
