using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.IO;
using StructureHelper;
using ProjectInfinity.Content.Tiles.CrystalDesert;
using static Terraria.ModLoader.ModContent;
using static Terraria.WorldGen;

namespace ProjectInfinity.Content.WorldGen
{
    public partial class ProjectInfinityWorldGen : AutoGenPass
    {
        private static Mod instance => ProjectInfinity.Instance;
        public override string PassName => "test";
        public override string InsertAfter => "Micro Biomes";
        public override void GenPass(GenerationProgress progress, GameConfiguration configuration)
        {
            if(progress != null)
                progress.Message = "Mixing crystal and sand";

            int entranceHeight = 50;
            int entranceWidth = 200;

            Rectangle entranceInfo = new Rectangle(GenVars.UndergroundDesertLocation.X + Main.rand.Next(60), GenVars.UndergroundDesertLocation.Y + GenVars.UndergroundDesertLocation.Height / 2 + Main.rand.Next(50), (int)(entranceWidth * 1.5f), (int)(entranceHeight * 1.5f));

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    if(entranceInfo.Contains(i,j))
                    {
                        Tile tile = Main.tile[i,j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<CrystalSandstone_Tile>();
                    }
                }
            }
            //Generator.GenerateStructure("asda",new Point16((int)(entranceInfo.X / 1.5f), (int)(entranceInfo.Y / 1.5f + entranceHeight)),Mod);
        }
    }
}
