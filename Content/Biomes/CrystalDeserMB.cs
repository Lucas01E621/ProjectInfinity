using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using SubworldLibrary;
using Terraria.ID;
using ProjectInfinity.Content.Subworlds;
using ProjectInfinity.Content.Buffs.Debuffs;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProjectInfinity.Core.Systems;

namespace ProjectInfinity.Content.Biomes
{
    internal class CrystalDeserMB : ModBiome
    {
        public override void SetStaticDefaults()
        {
            
        }
        public override bool IsBiomeActive(Player player)
        {
            // First, we will use the exampleBlockCount from our added ModSystem for our first custom condition
            bool b1 = ModContent.GetInstance<BiomeHandler>().CrystanSandstoneBlockCount >= 40;
            bool b2 = SubworldSystem.IsActive<CrystalDesertSubWorld>();
            return b1 || b2;
        }
        public override void OnInBiome(Player player)
        {
            player.AddBuff(ModContent.BuffType<CDCurse>(), 1);
        }
        
    }
}
