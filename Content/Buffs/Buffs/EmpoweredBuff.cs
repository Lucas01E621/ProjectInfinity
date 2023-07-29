using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Buffs
{
    internal class EmpoweredBuff : ModBuff
    {
        public bool Magic = false;
        public bool Melee = false;
        public bool Summoner = false;
        public bool Ranged = false;
        public override string Texture => AssetDirectory.Buffs + Name;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        
        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            float gamemodeMult = 1f;
            if(Main.expertMode)
                gamemodeMult = 1.5f;
            if(Main.masterMode)
                gamemodeMult = 2f;
            
            buffName = "Empowered";

            if(Magic)
            {
                tip = "Magigican Empowerment\nMagic damage increse: " + (int)(gamemodeMult * 8) + "%\nMagic crit increse: "+ 10 * gamemodeMult  + "\nMana regen increse: " + (int)(gamemodeMult * 5);
                rare = ItemRarityID.Blue;
            }
            if(Ranged)
            {
                tip = "Ranger Empowerment\nRanged damage increse: " + (int)(gamemodeMult * 6) + "%\nMagic crit increse: "+ 10 * gamemodeMult  + "%\nAttack speed increse: " + (int)(gamemodeMult * 10);
                rare = ItemRarityID.Green;
            }
            if(Melee)
            {
                tip = "Warrior Empowerment\nMelee damage increse: " + (int)(gamemodeMult * 6) + "%\nMagic crit increse: "+ 10 * gamemodeMult  + "%\nAttack speed increse: " + (int)(gamemodeMult * 10);
                rare = ItemRarityID.Orange;
            }
            if(Summoner)
            {
                tip = "Commander Empowerment\nSummon damage increse: " + (int)(gamemodeMult * 8) + "%\nMagic crit increse: "+ 10 * gamemodeMult  + "%\nAttack speed increse: " + (int)(gamemodeMult * 10);
                rare = ItemRarityID.LightPurple;
            }        
        }
    }
}
