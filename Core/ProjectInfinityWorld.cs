using Terraria;
using Terraria.ModLoader;
using System;
using ProjectInfinity.Core;
using ProjectInfinity.Core.Systems.ForegroundSystem;

namespace ProjectInfinity.Core
{
    public class ProjectInfinityWorld : ModSystem
    {
        public static float visualTimer;
        public static float timer;
        public bool NigtmareMode;

        public override void PreUpdateWorld()
		{
			visualTimer += (float)Math.PI / 60;

			if (visualTimer >= Math.PI * 2)
				visualTimer = 0;

            

		}
    }
}