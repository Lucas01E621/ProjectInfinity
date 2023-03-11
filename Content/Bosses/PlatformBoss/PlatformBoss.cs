using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Bosses.PlatformBoss
{
    
    internal class PlatformBoss : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalHeart + "CrystalHeartBody"; 
        public int bossPlatformTimer { get; set; }
        public override void SetDefaults()
        {
            NPC.width = 500;
            NPC.height = 500;
            NPC.lifeMax = 500;
            NPC.noGravity = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 5; i++)
            {
                int offsetX = 0;
                if (i == 1)
                    offsetX = -160;
                if (i == 2)
                    offsetX = -80;
                if (i == 3)
                    offsetX = 0;
                if (i == 4)
                    offsetX = 80;
                if (i == 5)
                    offsetX = 160;
                int index = NPC.NewNPC(source, (int)NPC.Center.X + offsetX, (int)NPC.Center.Y + 560, ModContent.NPCType<PlatformBossPlatform>(), NPC.whoAmI);
                NPC child = Main.npc[index];
                if (child.ModNPC is PlatformBossPlatform)
                    (child.ModNPC as PlatformBossPlatform).parent = this;
            }
            
        }
        public override void AI()
        {
            bossPlatformTimer--;
            NPC.ai[0]++;
            if(NPC.ai[0] >= 60)
            {
                bossPlatformTimer = 10;
            }
            Main.NewText(bossPlatformTimer);
        }
    }
}
