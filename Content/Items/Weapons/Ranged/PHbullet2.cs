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
    internal class PHbullet2 : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PHbullet2";

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 28;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 180;
            Projectile.penetrate = -1;
            Projectile.damage = 5;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void OnSpawn(IEntitySource source)
        {
            
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            Vector2 initialCenter = Projectile.Center;
            Vector2 initialVelocity = Projectile.velocity;
            Vector2 offset = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2);
            
            int wavesPerSecond = 2;
            float sine = -(float)Math.Sin(MathHelper.ToRadians(Projectile.ai[0] * 6f * wavesPerSecond));
            float amplitude = 18f;  // 1.5 tiles
            offset *= sine * amplitude;
            initialCenter += Projectile.velocity;
            Projectile.Center = initialCenter + offset;
            //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ProjectileID.ChlorophyteArrow,Projectile.damage, Projectile.knockBack,Main.myPlayer);
            Dust.NewDust(Projectile.position, 1, 1, DustID.InfernoFork);
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
    
}
