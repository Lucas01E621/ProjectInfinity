using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles
{
    internal class CrystalLaser : ModProjectile
    {
        //laser
        public LaserCrystal parent { get; set; }
        public int maxDist = 1000;
        public int active = 1;
        public float Distance
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        private const float MOVE_DISTANCE = 60f;
        public override string Texture => AssetDirectory.Projectiles + "raycastproj1";
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.damage = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (!parent.Projectile.active) Projectile.Kill();
            Projectile.velocity = new Vector2(0, 1);

            //Main.NewText(checkBox.BottomRight());
            Projectile.position = parent.Projectile.position;
            Projectile.netUpdate = true;
            SetLaserPosition(parent.Projectile);

            if (active > 1) active = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // We start drawing the laser if we have charged up
            if(active == 1)
            {
                Vector2 c = parent.Projectile.Center;
                DrawLaser(Main.spriteBatch, TextureAssets.Projectile[ModContent.ProjectileType<CrystalLaser>()].Value, c,
                    Projectile.velocity, 2, -1.57f, 1f, (int)MOVE_DISTANCE);
            }
            return false;

        }
        public override bool ShouldUpdatePosition() => false;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
                // We can only collide if we are at max charge, which is when the laser is actually fired

                Player player = Main.player[Projectile.owner];
                Vector2 unit = Projectile.velocity;
                float point = 0f;
                // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
                // It will look for collisions on the given line using AABB
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
                    player.Center + unit * Distance, 22, ref point);
        }
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            // Draws the laser 'body'
            for (float i = transDist; i <= Distance; i += step)
            {
                Color c = Color.DarkRed;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 22, 26), i < transDist ? Color.Transparent : c, r,
                    new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);
            }

            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 22, 26), Color.White, r, new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 22, 26), Color.White, r, new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);
        }
        private void SetLaserPosition(Projectile proj)
        {
            for (Distance = MOVE_DISTANCE; Distance <= maxDist; Distance += 5f)
            {
                var start = proj.Center + Projectile.velocity * Distance;
                if (!Collision.CanHit(proj.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }
        }

    }
}
