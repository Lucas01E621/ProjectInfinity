using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs
{
    internal class UndeadSkeleton : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalDesertNPC + "CrystalHive";
        public ref float hp => ref NPC.ai[0];
        
        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 44;
            NPC.lifeMax = 100 + (int)hp;
            NPC.friendly = true;
            NPC.damage = 15;
            NPC.aiStyle = NPCAIStyleID.Fighter;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.NewText(hp);
        }
        public override void AI()
        {
            DamageHostile();
        }
        void DamageHostile()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if(npc.active && npc.lifeMax > 5 && !npc.friendly && NPC.Hitbox.Intersects(npc.Hitbox))
                {
                    //implement a new way to deal damage
                    npc.StrikeNPC(new NPC.HitInfo { Damage = NPC.damage, HitDirection = NPC.direction, Knockback = 0.5f });
                }
            }
        }
    }
}
