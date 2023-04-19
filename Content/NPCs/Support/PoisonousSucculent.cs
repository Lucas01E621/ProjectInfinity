using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectInfinity.Content.NPCs.enemies.CrystalDesert;
using ProjectInfinity.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs.Support
{
    internal class PoisonousSucculent : ModNPC
    {
        bool healing = false;
        public override string Texture => AssetDirectory.Support + Name;
        public override void SetDefaults()
        {
            NPC.lifeMax = 500;
            NPC.defense = 10;
            NPC.damage = 25;
            NPC.height = 50;
            NPC.width = 50;
            NPC.friendly = false;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0;
        }
        public override void AI()
        {
            NPC.TargetClosest();

            HealEnemies();
            Attack();
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if(NPC.Distance(player.position) < 80)
            {
                hit.Crit = false;
                hit.SourceDamage /= 2;
                Vector2 vel = player.Center - NPC.Center;
                vel.Normalize();
                player.velocity = vel * 9;
                player.Hurt(PlayerDeathReason.ByNPC(NPC.type), NPC.damage / 3, 0);
            }
        }
        void HealEnemies()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] >= 240)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly && NPC.WithinRange(npc.Center, 200) && npc.type != NPC.type)
                    {

                        healing = true;
                        npc.HealEffect(NPC.damage / 3);
                        npc.life += NPC.damage / 3;
                        if (npc.life > npc.lifeMax)
                            npc.life = npc.lifeMax;
                        NPC.ai[0] = 0;
                    }
                    else
                    {
                        healing = false;
                    }
                }
            }
        }
        int timesFired = 0;
        int timer = 0;
        void Attack()
        {
            NPC.ai[1]++;
            timer++;
            
            if(!healing && NPC.ai[1] >= 180 && timesFired <= 3)
            {
                Player target = Main.player[NPC.target];
                Vector2 vel = target.Center - NPC.Center;
                vel.Normalize();
                if(timer > 0)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center,vel * 16, ProjectileID.JavelinHostile, NPC.damage / 3, 1);
                    timer = 0;
                }
                timesFired++;
                

            }
            if (timesFired >= 3)
                timesFired = 0;
        }
    }
}
