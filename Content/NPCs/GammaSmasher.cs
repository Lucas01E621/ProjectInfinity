using ProjectInfinity.Core;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using Newtonsoft.Json.Bson;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProjectInfinity.Content.NPCs
{
    internal class GammaSmasher : ModNPC
    {
        private bool onAir;

        private int jumpTimer = 0;
        private int jumpDuration = 60;
        private int jumpVelocity = 20;
        private int impactDamage = 50;

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];

        private enum ActionState
        {
            Hover,
            Slam,
            Missle,
        }

        public override string Texture => AssetDirectory.NPCs + Name;

        public override void SetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.immortal = true;
            NPC.damage = 50;
            NPC.width = 20;
            NPC.height = 20;
            NPC.friendly = true;
            //NPC.aiStyle = -1;
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0;
        }
        public override void OnSpawn(IEntitySource source)
        {
            AI_State = (float)ActionState.Hover;
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            onAir = !Collision.SolidCollision(NPC.BottomLeft, 20, 8, true);
            int neededTimeToAttack = 0;
            neededTimeToAttack++;
            NPC.ai[2]++;
            //Hover(player, (int)NPC.ai[2]);
            //SlamAttack(player, onAir);

            
            Main.NewText("ai state: " + AI_State);
            switch (AI_State)
            {
                case (float)ActionState.Hover:
                    Hover(player, (int)NPC.ai[2], neededTimeToAttack);
                    break;
                case (float)ActionState.Slam:
                    SlamAttack(player, onAir);
                    break;
                case (float)ActionState.Missle:
                    Missile();
                    break;
            }
        }
        private void Missile()
        {

        }
        private void SlamAttack(Player player, bool onAir)
        {
            if (onAir)
            {
                NPC.velocity.Y = 17;
            }else
            {
                //make it go back to its old position
                AI_State = (float)ActionState.Hover;
            }
        }
        private void Hover(Player player,int homingCooldown, int neededTimeToAttack)
        {
            const int homingDelay = 15;
            float desiredFlySpeedInPixelsPerFrame = NPC.Distance(player.Center) > 500 ? 120 : 50;
            const float amountOfFramesToLerpBy = 30;

            NPC.position.Y = player.position.Y - 256;

            if(homingCooldown > homingDelay)
            {
                Player target = Main.player[NPC.target];
                Vector2 desiredVel = NPC.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                NPC.velocity = Vector2.Lerp(NPC.velocity, desiredVel, 1f / amountOfFramesToLerpBy);
            }
            
            
            if (NPC.Center.X >= player.Center.X - 32 && NPC.Center.X <= player.Center.X + 32)
            {
                neededTimeToAttack++;
            }
            if (neededTimeToAttack >= 30)
            {
                AI_State = (float)ActionState.Slam;
                Main.NewText("attack");
                neededTimeToAttack = 0;
            }

        }
    }
}
