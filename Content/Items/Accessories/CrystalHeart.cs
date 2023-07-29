using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using ProjectInfinity.Common.Players;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Accessories.CrystalHeart
{
    internal class CrystalHeart : ModItem
    {
        public override string Texture => AssetDirectory.Accessories + Name;
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            Item.height = 30;
            Item.width = 30;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<MPlayer>().CrystalHeart = true;
            if (player.GetModPlayer<MPlayer>().died)
            {
                player.GetModPlayer<MPlayer>().CrystalHeart = false;
                Item.TurnToAir();
            }
        }
    }
}
