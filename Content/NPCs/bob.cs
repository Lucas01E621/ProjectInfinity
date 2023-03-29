using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;

namespace ProjectInfinity.Content.NPCs
{
    internal class bob : ModNPC
    {
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 44;
            NPC.friendly = false;
            NPC.lifeMax = 100;
            NPC.aiStyle = NPCAIStyleID.Spider;
            NPC.damage = 15;
        }
        public override void AI()
        {
            
        }
        public override void OnKill()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (i == NPC.whoAmI)
                    continue;

                NPC Hive = Main.npc[i];

                if (!Hive.active)
                    continue;
                //Hive.ai[3]--;
                
            }
        }
    }
}
