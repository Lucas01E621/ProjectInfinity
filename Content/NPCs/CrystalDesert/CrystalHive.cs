using Microsoft.Xna.Framework;
using Mono.Cecil;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace ProjectInfinity.Content.NPCs.enemies.CrystalDesert
{
    internal class CrystalHive : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalDesertNPC + "CrystalHive";
        public static int MinionType()
        {
            return ModContent.NPCType<bob>();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Hive");

        }
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 50;
            NPC.aiStyle = -1;
            NPC.damage = 15;
            NPC.defDefense = 5;
            NPC.lifeMax = 450;
            NPC.knockBackResist = 0;
        }
        public override bool NeedSaving()
        {
            return true;
        }
        public override void AI()
        {
            // 
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            NPC.ai[0]++;
            Player player = Main.player[NPC.target];
            if (NPC.ai[0] > 60)
            {
                NPC.ai[0] = 0;

                int speed = 5;
                Vector2 velocity = NPC.DirectionTo(player.Center) * speed;
                Vector2 position = NPC.Center;
                position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
                float rotation = MathHelper.ToRadians(30);
                int numberProjectiles = Main.rand.Next(5, 8);
                if (NPC.Distance(player.position) / 16 > 100) return;
                if (NPC.Distance(player.position) / 16 < 15)
                {
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X - 2, position.Y - 2, perturbedSpeed.X * speed, perturbedSpeed.Y * speed, ModContent.ProjectileType<CrystalHiveProj>(), NPC.damage, 0f, Main.myPlayer);
                    }
                }
                else
                {
                    SpawnMinions();
                }
            }
            //


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

                if (minionNPC.ModNPC is bob minion)
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
    internal class CrystalHiveProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "CrystalHiveProj";
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 999;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.25f;
        }
    }
}
