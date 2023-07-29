using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;
using Terraria.WorldBuilding;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using StructureHelper;

namespace ProjectInfinity.Content.Subworlds
{
    internal class CrystalDesertSubWorld : Subworld
    {
        public override int Width => 500;
        public override int Height => 500;
        
        
        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => true;

        public override List<GenPass> Tasks => new List<GenPass>()
        {
            new ExampleGenPass()
        };

        // Sets the time to the middle of the day whenever the subworld loads
        public override void OnLoad()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }
        public class ExampleGenPass : GenPass
        {
            public ExampleGenPass() : base("Terrain", 1) { }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Mixing sand with crystals"; // Sets the text displayed for this pass
                Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
                Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds
                //Main.UnderworldLayer
                
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); // Controls the progress bar, should only be set between 0f and 1f
                        int x = i % 16 == 0 ? i : 0;
                        int y = j % 16 == 0 ? j : 0;
                        
                        Generator.GenerateStructure("Content/Structures/deneme", new Point16(x, y), ProjectInfinity.Instance);
                    }
                }
            }
        }
    }
}
