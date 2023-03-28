using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class ScreenShaderDebug : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "CrystalWand";
        public override void SetStaticDefaults()
        {
            
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 15;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                Filters.Scene.Activate("testscreenshader");
            } 
            else
            {
                Filters.Scene["testscreenshader"].Deactivate();
            }
            
            return false;
        }
    }
}
