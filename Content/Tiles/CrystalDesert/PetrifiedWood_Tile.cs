using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Items.Blocks.CrystalDesert;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    internal class PetrifiedWood_Tile : ModTile 
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + Name;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Microsoft.Xna.Framework.Color(127, 133, 102), name);
        }
    }
}
