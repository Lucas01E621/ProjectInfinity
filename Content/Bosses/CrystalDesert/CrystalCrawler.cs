using Microsoft.Xna.Framework;
using Mono.Cecil;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    public class CrystalCrawler : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalDesertNPC + "CrystalHive";
        
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 50;
            NPC.aiStyle = NPCAIStyleID.Spider;
            NPC.damage = 15;
            NPC.defDefense = 5;
            NPC.lifeMax = 150;
            NPC.knockBackResist = 0;
        }
    }
}
