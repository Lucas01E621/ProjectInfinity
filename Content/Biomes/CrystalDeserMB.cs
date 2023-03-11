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

            // Second, we will limit this biome to the inner horizontal third of the map as our second custom condition
            bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;

            // Finally, we will limit the height at which this biome can be active to above ground (ie sky and surface). Most (if not all) surface biomes will use this condition.
            bool b3 = player.ZoneSkyHeight || player.ZoneOverworldHeight;
            return b1 && b2 && b3 || player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight;
        }
        public override void OnInBiome(Player player)
        {
            player.ClearBuff(BuffID.Featherfall);
        }
        
    }
}
