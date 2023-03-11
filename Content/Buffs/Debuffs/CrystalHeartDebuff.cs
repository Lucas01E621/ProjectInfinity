using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Buffs.Debuffs
{
    internal class CrystalHeartDebuff : ModBuff
    {
        public override string Texture => AssetDirectory.Buffs + "CrystalHeartDebuff";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Heart Cooldown");
            Description.SetDefault("You can revive again");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
            
        }
    }
}
