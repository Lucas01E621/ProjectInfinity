using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Tiles
{
    internal class InvisibleWall : ModWall
    {
        public override string Texture => AssetDirectory.Tiles + "InvisibleWall";
        public override void SetStaticDefaults()
        {
            Main.tileMergeDirt[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.lightning = 5f;
        }
    }
}
