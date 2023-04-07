using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
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
        int timer = 0;
        int progress = 0;
        int sweffecttimer = 0;
        public sealed override void AI()
        {
            progress++;
            NPC.TargetClosest();

            Player player = Main.player[NPC.target];
            Projectile[] proj = Main.projectile.Take(Main.maxProjectiles).Where(p => p.active && p.owner == player.whoAmI).ToArray();
            bool any = proj.Any(p => p.Hitbox.Intersects(NPC.Hitbox));

            if (fightStarted)
            {
                timer++;
            }
            //UpdateFX(pushed);
            ImmuneBeforeFight(hasImmunityBeforeFight);
            StartFight(any);
            PushPlayer(player, NPC.Hitbox.Intersects(player.Hitbox));
            SafeAI();
        }
        public void ImmuneBeforeFight(bool immunityBeforeStart)
        {
            if (immunityBeforeStart && !fightStarted)
            {
                NPC.dontTakeDamage = true;
                NPC.boss = false;
                NPC.friendly = true;
            }
        }
        public void StartFight(bool projectile)
        {
            if(!fightStarted &&  (projectile || !hasImmunityBeforeFight))
            {
                NPC.dontTakeDamage = false;
                NPC.boss = true;
                fightStarted = true;
            }
        }
        bool pushed = false;
        public void PushPlayer(Player player,bool insideBoss)
        {
            if (insideBoss && fightStarted && !pushed)
            {
                Vector2 dir = player.Center - NPC.Center;
                pushed = true;
                dir.Normalize();
                player.velocity = dir * 20;
            }
            if (timer >= 60)
            {
                NPC.friendly = false;
            }

            /*if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
            {
                Filters.Scene.Activate("Shockwave", NPC.Center).GetShader().UseColor(3, 5, 13).UseTargetPosition(NPC.Center);
            }*/
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
        /// Basically AI() but it doesnt overrides custom logic 
        /// </summary>
        public virtual void SafeAI() { }
        /// <summary>
        /// Basically SetDefaults() but it doesnt overrides custom logic 
        /// </summary>
        public virtual void SafeSetDefaults() { }
        /// <summary>
        /// Basically SetStaticDefaults() but it doesnt overrides custom logic 
        /// </summary>
        public virtual void SafeSetStaticDefaults() { }
    }
}
