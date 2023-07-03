using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles
{
    internal class debugproj : ModProjectile
    {
        public override string Texture => AssetDirectory.Parkour_CrystalDesert + "UpwardsBoost";
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.timeLeft = 999999999;
        }
        
    }
}
