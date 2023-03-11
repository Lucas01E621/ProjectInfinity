using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Biomes;
using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Content.Parkour;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Common.Players
{
    public class MPlayer : ModPlayer
    {
        public bool lifeRegenDebuff;
        public bool CrystalHeart;
        public bool died;
        public bool hasUpwardsBoostBuff;
        public bool waitDoubleJump;
        public bool hasDoubleJump;
        public bool canDoubleJump;
        public int SpaceBreathTimer;
        public int platformTimer = 0;
        public override void ResetEffects()
        {
            lifeRegenDebuff = false;
            hasUpwardsBoostBuff = false;
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
            #region upwards boost
            if (UsefulFunctions.IsGrounded(Player) && hasUpwardsBoostBuff)//fix this
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
                    SoundEngine.PlaySound(new SoundStyle("ProjectInfinity/Assets/Sounds/scream"), Player.position);
                    for (int i = 0; i < 20; i++)
                        Dust.NewDust(Player.Bottom, 2, 2, Terraria.ID.DustID.TreasureSparkle);
                    
                }
                waitDoubleJump = Player.controlJump;
            }
            #endregion
        }
        public override void PreUpdate()
        {
            #region asteriod biome breath
            ModBiome biome = ModContent.GetInstance<AsteriodBiomeMB>();
            if(biome.IsBiomeActive(Player))
            {
                Player player = ((ModPlayer)this).Player;
                player.breath -= 3;
                if (++SpaceBreathTimer > 10)
                {
                    SpaceBreathTimer = 0;
                    Player player2 = ((ModPlayer)this).Player;
                    player2.breath--;
                }
                if (((ModPlayer)this).Player.breath == 0)
                {
                    SoundEngine.PlaySound(SoundID.Drown, (Vector2?)null);
                }
                if (((ModPlayer)this).Player.breath <= 0)
                {
                    ((ModPlayer)this).Player.AddBuff(68, 2, true, false);
                }
            }
            #endregion
            platformTimer--;
            
        }
    }
}

