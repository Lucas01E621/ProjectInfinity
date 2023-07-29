using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Summoner.Necromancer
{
    internal class NecroStaff : ModItem
    {
        public override string Texture => AssetDirectory.Magic + "PHwand";
        public override void SetDefaults()
        {
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<ProjectInfinityPlayer>().NecroStaff = true;
        }
    }
}
