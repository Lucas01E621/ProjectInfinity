using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    internal class CrystalGlassWall_Tile : ModWall
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + Name;
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.lightning = 5f;
            
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.DeepPink, name);
            name.SetDefault("");
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.45f;
            g = 0.25f;
            b = 0.4f;
        }
    }
}
