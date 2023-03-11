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
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Items.Weapons.Magic
{
    internal class PHwand : ModItem
    {
        public int timesFired = 0;

        public override string Texture => AssetDirectory.Magic + "PHwand";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("placeholder wand");
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
            Item.shootSpeed = 15;
            

            Item.shoot = ModContent.ProjectileType<PHwandproj>();
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            timesFired++;
            if(timesFired == 3)
            {    
                timesFired = 0;
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PHwandproj1>(), damage * 2, knockback,player.whoAmI);
            } else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PHwandproj>(), damage, knockback, player.whoAmI);
            }
            
            return false;
            
        }
        
    }
    internal class PHwandproj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "DenemeProj2";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
        }
        public override void AI()
        {
            base.AI();
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
    }
    internal class PHwandproj1 : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "DenemeProj";

        public int timesFired = 0;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 180;
            Projectile.penetrate = 1;
        }
        public override void OnSpawn(IEntitySource source)
        {
            
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation();
            Projectile.spriteDirection = Projectile.direction;
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundID.MaxMana);
            Main.player[Projectile.owner].statMana += (Main.player[Projectile.owner].statManaMax2 * 2) / 50;
            Main.player[Projectile.owner].ManaEffect((Main.player[Projectile.owner].statManaMax2 * 2) / 50);
            
        }

    }
}
