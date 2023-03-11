using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace ProjectInfinity.Core
{
    internal static class UsefulFunctions
    {
        public static void MultipleShot(Player player, IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int numberProjectiles, float rotationAmount, bool randomSpread)
        {
            position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
            float rotation = MathHelper.ToRadians(rotationAmount);
            Vector2 perturbedSpeed;
            for (int i = 0; i < numberProjectiles; i++)
            {
                switch (randomSpread)
                {
                    case true:
                        perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                        break;
                    case false:
                        perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
                        break;
                }
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
        }
        public static void DustRing(Vector2 center, float radius, int dustID, int dustCount = 5, float dustSpeed = 2f)
        {
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 dir = Utils.NextVector2CircularEdge(Main.rand, radius, radius);
                Vector2 val = center + dir;
                Vector2 dustVel = Utils.RotatedBy(new Vector2(dustSpeed, 0f), (double)(Utils.ToRotation(dir) + (float)Math.PI / 2f), default(Vector2));
                Dust.NewDustPerfect(val, dustID, (Vector2?)dustVel, 200, default(Color), 1f).noGravity = true;
            }
        }
        public static bool IsGrounded(this Player player)
        {
            return player.velocity.Y >= 0f && Collision.SolidCollision(player.BottomLeft, 32, 8, true);
        }
    }
}
