using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Bosses.PlatformBoss
{
    internal class PlatformBoss : ModBoss
    {
        public override bool hasImmunityBeforeFight => true;
        public override int maxHP => 15000;
        public override int defense => 20;
        public override string Texture => AssetDirectory.CrystalHeart + "CrystalHeartBody";
        public override string BossHeadTexture => AssetDirectory.CrystalHeart + "CrystalHeartBody_Head_Boss";

        public int bossPlatformTimer { get; set; }
        public override void SafeSetDefaults()
        {
            NPC.width = 500;
            NPC.height = 500;
            NPC.noGravity = true;
            NPC.dontTakeDamage = true;
            NPC.damage = 100;
        }
        public override void SafeAI()
        {
            
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
    }
}
