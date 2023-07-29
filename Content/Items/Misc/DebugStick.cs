using ProjectInfinity.Common.Players;
using ProjectInfinity.Content.CustomHooks;
using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class DebugStick : ModItem
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
            Item.autoReuse = true;
            Item.channel = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 15;
        }
        public override bool AltFunctionUse(Player player) => true;
        
        public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (!npc.active || npc.ModNPC == null || !(npc.ModNPC is MovingPlatform))
                        continue;
                    npc.active = false;
                    npc.life = 0;
                    NetMessage.SendData(MessageID.SyncNPC, number: npc.whoAmI);
                }
            } else
            {
                player.GetModPlayer<MPlayer>().platformTimer = 10;
            }
            return false;
        }
    }
}
