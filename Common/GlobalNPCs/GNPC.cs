using ProjectInfinity.Common.Players;
using ProjectInfinity.Content.Items.Misc;
using ProjectInfinity.Content.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
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
        public override void OnKill(NPC npc)
        {
            if (ModContent.GetInstance<MPlayer>().NecroStaff && npc.lifeMax > 5)
            {
                Projectile.NewProjectile(npc.GetSource_FromAI(), npc.position, Vector2.Zero, ModContent.ProjectileType<BoneShard>(), 20, 0, Main.myPlayer);
            }
        }
    }
}
