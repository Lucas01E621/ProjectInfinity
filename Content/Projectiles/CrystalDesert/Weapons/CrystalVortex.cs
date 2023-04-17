using Microsoft.Xna.Framework;
using Mono.Cecil;
using ProjectInfinity.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles.CrystalDesert.Weapons
{
    internal class CrystalVortex : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "NightmareShriekProj"; 
        public override void SetDefaults()
        {
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5 * 60;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (Main.player[Projectile.owner].ownedProjectileCounts[Projectile.type] >= 3)
            {
                Projectile.Kill();
            }
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.velocity *= 0.97f;
            
                
            if (Projectile.ai[0] >= 60)
            {
                Main.NewText("pos");
                Main.NewText(Projectile.ai[0]);
                SpawnShards();
                Projectile.ai[0] = 0;
            }
            
        }
        private void SpawnShards()
        {
            Vector2 vel = Main.player[Projectile.owner].Center - Main.MouseWorld;
            vel.Normalize();

            for (int i = 0; i < 5; i++)
            {
                UsefulFunctions.MultipleShot(Main.player[Projectile.owner], Projectile.GetSource_FromAI(), Projectile.Center, new(10,10), ModContent.ProjectileType<CrystalVortexShard>(), Projectile.damage, Projectile.knockBack, 2,359,true);
            }
        }
    }
}
