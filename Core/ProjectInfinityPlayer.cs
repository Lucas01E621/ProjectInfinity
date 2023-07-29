using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Biomes;
using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Content.Buffs.Debuffs;
using ProjectInfinity.Content.Subworlds;
using ProjectInfinity.Core.Systems.ForegroundSystem;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Core
{
    public class ProjectInfinityPlayer : ModPlayer
    {
        //accessories
        public bool CrystalHeart;
        public bool died;
        public bool NecroStaff;
        public bool ShieldMaker;
        public bool HasSuit;
        //
        public bool lifeRegenDebuff;
        public bool CDCurse;
        public bool hasUpwardsBoostBuff;
        public bool waitDoubleJump;
        public bool hasDoubleJump;
        public bool canDoubleJump;
        public bool Empowered;
        public int SpaceBreathTimer;
        public int platformTimer = 0;

        public override void ResetEffects()
        {
            lifeRegenDebuff = false;
            hasUpwardsBoostBuff = false;
            Empowered = false;
            HasSuit = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (lifeRegenDebuff)//make it golbal
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 16;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (CrystalHeart)
            {
                Player.Heal(Player.statLifeMax2);
                died = true;
                genGore = false;
                playSound = false;
                return false;
            } else
            {
                died = false;
            }

            return true;
        }

        public override void PreUpdateMovement()
        {
            UpwardsBoost();
        }

        public override void PreUpdate()
        {
            platformTimer--;
            SpaceBreath();
            Empowerment();
            
            /*if(!HasSuit) //check if the player is in the subworld before killing them
                Player.KillMe(PlayerDeathReason.ByCustomReason($"[c/FF0000: {Player.name}]" + " [c/FF0000: Didn't read the instructions about going into space.]"), 4564865345, 1);
            */
        }
        
        public override void PostUpdateEquips()
        {
            Curse();
        }
        
        //Void
        void UpwardsBoost()
        {
            if (UsefulFunctions.IsGrounded(Player) && hasUpwardsBoostBuff)
            {
                canDoubleJump = true;
                waitDoubleJump = true;
            }
            else
            {
                if (canDoubleJump && Player.controlJump && !waitDoubleJump)
                {
                    if (Player.jump > 0) return;

                    Player.velocity.Y = (0f - Player.jumpSpeed) * Player.gravDir;

                    Player.jump = (int)(Player.jumpHeight);
                    canDoubleJump = false;
                    Player.ClearBuff(ModContent.BuffType<UpwardsBoostBuff>());

                    //sound and dust
                    SoundEngine.PlaySound(new SoundStyle("ProjectInfinity/Sounds/scream"), Player.position);
                    for (int i = 0; i < 20; i++)
                        Dust.NewDust(Player.Bottom, 2, 2, Terraria.ID.DustID.TreasureSparkle);
                    
                }

                waitDoubleJump = Player.controlJump;
            }
        }

        void Empowerment()
        {
            if(Player.HasBuff(ModContent.BuffType<EmpoweredBuff>()))
            {
                float gamemodeMult = 1f;

                if(Main.expertMode)
                    gamemodeMult = 1.5f;
                if(Main.masterMode)
                    gamemodeMult = 2f;

                //by direwolf420
                Dictionary<DamageClass, int> damageTypeToCount = new();
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    Item item = Player.inventory[i];
                    if (item.IsAir || item.DamageType == DamageClass.Generic || item.damage == 0 || item.pick > 0 || item.hammer > 0 || item.axe > 0)
                    {
                        continue;
                    }

                    DamageClass damageType = item.DamageType;
                    if (!damageTypeToCount.ContainsKey(damageType))
                    {
                        damageTypeToCount[damageType] = 0;
                    }

                    damageTypeToCount[damageType]++;
                }

                DamageClass highestItemCountClass = null;
                int highestCount = 0;
                foreach (var pair in damageTypeToCount)
                {
                    if (pair.Value > highestCount)
                    {
                        highestCount = pair.Value;
                        highestItemCountClass = pair.Key;
                    }
                }
                if (highestItemCountClass != null && highestItemCountClass != DamageClass.Generic)
                {
                    if(highestItemCountClass == DamageClass.Magic)
                    {
                        Player.manaRegen += (int)(gamemodeMult * 5);
                        Player.GetDamage(DamageClass.Magic) += 0.08f * gamemodeMult;
                        Player.GetCritChance(DamageClass.Magic) += 0.1f* gamemodeMult;
                        ModContent.GetInstance<EmpoweredBuff>().Magic = true;
                    }
                    if(highestItemCountClass == DamageClass.Ranged)
                    {
                        Player.GetCritChance(DamageClass.Ranged) += 0.1f * gamemodeMult;
                        Player.GetDamage(DamageClass.Ranged) += 0.06f * gamemodeMult;
                        Player.GetAttackSpeed(DamageClass.Ranged) += 0.1f * gamemodeMult;
                        ModContent.GetInstance<EmpoweredBuff>().Ranged = true;
                    }
                    if(highestItemCountClass == DamageClass.Summon)
                    {
                        Player.GetAttackSpeed(DamageClass.Summon) += 0.1f * gamemodeMult;
                        Player.GetDamage(DamageClass.Summon) += 0.08f * gamemodeMult;
                        Player.GetCritChance(DamageClass.Summon) += 0.1f* gamemodeMult;
                        ModContent.GetInstance<EmpoweredBuff>().Summoner = true;
                    }
                    if(highestItemCountClass == DamageClass.Melee)
                    {
                        Player.GetAttackSpeed(DamageClass.Melee) += 0.09f * gamemodeMult;
                        Player.GetDamage(DamageClass.Melee) += 0.06f * gamemodeMult;
                        Player.GetCritChance(DamageClass.Melee) += 0.1f* gamemodeMult;
                        ModContent.GetInstance<EmpoweredBuff>().Melee = true;
                    }//Implement class specific special stuff !!!!!!!!!!!!!!!!!!!!!!!!
                }
                //By direwolf420
            }
        }

        void SpaceBreath()
        {
            if((Player.position.Y / 8) / 16 > Main.maxTilesY / 8 )
            {
                Player.breath -= 3;

                if (++SpaceBreathTimer > 10)
                {
                    SpaceBreathTimer = 0;
                    Player player2 = ((ModPlayer)this).Player;
                    player2.breath--;
                }
                if (Player.breath == 0)
                {
                    SoundEngine.PlaySound(SoundID.Drown);
                }
                if (Player.breath <= 0)
                {
                    Player.AddBuff(68, 2, true, false);
                }
            }
        }
        
        void Curse()
        {
            if(Player.HasBuff<CDCurse>())
            {
                Player.hasJumpOption_Basilisk = false;
                Player.hasJumpOption_Blizzard = false;
                Player.hasJumpOption_Cloud = false;
                Player.hasJumpOption_Fart = false;
                Player.hasJumpOption_Sail = false;
                Player.hasJumpOption_Sandstorm = false;
                Player.hasJumpOption_Santank = false;
                Player.hasJumpOption_Unicorn = false;
                Player.hasJumpOption_WallOfFleshGoat = false;

                Player.wingTime = 0;
                Player.rocketBoots = 0;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if(proj.active && proj.aiStyle == 7)
                        proj.Kill();
                }
            }
        }
    }
}