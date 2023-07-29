using Terraria;
using Terraria.ModLoader;
using ProjectInfinity.Content.Projectiles.BaseProjectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectInfinity.Core;

namespace ProjectInfinity.Content.Projectiles
{
    internal class CrystalWandLaser1 : LaserBase
    {
        public override string Texture => AssetDirectory.Projectiles + "LaserBase1";
        public override bool tileCollide => false;
        public override bool castLight => true;
        public override float MaxRange => 2000;
        public override bool hasMaxCharge => true;
        public override float MaxCharge => 120;



    }
}
