using Terraria.WorldBuilding;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using Terraria.IO;
using System.Collections.Generic;
using System;

namespace ProjectInfinity.Content.WorldGen
{
    public abstract class AutoGenPass : ModSystem
    {
        private static Mod instance => ProjectInfinity.Instance;
        public abstract string InsertAfter {get;}
        public abstract string PassName {get;}
        public override sealed void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int Index = tasks.FindIndex(genpass => genpass.Name.Equals(InsertAfter));
            
            tasks.Insert(Index + 1, new PassLegacy(PassName, GenPass));
        }
        public virtual void GenPass(GenerationProgress progress, GameConfiguration config) { }
    }
}
