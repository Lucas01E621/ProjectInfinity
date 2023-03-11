/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace ProjectInfinity.Content.Buffs.Debuffs
{
    internal class CrystalCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Curse");
            Description.SetDefault("You feel like everything has taken from you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GPlayer>().lifeRegenDebuff = true;
        }
    }
}*/
