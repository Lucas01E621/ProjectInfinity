using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Tiles.CrystalDesert;

namespace ProjectInfinity.Content.Items.Blocks.CrystalDesert
{
    internal class CrystalSandstoneWall : ModItem
    {
        public override string Texture => AssetDirectory.CrystalDesertBlocks + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal sandstone wall item");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createWall = ModContent.WallType<CrystalSandstoneWall_Tile>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
