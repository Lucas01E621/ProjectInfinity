using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs
{
    internal class TestPlatform : MovingPlatform
    {
        public int bobTime = 0;
        public ref float State => ref NPC.ai[1];
        public ref float HomeYPosition => ref NPC.ai[2];
        public ref float FallTime => ref NPC.ai[3];

        public override string Texture => AssetDirectory.NPCs + Name;
        public override void SafeSetDefaults()
        {
            NPC.width = 200;
            NPC.height = 32;
        }

        public override void SafeAI()
        {
            if (HomeYPosition == 0)
                HomeYPosition = NPC.position.Y;

            if (FallTime != 0)
            {
                if (FallTime > 360)
                    NPC.position.Y += 8;

                if (FallTime <= 90 && FallTime > 0)
                    NPC.position.Y -= 9;

                FallTime--;

                return;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
            spriteBatch.Draw(tex, NPC.Center - Main.screenPosition, null, Lighting.GetColor((int)NPC.Center.X / 16, (int)NPC.Center.Y / 16) * 1.5f, NPC.rotation, tex.Size() / 2, 1, 0, 0);
            return false;
        }
    }
}
