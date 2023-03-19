using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Items.Blocks;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    internal class PetrifiedWood : ModTile 
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + Name;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Microsoft.Xna.Framework.Color(127, 133, 102), name);
            name.SetDefault("");
        }
        public override bool Drop(int i, int j)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<PetrifiedWoodBlock>());
            return base.Drop(i, j);
        }
    }
}
