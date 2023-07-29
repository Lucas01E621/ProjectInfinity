using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ProjectInfinity.Content.Projectiles
{
    internal class BezierCurveTest : ModProjectile
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
            Player player = Main.player[Projectile.owner];

            Vector2 p1 = player.Center;
            Vector2 p2 = new Vector2(Main.MouseWorld.X - player.Center.X, Main.MouseWorld.Y - player.Center.Y - 56) / 2;
            Vector2 p3 = Main.MouseWorld;

            Projectile.position = BezierCurve.QuadrantBezierCurve(p1, p2, p3, 0.1f);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.NewText("test");
        }
    }
}
