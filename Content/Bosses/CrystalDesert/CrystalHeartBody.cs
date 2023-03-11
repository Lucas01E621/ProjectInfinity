using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    [AutoloadBossHead]
    internal class CrystalHeartBody : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalHeart + "CrystalHeartBody";
        public override string BossHeadTexture => AssetDirectory.CrystalHeart + "CrystalHeartBody_Head_Boss";

        public bool fightStarted = false;
        public bool SpawnedMinions
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }
        public static int MinionType()
        {
            return ModContent.NPCType<CrystalVein>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Heart");
            //Main.npcFrameCount[Type] = 6;
            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "ProjectInfinity/Assets/Bosses/CrystalHeart/CrystalHeartPreview",
                PortraitScale = 0.6f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 0f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);


            //immune
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.OnFire
                    ,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

        }
        public override void SetDefaults()
        {
            NPC.width = 560;
            NPC.height = 400;
            NPC.damage = 12;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f; // Take up open spawn slots, preventing random NPCs from spawning during the fight

            // Don't set immunities like this as of 1.4:
            // NPC.buffImmune[BuffID.Confused] = true;
            // immunities are handled via dictionaries through NPCID.Sets.DebuffImmunitySets

            // Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
            NPC.aiStyle = -1;

            // Custom boss bar
            //NPC.BossBar = ModContent.GetInstance<>();

            // The following code assigns a music track to the boss in a simple way.
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/orthodoxia");
            }
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            UsefulFunctions.DustRing(NPC.Center, 16 * 30, DustID.Adamantite);

            if (NPC.Distance(player.position) > 16 * 30)
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }

        }
        public override void OnSpawn(IEntitySource source)
        {
            SpawnVeins();
            fightStarted = false;
        }
        public int ReturnStage()
        {
            int stage = 0;
            int stageHP = NPC.lifeMax / 4;
            if (NPC.life <= stageHP * 1)
            {
                stage = 4;
            }
            else if (NPC.life <= stageHP * 2)
            {
                stage = 3;
            }
            else if (NPC.life <= stageHP * 3)
            {
                stage = 2;
            }
            else if (NPC.life <= stageHP * 4)
            {
                stage = 1;
            }
            return stage;
        }
        public int ReturnDifficulty()
        {
            return 0;
        }
        private void SpawnVeins()
        {
            if (SpawnedMinions)
            {
                // No point executing the code in this method again
                return;
            }

            SpawnedMinions = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // Because we want to spawn minions, and minions are NPCs, we have to do this on the server (or singleplayer, "!= NetmodeID.MultiplayerClient" covers both)
                // This means we also have to sync it after we spawned and set up the minion
                return;
            }

            int count = 4;
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int offsetX = 0, offsetY = 0;
                if (i == 0)
                {
                    offsetX = -16 * 30;
                    offsetY = -16 * 10 + 10;
                }
                else if (i == 1)
                {
                    offsetX = 16 * 30;
                    offsetY = -16 * 10 + 10;
                }
                else if (i == 2)
                {
                    offsetY = 16 * 30;
                    offsetX = 16 * 30;
                }
                else if (i == 3)
                {
                    offsetY = 16 * 30;
                    offsetX = -16 * 30;
                }

                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X + offsetX, (int)NPC.Center.Y + offsetY, ModContent.NPCType<CrystalVein>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                // Now that the minion is spawned, we need to prepare it with data that is necessary for it to work
                // This is not required usually if you simply spawn NPCs, but because the minion is tied to the body, we need to pass this information to it

                if (minionNPC.ModNPC is CrystalVein minion)
                {
                    // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                    minion.ParentIndex = NPC.whoAmI; // Let the minion know who the "parent" is
                    minion.PositionIndex = i; // Give it the iteration index so each minion has a separate one, used for movement
                }

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

    }
}
