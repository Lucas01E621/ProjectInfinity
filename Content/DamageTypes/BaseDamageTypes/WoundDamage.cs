using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.IO;

namespace ProjectInfinity.Content.DamageTypes.BaseDamageTypes
{
    class WoundDamage : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            return StatInheritanceData.Full;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return false;
        }
    }
}