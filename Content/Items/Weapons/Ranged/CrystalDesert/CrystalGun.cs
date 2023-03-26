using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Items.Misc;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Ranged
{
    internal class CrystalGun : ModItem
    {
        public override string Texture => AssetDirectory.Ranged + "CrystalGun";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal gun");
            Tooltip.SetDefault("'A giant crystal cannon forged in the crystal desert'\nit somewhat look similar");
        }
        public override void SetDefaults()
        {
            // Common Properties
            Item.width = 115; 
            Item.height = 43; 
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Green; 

            // Use Properties
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;

            // Weapon Properties
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.knockBack = 5f;
            Item.noMelee = true;

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<CrystalGunProj>();
            Item.shootSpeed = 16f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PrismProj>(), damage, knockback, player.whoAmI);
            } else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CrystalGunProj>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<CrystalGun>())
                .AddIngredient<ShardPiece>(5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    internal class CrystalGunProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "CrystalGunProj";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal laser");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (i == Projectile.whoAmI)
                    continue;
                Projectile prism = Main.projectile[i];
                if (!prism.active)
                    continue;
                if (Projectile.getRect().Intersects(prism.getRect()) && prism.type == ModContent.ProjectileType<PrismProj>())//collision check
                {
                    Projectile.Kill();
                    prism.Kill();
                    UsefulFunctions.MultipleShot(Main.player[Projectile.owner], prism.GetSource_FromThis(), prism.position, Projectile.velocity / 2, ModContent.ProjectileType<PrismParticleProj>(), prism.damage, prism.knockBack, Main.rand.Next(10,21), 360, true);

                }
            }
        }
    }
    public class PrismProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PrismProj";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalprism");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Projectile.velocity *= 0.85f;
            
        }
    }
    public class PrismParticleProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PrismParticleProj";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystalprism");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;

        }
    }
}
