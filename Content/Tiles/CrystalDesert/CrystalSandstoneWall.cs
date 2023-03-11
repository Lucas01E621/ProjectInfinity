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
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    internal class CrystalSandstoneWall : ModWall
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + "CrystalSandstoneWall";
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.Gray, name);
            name.SetDefault("");
        }
    }
}
