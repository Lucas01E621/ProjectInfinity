using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Accessories.ManaOverloader
{
    internal class ManaOverloader : ModItem
    {
        public override string Texture => AssetDirectory.Accessories + "ManaOverloader";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mana Overloader");
            Tooltip.SetDefault("Grants Magic damage and crit chance based on your mana.");
        }
        public override void SetDefaults()
        {
            Item.height = 30;
            Item.width = 30;
            Item.accessory = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "Tooltip0", $"Magic damage bonus: %{GetStage(Main.LocalPlayer) * 2.5f}");
            tooltips.Add(tooltip);

            tooltip = new TooltipLine(Mod, "Tooltip1", $"Magic Crit bonus: +%{GetStage(Main.LocalPlayer) * 1.5f}");
            tooltips.Add(tooltip);
        }
        
        public override void UpdateEquip(Player player)
        {
            float stage = GetStage(player);
            if (stage >= 1)
            {
                player.GetDamage(DamageClass.Magic) += stage * 0.025f;
                player.GetCritChance(DamageClass.Magic) += stage * 1.5f;
            }
        }
        private float GetStage(Player player)
        {
            return MathF.Floor(player.statManaMax2 / 50);
        }
    }
}
