using ProjectInfinity.Content.Bosses.CrystalDesert;
using ProjectInfinity.Content.NPCs;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    public class CrystalVein : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalDesert + "CrystalVein";

        public bool downed = false;
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;

        public const float RotationTimerMax = 360;
        public ref float RotationTimer => ref NPC.ai[2];

        // Helper method to determine the body type
        public static int BodyType()
        {
            return ModContent.NPCType<CrystalHeartBody>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Vein");
        }
        public override void SetDefaults()
        {
            NPC.height = 64;
            NPC.width = 96;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.damage = 0;
            NPC.defense = 9999;
            NPC.lifeMax = 20;
            NPC.knockBackResist = 0;
        }
        public override void AI()
        {
            if (Despawn()) return;
            NPC.position.X = (int)NPC.position.X;
            NPC.position.Y = (int)NPC.position.Y;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (NPC.life < 2)
            {
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                downed = true;
                Main.NewText("onhit");
                SpawnMinions();
            }
        }
        private bool Despawn()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient &&
                (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType()))
            {
                // * Not spawned by the boss body (didn't assign a position and parent) or
                // * Parent isn't active or
                // * Parent isn't the body
                // => invalid, kill itself without dropping any items
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                return true;
            }
            return false;
        }
        public void SpawnMinions()
        {
            int count = 1;
            var entitySource = NPC.GetSource_FromAI();
            for (int i = 0; i < count; i++)
            {
                if (NPC.ai[3] >= 6) return;
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<bob>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                NPC.ai[3]++;
                minionNPC.ai[0] = NPC.whoAmI;


                // Now that the minion is spawned, we need to prepare it with data that is necessary for it to work
                // This is not required usually if you simply spawn NPCs, but because the minion is tied to the body, we need to pass this information to it



                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }

            }
        }
    }
}
