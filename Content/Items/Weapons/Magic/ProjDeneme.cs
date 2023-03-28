using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Items.Bars;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Content.NPCs.enemies.CrystalDesert;
using SteelSeries.GameSense.DeviceZone;

namespace ProjectInfinity.Content.Items.Weapons.Magic
{
    internal class ProjDeneme : ModItem
    {
        public int timesFired = 0;
        
        public override string Texture => AssetDirectory.Magic + "ProjDeneme";

        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 10;
            Item.shootSpeed = 5;
            

            Item.shoot = ModContent.ProjectileType<DenemeProj>();
        }
        

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<PHwand>())
                .AddIngredient<examplebar>(10)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source,Main.MouseWorld,Vector2.Zero,ModContent.ProjectileType<TestRay>(),damage,knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<DenemeProj>(), damage, knockback, player.whoAmI);
            }

            return false;
            
        } 
    }
    public class DenemeProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "DenemeProj1";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 1;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation();
            Projectile.spriteDirection = Projectile.direction;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 60)
            {
                for (int i = 0; i < 72; i++)
                {
                        Vector2 vel = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(i * 5));
                        vel.Normalize();
                        SpawnRaycast(vel, 5);
                }
            }
            //Projectile.velocity *= 0.85f;
        }
        public void SpawnRaycast(Vector2 vel, int degrees)
        {
            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center,vel,ModContent.ProjectileType<raycastproj>(),0,0,Projectile.owner);
            Projectile proj= Main.projectile[index];
            if (proj.ModProjectile is raycastproj minion)
            {
                // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                minion.ParentIndex = Projectile.whoAmI; // Let the minion know who the "parent" is
                minion.maxDist = 300f;
            }
        }
    }
}
