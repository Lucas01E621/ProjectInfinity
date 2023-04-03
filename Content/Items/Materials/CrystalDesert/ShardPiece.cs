using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Misc.CrystalDesert
{
    public class ShardPiece : ModItem
    {
        public override string Texture => AssetDirectory.Misc + Name;
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Pink;
        }
    }
}