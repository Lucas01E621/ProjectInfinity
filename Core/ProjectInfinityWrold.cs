using Terraria;
using Terraria.ModLoader;
using System;

namespace ProjectInfinity.Core
{
    public class ProjectInfinityWorld : ModSystem
    {
        public static float visualTimer;

        public override void PreUpdateWorld()
		{
			visualTimer += (float)Math.PI / 60;

			if (visualTimer >= Math.PI * 2)
				visualTimer = 0;
		}
    }
}