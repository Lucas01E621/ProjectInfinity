using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Magic.CrystalDesert
{
    public class KristalWand : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "CrystalWand";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kristal Wand");
            Tooltip.SetDefault("Right-click to shoot a Kristal projectile straight up");
        }

        public override void SetDefaults()
        {
            Item.channel = true;
            Item.damage = 10;
            Item.mana = 10;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<KristalProjectile>();
            Item.shootSpeed = 10f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse ==2 )
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<KristalProjectile>(), damage, knockback, player.whoAmI);
              
            } else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CrystalWandLaser>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}