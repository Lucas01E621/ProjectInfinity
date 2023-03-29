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
using System.Threading;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Buffs.Debuffs;

namespace ProjectInfinity.Content.Items.Weapons.Melee
{
    internal class SoulSword : ModItem
    {
        public override string Texture => AssetDirectory.Melee + "SoulSword"; 
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useAnimation = 25;
            Item.useTime = 60;
            Item.noMelee = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 10;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<SoulSwordProj>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            UsefulFunctions.MultipleShot(player, source, position, velocity, type, damage, knockback, 5, 30, false);
            return false;
        }
    }
    internal class SoulSwordProj : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "SoulSwordProj";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 120;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).ToRotation();
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 30)
            {
                Projectile.alpha += 11;
            }
            Projectile.velocity *= 0.93f;
            if(Projectile.velocity.Length() < 0.2)
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<SoulDecay>(), 240);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<SoulDecay>(), 240);
        }
    }
}
