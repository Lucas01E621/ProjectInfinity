using Terraria;
using Terraria.ModLoader;
using ProjectInfinity.Core;
using Microsoft.Xna.Framework;

namespace ProjectInfinity.Core.Systems.CollisionTest
{
    class CollidableObject : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "DenemeProj";

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.timeLeft = 99999;
            Projectile.aiStyle = -1;
        }
        public override void AI()
        {
            Rectangle projrec = new((int)(Projectile.position.X * 1.2f), (int)(Projectile.position.Y * 1.2f), (int)(Projectile.height * 1.2f), (int)(Projectile.width * 1.2f));

                if(Main.LocalPlayer.Hitbox.Intersects(projrec))
                {
                    Projectile.velocity += Vector2.Normalize(Projectile.Center - Main.LocalPlayer.position);
                }
        }
    }
}