using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Projectiles
{
    internal class LaserCrystal : ModProjectile
    {
        //mainproj
        public override string Texture => AssetDirectory.Projectiles + Name;
        public int parentTileX { get; set; }
        public int parentTileY { get; set; }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.damage = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation();
            Projectile.spriteDirection = Projectile.direction;
            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(0,16), new Vector2(0,1), ModContent.ProjectileType<CrystalLaser>(), 20, 0, Projectile.owner);
            Projectile proj = Main.projectile[index];
            if (proj.ModProjectile is CrystalLaser)
            {
                // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                (proj.ModProjectile as CrystalLaser).parent = this;
            }
        }
        public override void AI()
        {
            Projectile.position = new Vector2(parentTileX, parentTileY) * 16;
            if (Main.tile[parentTileX,parentTileY].HasTile && Main.tile[parentTileX, parentTileY].TileType == ModContent.TileType<CrystalLaserTile>())
            {
                Projectile.timeLeft = 2;
            }else
            {
                Projectile.Kill();
            }
            Main.NewText(parentTileY);
            Main.NewText(parentTileX);
        }
    }
}
