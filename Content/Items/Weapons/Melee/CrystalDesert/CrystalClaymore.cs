using Microsoft.Xna.Framework;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Melee.CrystalDesert
{
    public class CrystalClaymore : ModItem
    {
        public override string Texture => AssetDirectory.Melee + "SoulSword";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Claymore");
            Tooltip.SetDefault("A massive sword made of crystal");
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return player.statMana > 0;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.statMana -= 10;

                Vector2 dir = Main.MouseWorld - player.position;
                dir.Normalize();

                player.velocity += dir * 12;

                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.Silver, player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
                }
                if (player.statMana < 0) player.statMana = 0;
            }
            return true;
        }

    }
}

// garen E shit