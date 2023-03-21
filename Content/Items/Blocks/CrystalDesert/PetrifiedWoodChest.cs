using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Blocks.CrystalDesert
{
    internal class PetrifiedWoodChest : ModItem
    {
        public override string Texture => AssetDirectory.CrystalDesertBlocks + Name; 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petrified wood chest");
            Tooltip.SetDefault("Wood block belongs to crystal desert");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<PetrifiedWoodChest_Tile>();

        }
    }
}
