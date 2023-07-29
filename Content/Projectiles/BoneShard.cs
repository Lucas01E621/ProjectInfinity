using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles
{
    internal class BoneShard : ModProjectile
    {
        public override string Texture => AssetDirectory.Misc + "ShardPiece";
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
    }
}
