using ProjectInfinity.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using ProjectInfinity.Common.Systems;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Bosses.RadioactiveSludge
{
    [AutoloadBossHead]
    internal class Radioactive_sludge : ModNPC
    {
        public override string Texture => AssetDirectory.Radioactive_sludge + Name;
        public override string BossHeadTexture => AssetDirectory.Radioactive_sludge + Name + "_Head_Boss";
        public bool SecondStage
        {
            get => NPC.ai[0] == 1f;
            set => NPC.ai[0] = value ? 1f : 0f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radioactive Sludge");
            Main.npcFrameCount[Type] = 6;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCDebuffImmunityData debuffdata = new()
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.OnFire,
                    BuffID.Oiled,
                    BuffID.ShadowFlame,
                    BuffID.Poisoned,
                    BuffID.Confused,
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffdata);
        }

        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 80;
            NPC.defense = 40;
            NPC.lifeMax = 20000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(gold: 20);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
            NPC.scale = 2f;
            


            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Radioactive_catastrophe");
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {

            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Emerged from the nuclear waste as a multicellular organism,its diet consist of:cellulose,Radium and meat,human meat to be precise.")
            });
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses;
            return true;
        }

        private enum Frame
        {
            frame1,
            frame2,
            frame3,
            frame4,
            frame5,
            frame6,

        }

        public override void FindFrame(int frameHeight)
        {

            // For the most part, our animation matches up with our states.

            NPC.frameCounter++;

            if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = (int)Frame.frame1 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = (int)Frame.frame2 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = (int)Frame.frame3 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = (int)Frame.frame4 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = (int)Frame.frame5 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = (int)Frame.frame6 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }



        }
        private enum ActionState
        {
            Idle,
            Idle2,
            Jumpy,
            Jump,
            Jump2,
            Jump3,
            Jump4,
            Missile,
            Prehover,
            Hover,
            Fall,
            Gamma,
        }

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_HoverTime => ref NPC.ai[2];



        public override void AI()
        {

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            CheckSecondStage();


            if (SecondStage)
            {
                DoSecondStage(player);
            }
            else
            {
                DoFirstStage(player);
            }


            if (NPC.Distance(Main.player[NPC.target].position) > 2500)
            {
                NPC.position.X = Main.player[NPC.target].position.X;
                NPC.position.Y = Main.player[NPC.target].position.Y - 400f;
                Vector2 speed = Main.rand.NextVector2Unit((float)MathHelper.Pi * 3 / 2, (float)MathHelper.Pi);
                Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.Flare, speed * 20, 5, Color.Green);
                dust.noGravity = true;
            }



        }

        private void CheckSecondStage()
        {
            if (SecondStage)
            {
                // No point checking if the NPC is already in its second stage
                return;
            }

            if (NPC.GetLifePercent() < 0.5f) {
                SecondStage = true;
                NPC.netUpdate = true;
            }
        }

        private void DoFirstStage(Player player)
        {

            switch (AI_State)
            {
                case (float)ActionState.Idle:
                    Idle();
                    break;
                case (float)ActionState.Jump:
                    Jump();
                    break;
                case (float)ActionState.Jump2:
                    Jump2();
                    break;
                case (float)ActionState.Jump3:
                    Jump3();
                    break;
                case (float)ActionState.Jump4:
                    Jump4();
                    break;
                case (float)ActionState.Missile:
                    Missile();
                    break;
                case (float)ActionState.Prehover:
                    Prehover();
                    break;
                case (float)ActionState.Hover:
                    Hover();
                    break;
                case (float)ActionState.Fall:
                    Fall();
                    break;
                case (float)ActionState.Gamma:
                    Gamma();

                    break;
            }
        }

        private void DoSecondStage(Player player)
        {
            switch (AI_State)
            {
                case (float)ActionState.Idle2:
                    Idle2();
                    break;
                case (float)ActionState.Jumpy:
                    Jumpy();
                    break;



            }

        }



        public override bool? CanFallThroughPlatforms()
        {
            if (AI_State == (float)ActionState.Fall && NPC.HasValidTarget && Main.player[NPC.target].Top.Y > NPC.Bottom.Y)
            {
                return true;
            }

            return false;
        }

        // start of the attacks

        private void Idle()
        {
            AI_Timer++;

            if (AI_Timer == 120)
            {
                AI_State = (float)ActionState.Jump;
                AI_Timer = 0;
            }

        }

        private void Idle2()
        {
            AI_Timer++;

            if (AI_Timer == 120)
            {
                Main.NewText("second phase");

                AI_State = (float)ActionState.Jumpy;
                AI_Timer = 0;
            }
            
        }


        private void Jumpy()
        {
            AI_Timer++;

            if (AI_Timer == 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                }

                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 2, -5f);
            }

            else if (NPC.collideX == true || NPC.collideY == true && AI_Timer > 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);

                }
                NPC.velocity = new Vector2(0, 0);
                AI_Timer = 0;
            }

        }

        private void Jump()
        {
            AI_Timer++;


            if (AI_Timer == 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                }

                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 2, -15f);
            }

            else if (NPC.collideX == true || NPC.collideY == true && AI_Timer > 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);

                }
                NPC.velocity = new Vector2(0, 0);
                AI_Timer = 0;
                AI_State = (float)ActionState.Jump2;
            }
        }



        private void Jump2()
        {

            AI_Timer++;


            if (AI_Timer == 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 40, 10, DustID.Flare, 0,0,0,default,2);
                    Dust.NewDust(NPC.BottomRight, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                    
                    
                }
                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 2, -15f);
            }

            else if (NPC.collideX == true || NPC.collideY == true && AI_Timer > 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);

                }
                NPC.velocity = new Vector2(0, 0);
                AI_Timer = 0;
                AI_State = (float)ActionState.Jump3;
            }
        }
        private void Jump3()
        {
            AI_Timer++;


            if (AI_Timer == 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                }
                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 2, -10f);
            }

            else if (NPC.collideX == true || NPC.collideY == true && AI_Timer > 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);

                }
                NPC.velocity = new Vector2(0, 0);
                AI_Timer = 0;
                AI_State = (float)ActionState.Jump4;
            }
        }
        private void Jump4()
        {
            AI_Timer++;


            if (AI_Timer == 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 40, 10, DustID.Flare, 0, 0, 0, default, 2);
                }
                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(NPC.direction * 2, -20f);
            }

            else if (NPC.collideX == true || NPC.collideY == true && AI_Timer > 60)
            {
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(NPC.BottomLeft, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);
                    Dust.NewDust(NPC.BottomRight, 100, 10, DustID.Smoke, 0, 0, 0, default, 2);

                }
                NPC.velocity = new Vector2(0, 0);
                AI_Timer = 0;
                AI_State = (float)ActionState.Missile;
            }
        }



        private void Missile()
        {
            AI_Timer++;



            if (AI_Timer == 0)
            {

                NPC.velocity = new Vector2(0, 0);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, new Vector2(0, -2), ModContent.ProjectileType<Missile>(), 50, 3);
            }

            if (AI_Timer == 60)
            {

                NPC.velocity = new Vector2(0, 0);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.TopLeft, new Vector2(0, -2), ModContent.ProjectileType<Missile>(), 50, 3);
            }

            if (AI_Timer == 120)
            {

                NPC.velocity = new Vector2(0, 0);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.TopRight, new Vector2(0, -2), ModContent.ProjectileType<Missile>(), 50, 3);
            }

            if (AI_Timer == 240)
            {

                NPC.velocity = new Vector2(0, 0);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, new Vector2(0, -2), ModContent.ProjectileType<Missile>(), 50, 3);
            }

            else if (AI_Timer == 400)
            {
                AI_State = (float)ActionState.Prehover;
                AI_Timer = 0;
            }

        }



        private void Prehover()
        {
            AI_Timer++;

            if (AI_Timer == 1)
            {
                NPC.TargetClosest(true);
                // We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.
                NPC.velocity = new Vector2(NPC.direction * 2, -15f);

            }
            else if (AI_Timer > 40)
            {
                // after .66 seconds, we go to the hover state. //TODO, gravity?
                AI_State = (float)ActionState.Hover;
                AI_Timer = 0;
            }
        }
        private void Hover()
        {
            NPC.TargetClosest(true);

            Player player = Main.player[NPC.target];
            AI_Timer++;


            AI_HoverTime = Main.rand.NextBool() ? 200 : 100;

            NPC.velocity = new Vector2(NPC.direction * 6, 0);

            float disty = NPC.position.Y - player.position.Y;
            float distx = NPC.position.X - player.position.X;


            if (disty < 0 && distx < 10)
            {
                AI_State = (float)ActionState.Fall;
                AI_Timer = 0;
            }

        }


        private void Fall()
        {
            AI_Timer++;


            NPC.velocity = new Vector2(0, 10);

            if (NPC.collideX == true || NPC.collideY == true)
            {
                AI_State = (float)ActionState.Gamma;
                AI_Timer = 0;
            }

        }


        private void Gamma()
        {
            AI_Timer++;


            if (AI_Timer < 200)
            {
                Vector2 pos = Main.rand.NextVector2CircularEdge(10f, 10f);
                Dust d = Dust.NewDustPerfect(NPC.Center + pos * 20, DustID.Firework_Green, pos, 10,Color.Green);
                d.noGravity = true;
                d.fadeIn = 2;
                

            }

            else if (AI_Timer == 200)
            {
                for (int i = 0; i < 100; i++)
                {

                    Vector2 speed = Main.rand.NextVector2Unit((float)MathHelper.Pi * 3 / 2, (float)MathHelper.Pi);
                    Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.Firework_Green, speed * 20, 5,Color.Green);
                    dust.noGravity = true;
                    
                }
                Vector2 vel = new(NPC.direction * 2, 5);
                vel.Normalize();
                int index = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(5,0), ModContent.ProjectileType<Gamma_ray>(), 0, 0);
                Projectile proj = Main.projectile[index];
                if (proj.ModProjectile is Gamma_ray minion)
                {
                    // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                    minion.ParentIndex = NPC.whoAmI; // Let the minion know who the "parent" is
                }

                AI_State = (int)ActionState.Jump;
                AI_Timer = 0;

            }
        }

        // end of the attacks
        // end of the first stage (finaly)


    }
}
