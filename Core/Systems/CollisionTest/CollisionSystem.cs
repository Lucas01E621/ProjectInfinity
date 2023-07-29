using Terraria;
using Terraria.ModLoader;
using ProjectInfinity.Core;
using Microsoft.Xna.Framework;

namespace ProjectInfinity.Core.Systems.CollisionTest
{
    public class CollisionTest : ModPlayer
    {
        public override void PreUpdateMovement()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if(!proj.active && proj.type != ModContent.ProjectileType<CollidableObject>()) continue;

                
            }
        }
    }
}