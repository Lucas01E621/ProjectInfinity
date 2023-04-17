using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectInfinity.Common.Systems.CameraHandler;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs.BaseTypes
{
    internal abstract class ModBoss : ModNPC
    {
        public abstract int maxHP { get; }
        public abstract int defense { get; }
        /// <summary>
        /// Makes the boss not damageable and not active before player tries to damage it first, needs projectile to work
        /// </summary>
        public virtual bool hasImmunityBeforeFight { get; set; }
        public virtual bool canDamageHostile { get; set; }
        public virtual bool hasDeathAnim { get; set; }
        public int deathAnimDuration;
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

            Player player = Main.player[NPC.target];
            Projectile[] proj = Main.projectile.Take(Main.maxProjectiles).Where(p => p.active && p.owner == player.whoAmI).ToArray();
            bool any = proj.Any(p => p.Hitbox.Intersects(NPC.Hitbox));

            if (fightStarted)
            {
                timer[0]++;
            }
                

            //UpdateFX(pushed);
            ImmuneBeforeFight(hasImmunityBeforeFight);
            StartFight(any);
            PushPlayer(player, NPC.Hitbox.Intersects(player.Hitbox));
            DeathAnim();

            //this needs to be at the end for extra safety
            SafeAI();
        }
        public sealed override bool PreKill()
        {
            SafePreKill();
            return !hasDeathAnim;
        }
        public sealed override void OnSpawn(IEntitySource source)
        {
            if (hasIntro && fightStarted)
                CameraSystem.AsymetricalPan(120, introDuration, 120, NPC.Center);

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

            /*if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene.Activate("Shockwave", NPC.Center).GetShader().UseColor(3, 5, 13).UseTargetPosition(NPC.Center);
            }*/
        }
        private void DeathAnim()
        {
            if(hasDeathAnim && NPC.life <= 2)
                CameraSystem.AsymetricalPan(120, deathAnimDuration, 120, NPC.Center);
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

        public virtual void SafeAI() { }
        public virtual void SafeSetDefaults() { }
        public virtual void SafeSetStaticDefaults() { }
        public virtual void SafePreKill() { }
        public virtual void SafeOnSpawn(IEntitySource source) { }
    }
}
