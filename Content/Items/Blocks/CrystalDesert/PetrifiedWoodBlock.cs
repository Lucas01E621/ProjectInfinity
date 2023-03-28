using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;


namespace ProjectInfinity.Content.Items.Blocks.CrystalDesert
{
    internal class PetrifiedWood : ModItem
    {
        public override string Texture => AssetDirectory.CrystalDesertBlocks + "CrystalSandstone";
        public override void SetStaticDefaults()
        {

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
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<PetrifiedWood_Tile>();

        }
    }
}
