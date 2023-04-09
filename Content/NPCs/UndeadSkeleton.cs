using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.NPCs
{
    public class UndeadSkeleton : ModNPC
    {
        public override string Texture => AssetDirectory.CrystalDesertNPC + "CrystalHive";
        public ref float hp => ref NPC.ai[0];
        public ref float dmg => ref NPC.ai[1];
        int timer = 0;
        
        public override void SetDefaults()
        {
            NPC.width = 76;
            NPC.height = 44;
            NPC.lifeMax = 100 + (int)hp;
            NPC.friendly = true;
            NPC.damage = 15 + (int)dmg;
            NPC.aiStyle = NPCAIStyleID.Fighter;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Main.NewText(hp);
            int index = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position, Vector2.Zero,ModContent.ProjectileType<UndeadSkeletonHitbox>(),NPC.damage,1,Main.myPlayer);
            Projectile proj = Main.projectile[index];
            if(proj.ModProjectile is UndeadSkeletonHitbox)
            {
                (proj.ModProjectile as UndeadSkeletonHitbox).parent = this;
            }
        }
        public override void AI()
        {
            timer++;
            if(timer >= 60)
            {
                NPC.direction = 1;
            }
            if(timer >= 120)
            {
                NPC.direction = -1;
                timer = 0;

            }
        }
    }
    public class UndeadSkeletonHitbox : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + "PHbullet";
        public UndeadSkeleton parent { get; set; }
        public override void SetDefaults()
        {
            Projectile.hide = true;
            Projectile.timeLeft = 99999999;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.width = 85;
            Projectile.height = 55;
        }
        public override void AI()
        {
            Projectile.position.X = parent.NPC.position.X - 4;
            Projectile.position.Y = parent.NPC.position.Y - 6;
        }
    }
}
