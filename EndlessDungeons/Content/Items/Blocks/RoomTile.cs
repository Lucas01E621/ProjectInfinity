using ProjectInfinity.Content.Tiles;
using ProjectInfinity.Core;
using ProjectInfinity.EndlessDungeons.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.EndlessDungeons.Content.Items.Blocks
{
    internal class RoomTile : ModItem
    {
        public override string Texture => AssetDirectory.EDBlock + Name;
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.createTile = ModContent.TileType<Room_Tile>();
            Item.autoReuse = true;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.maxStack = 999;
        }
    }
}
