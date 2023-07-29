using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Subworlds;
using ProjectInfinity.Core;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class SubworldItem : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "SubworldItem";
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 10;
            Item.shootSpeed = 15;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                SubworldSystem.Exit();
                return true;
            }
            else
            {
                SubworldSystem.Enter<CrystalDesertSubWorld>();
                return true;
            }
            return true;
        }
    }
}
