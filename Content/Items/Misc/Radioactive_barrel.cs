using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ProjectInfinity.Content.Bosses;
using ProjectInfinity.Content.Bosses.RadioactiveSludge;

namespace ProjectInfinity.Content.Items.Misc
{
    public class Radioactive_barrel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radioactive Barrel");
            Tooltip.SetDefault("It attracts an abomination");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {

            return !NPC.AnyNPCs(ModContent.NPCType<Radioactive_sludge>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {

                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<Radioactive_sludge>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {

                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }


    }
}