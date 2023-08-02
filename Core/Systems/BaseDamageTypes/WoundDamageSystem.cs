using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Graphics;
using System;

namespace ProjectInfinity.Core.GlobalNPCs
{
    public class WoundDamageSystem: GlobalNPC
    {
        public NPC targetBoss;
        public bool WoundCanCrit = false;
        public bool canDealWoundDamage = true;
        public int MaxWoundHealth = 1;
        public int WoundHealth = 1;
        public int empoweredBuffMult = 1;
        public int lastWoundHealth = 0;
        public override bool InstancePerEntity => true;

        
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			//We need to use the backdoor here because we need to know the final damage to correctly subtract from WoundHealth!
            if(npc.boss && ModContent.GetInstance<ProjectInfinityConfig>().WoundDamage && canDealWoundDamage)
			    modifiers.ModifyHitInfo += (ref NPC.HitInfo n) => ModifyDamage(npc, ref n);
        }

        public override void ResetEffects(NPC npc)
		{
			if (MaxWoundHealth != 0)
				lastWoundHealth = MaxWoundHealth;
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

                MaxWoundHealth = npc.lifeMax / WoundHealthMult;
                WoundHealth = MaxWoundHealth;
                targetBoss = npc;
            }
        }

        public void ModifyDamage(NPC NPC, ref NPC.HitInfo info)
		{
			if (WoundHealth > 0)
			{
				if (WoundHealth > info.Damage)
				{
					CombatText.NewText(NPC.Hitbox, Color.Red, info.Damage);

					WoundHealth -= info.Damage / 2;
					info.Damage /= 2;
				}
				else
				{
					Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.NPCDeath57, NPC.Center);

					CombatText.NewText(NPC.Hitbox, Color.Red, WoundHealth);

					int overblow = info.Damage - WoundHealth;
					info.Damage = (int)(WoundHealth / 2) + overblow;

					WoundHealth = 0;
				}

				info.Knockback *= 0.5f;
			}
		}

        public override void AI(NPC npc)
        {
            if(WoundHealth == 0)
            {
                if(Main.expertMode)
                    empoweredBuffMult = 2;
                if(Main.masterMode)
                    empoweredBuffMult = 3;

                Main.LocalPlayer.AddBuff(ModContent.BuffType<EmpoweredBuff>(),  10 * 60 * empoweredBuffMult);

                WoundHealth = MaxWoundHealth;
            }
            if(Main.LocalPlayer.HasBuff(ModContent.BuffType<EmpoweredBuff>()))
            {
                canDealWoundDamage = false;
            }
            else
            {
                canDealWoundDamage = true;
            }
        }

        public override bool? DrawHealthBar(NPC NPC, byte hbPosition, ref float scale, ref Vector2 position)
		{
			if (WoundHealth > 0 && canDealWoundDamage && NPC.boss)
			{
				float Light = Lighting.Brightness((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16);

				Main.instance.DrawHealthBar((int)position.X, (int)position.Y, NPC.life, NPC.lifeMax, Light, scale);

				Texture2D WoundHealthBar = ModContent.Request<Texture2D>(AssetDirectory.GUI + "WoundHealthBar").Value;

				float progress = Math.Min(WoundHealth / (float)lastWoundHealth, 1);

				Rectangle source = new Rectangle(0, 0, (int)(WoundHealthBar.Width), WoundHealthBar.Height);
				Rectangle target = new Rectangle((int)(position.X - Main.screenPosition.X), (int)(position.Y - Main.screenPosition.Y) + 16, (int)(progress * WoundHealthBar.Width * scale), (int)(WoundHealthBar.Height * scale));

                Texture2D WoundHealthBarFull = ModContent.Request<Texture2D>(AssetDirectory.GUI + "WoundHealthBarFull").Value;

				Rectangle _source = new Rectangle(0, 0, (int)(progress * WoundHealthBar.Width), WoundHealthBar.Height);

                Main.spriteBatch.Draw(WoundHealthBarFull, target, _source, Color.White * Light * 1.5f, 0, new Vector2(WoundHealthBarFull.Width / 2, 0), 0, 0);
                Main.spriteBatch.Draw(WoundHealthBar, target, source, Color.White * Light * 1.5f, 0, new Vector2(WoundHealthBar.Width / 2, 0), 0, 0);
				
				return false;
			}
            return base.DrawHealthBar(NPC, hbPosition, ref scale, ref position);
        }
    }
}
