using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core.Systems.CameraHandler;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs.BaseTypes
{
    public abstract class ModBoss : ModNPC
    {
        public abstract int maxHP { get; }
        public abstract int defense { get; }

        /// <summary>
        /// Makes the boss not damageable and not active before player tries to damage it first, needs projectile to work
        /// </summary>
        public virtual bool hasImmunityBeforeFight { get; set; }

        public virtual bool canDamageHostile { get; set; }

        public virtual bool hasDeathAnim { get; set; }
        public int deathAnimNPCType;
        public virtual int DeathAnimNPCType
        {
            get{ return deathAnimNPCType ;}
            set{
                if(hasDeathAnim)
                    deathAnimNPCType = value;
            }
        }

        public static int deathAnimDuration;

        public virtual int DeathAnimDuration
        {
            get
            {
                return deathAnimDuration;
            }
            set
            {
                if (hasDeathAnim)
                    deathAnimDuration = value;
            }
        }

        public virtual bool hasIntro { get; set; }
        public int introDuration;

        public virtual int IntroDuration
        {
            get
            {
                return introDuration;
            }
            set
            {
                if (hasIntro)
                    introDuration = value;
            }
        }

        public int[] ExtraAI = new int[99]; 
        public bool fightStarted = false;
        public Rectangle PushBox;
        bool fightCheck = false;

        public sealed override void SetStaticDefaults()
        {
            SafeSetStaticDefaults();
        }

        public sealed override void SetDefaults()
        {
            NPC.lifeMax = maxHP;
            NPC.defense = defense;
            NPC.knockBackResist = 0;
            NPC.boss = true;
            NPC.dontTakeDamage = false;
            NPC.friendly = false;
            SafeSetDefaults();
        }

        int[] timer = new int[99];

        int progress = 0;

        int sweffecttimer = 0;

        public sealed override void AI()
        {
            NPC.TargetClosest();

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player plr = Main.player[i];
                if(!plr.active && plr.dead) continue;

                for (int j = 0; j < Main.maxProjectiles; j++)
                {
                    Projectile proj = Main.projectile[i];
                    if(!proj.active && proj.owner != plr.whoAmI) continue;
                    
                    if(proj.Hitbox.Intersects(NPC.Hitbox))
                        fightCheck = true;
                }
                PushPlayer(plr, plr.Hitbox.Intersects(PushBox));
            }

            if (fightStarted)
            {
                timer[0]++;
            }
            if (NPC.life < 2)
                timer[1]++;

            //UpdateFX(pushed);
            ImmuneBeforeFight(hasImmunityBeforeFight);
            StartFight(fightCheck);
            
            

            //this needs to be at the end for extra safety
            SafeAI();
        }

        public sealed override bool PreKill()
        {
            SafePreKill();
            DeathAnim();
            return !hasDeathAnim;
        }

        public sealed override void OnSpawn(IEntitySource source)
        {
            if (hasIntro && fightStarted)
                CameraSystem.AsymetricalPan(120, introDuration, 120, NPC.Center);

            PushBox = new( (int)NPC.position.X * 2, (int)NPC.position.Y * 2, 60 * 16, 60 * 16 );

            SafeOnSpawn(source);
        }

        private void ImmuneBeforeFight(bool immunityBeforeStart)
        {
            if (immunityBeforeStart && !fightStarted)
            {
                NPC.dontTakeDamage = true;
                NPC.boss = false;
                NPC.friendly = true;
            }
        }

        private void StartFight(bool projectile)
        {
            if(!fightStarted &&  (projectile || !hasImmunityBeforeFight))
            {
                NPC.dontTakeDamage = false;
                NPC.boss = true;
                fightStarted = true;
            }
        }

        bool pushed = false;

        private void PushPlayer(Player player,bool insideBoss)
        {
            if (insideBoss && fightStarted && !pushed)
            {
                Vector2 dir = player.Center - NPC.Center;
                pushed = true;
                dir.Normalize();
                player.velocity = dir * 20;
            }
            if (timer[0] >= 60)
            {
                NPC.friendly = false;
            }
            
            if(!insideBoss && fightStarted && !pushed)
                pushed = true;


            /*if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene.Activate("Shockwave", NPC.Center).GetShader().UseColor(3, 5, 13).UseTargetPosition(NPC.Center);
            }*/
        }

        private void DeathAnim()
        {
            if(hasDeathAnim && NPC.life <= 2)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.position.X, (int)NPC.position.Y, deathAnimNPCType, NPC.whoAmI);
                CameraSystem.AsymetricalPan(40, deathAnimDuration, 40, NPC.Center);
            }
        }

        /*void UpdateFX(bool pushed)
        {
            Main.NewText(sweffecttimer);
            if (pushed)
            {
                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float sprogress = (180f - progress) / 60f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(-sprogress).UseOpacity(100f * (1 - sprogress / 3f));
                    sweffecttimer++;
                }
                if (sweffecttimer >= 3)
                {
                    Filters.Scene["Shockwave"].Deactivate();
                }
            }
        }*/

        /// <summary>
        /// safe variant of the original hook, prevents overriding the ModBoss code
        /// </summart>
        public virtual void SafeAI() { }

        /// <summary>
        /// safe variant of the original hook, prevents overriding the ModBoss code
        /// </summart>
        public virtual void SafeSetDefaults() { }

        /// <summary>
        /// safe variant of the original hook, prevents overriding the ModBoss code
        /// </summart>
        public virtual void SafeSetStaticDefaults() { }

        /// <summary>
        /// safe variant of the original hook, prevents overriding the ModBoss code
        /// </summart>
        /// <returns> Ture by default, returun False to prevent the npc from dropping items. It does not prevent the npc from dying npc still dies! </returns>
        public virtual bool SafePreKill() { return true; }

        /// <summary>
        /// safe variant of the original hook, prevents overriding the ModBoss code
        /// </summart>
        public virtual void SafeOnSpawn(IEntitySource source) { }
    }
}
