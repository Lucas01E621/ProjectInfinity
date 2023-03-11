using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Parkour
{
    internal class UpwardsBoost : ModProjectile
    {
        public override string Texture => AssetDirectory.Parkour_CrystalDesert + "UpwardsBoost";
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.timeLeft = 999999999;
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                if (Main.LocalPlayer.getRect().Intersects(Projectile.getRect()))
                {
                    Projectile.Kill();
                    p.AddBuff(ModContent.BuffType<UpwardsBoostBuff>(), 1200);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return true;
        }
    }
}
