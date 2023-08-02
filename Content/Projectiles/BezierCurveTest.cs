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
        float Timer = 0;
        Vector2 storedPos;
        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 5 * 60;
            Projectile.friendly = true;
            Projectile.damage = 20;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.ai[0]++;



            Vector2 p1 = new(0, 0);
            Vector2 p2 = new(50, 50);
            Vector2 p3 = new(0, 100);

            if (Timer <= 1)
                Timer += 0.0005f;

            Main.NewText(BezierCurve.QuadrantBezierCurve(p1, p2, p3, Timer));

            if (Projectile.ai[0] == 1)
            {
                storedPos = BezierCurve.QuadrantBezierCurve(p1, p2, p3, Timer);
            }

            Projectile.position = storedPos - BezierCurve.QuadrantBezierCurve(p1, p2, p3, Timer);

            Main.NewText(Projectile.position);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.NewText("test");
        }
    }
}
