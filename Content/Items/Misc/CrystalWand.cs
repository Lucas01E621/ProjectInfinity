using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using ProjectInfinity.Content.Parkour;
using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Core.Systems.CollisionTest;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class CrystalWand : ModItem
    {
        public override string Texture => AssetDirectory.Misc + Name;

        public int timesFired = 0;

        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 10;
            Item.shootSpeed = 15;
            //Item.shoot = ModContent.ProjectileType<LaserBase1>();
            
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                //Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<debugproj>(), damage, knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, Main.MouseWorld, velocity, ModContent.ProjectileType<CollidableObject>(), damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
