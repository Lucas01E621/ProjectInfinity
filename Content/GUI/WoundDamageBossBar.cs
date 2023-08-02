using Terraria;
using Terraria.UI;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ProjectInfinity.Core.Systems.BaseDamageTypes;

namespace ProjectInfinity.Content.GUI
{
    public class WoundDamageBossBar : SmartUIState
    {
        public static bool visible = ModContent.GetInstance<WoundDamageSystem>().targetBoss.active;
        public static Texture2D bossFrame = Request<Texture2D>(AssetDirectory.GUI + "WoundBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        private readonly BarOverlay bossBar = new();

        public override bool Visible => visible;

        public override void OnInitialize()
        {
            bar.Left.Set(-258, 0.5f);
			bar.Top.Set(-76, 1);
			bar.Width.Set(516, 0);
			bar.Height.Set(46, 0);
			Append(bar);
        }
        
        public override void SafeUpdate()
        {
            Recalculate();
        }

        public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}

    }
    
    public class WoundDamageBossBarOverlay : SmartUIElement //smart uielemet doesnt exist slr created one thats why it doesnt work and same goes for the above and dont forget to load the ui's peace out
    {
        public override Draw(SpriteBatch spriteBatch)
        {

            Vector2 pos = GetDimensions().ToRectangle().TopLeft() + new Vector2(0, 1);
            Vector2 off = new Vector2(10, 2);

            if(!Main.LocalPlayer.active)
            {
                WoundDamageBossBar.visible = false;
                return;
            }

            int t = (int)(npc.life / npc.lifeMax * 490);

            Texture2D bossBarFill = ModContent.Request<Texture2D>(AssetDirectory.GUI + "WoundHealthBossBarFill").Value;
            Texture2D edge = ModContent.Request<Texture2D>(AssetDirectory.GUI + "WoundHealthBossBarEdge").Value; // flip the drawing at the other end and actually draw it

            spriteBatch.Draw(bossBarFill, new Rectangle((int)(pos.X + off.X), (int)(pos.Y + off.Y) + 2, t, bossBarFill.Height - 4), Color.White);
            
            spriteBatch.End();
            spriteBatch.Begin(default, default, default, default, default, default, Main.UIScaleMatrix);

            Utils.DrawBorderString(spriteBatch, "Wound health: " + ModContent.GetInstance<WoundDamageSystem>().WoundHealth + " / " + ModContent.GetInstance<WoundDamageSystem>().MaxWoundHealth, pos + new Vector2(WoundBossBarFrame.Width / 2, -20), Color.White, 1, 0.5f, 0);
        }
    }
}