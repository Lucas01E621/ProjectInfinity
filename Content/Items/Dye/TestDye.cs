using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;

namespace ProjectInfinity.Content.Items.Dye
{
    internal class TestDye : ModItem
    {
        public override string Texture => AssetDirectory.Items + "Dye/" + Name;
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(
                    Item.type,
                    new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Effects/test", AssetRequestMode.ImmediateLoad).Value), "ArmorNoise")).UseImage("Images/Misc/noise");
            }
        }
        public override void SetDefaults()
        {
            //item.dye
            int dye = Item.dye;
            Item.CloneDefaults(ItemID.GelDye);
            Item.dye = dye;
        }
    }
}
