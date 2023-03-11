using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using SteelSeries.GameSense.DeviceZone;
using ProjectInfinity.Content.Items.Weapons.Magic;
using ProjectInfinity.Content.Projectiles;

namespace ProjectInfinity.Core
{
    internal abstract class Raycast : ModProjectile
    {
        public abstract int lenght { get; set; }
        public abstract int freq { get; set; }
        public abstract int degrees { get; set; }
        public static Rectangle checkBox = new();
        public override void Load()
        {
            checkBox = new Rectangle((int)Projectile.Center.X, (int)Projectile.Center.Y, lenght, lenght);
        }
        public override void AI()
        {
            Projectile.Center = new Vector2((int)Projectile.Center.X, (int)Projectile.Center.Y);
            if (!Projectile.getRect().Intersects(checkBox)) Projectile.Kill();
            RayCast();
        }
        public virtual void RayCast()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 1)
            {
                for (int i = 0; i < 360 / degrees; i++)
                {
                    for (int j = 0; !Projectile.getRect().Intersects(checkBox); j++)
                    {
                        Vector2 vel = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(i * degrees)) * (freq * j);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + vel.X, Projectile.Center.Y + vel.Y, 0, 0, ModContent.ProjectileType<raycastproj>(), Projectile.damage, Projectile.knockBack, Main.LocalPlayer.whoAmI);
                    }
                }
            }
        }
    }
}
