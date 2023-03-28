using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ProjectInfinity.Common.Players;
using ProjectInfinity.Common.GlobalNPCs;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Buffs.Debuffs
{
    internal class souldecay : ModBuff
    {
        public override string Texture => AssetDirectory.Buffs + Name;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MPlayer>().lifeRegenDebuff = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GNPC>().lifeRegenDebuff = true;
            npc.lifeRegen -= 16;
        }
    }
}
