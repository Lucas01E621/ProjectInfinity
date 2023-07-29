using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Items.Weapons.Magic;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
namespace ProjectInfinity.Content.Projectiles
{
    public class TestRay : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "DenemeProj1";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
        }
        
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation();
            Projectile.spriteDirection = Projectile.direction;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 60)
            {
                for (int i = 0; i < 72; i++)
                {
                    Vector2 vel = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(i * 5));
                    vel.Normalize();
                    SpawnRaycast(vel, 5);
                }
            }
            //Projectile.velocity *= 0.85f;
        }
        public void SpawnRaycast(Vector2 vel, int degrees)
        {
            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<raycastproj1>(), 0, 0, Projectile.owner);
            Projectile proj = Main.projectile[index];
            if (proj.ModProjectile is raycastproj1)
            {
                (proj.ModProjectile as raycastproj1).parent = this;
                (proj.ModProjectile as raycastproj1).maxDist = 300f;
            }
                
        }
    }
}
