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

namespace ProjectInfinity.Content.Items.Weapons.Ranged
{
    internal class PHbow : ModItem
    {
        public override string Texture => AssetDirectory.Ranged + "PHbow";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("placeholder bow");
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<PHbullet>();
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            UsefulFunctions.MultipleShot(player, source, position, velocity, type, damage, knockback, 15, 180, true);
            return false;
        }
    }
}
