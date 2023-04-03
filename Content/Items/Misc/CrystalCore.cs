using ProjectInfinity.Common.Players;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class CrystalCore : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "ShardPiece";
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Pink;
        }
    }
}
