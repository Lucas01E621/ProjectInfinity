using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ProjectInfinity.Content.Tiles;

namespace ProjectInfinity.Content.Items.Blocks
{
    internal class InvisibleWallItem : ModItem
    {
        public override string Texture => AssetDirectory.Blocks + "InvisibleWallItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invisible wall item");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createWall = ModContent.WallType<InvisibleWall>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
