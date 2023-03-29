using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using ProjectInfinity.Content.Items.Blocks;

namespace ProjectInfinity.Content.Tiles
{
    internal class exampletile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.Aqua, name);
        }
    }
}
