using ProjectInfinity.Core.Systems.ForegroundSystem;
using ProjectInfinity.Core;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ProjectInfinity.Content.Foregrounds
{
	class Vignette2 : Foreground
	{
		public static Vector2 offset;
		public static float opacityMult = 1;
		public static bool visible;
		public static Vector2 holeSize;

		public override bool Visible => visible;
		public override bool OverUI => true;

		public override void Draw(SpriteBatch spriteBatch, float opacity)
		{
			Texture2D tex = ModContent.Request<Texture2D>(AssetDirectory.Assets + "Foregrounds/Vignette").Value;
			var targetRect = new Rectangle(Main.screenWidth / 2 + (int)offset.X - (int)holeSize.X / 2, Main.screenHeight / 2 + (int)offset.Y - (int)holeSize.Y / 2, (int)holeSize.X, (int)holeSize.Y);

			var targetLeft = new Rectangle(0, 0, targetRect.X, Main.screenHeight);
			var targetRight = new Rectangle(targetRect.X + targetRect.Width, 0, Main.screenWidth - (targetRect.X + targetRect.Width), Main.screenHeight);
			var targetTop = new Rectangle(targetRect.X, 0, targetRect.Width, targetRect.Y);
			var targetBottom = new Rectangle(targetRect.X, targetRect.Y + targetRect.Height, targetRect.Width, Main.screenHeight - (targetRect.Y + targetRect.Height));

			spriteBatch.Draw(tex, targetRect, null, Color.White * opacity * opacityMult, 0, Vector2.Zero, 0, 0);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetLeft, Color.Black * opacity * opacityMult);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetRight, Color.Black * opacity * opacityMult);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetTop, Color.Black * opacity * opacityMult);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetBottom, Color.Black * opacity * opacityMult);
		}

		public override void Reset()
		{
			offset = Vector2.Zero;
			opacityMult = 1;
			visible = false;
			holeSize = Vector2.One * 64;
		}
	}
}