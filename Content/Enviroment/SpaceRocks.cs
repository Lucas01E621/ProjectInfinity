//flying space rocks that will deal damage to anything if they are speedy enough
using System;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Audio;
using ProjectInfinity.Core;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ProjectInfinity.Content.Enviroment 
{
    public class SpaceRock : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "LaserCrystal";
        public Vector2 SpeedLimitDamage = new Vector2(5,5);
        public Vector2 SpeedLimit = new Vector2(10,10);
        public int maxBounceAmount = 50;
        public int projHealt = 35;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.damage = 1;
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.timeLeft = 999;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            int gamemodeDamageMult = 1;
            if(Main.expertMode == true)
                gamemodeDamageMult = 2;
            if(Main.masterMode == true)
                gamemodeDamageMult = 3;
            
            //health check
            if(projHealt <= 0)
            {
                projHealt = 0;
                Projectile.Kill();
                SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, Projectile.Center);
            }


            //main logic
            Hit(gamemodeDamageMult);
            SpeedBoost();

        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.velocity = new Vector2(-Projectile.velocity.X / 2,-Projectile.velocity.Y / 2);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Rectangle aroundPlayer = new((int)Main.LocalPlayer.position.X * 2, (int)Main.LocalPlayer.position.Y * 2, Main.LocalPlayer.width * 2, Main.LocalPlayer.height * 2);

            if(Projectile.Hitbox.Intersects(aroundPlayer))
            {
                Projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
            // If collide with tile, reduce the penetrate.
            // So the projectile can reflect at most 5 times
            maxBounceAmount--;

            if(maxBounceAmount <= 0)
                Projectile.Kill();
            
            
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
                Projectile.velocity.X = -oldVelocity.X;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            

            return false;
        }
        void Hit(int gamemodeDamageMult)
        {
            if(Math.Abs(Projectile.velocity.X) > SpeedLimit.X || Math.Abs(Projectile.velocity.Y) > SpeedLimit.Y)
            {
                Projectile.friendly = false;
                Projectile.damage = 25 * gamemodeDamageMult;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC target = Main.npc[i];
                    if(target.active && Projectile.Hitbox.Intersects(target.Hitbox))
                    {
                        target.SimpleStrikeNPC(Projectile.damage, Projectile.direction);
                        Projectile.velocity = new Vector2(-Projectile.velocity.X / 2, -Projectile.velocity.Y / 2);
                    }
                }
                
            }
        }
        void SpeedBoost()
        {
            Vector2 maxProjVel = new(8,8);
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile hitproj = Main.projectile[i];
                if(hitproj.active && hitproj.aiStyle != 7 && Projectile.Hitbox.Intersects(hitproj.Hitbox))
                {
                    hitproj.Kill();
                    projHealt--;
                    if(Math.Abs(hitproj.velocity.X) > maxProjVel.X)
                        hitproj.velocity.X = maxProjVel.X;

                    if(Math.Abs(hitproj.velocity.Y) > maxProjVel.Y)
                        hitproj.velocity.Y = maxProjVel.Y;

                    Projectile.velocity.X += hitproj.velocity.X / 3;
                    Projectile.velocity.Y += hitproj.velocity.Y / 3;
                }
            }
        }
    }
}