using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    internal class CrystalAltar : ModTile
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + "CrystalAltar";
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Height = 14;
            TileObjectData.newTile.Width = 11;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0,0);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.Pink, name);
            name.SetDefault("Crystal Altar");
        }
    }
}
