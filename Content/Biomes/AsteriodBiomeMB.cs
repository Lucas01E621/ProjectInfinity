using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ProjectInfinity.Core.Systems;
using ProjectInfinity.Content.Enviroment;

namespace ProjectInfinity.Content.Biomes
{
    internal class AsteriodBiomeMB : ModBiome
    {
        public override void SetStaticDefaults()
        {
            
        }
        public override bool IsBiomeActive(Player player)
        {
            return ModContent.GetInstance<BiomeHandler>().AsteriodBlockCount >= 40;//change it to subworld check not block
        }
        public override void OnInBiome(Player player)
        {
            SpawnAsteroids(player);
            player.gravity = 0f;
        }

        void SpawnAsteroids(Player player)
        {
            const int maxAsteroids = 5;

            int numAsteroids = 0;

            Rectangle screen = new((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            
            for (int i = 0; i < Main.maxProjectiles; i++) 
            {
                var proj = Main.projectile[i];
                if (proj.active && proj.type == ModContent.ProjectileType<SpaceRock>()) 
                {
                    numAsteroids++;
                    if(!proj.Hitbox.Intersects(screen))
                    {
                        proj.Kill();
                    }
                }
            }

            for (int i = numAsteroids; i < maxAsteroids; i++) 
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(player.position.X + Main.rand.Next(-200,200), player.position.Y + Main.rand.Next(-200,200)), new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-2, 2)), ModContent.ProjectileType<SpaceRock>(),0,0,Main.myPlayer);
            }
            
        }
    }
}
