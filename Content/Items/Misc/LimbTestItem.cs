using ProjectInfinity.Core.Systems.CameraHandler;
using ProjectInfinity.Content.NPCs.InverseKinematicsTest;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectInfinity.Content.Foregrounds;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class LimbTestItem : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "CrystalWand";
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            //Item.Shoot = ModContent.ProjectileType<testShaderProj>();
            
        }
        public override bool AltFunctionUse(Player player) => true;
        public override bool? UseItem(Player player)
        {
            
            return true;
        }

    }
}
