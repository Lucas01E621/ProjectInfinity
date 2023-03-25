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

namespace ProjectInfinity.Content.Items.Weapons.Melee
{
    public class ShardBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shard Blade");
            Tooltip.SetDefault("A strong power that used to rule to crystal desert once upon a time...");
        }

        public override void SetDefaults()
        {
            Item.damage = 134;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.knockBack = 6;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }


    }
}