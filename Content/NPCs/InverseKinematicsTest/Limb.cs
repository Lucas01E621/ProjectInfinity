using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs.InverseKinematicsTest
{
    internal class Limb : ModNPC
    {
        public override string Texture => AssetDirectory.NPCs + "LimbTest";
        public sealed override void SetDefaults()
        {
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
            NPC.width = 200;
            NPC.height = 20;
            NPC.lifeMax = 1;
            NPC.noGravity = true;
            SafeSetDefault();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Item[this.Type].Value;
            Vector2 drawPosition = NPC.Center;
            Rectangle sourceRect = new(0, 0, texture.Width, texture.Height);
            Color lightColor = Color.White;
            float rotation = (Main.MouseWorld - NPC.Center).ToRotation();
            Vector2 originn = sourceRect.Size() / 2f;
            float scale = 1f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            float worthless = 0f;
            spriteBatch.Draw(texture, drawPosition, sourceRect, lightColor, rotation, originn, scale, spriteEffects, worthless);
            return true;
        }
        public virtual void SafeSetDefault() { }
        public virtual void SafePreDraw() { }
    }
}
