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
using log4net.Util;
using Terraria.Chat;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Weapons.Ranged
{
    internal class PHbulletsplit : ModProjectile
    {

        public override string Texture => AssetDirectory.Projectiles + "PHbulletsplit";
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
            //Main.NewText(Projectile.damage);
        }

        public override void AI()
        {
            /*Projectile.ai[0]++;
            if (Projectile.ai[0] > 60f)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<PHbulletsplit1>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                Projectile.ai[0] = 0f;
            }*/

            

            Dust.NewDust(Projectile.position, 1, 1, DustID.InfernoFork, 0, 0, 0, Color.AntiqueWhite);
        }

    }
    
}
