using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ProjectInfinity.Core
{
    [Label("Ocean of Infinity config")]
    public class ProjectInfinityConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        [DefaultValue(false)]
        [Label("Enable performance mode")]
        [Tooltip("Enables the performance mode for low end pcs.")]
        public bool PerformanceMode;

        [DefaultValue(true)]
        [Label("Enables the Wound damage")]
        [Tooltip("This is a new damage system will reduce the damage you deal to the bosses by %50 but that damage will turn into Wound damage which can be seen by the yellow text\nDealing enough wound damage to a boss will grant you buffs and you can deal normal damage for the buff duration")]
        public bool WoundDamage;
    }
}