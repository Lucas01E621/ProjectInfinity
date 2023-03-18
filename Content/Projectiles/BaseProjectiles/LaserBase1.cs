using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using System.Runtime.CompilerServices;
using static Terraria.Utils;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.ID;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Projectiles.BaseProjectiles
{
    public class LaserBase1 : LaserBase
    {
        public override string Texture => AssetDirectory.Projectiles + Name;
        public override bool tileCollide => true;
        public override bool Active => true;
        public override bool castLight => true;
        public override bool friendly => true;
        public override bool hasMaxCharge => true;
        public override float MaxCharge => 120;
        public override float MaxRange => Main.player[Projectile.owner].Distance(Main.MouseWorld);
    }
}
