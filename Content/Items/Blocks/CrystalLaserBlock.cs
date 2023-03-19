using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Blocks
{
    internal class CrystalLaserBlock : ModItem
    {
        public override string Texture => AssetDirectory.Blocks + "CrystalSandstoneBlock";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal laser");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createTile = ModContent.TileType<CrystalLaserTile>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
