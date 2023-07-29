using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProjectInfinity.Core.GlobalNPCs
{
    public class WoundDamageSystem: GlobalNPC
    {
        public bool WoundCanCrit = false;
        public bool canDealWoundDamage = true;
        public int WoundHealth = 1;
        public int empoweredBuffMult = 1;
        public override bool InstancePerEntity => true;

        

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            Main.NewText(WoundHealth + " " + canDealWoundDamage);
            if(npc.boss && canDealWoundDamage && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                hit.SourceDamage /= 2;
                DealWoundDamageProj(npc, projectile, hit);
                WoundHealth -= hit.Damage / 2;
                Main.NewText(WoundHealth);
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if(npc.boss && canDealWoundDamage && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                hit.SourceDamage /= 2;
                DealWoundDamageItem(npc, item, hit);
                WoundHealth -= hit.Damage / 2;
            }
        }
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if(npc.boss && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                int WoundHealthMult = 8;
                if(Main.expertMode)
                    WoundHealthMult = 4;
                if(Main.masterMode)
                    WoundHealthMult = 2;

                WoundHealth = npc.lifeMax / WoundHealthMult;
            }
        }
        public override void AI(NPC npc)
        {
            if(WoundHealth < 0 && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                WoundHealth = 0;

                if(Main.expertMode)
                    empoweredBuffMult = 2;
                if(Main.masterMode)
                    empoweredBuffMult = 3;
                    
                if(!Main.LocalPlayer.HasBuff(ModContent.BuffType<EmpoweredBuff>()))
                {
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<EmpoweredBuff>(),  30 * 60 * empoweredBuffMult);

                    canDealWoundDamage = true;

                    int WoundHealthMult = 8;

                    if(Main.expertMode)
                        WoundHealthMult = 4;
                    if(Main.masterMode)
                        WoundHealthMult = 2;

                    WoundHealth = npc.lifeMax / WoundHealthMult;
                }
            }
            if(Main.LocalPlayer.HasBuff(ModContent.BuffType<EmpoweredBuff>()))
            {
                canDealWoundDamage = false;
            }
        }
        void DealWoundDamageProj(NPC npc, Projectile proj, NPC.HitInfo hit)
        {
            if(canDealWoundDamage && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                CombatText.NewText(npc.Hitbox, new Color(0,255,255), hit.Damage / 2); //FIND A WAY TO CHANGE THE FONT
            }

        }
        void DealWoundDamageItem(NPC npc, Item item, NPC.HitInfo hit)
        {
            if(canDealWoundDamage && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage)
            {
                CombatText.NewText(npc.Hitbox, new Color(0,255,255), hit.Damage / 2); //FIND A WAY TO CHANGE THE FONT
            }
        }
    }
}
