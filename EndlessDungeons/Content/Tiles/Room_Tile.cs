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
using ProjectInfinity.Core;

namespace ProjectInfinity.EndlessDungeons.Content.Tiles
{
    internal class Room_Tile : ModTile
    {
        public override string Texture => AssetDirectory.EDTiles + Name; 
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
        }
    }
}
