using ProjectInfinity.Core;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;

namespace ProjectInfinity.Content.NPCs
{
    internal class GammaSmasher : ModNPC
    {
        private bool isJumping = false;

        private int jumpTimer = 0;
        private int jumpDuration = 60;
        private int jumpVelocity = 20;
        private int impactDamage = 50;

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];

        private enum ActionState
        {
            Idle,
            Ball,
            Missile,
            Attack,
        }

        public override string Texture => AssetDirectory.NPCs + Name;

        public override void SetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.immortal = true;
            NPC.damage = 50;
            NPC.width = 20;
            NPC.height = 20;
            NPC.noTileCollide = false;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];

            Attack(player);

            switch (AI_State)
            {
                case (float)ActionState.Idle:
                    Idle();
                    break;
                case (float)ActionState.Ball:
                    Ball(player);
                    break;
                case (float)ActionState.Missile:
                    Missile();
                    break;
            }
        }
        private void Idle()
        {
            AI_Timer++;
            if(AI_Timer == 120)
            {
                AI_State = (float)ActionState.Ball;
                AI_Timer = 0;
            }
        }
        private void Ball(Player player)
        {
            AI_Timer++;

            if(AI_Timer == 60)
            {
                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(player.direction + 2,10);
            }
        }
        private void Missile()
        {

        }
        private void Attack(Player player)
        {
            if (isJumping)
            {
                jumpTimer++;

                if (jumpTimer > jumpDuration)
                {
                    isJumping = false;
                    jumpTimer = 0;
                }
                else
                {
                    NPC.velocity.Y = -jumpVelocity;
                }
            }
            else if (player != null && NPC.Distance(player.Center) < 200f)
            {
                isJumping = true;
                NPC.velocity = Vector2.Zero;
                NPC.netUpdate = true;

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke);
                }

                SoundEngine.PlaySound(new SoundStyle(), NPC.position);

                Rectangle impactArea = new Rectangle((int)NPC.position.X - NPC.width / 2, (int)NPC.position.Y + NPC.height / 2, NPC.width * 2, NPC.height);
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (player.active && !player.dead && impactArea.Intersects(player.Hitbox))
                    {
                        player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), impactDamage, NPC.direction);
                    }
                }
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC otherNPC = Main.npc[i];
                    if (otherNPC.active && !otherNPC.friendly && impactArea.Intersects(otherNPC.Hitbox))
                    {
                        otherNPC.SimpleStrikeNPC(impactDamage, NPC.direction);
                    }
                }
            }
        }
    }
}
