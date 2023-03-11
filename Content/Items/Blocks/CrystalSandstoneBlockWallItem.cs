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

namespace ProjectInfinity.Content.Items.Blocks
{
    internal class CrystalSandstoneBlockWallItem : ModItem
    {
        public override string Texture => AssetDirectory.Blocks + "CrystalSandstoneBlockWallItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal sandstone wall item");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createWall = ModContent.WallType<CrystalSandstoneWall>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
