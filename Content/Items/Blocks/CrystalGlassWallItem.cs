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
    internal class CrystalGlassWallItem : ModItem
    {
        public override string Texture => AssetDirectory.Blocks + "CrystalGlassWallItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal glass wall item");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createWall = ModContent.WallType<CrystalGlassWall>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
