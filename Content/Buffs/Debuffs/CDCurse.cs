using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectInfinity.Core;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace ProjectInfinity.Content.Buffs.Debuffs
{
    internal class CDCurse : ModBuff
    {
        public override string Texture => AssetDirectory.Buffs + Name;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ProjectInfinityPlayer>().CDCurse = true;
        }
    }
}
