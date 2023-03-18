using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProjectInfinity.Common.Systems;

namespace ProjectInfinity.Content.Biomes
{
    internal class CrystalDeserMB : ModBiome
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Desert");
        }
        public override bool IsBiomeActive(Player player)
        {
            // First, we will use the exampleBlockCount from our added ModSystem for our first custom condition
            bool b1 = ModContent.GetInstance<BiomeHandler>().CrystanSandstoneBlockCount >= 40;
            return b1;
        }
        public override void OnInBiome(Player player)
        {
            player.ClearBuff(BuffID.Featherfall);
        }
        
    }
}
