using ProjectInfinity.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Biomes
{
    internal class AstralOceanMB : ModBiome
    {
        public override bool IsBiomeActive(Player player)
        {
            return ModContent.GetInstance<BiomeHandler>().AstralSandCount >= 40; //check gamemode and block count
            //make flying water
            //make shader for water
        }
    }
}
