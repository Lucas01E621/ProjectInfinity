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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Content.Foregrounds;
using ProjectInfinity.Content.Bosses.CrystalDesert;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Terraria.Enums;
using Terraria.GameContent;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    public partial class CrystalHeartBody : ModBoss
    {
        public ref int attackTimer => ref ExtraAI[0];
        public ref int healTimer => ref ExtraAI[1];
        public int LivingVeinCount = 0;

        void attackAI()// gets called at CrystalHearthBody.Main
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            UsefulFunctions.DustRing(NPC.Center, 16 * 30, DustID.BlueFairy);

            if ( NPC.Distance(player.position) > 16 * 30 )
            {
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.dontTakeDamage = false;
            }
            if(LivingVeinCount > 0 && ReturnDifficulty() > 0)
                VeinHeal();
        }

        public override void SafeOnSpawn(IEntitySource source)
        {
            SpawnVeins();
            MawPosition = new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height);
        }
        /// <summary>
        /// returns the "phases" which are called stages idk why
        /// </summart>
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
        /// <summary>
        /// returns the game difficulty
        /// </summart>
        public int ReturnDifficulty() 
        {
            int diffmMult = 1;
            if(Main.expertMode)
                diffmMult = 2;
            if(Main.masterMode)
                diffmMult = 3;
            return diffmMult;
        }

        private void SpawnVeins() //on spawn, spawns the veins
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
                int offsetX = 0, offsetY = 0;           //temp spawn code vein spawn will be handled by spawn tiles in boss arena
                if (i == 0)                             //temp spawn code vein spawn will be handled by spawn tiles in boss arena
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
                    offsetY = 16 * 30;                  //temp spawn code vein spawn will be handled by spawn tiles in boss arena
                    offsetX = -16 * 30;                 //temp spawn code vein spawn will be handled by spawn tiles in boss arena
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
                LivingVeinCount++;
            }
        }

        void SpawnCrawlers() //it should spawn crawlers depending on the difficulty if there isnt any active currently but it doesnt needs a fix
        {
            NPC[] crawlers = Main.npc.Take(Main.maxNPCs).Where( p => p.active ).ToArray();
            bool any = crawlers.Any( p => p.type == ModContent.NPCType<CrystalCrawler>() );

            var entitySource = NPC.GetSource_FromAI();
            int CrawlerCount = 3 * ReturnDifficulty();
            if(!any) attackTimer++;
            if(attackTimer >= 360 && any)
            {
                for (int i = 0; i < CrawlerCount; i++)
                {
                    NPC.NewNPC( entitySource, (int)MawPosition.X, (int)MawPosition.Y, ModContent.NPCType<CrystalCrawler>(), NPC.whoAmI );
                }
                attackTimer = 0;
            }
        }
        /// <summary>
        /// regenerates life depending on the vein count and game difficulty
        /// </summart>
        void VeinHeal()
        {
            healTimer++;
            if( NPC.life > NPC.lifeMax )
                NPC.life = NPC.lifeMax;
            
            if( healTimer >= 360 * ( 1 / MathF.Floor( LivingVeinCount + ReturnDifficulty() ) ) && NPC.life < NPC.lifeMax )
            {
                int healval = LivingVeinCount * ReturnDifficulty() * 25;

                NPC.life += healval;
                NPC.HealEffect(healval);
                healTimer = 0;
                Main.NewText(LivingVeinCount);
                Main.NewText(ReturnDifficulty());
            }
            if( NPC.life == NPC.lifeMax && LivingVeinCount > 0  && ModContent.GetInstance<ProjectInfinityWorld>().NigtmareMode)
            {
                //make it generate barrier
            }
        }
    }
}
