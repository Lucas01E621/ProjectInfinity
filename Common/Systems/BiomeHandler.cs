using ProjectInfinity.Content.Tiles;
using ProjectInfinity.Content.Tiles.CrystalDesert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Common.Systems
{
    internal class BiomeHandler : ModSystem
    {
        public int CrystanSandstoneBlockCount;
        public int AsteriodBlockCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            CrystanSandstoneBlockCount = tileCounts[ModContent.TileType<CrystalSandstone>()];
			AsteriodBlockCount = tileCounts[ModContent.TileType<AsteroidBlock>()];
        }
    }
}
