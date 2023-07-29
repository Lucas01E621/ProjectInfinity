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
using ProjectInfinity.Content.Parkour;
using ProjectInfinity.Content.Buffs;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Core.Systems.CollisionTest;

namespace ProjectInfinity.Content.Items.Misc
{
    internal class BezierCurveTestItem : ModItem
    {
        public override string Texture => AssetDirectory.Misc + "CrystalWand";

        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.height = 64;
            Item.width = 30;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 10;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<BezierCurveTest>();
            
        }
    }
}
