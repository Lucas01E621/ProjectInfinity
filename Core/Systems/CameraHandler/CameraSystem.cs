using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace ProjectInfinity.Core.Systems.CameraHandler
{
    internal class CameraSystem : ModSystem
    {
        public bool Shake { get; set; }
        public bool test { get; set; }
        private Vector2 targetPos = Vector2.Zero;
        private static MoveModifier MoveModifier = new();

        /// <summary>
		/// Sets up a panning animation with different or custom in/out times.
		/// </summary>
		/// <param name="durationOut"> How long it takes the camera to reach it's destination </param>
		/// <param name="durationHold"> How long the camera stays at it's destination </param>
		/// <param name="durationIn"> How long it takes the camera to return to the player </param>
		/// <param name="target"> Where the camera will pan to </param>
		/// <param name="easeOut"> Changes the easing function for the motion from the player to the target. Default is Vector2.Smoothstep </param>
		/// <param name="easeIn"> Changes the easing function for the motion from the target to the player. Default is Vector2.Smoothstep </param>
		public static void AsymetricalPan(int durationOut, int durationHold, int durationIn, Vector2 target, Func<Vector2, Vector2, float, Vector2> easeOut = null, Func<Vector2, Vector2, float, Vector2> easeIn = null)
        {
            MoveModifier.timeOut = durationOut;
            MoveModifier.timeHold = durationHold;
            MoveModifier.timeIn = durationIn;

            MoveModifier.target = target;
            MoveModifier.EaseOutFunction = easeOut ?? Vector2.SmoothStep;
            MoveModifier.EaseInFunction = easeIn ?? Vector2.SmoothStep;
        }
        public override void PostUpdateEverything()
        {
            MoveModifier.PassiveUpdate();
        }
        public override void ModifyScreenPosition()
        {
            if (Shake)
            {
                Main.instance.CameraModifiers.Add(new PunchCameraModifier(Main.LocalPlayer.position, Main.rand.NextFloat(3.14f).ToRotationVector2(), 15, 15f, 30, 2000, "test"));
            }
            if (MoveModifier.TotalDuration > 0 && MoveModifier.target != Vector2.Zero)
                Main.instance.CameraModifiers.Add(MoveModifier);
        }
        void Reset()
        {
            MoveModifier.Reset();
        }
        public override void OnWorldLoad()
        {
            
            Reset();
        }

        public override void Unload()
        {
            
            MoveModifier = null;
            
        }
    }
}
