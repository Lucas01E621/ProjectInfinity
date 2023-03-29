using ProjectInfinity.Core;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ProjectInfinity.Content.NPCs
{
    internal class GammaSmasher : ModNPC
    {
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
        private enum ActionState
        {
            Idle,
            Ball,
            Missile,
        }

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];

        public override void AI()
        {
            Player player = Main.player[NPC.target];


            switch (AI_State)
            {
                case (float)ActionState.Idle:
                    Idle();
                    break;
                    case (float)ActionState.Ball:
                    Ball();
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

        private void Ball()
        {
            AI_Timer++;

            if(AI_Timer == 60)
            {
                Player player = Main.player[NPC.target];

                NPC.TargetClosest(true);
                NPC.velocity = new Vector2(player.direction + 2,10);
            }

        }

        private void Missile()
        {

        }

    }
}
