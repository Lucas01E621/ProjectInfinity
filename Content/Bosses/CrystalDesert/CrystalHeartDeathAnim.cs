using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using ProjectInfinity.Core;
using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Content.Foregrounds;
using ProjectInfinity.Content.Bosses.CrystalDesert;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    internal class CrystalHeartDeathAnim : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalHeart + "CrystalHeartBody";

        public override void SetDefaults()
        {
            NPC.width = 560;
            NPC.height = 400;
            NPC.damage = 12;
            NPC.lifeMax = 1;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.dontTakeDamage = true;
            NPC.friendly = true;
            NPC.npcSlots = 10f; 
            NPC.aiStyle = -1;
        }
        public override void AI()
        {
            ModContent.GetInstance<CrystalHeartBody>().deathAnimTimer--;

            Vignette.offset = (NPC.Center - Main.LocalPlayer.Center) * 0.8f;
            Vignette.opacityMult = 0.3f * (1 / ModContent.GetInstance<CrystalHeartBody>().deathAnimTimer);

            if(Vignette.opacityMult > 0 )
                Vignette.visible = true;

            if(Vignette.opacityMult < 0.01f)
                Vignette.visible = false;

            if(ModContent.GetInstance<CrystalHeartBody>().deathAnimTimer <= 0)
            {
                ModContent.GetInstance<CrystalHeartBody>().deathAnimTimer = 0;
                NPC.life = 0;
            }
        }
    }
}
