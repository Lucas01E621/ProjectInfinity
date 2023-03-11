using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Common.GlobalNPCs
{
    public class GNPC : GlobalNPC
    {
        public bool lifeRegenDebuff;
        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            lifeRegenDebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (lifeRegenDebuff)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                npc.lifeRegen -= 16;
            }
        }
    }
}
