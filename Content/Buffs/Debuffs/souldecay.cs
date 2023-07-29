using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Buffs.Debuffs
{
    internal class SoulDecay : ModBuff
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
            player.GetModPlayer<ProjectInfinityPlayer>().lifeRegenDebuff = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            /*npc.GetGlobalNPC<>().lifeRegenDebuff = true;
            npc.lifeRegen -= 16;*/
        }
    }
}
