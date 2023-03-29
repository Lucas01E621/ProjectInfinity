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
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Weapons.Melee
{
    public class ShardBlade : ModItem
    {
        public override string Texture => AssetDirectory.Melee + "CrystalDesert/" + Name;
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.damage = 134;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.knockBack = 6;
            Item.value = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ProjectileID.BeeArrow;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 cursorPosition = Main.MouseWorld;
            Vector2 direction = cursorPosition - position;
            direction.Normalize();
            float speed = 16f;
            Projectile.NewProjectile(source,position.X, position.Y, direction.X * speed, direction.Y * speed, ProjectileID.WoodenArrowFriendly, damage, knockback, player.whoAmI);
            return false;

        }


    }
}