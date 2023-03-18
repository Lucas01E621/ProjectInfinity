using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Content.Items.Dye;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using ReLogic.Content;

namespace ProjectInfinity.Core.Loaders
{
    internal class DyeLoader : IOrderedLoadable
    {
        public float Priority => 1.2f;
        public void Load()
        {
            if (Main.dedServ)
                return;
        }
        public void Unload()
        {

        }
    }
}
