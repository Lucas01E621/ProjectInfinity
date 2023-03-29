using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Bars
{
    internal class examplebar : ModItem
    {
        public override string Texture => AssetDirectory.Bars + Name;
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
        }
    }
}
