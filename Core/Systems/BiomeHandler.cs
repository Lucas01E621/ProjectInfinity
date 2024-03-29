﻿using ProjectInfinity.Content.Tiles;
using ProjectInfinity.Content.Tiles.CrystalDesert;
using ProjectInfinity.Content.Tiles.AstralOcean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Core.Systems
{
    internal class BiomeHandler : ModSystem
    {
        public int CrystanSandstoneBlockCount;
        public int AsteriodBlockCount;
        public int AstralSandCount;
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            CrystanSandstoneBlockCount = tileCounts[ModContent.TileType<CrystalSandstone_Tile>()];
			AsteriodBlockCount = tileCounts[ModContent.TileType<AsteroidBlock_Tile>()];
            AstralSandCount = tileCounts[ModContent.TileType<AstralSand_Tile>()];
        }
    }
}
