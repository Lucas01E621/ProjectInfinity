using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ProjectInfinity.Core.Systems
{
    public abstract class StackableBuff : ModBuff
    {
        public int stack;
        public int maxStack;


    }
}
