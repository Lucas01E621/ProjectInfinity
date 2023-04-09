using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Projectiles.CrystalDesert;
using ProjectInfinity.Content.Projectiles.CrystalDesert.Weapons;
using ProjectInfinity.Core;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Summoner.CrystalDesert
{
    internal class CrystalVortexStaff : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "CrystalWand";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.damage = 25;
            Item.ArmorPenetration = 15;
            Item.DamageType = DamageClass.Summon;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 10;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CrystalVortex>(), Item.damage, knockback, Main.myPlayer);
            return false;
        }
    }
}
